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

namespace Anazon.Features.Auth;


public static class CreateCategory
{

    public record CreateCategoryCommand(
        string Name,
        string? Description
    ) : IRequest<Result<CategoryDetails>>;




    public class Validator : AbstractValidator<CreateCategoryCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }



    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<CreateCategoryCommand, Result<CategoryDetails>>
    {
        public async Task<Result<CategoryDetails>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Models.Category
            {
                Name = request.Name.AsNormalized(),
                Description = request.Description
            };

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(category.ToCategoryDetailsContract());
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
        .RequirePermission(Permissions.Category.View)
        .WithTags(AppRouteTags.Categories);
    }
}