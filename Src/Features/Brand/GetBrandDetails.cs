using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;

using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class GetBrandDetails
{

    public record GetBrandQuery (int Id) :  IRequest<Result<BrandDetails>>;

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<GetBrandQuery, Result<BrandDetails>>
    {

        public async Task<Result<BrandDetails>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
        {
            var brand = await dbContext.Brands
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (brand == null)
            {
                return Result.Failure<BrandDetails>(Error.BrandNotFound);
            }

            return Result.Success(brand.ToBrandDetailsContract());
        }
    }

}
public class GetBrandDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppRoutes.Brands + "/{id:int}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetBrandDetails.GetBrandQuery(id), cancellationToken);
            return result.Match(
                () => result.ToSuccessHttpResult(),
                error => error == Error.BrandNotFound ? result.ToNotFoundHttpResult() : result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Brand.View)
        .WithTags(AppRouteTags.Brands);
    }
}