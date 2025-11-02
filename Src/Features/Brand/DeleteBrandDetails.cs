using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Contracts;

using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class DeleteBrand
{

    public record DeleteBrandCommand (int Id) :  IRequest<Result>;

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<DeleteBrandCommand, Result>
    {

        public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await dbContext.Brands
                .AsNoTracking()
                .Select(b=> new {b.Id})
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (brand == null)
            {
                return Result.Failure<BrandDetails>(Error.BrandNotFound);
            }

            dbContext.Brands.Remove(new Models.Brand { Id = brand.Id });
            await dbContext.SaveChangesAsync();
            return Result.Success();
        }
    }

}
public class DeleteBrandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(AppRoutes.Brands + "/{id:int}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteBrand.DeleteBrandCommand(id), cancellationToken);
            return result.Match(
                () => result.ToSuccessHttpResult(),
                error => error == Error.BrandNotFound ? result.ToNotFoundHttpResult() : result.ToBadRequestHttpResult()
            );
        })
        .WithTags(AppRouteTags.Brands);
    }
}