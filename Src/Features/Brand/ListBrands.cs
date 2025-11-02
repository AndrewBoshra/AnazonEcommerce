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


public static class ListBrands
{

    public class ListBrandsQuery : BasePagedQuery, IRequest<Result<ListingResult<Brand>>>;

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<ListBrandsQuery, Result<ListingResult<Brand>>>
    {

        public void ApplyFilters(ref IQueryable<Models.Brand> query, string? normalizedSearch)
        {
            if (!string.IsNullOrEmpty(normalizedSearch))
            {
                query = query.Where(b => b.Name.Contains(normalizedSearch));
            }
        }
        public async Task<Result<ListingResult<Brand>>> Handle(ListBrandsQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Brands.AsNoTracking();

            var normalizedSearch = request.Q?.AsNormalized();
            ApplyFilters(ref query, normalizedSearch);

            var totalItems = await query.CountAsync(cancellationToken);
            var paginationContext = request.GetPaginationContext();
            var items = await query
                .OrderByDescending(b => b.CreatedAt)
                .ApplyPagination(paginationContext)
                .ToListAsync(cancellationToken);

            return Result.Success(ListingResult<Brand>.FromQueryResult(
                paginationContext,
                items.Select(b => b.ToBrandContract()),
                totalItems
            ));
        }
    }

}
public class ListBrandsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppRoutes.Brands + "/", async ([AsParameters] ListBrands.ListBrandsQuery query, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        })
        .RequirePermission(Permissions.Brands.List)
        .WithTags(AppRouteTags.Brands);
    }
}