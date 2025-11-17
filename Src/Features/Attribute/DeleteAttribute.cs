using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;

using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class DeleteAttribute
{

    public record DeleteAttributeCommand(int Id) :  IRequest<Result>;

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<DeleteAttributeCommand, Result>
    {

        public async Task<Result> Handle(DeleteAttributeCommand request, CancellationToken cancellationToken)
        {
            var Attribute = await dbContext.Attributes
                .AsNoTracking()
                .Select(a=> new {a.Id , HasProducts = a.Values.Any(v => v.ProductAttributes.Any()) })
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (Attribute == null)
            {
                return Result.Failure(Error.AttributeNotFound);
            }

            if (Attribute.HasProducts)
            {
                return Result.Failure(Error.AttributeCantBeDeletedContainsProducts);
            }

            dbContext.Attributes.Remove(new Models.Attribute { Id = Attribute.Id });

            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

}
public class DeleteAttributeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(AppRoutes.Attributes + "/{id:int}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteAttribute.DeleteAttributeCommand(id), cancellationToken);
            return result.Match(
                () => result.ToSuccessHttpResult(),
                error => error == Error.AttributeNotFound ? result.ToNotFoundHttpResult() : result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Attribute.Delete)
        .WithTags(AppRouteTags.Attributes);
    }
}