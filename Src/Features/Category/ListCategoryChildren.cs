using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;
using Anazon.Shared.Db;
using Anazon.Utils;

using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class ListCategoryChildren
{

    public class ListCategoriesFilters : BasePagedQuery;
    public record ListCategoryChildrenQuery(int Id, ListCategoriesFilters Filters) : IRequest<Result<ListingResult<Category>>>;

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<ListCategoryChildrenQuery, Result<ListingResult<Category>>>
    {

        public void ApplyFilters(ref IQueryable<Models.Category> query, string? normalizedSearch)
        {
            if (!string.IsNullOrEmpty(normalizedSearch))
            {
                query = query.Where(b => b.Name.Contains(normalizedSearch));
            }
        }
        public async Task<Result<ListingResult<Category>>> Handle(ListCategoryChildrenQuery request, CancellationToken cancellationToken)
        {
            var parentId = request.Id;

            var validId = await dbContext.Categories.AnyAsync(c => c.Id == parentId, cancellationToken);

            if (!validId)
            {
                return Result.Failure<ListingResult<Category>>(Error.CategoryNotFound);
            }
            
            var filters = request.Filters;
            var normalizedSearch = filters.Q?.AsNormalized();

            var query = dbContext.Categories
                    .Where(c => c.ParentCategoryId == parentId)
                    .AsNoTracking();
            
            ApplyFilters(ref query, normalizedSearch);

            var totalItems = await query.CountAsync(cancellationToken);
            var paginationContext = filters.GetPaginationContext();
            var items = await query
                .OrderByDescending(b => b.CreatedAt)
                .ApplyPagination(paginationContext)
                .ToListAsync(cancellationToken);

            return Result.Success(ListingResult<Category>.FromQueryResult(
                paginationContext,
                items.Select(b => b.ToCategoryContract()),
                totalItems
            ));
        }
    }

}
public class ListCategoryChildrenEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppRoutes.Categories + "/{id:int}/children", async(int id, [AsParameters] ListCategoryChildren.ListCategoriesFilters filters, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var query = new ListCategoryChildren.ListCategoryChildrenQuery(id, filters);
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        })
        .RequirePermission(Permissions.Category.List)
        .WithTags(AppRouteTags.Categories);
    }
}