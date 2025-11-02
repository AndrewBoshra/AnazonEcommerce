using Anazon.Configs;
using Anazon.Database;
using Anazon.Models;
using Anazon.Shared;
using Anazon.Shared.Contracts;
using Anazon.Shared.Services;
using Anazon.Utils;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;

namespace Anazon.Features.Auth;


public static class UpdateBrand
{


    public record UpdateBrandCommand(
        UpdateBrandData Data,
        int Id 
    ) : IRequest<Result<BrandDetails>>;

    public record UpdateBrandData(
        string? Name,
        string? Description
    );






    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<UpdateBrandCommand, Result<BrandDetails>>
    {
 

        public async Task<Result<BrandDetails>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {

             var brand = await dbContext.Brands.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (brand == null)
            {
                return Result.Failure<BrandDetails>(Error.BrandNotFound);
            }
            var data = request.Data;
            brand.Name = data.Name ?? brand.Name;
            brand.Description = data.Description ?? brand.Description;

            dbContext.Brands.Update(brand);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(brand.ToBrandDetailsContract());
        }
    }


}
public class UpdateBrandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch(AppRoutes.Brands + "/{id:int}" , async (int id, UpdateBrand.UpdateBrandData data, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var command = new UpdateBrand.UpdateBrandCommand(
                Data: data,
                Id: id
            );
            var result = await mediator.Send(command, cancellationToken);
            return result.Match(
                () => result.ToSuccessHttpResult(),
                error => error == Error.BrandNotFound ? result.ToNotFoundHttpResult() : result.ToBadRequestHttpResult()
            );
        })
        .WithTags(AppRouteTags.Brands);
    }
}