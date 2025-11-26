using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;

using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class GetProductDetails
{

    public record GetProductQuery (int Id) :  IRequest<Result<ProductDetails>>;

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<GetProductQuery, Result<ProductDetails>>
    {

        public async Task<Result<ProductDetails>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var Product = await dbContext.Products
                .Include(p=>p.Brand)
                .Include(p=>p.Category)
                .Include(p=>p.ProductTags)
                .Include(p=>p.Variants)
                    .ThenInclude(v=>v.AttributeValues)
                        .ThenInclude(av=>av.AttributeValue)
                            .ThenInclude(av=>av.Attribute)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (Product == null)
            {
                return Result.Failure<ProductDetails>(Error.ProductNotFound);
            }

            return Result.Success(Product.ToProductDetailsContract());
        }
    }

}
public class GetProductDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppRoutes.Products + "/{id:int}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetProductDetails.GetProductQuery(id), cancellationToken);
            return result.Match(
                () => result.ToSuccessHttpResult(),
                error => error == Error.ProductNotFound ? result.ToNotFoundHttpResult() : result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Product.View)
        .WithTags(AppRouteTags.Products);
    }
}