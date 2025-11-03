using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;

using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class DeleteCategory
{

    public record DeleteCategoryCommand(int Id) :  IRequest<Result>;

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<DeleteCategoryCommand, Result>
    {

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var Category = await dbContext.Categories
                .AsNoTracking()
                .Select(b=> new {b.Id , HasProducts = b.Products.Any()})
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (Category == null)
            {
                return Result.Failure<CategoryDetails>(Error.CategoryNotFound);
            }

            if (Category.HasProducts)
            {
                return Result.Failure<CategoryDetails>(Error.CategoryCantBeDeletedContainsProducts);
            }

            dbContext.Categories.Remove(new Models.Category { Id = Category.Id });
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

}
public class DeleteCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(AppRoutes.Categories + "/{id:int}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteCategory.DeleteCategoryCommand(id), cancellationToken);
            return result.Match(
                () => result.ToSuccessHttpResult(),
                error => error == Error.CategoryNotFound ? result.ToNotFoundHttpResult() : result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Category.Delete)
        .WithTags(AppRouteTags.Categories);
    }
}