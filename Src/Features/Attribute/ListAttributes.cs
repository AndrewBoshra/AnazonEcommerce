using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;
using Anazon.Utils;

using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class ListAttributes
{

    public class AttributesList
    {
        public List<Shared.Contracts.Attribute> Items { get; set; } = new();
    }
    public class ListAttributesQuery : BaseQuery, IRequest<Result<AttributesList>>
    {
        public int? CategoryId { get; set; }
    }

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<ListAttributesQuery, Result<AttributesList>>
    {

        public void ApplyFilters(ref IQueryable<Models.Attribute> query, string? normalizedSearch, int? categoryId)
        {
            if (!string.IsNullOrEmpty(normalizedSearch))
            {
                query = query.Where(b => b.Name.Contains(normalizedSearch));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == categoryId.Value);
            }
        }
        public async Task<Result<AttributesList>> Handle(ListAttributesQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Attributes.AsNoTracking();

            var normalizedSearch = request.Q?.AsNormalized();
            ApplyFilters(ref query, normalizedSearch, request.CategoryId);

            var items = await query
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);

            return Result.Success(new AttributesList
            {
                Items = [.. items.Select(i => i.ToAttributeContract())]
            });
        }
    }

}
public class ListAttributesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppRoutes.Attributes + "/", async ([AsParameters] ListAttributes.ListAttributesQuery query, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        })
        .RequirePermission(Permissions.Attribute.List)
        .WithTags(AppRouteTags.Attributes);
    }
}