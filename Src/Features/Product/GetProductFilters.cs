using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;

using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class GetProductFilters
{


    public record Query : IRequest<Response>;


    public class Response
    {
        public PriceRange Price { get; set; } = null!;
        public List<BrandDto> Brands { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
        public List<AttributeDto> Attributes { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }

    public record PriceRange(decimal Min, decimal Max);
    public record BrandDto(int Id, string Name);
    public record CategoryDto(int Id, string Name);

    public class AttributeDto
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public List<string> Values { get; set; } = new();
    }

    // -----------------------------
    // Handler
    // -----------------------------
    public class Handler(AppDbContext db) : IRequestHandler<Query, Response>
    {
        public async Task<Response> Handle(Query request, CancellationToken ct)
        {
            // -----------------------------
            // Price range
            // -----------------------------
            var minPrice = await db.ProductVariants.MinAsync(p => p.Price, ct);
            var maxPrice = await db.ProductVariants.MaxAsync(p => p.Price, ct);

            // -----------------------------
            // Brands
            // -----------------------------
            var brands = await db.Brands
                .Select(b => new BrandDto(b.Id, b.Name))
                .ToListAsync(ct);

            // -----------------------------
            // Categories
            // -----------------------------
            var categories = await db.Categories
                .Select(c => new CategoryDto(c.Id, c.Name))
                .ToListAsync(ct);

            // -----------------------------
            // Tags
            // -----------------------------
            var tags = await db.Tags
                .Select(p => p.Key)
                .Distinct()
                .ToListAsync(ct);



            var attributes = await db.AttributeValues
                .Where(av => av.ProductAttributes.Any())
                .GroupBy(a => a.Attribute.Id)
                .Select(g => new AttributeDto
                {
                    Id = g.Key,
                    Name = g.First().Attribute.Name,
                    Values = g.Select(v => v.Value).OrderBy(v=>v).Distinct().ToList()
                })
                .ToListAsync(ct);


            return new Response
            {
                Price = new PriceRange(minPrice, maxPrice),
                Brands = brands,
                Categories = categories,
                Attributes = attributes,
                Tags = tags
            };
        }
    }
}

public class GetProductFiltersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppRoutes.Products + "/filters", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductFilters.Query());

            return Results.Ok(result);
        }).RequirePermission(Permissions.Product.List)
        .WithTags(AppRouteTags.Products);
    }
}