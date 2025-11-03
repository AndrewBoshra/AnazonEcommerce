using System.Diagnostics.Contracts;
using Anazon.Configs;
using Anazon.Database;
using Anazon.Models;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;
using Anazon.Shared.Services;
using Anazon.Utils;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class CreateCategory
{

    public record CreateCategoryCommand(
        string Name,
        string? Description,
        int? ParentCategoryId
    ) : IRequest<Result<Shared.Contracts.Category>>;




    public class Validator : AbstractValidator<CreateCategoryCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }



    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<CreateCategoryCommand, Result<Shared.Contracts.Category>>
    {
        public async Task<Result<Shared.Contracts.Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validParent = request.ParentCategoryId is null || await dbContext.Categories.AnyAsync(c => c.Id == request.ParentCategoryId, cancellationToken: cancellationToken);
            if (!validParent) return Result.Failure<Shared.Contracts.Category>(Error.CategoryInvalidParentId);
            var category = new Models.Category
            {
                Name = request.Name.AsNormalized(),
                Description = request.Description,
                ParentCategoryId  = request.ParentCategoryId
            };

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(category.ToCategoryContract());
        }
    }


}
public class CreateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AppRoutes.Categories, async (CreateCategory.CreateCategoryCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Match(
                () => result.ToCreatedHttpResult(),
                error => result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Category.Create)
        .WithTags(AppRouteTags.Categories);
    }
}