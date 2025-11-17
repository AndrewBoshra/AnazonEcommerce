using Anazon.Configs;
using Anazon.Database;
using Anazon.Models;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;
using Anazon.Utils;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class CreateAttribute
{

    public record CreateAttributeCommand(
        string Name,
        int CategoryId
    ) : IRequest<Result<Shared.Contracts.Attribute>>;




    public class Validator : AbstractValidator<CreateAttributeCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }



    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<CreateAttributeCommand, Result<Shared.Contracts.Attribute>>
    {
        public async Task<Result<Shared.Contracts.Attribute>> Handle(CreateAttributeCommand request, CancellationToken cancellationToken)
        {

            var validCategory = await dbContext.Categories
                .AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
            if (!validCategory) return Result.Failure<Shared.Contracts.Attribute>(Error.CategoryInvalidId);
            
            var Attribute = new Models.Attribute
            {
                Name = request.Name.AsNormalized(),
                CategoryId = request.CategoryId
            };

            dbContext.Attributes.Add(Attribute);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(Attribute.ToAttributeContract());
        }
    }


}
public class CreateAttributeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AppRoutes.Attributes , async (CreateAttribute.CreateAttributeCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Match(
                () => result.ToCreatedHttpResult(),
                error => result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Attribute.Create)
        .WithTags(AppRouteTags.Attributes);
    }
}