using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;

using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class DeleteAttributeValues
{

    public record DeleteAttributeValueCommand(int AttributeId , string Value) :  IRequest<Result>;

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<DeleteAttributeValueCommand, Result>
    {

        public async Task<Result> Handle(DeleteAttributeValueCommand request, CancellationToken cancellationToken)
        {
            var result = await dbContext.AttributeValues
            .Where(av => av.Value == request.Value && av.AttributeId == request.AttributeId)
            .Select(av=> new
            {
                Id = av.Id,
                IsUsed = av.ProductAttributes.Any()
            }).FirstOrDefaultAsync(cancellationToken);
            
            if (result == null)
            {
                return Result.Failure(Error.AttributeValueNotFound);
            }

            if(result.IsUsed)
            {
                return Result.Failure(Error.AttributeValueInUse);
            }

            dbContext.AttributeValues.Remove(new Models.AttributeValue { Id = result.Id });

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
public class DeleteAttributeValueEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(AppRoutes.AttributeValues + "/{Value}", async (int attributeId, string value, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteAttributeValues.DeleteAttributeValueCommand(attributeId, value), cancellationToken);
            return result.Match(
                () => result.ToSuccessHttpResult(),
                error => error == Error.AttributeValueNotFound ? result.ToNotFoundHttpResult() : result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.AttributeValue.Delete)
        .WithTags([
            AppRouteTags.Attributes,
            AppRouteTags.AttributeValues
        ]);
    }
}