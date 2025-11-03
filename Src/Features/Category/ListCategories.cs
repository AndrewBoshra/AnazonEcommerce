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


public static class ListCategories
{

    public class ListCategoriesQuery : BasePagedQuery, IRequest<Result<ListingResult<Category>>>;

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<ListCategoriesQuery, Result<ListingResult<Category>>>
    {

        public void ApplyFilters(ref IQueryable<Models.Category> query, string? normalizedSearch)
        {
            if (!string.IsNullOrEmpty(normalizedSearch))
            {
                query = query.Where(b => b.Name.Contains(normalizedSearch));
            }
        }
        public async Task<Result<ListingResult<Category>>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Categories.AsNoTracking();

            var normalizedSearch = request.Q?.AsNormalized();
            ApplyFilters(ref query, normalizedSearch);

            var totalItems = await query.CountAsync(cancellationToken);
            var paginationContext = request.GetPaginationContext();
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
public class ListCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppRoutes.Categories + "/", async ([AsParameters] ListCategories.ListCategoriesQuery query, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        })
        .RequirePermission(Permissions.Category.List)
        .WithTags(AppRouteTags.Categories);
    }
}