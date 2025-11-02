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

namespace Anazon.Features.Auth;


public static class CreateBrand
{

    public record CreateBrandCommand(
        string Name,
        string? Description
    ) : IRequest<Result<BrandDetails>>;




    public class Validator : AbstractValidator<CreateBrandCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }



    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<CreateBrandCommand, Result<BrandDetails>>
    {
        public async Task<Result<BrandDetails>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = new Models.Brand
            {
                Name = request.Name.AsNormalized(),
                Description = request.Description
            };

            dbContext.Brands.Add(brand);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(brand.ToBrandDetailsContract());
        }
    }


}
public class CreateBrandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AppRoutes.Brands , async (CreateBrand.CreateBrandCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Match(
                () => result.ToCreatedHttpResult(),
                error => result.ToBadRequestHttpResult()
            );
        })
        .WithTags(AppRouteTags.Brands);
    }
}