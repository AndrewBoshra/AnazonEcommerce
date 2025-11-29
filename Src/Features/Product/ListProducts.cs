using System;
using System.Diagnostics.CodeAnalysis;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Product;


public static class ListProducts
{


    public record ProductVariantAttributeDto(
        int AttributeId,
        string Name,
        string Value
    );
    public record ProductVariantDto(
        int Id,
        decimal Price,
        int Stock,
        List<ProductVariantAttributeDto> Details
    );


    public record ProductDto(
     string Name,
     string? Description,
     int? BrandId,
     int CategoryId,
     Category Category,
     Brand? Brand,
     List<string> Tags,
     List<ProductVariantDto> Variants
    )
    { }

    public class AttributeFilter : IParsable<AttributeFilter>
    {
        public int Id { get; set; } = default!;
        public List<string> Values { get; set; } = new();

        public static AttributeFilter Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }


        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out AttributeFilter result)
        {
            char separator = ':';
            if (s is null || !s.Contains(separator))
            {
                result = null;
                return false;
            }

            var parts = s.Split(separator);

            if (parts.Length != 2)
            {
                result = null;
                return false;
            }


            result = new()
            {
                Id = int.Parse(parts[0]),
                Values = parts[1].Split(",").ToList()
            };
            return true;

        }
    }
    public class ListProductsQuery : BasePagedQuery, IRequest<Result<ListingResult<ProductDto>>>
    {

        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public List<int>? Brands { get; set; } = new();
        public List<int>? Categories { get; set; } = new();
        public List<AttributeFilter>? Attributes { get; set; } = new();
        public List<string>? Tags { get; set; } = new();
    }

    public class Handler(AppDbContext db)
        : IRequestHandler<ListProductsQuery, Result<ListingResult<ProductDto>>>
    {
        public async Task<Result<ListingResult<ProductDto>>> Handle(
            ListProductsQuery request,
            CancellationToken ct)
        {
            // 1. Start Query
            var query = db.Products.AsNoTracking();

            // 2. Apply Filters (each extracted)
            query = ApplyFullTextSearch(query, request);
            query = ApplyPriceFilters(query, request);
            query = ApplyBrandFilter(query, request);
            query = ApplyCategoryFilter(query, request);
            query = ApplyTagsFilter(query, request);
            query = ApplyAttributeFilters(query, request);

            // 3. Pagination
            var pageContext = request.GetPaginationContext();

            var pagedQuery = query
                .ApplyPagination(pageContext)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductTags)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.AttributeValues)
                    .ThenInclude(av => av.AttributeValue)
                    .ThenInclude(av => av.Attribute);

            // 4. Project to DTO
            var items = await pagedQuery
                .Select(p => ProductToDto(p))
                .ToListAsync(cancellationToken: ct);

            // 5. Count
            var total = await query.CountAsync(ct);

            return Result.Success(
                ListingResult<ProductDto>.FromQueryResult(pageContext, items, total)
            );
        }

        // ---------------------------
        //  FILTER HELPERS
        // ---------------------------

        private IQueryable<Models.Product> ApplyFullTextSearch(
            IQueryable<Models.Product> query,
            ListProductsQuery request)
        {
            if (string.IsNullOrWhiteSpace(request.Q))
                return query;

            return db.Products
                .FromSql($"SELECT * FROM Products WHERE MATCH(Name, Description) AGAINST ({request.Q} WITH QUERY EXPANSION)")
                .AsNoTracking();
        }

        private static IQueryable<Models.Product> ApplyPriceFilters(
            IQueryable<Models.Product> query,
            ListProductsQuery request)
        {
            if (request.MinPrice.HasValue)
                query = query.Where(p => p.Variants.Any(v => v.Price >= request.MinPrice));

            if (request.MaxPrice.HasValue)
                query = query.Where(p => p.Variants.Any(v => v.Price <= request.MaxPrice));

            return query;
        }

        private static IQueryable<Models.Product> ApplyBrandFilter(
            IQueryable<Models.Product> query,
            ListProductsQuery request)
        {
            if (request.Brands?.Count > 0)
                query = query.Where(p => p.BrandId.HasValue && request.Brands.Contains(p.BrandId.Value));

            return query;
        }

        private static IQueryable<Models.Product> ApplyCategoryFilter(
            IQueryable<Models.Product> query,
            ListProductsQuery request)
        {
            if (request.Categories?.Count > 0)
                query = query.Where(p => request.Categories.Contains(p.CategoryId));

            return query;
        }

        private static IQueryable<Models.Product> ApplyTagsFilter(
            IQueryable<Models.Product> query,
            ListProductsQuery request)
        {
            if (request.Tags?.Count > 0)
                query = query.Where(p => p.ProductTags.Any(t => request.Tags.Contains(t.Tag)));

            return query;
        }

        private static IQueryable<Models.Product> ApplyAttributeFilters(
            IQueryable<Models.Product> query,
            ListProductsQuery request)
        {
            if (request.Attributes?.Count > 0)
            {
                foreach (var f in request.Attributes)
                {
                    var normalized = f.Values.Select(StringUtils.AsNormalized).ToList();

                    query = query.Where(p =>
                        p.Variants.Any(v =>
                            v.AttributeValues.Any(av =>
                                av.AttributeValue.AttributeId == f.Id &&
                                normalized.Contains(av.AttributeValue.Value)
                            )
                        )
                    );
                }
            }

            return query;
        }

        // ---------------------------
        //  DTO MAPPING
        // ---------------------------

        private static ProductDto ProductToDto(Models.Product p) =>
            new(
                p.Name,
                p.Description,
                p.BrandId,
                p.CategoryId,
                p.Category.ToCategoryContract(),
                p.Brand?.ToBrandContract(),
                p.ProductTags.Select(t => t.Tag).ToList(),
                p.Variants.Select(VariantToDto).ToList()
            );

        private static ProductVariantDto VariantToDto(Models.ProductVariant v) =>
            new(
                v.Id,
                v.Price,
                v.Stock,
                v.AttributeValues.Select(AttributeToDto).ToList()
            );

        private static ProductVariantAttributeDto AttributeToDto(
            Models.ProductVariantAttributeValue av) =>
            new(
                av.AttributeValue.AttributeId,
                av.AttributeValue.Attribute.Name,
                av.AttributeValue.Value
            );
    }

}

public class ListProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppRoutes.Products + "/", async (
            int? Page,
            int? PageSize,
            string? Q,
            int? MinPrice,
            int? MaxPrice,
            [FromQuery] int[]? Brands,
            [FromQuery] int[]? Categories,
            [FromQuery] ListProducts.AttributeFilter[]? Attributes,
            [FromQuery] string[]? Tags,

            IMediator mediator,
            CancellationToken cancellationToken
         ) =>
        {

            var query = new ListProducts.ListProductsQuery()
            {
                MinPrice = MinPrice,
                MaxPrice = MaxPrice,
                Brands = Brands?.ToList(),
                Categories = Categories?.ToList(),
                Attributes = Attributes?.ToList(),
                Tags = Tags?.ToList(),
                Page = Page,
                PageSize = PageSize,
                Q = Q
            };
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        })
        .RequirePermission(Permissions.Product.List)
        .WithTags(AppRouteTags.Products);
    }
}