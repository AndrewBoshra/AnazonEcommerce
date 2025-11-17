using Anazon.Configs;
using Anazon.Database;
using Anazon.Models;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;
using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class UpdateAttribute
{


    public record UpdateAttributeCommand(
        UpdateAttributeData Data,
        int Id
    ) : IRequest<Result<Shared.Contracts.Attribute>>;

    public record UpdateAttributeData(
        string? Name
    );






    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<UpdateAttributeCommand, Result<Shared.Contracts.Attribute>>
    {


        public async Task<Result<Shared.Contracts.Attribute>> Handle(UpdateAttributeCommand request, CancellationToken cancellationToken)
        {

            var Attribute = await dbContext.Attributes.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            var data = request.Data;
           
            if (Attribute == null)
            {
                return Result.Failure<Shared.Contracts.Attribute>(Error.AttributeNotFound);
            }
            Attribute.Name = data.Name ?? Attribute.Name;

            dbContext.Attributes.Update(Attribute);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(Attribute.ToAttributeContract());
        }
    }


}
public class UpdateAttributeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch(AppRoutes.Attributes + "/{id:int}", async (int id, UpdateAttribute.UpdateAttributeData data, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var command = new UpdateAttribute.UpdateAttributeCommand(
                Data: data,
                Id: id
            );
            var result = await mediator.Send(command, cancellationToken);
            return result.Match(
                () => result.ToSuccessHttpResult(),
                error => error == Error.AttributeNotFound ? result.ToNotFoundHttpResult() : result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Attribute.Update)
        .WithTags(AppRouteTags.Attributes);
    }
}