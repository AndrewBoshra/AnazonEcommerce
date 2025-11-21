using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;

using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class ListAttributeValues
{

    public class AttributesValuesList
    {
        public string AttributeName { get; set; } = string.Empty;
        public List<string> Values { get; set; } = new();
        public int Id { get; set; } = new();
    }
    public record ListAttributeValuesQuery(int AttributeIdId) : IRequest<Result<AttributesValuesList>>
    {}

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<ListAttributeValuesQuery, Result<AttributesValuesList>>
    {

        public async Task<Result<AttributesValuesList>> Handle(ListAttributeValuesQuery request, CancellationToken cancellationToken)
        {
            var attribute = dbContext.Attributes.Include(a=>a.Values)
                                                .FirstOrDefault(a => a.Id == request.AttributeIdId);


            if (attribute == null)
            {
                return Result.Failure<AttributesValuesList>(Error.AttributeNotFound);
            }

            return Result.Success(new AttributesValuesList
            {
                Values = attribute.Values.Select(v => v.Value).ToList(),
                AttributeName = attribute.Name,
                Id = attribute.Id
            });
        }
    }

}
public class ListAttributeValuesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppRoutes.AttributeValues , async (int attributeId, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new ListAttributeValues.ListAttributeValuesQuery(attributeId), cancellationToken);
            return result.Match(
                () => result.ToSuccessHttpResult(),
                failure => result.ToNotFoundHttpResult()
            );
        })
        .RequirePermission(Permissions.AttributeValue.List)
        .WithTags([
            AppRouteTags.Attributes,
            AppRouteTags.AttributeValues
        ]);
    }
}