using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;
using Anazon.Utils;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Product;


public static class CreateProduct
{

    public record CreateProductResult(int ProductId);

    public record CreateProductVariant(
        decimal Price,
        int Stock,
        Dictionary<int, string> Attributes
    );
    public record CreateProductCommand(
        string Name,
        string Description,
        int? BrandId,
        int CategoryId,
        List<string> Tags,
        List<CreateProductVariant> Variants
    ) : IRequest<Result<CreateProductResult>>;



    private class ProductVariantValidator : AbstractValidator<CreateProductVariant>
    {
        public ProductVariantValidator()
        {
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Attributes).NotEmpty();
        }
    }
    public class Validator : AbstractValidator<CreateProductCommand>
    {

        private static readonly int MinTags = 5;
        private static readonly int MaxTags = 20;
        public Validator()
        {


            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleForEach(x => x.Tags).NotNull().WithMessage("Tag {CollectionIndex} is required.");
            RuleFor(x => x.Tags).Must(tags => tags.Distinct().Count() == tags.Count).WithMessage("Tags must be unique.")
                              .Must(tags => tags.Count >= MinTags).WithMessage($"At least {MinTags} tags are required.")
                              .Must(tags => tags.Count <= MaxTags).WithMessage($"A maximum of {MaxTags} tags are allowed.");

            RuleForEach(x => x.Variants).SetValidator(new ProductVariantValidator());

            // make sure each variant has unique attribute combinations
            RuleFor(x => x.Variants).Must(variants =>
            {
                var seenCombinations = new HashSet<string>();
                foreach (var variant in variants)
                {
                    var combinationKey = string.Join("|", variant.Attributes.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key}:{kv.Value}"));
                    if (seenCombinations.Contains(combinationKey))
                    {
                        return false; // Duplicate combination found
                    }
                    seenCombinations.Add(combinationKey);
                }
                return true;
            }).WithMessage("Each product variant must have a unique combination of attributes.");

        }
    }

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<CreateProductCommand, Result<CreateProductResult>>
    {


        private async Task<IEnumerable<Models.Attribute>> GetCategoryAttributes(int categoryId, CancellationToken ct)
        {
            return await dbContext.Attributes
                .Where(a => a.CategoryId == categoryId)
                .Include(a => a.Values)
                .ToListAsync(ct);
        }
        private async Task<Result> ValidateIds(CreateProductCommand request, CancellationToken ct)
        {
            var brandExists = true;
            if (request.BrandId.HasValue)
            {
                brandExists = await dbContext.Brands.AnyAsync(b => b.Id == request.BrandId.Value, ct);
            }

            if (!brandExists)
            {
                return Result.Failure(Error.InvalidBrandId);
            }
            var categoryExists = await dbContext.Categories.AnyAsync(c => c.Id == request.CategoryId, ct);


            if (!categoryExists)
            {
                return Result.Failure(Error.InvalidCategoryId);
            }

            return Result.Success();
        }


        private async Task<Result> ValidateTags(CreateProductCommand request, CancellationToken ct)
        {
            var existingTags = await dbContext.Tags
                .Where(t => request.Tags.Select(StringUtils.AsNormalized).Contains(t.Key))
                .Select(t => t.Key)
                .ToListAsync(ct);

            if (existingTags.Count != request.Tags.Count)
            {
                var missingTags = request.Tags.Except(existingTags).ToList();
                return Result.Failure(Error.TagsNotFound(missingTags));
            }
            return Result.Success();
        }

        private async Task<Result> ValidateAttributes(CreateProductCommand request, CancellationToken ct)
        {
            var categoryAttributes = await GetCategoryAttributes(request.CategoryId, ct);

            foreach (var variant in request.Variants)
            {
                foreach (var attr in variant.Attributes)
                {
                    var attribute = categoryAttributes.FirstOrDefault(a => a.Id == attr.Key);
                    if (attribute == null)
                    {
                        return Result.Failure(Error.InvalidAttributeId(attr.Key));
                    }

                    var valueExists = attribute.Values.Any(v => v.Value == attr.Value);
                    if (!valueExists)
                    {
                        return Result.Failure(Error.InvalidAttributeValue(attr.Key, attr.Value));
                    }
                }
            }

            return Result.Success();
        }

        private async Task<Result> Validate(CreateProductCommand request, CancellationToken ct)
        {
            var idValidation = await ValidateIds(request, ct);
            if (!idValidation.IsSuccess)
            {
                return idValidation;
            }

            var tagValidation = await ValidateTags(request, ct);
            if (!tagValidation.IsSuccess)
            {
                return tagValidation;
            }

            var attributeValidation = await ValidateAttributes(request, ct);
            if (!attributeValidation.IsSuccess)
            {
                return attributeValidation;
            }

            return Result.Success();
        }
        public async Task<Result<CreateProductResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validation = await Validate(request, cancellationToken);
            if (!validation.IsSuccess)
            {
                return Result.Failure<CreateProductResult>(validation.Error);
            }


            var attributes = await GetCategoryAttributes(request.CategoryId, cancellationToken);

            // Create Product
            var product = new Models.Product
            {
                Name = request.Name.AsNormalized(),
                Description = request.Description,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
                ProductTags = [.. request.Tags.Select(tag => new Models.ProductTag
                {
                    Tag = tag.AsNormalized()
                })],
                Variants = [.. request.Variants.Select(variantDto => new Models.ProductVariant
                {
                    Price = variantDto.Price,
                    Stock = variantDto.Stock,
                    AttributeValues = [.. variantDto.Attributes.Select(attr => new Models.ProductVariantAttributeValue
                    {
                        AttributeValueId = attributes
                            .First(a => a.Id == attr.Key)
                            .Values
                            .First(v => v.Value == attr.Value)
                            .Id
                    })]
                }

            )]
            };


            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return Result.Success(new CreateProductResult(product.Id));
        }
    }
}


public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AppRoutes.Categories, async (CreateProduct.CreateProductCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Match(
                () => result.ToCreatedHttpResult(),
                error => result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Product.Create)
        .WithTags(AppRouteTags.Products);
    }
}