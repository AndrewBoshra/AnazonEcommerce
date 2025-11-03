using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;

using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class GetCategoryDetails
{

    public record GetCategoryQuery (int Id) :  IRequest<Result<CategoryDetails>>;

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<GetCategoryQuery, Result<CategoryDetails>>
    {

        public async Task<Result<CategoryDetails>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var Category = await dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (Category == null)
            {
                return Result.Failure<CategoryDetails>(Error.CategoryNotFound);
            }
            var Children = await dbContext.Categories
                .AsNoTracking()
                .Where(c => c.ParentCategoryId == request.Id)
                .ToListAsync(cancellationToken);

            return Result.Success(Category.ToCategoryDetailsContract(Children));
        }
    }

}
public class GetCategoryDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppRoutes.Categories + "/{id:int}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetCategoryDetails.GetCategoryQuery(id), cancellationToken);
            return result.Match(
                () => result.ToSuccessHttpResult(),
                error => error == Error.CategoryNotFound ? result.ToNotFoundHttpResult() : result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Category.View)
        .WithTags(AppRouteTags.Categories);
    }
}