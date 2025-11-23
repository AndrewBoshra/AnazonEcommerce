using Anazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anazon.Database.Config;

public class CategorySeedConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        var now = new DateTime(2025, 11, 22, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            new Category { Id = 1, Name = "Clothing", Description = "All clothing items", ParentCategoryId = null, CreatedAt = now },
            new Category { Id = 2, Name = "Shoes", Description = "Footwear and shoes", ParentCategoryId = 1, CreatedAt = now },
            new Category { Id = 3, Name = "Shirts", Description = "Shirts and tops", ParentCategoryId = 1, CreatedAt = now },
            new Category { Id = 4, Name = "Tech Products", Description = "Electronic and tech products", ParentCategoryId = null, CreatedAt = now },
            new Category { Id = 5, Name = "Smartphones", Description = "Mobile phones and smartphones", ParentCategoryId = 4, CreatedAt = now },
            new Category { Id = 6, Name = "Laptops", Description = "Laptops and notebooks", ParentCategoryId = 4, CreatedAt = now },
            new Category { Id = 7, Name = "Headphones", Description = "Headphones and earbuds", ParentCategoryId = 4, CreatedAt = now },
            new Category { Id = 8, Name = "Tech Accessories", Description = "Chargers, cables and accessories", ParentCategoryId = 4, CreatedAt = now },
            new Category { Id = 9, Name = "Women's Clothing", Description = "Clothing for women", ParentCategoryId = 1, CreatedAt = now },
            new Category { Id = 10, Name = "Men's Clothing", Description = "Clothing for men", ParentCategoryId = 1, CreatedAt = now },
            new Category { Id = 11, Name = "Kids' Clothing", Description = "Clothing for children", ParentCategoryId = 1, CreatedAt = now },
            new Category { Id = 12, Name = "Home & Kitchen", Description = "Home appliances and kitchenware", ParentCategoryId = null, CreatedAt = now },
            new Category { Id = 13, Name = "Sports & Outdoors", Description = "Sporting goods and outdoor", ParentCategoryId = null, CreatedAt = now },
            new Category { Id = 14, Name = "Beauty & Personal Care", Description = "Cosmetics and personal care", ParentCategoryId = null, CreatedAt = now },
            new Category { Id = 15, Name = "Toys & Games", Description = "Toys and games for children", ParentCategoryId = null, CreatedAt = now },
            new Category { Id = 16, Name = "Books", Description = "Books across genres", ParentCategoryId = null, CreatedAt = now },
            new Category { Id = 17, Name = "Automotive", Description = "Automotive parts and accessories", ParentCategoryId = null, CreatedAt = now },
            new Category { Id = 18, Name = "Jewelry & Watches", Description = "Jewelry and watches", ParentCategoryId = null, CreatedAt = now },
            new Category { Id = 19, Name = "Bags & Luggage", Description = "Bags, backpacks and luggage", ParentCategoryId = null, CreatedAt = now },
            new Category { Id = 20, Name = "Office Supplies", Description = "Office stationery and supplies", ParentCategoryId = null, CreatedAt = now }
        );
    }
}

public class ProductSeedConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        var now = new DateTime(2025, 11, 22, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            new Product { Id = 1, Name = "Running Sneakers", Description = "Lightweight running shoes", CategoryId = 2, BrandId = null, CreatedAt = now },
            new Product { Id = 2, Name = "Casual Shirt", Description = "Comfortable casual shirt", CategoryId = 3, BrandId = null, CreatedAt = now },
            new Product { Id = 3, Name = "Smartphone X", Description = "Flagship smartphone", CategoryId = 5, BrandId = null, CreatedAt = now },
            new Product { Id = 4, Name = "Trail Running Shoes", Description = "Durable trail shoes", CategoryId = 2, BrandId = null, CreatedAt = now },
            new Product { Id = 5, Name = "Formal Shirt", Description = "Men's formal shirt", CategoryId = 3, BrandId = null, CreatedAt = now },
            new Product { Id = 6, Name = "Women's Dress", Description = "Elegant evening dress", CategoryId = 9, BrandId = null, CreatedAt = now },
            new Product { Id = 7, Name = "Kids Sneakers", Description = "Comfortable kids shoes", CategoryId = 11, BrandId = null, CreatedAt = now },
            new Product { Id = 8, Name = "Laptop Pro 14", Description = "High performance laptop", CategoryId = 6, BrandId = null, CreatedAt = now },
            new Product { Id = 9, Name = "Wireless Headphones", Description = "Noise cancelling headphones", CategoryId = 7, BrandId = null, CreatedAt = now },
            new Product { Id = 10, Name = "USB-C Charger", Description = "Fast charger 65W", CategoryId = 8, BrandId = null, CreatedAt = now },
            new Product { Id = 11, Name = "Blender 3000", Description = "Kitchen blender", CategoryId = 12, BrandId = null, CreatedAt = now },
            new Product { Id = 12, Name = "Yoga Mat", Description = "Non-slip yoga mat", CategoryId = 13, BrandId = null, CreatedAt = now },
            new Product { Id = 13, Name = "Lipstick", Description = "Long lasting lipstick", CategoryId = 14, BrandId = null, CreatedAt = now },
            new Product { Id = 14, Name = "Building Blocks Set", Description = "Educational toy", CategoryId = 15, BrandId = null, CreatedAt = now },
            new Product { Id = 15, Name = "Bestselling Novel", Description = "Top seller fiction book", CategoryId = 16, BrandId = null, CreatedAt = now },
            new Product { Id = 16, Name = "Car Air Freshener", Description = "Vanilla scent", CategoryId = 17, BrandId = null, CreatedAt = now },
            new Product { Id = 17, Name = "Gold Necklace", Description = "Elegant necklace", CategoryId = 18, BrandId = null, CreatedAt = now },
            new Product { Id = 18, Name = "Travel Backpack", Description = "Water resistant backpack", CategoryId = 19, BrandId = null, CreatedAt = now },
            new Product { Id = 19, Name = "Ballpoint Pens (12)", Description = "Blue ink pens", CategoryId = 20, BrandId = null, CreatedAt = now },
            new Product { Id = 20, Name = "Smartwatch Z", Description = "Fitness & notifications", CategoryId = 18, BrandId = null, CreatedAt = now },
            new Product { Id = 21, Name = "Gaming Laptop", Description = "High-end GPU laptop", CategoryId = 6, BrandId = null, CreatedAt = now },
            new Product { Id = 22, Name = "Wireless Mouse", Description = "Ergonomic mouse", CategoryId = 8, BrandId = null, CreatedAt = now },
            new Product { Id = 23, Name = "Cookbook", Description = "Healthy recipes", CategoryId = 16, BrandId = null, CreatedAt = now },
            new Product { Id = 24, Name = "Action Camera", Description = "Waterproof action camera", CategoryId = 4, BrandId = null, CreatedAt = now }
        );
    }
}

public class ProductVariantSeedConfig : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        var now = new DateTime(2025, 11, 22, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            new ProductVariant { Id = 1, ProductId = 1, Price = 79.99m, Stock = 50, CreatedAt = now },
            new ProductVariant { Id = 2, ProductId = 1, Price = 89.99m, Stock = 30, CreatedAt = now },
            new ProductVariant { Id = 3, ProductId = 2, Price = 29.99m, Stock = 100, CreatedAt = now },
            new ProductVariant { Id = 4, ProductId = 3, Price = 699.00m, Stock = 25, CreatedAt = now },
            new ProductVariant { Id = 5, ProductId = 4, Price = 109.99m, Stock = 40, CreatedAt = now },
            new ProductVariant { Id = 6, ProductId = 5, Price = 39.99m, Stock = 80, CreatedAt = now },
            new ProductVariant { Id = 7, ProductId = 6, Price = 129.99m, Stock = 20, CreatedAt = now },
            new ProductVariant { Id = 8, ProductId = 7, Price = 49.99m, Stock = 60, CreatedAt = now },
            new ProductVariant { Id = 9, ProductId = 8, Price = 1499.00m, Stock = 10, CreatedAt = now },
            new ProductVariant { Id = 10, ProductId = 9, Price = 199.99m, Stock = 45, CreatedAt = now },
            new ProductVariant { Id = 11, ProductId = 10, Price = 29.99m, Stock = 200, CreatedAt = now },
            new ProductVariant { Id = 12, ProductId = 11, Price = 89.99m, Stock = 35, CreatedAt = now },
            new ProductVariant { Id = 13, ProductId = 12, Price = 24.99m, Stock = 120, CreatedAt = now },
            new ProductVariant { Id = 14, ProductId = 13, Price = 14.99m, Stock = 150, CreatedAt = now },
            new ProductVariant { Id = 15, ProductId = 14, Price = 59.99m, Stock = 70, CreatedAt = now },
            new ProductVariant { Id = 16, ProductId = 15, Price = 19.99m, Stock = 500, CreatedAt = now },
            new ProductVariant { Id = 17, ProductId = 16, Price = 7.99m, Stock = 300, CreatedAt = now },
            new ProductVariant { Id = 18, ProductId = 17, Price = 249.99m, Stock = 5, CreatedAt = now },
            new ProductVariant { Id = 19, ProductId = 18, Price = 79.99m, Stock = 80, CreatedAt = now },
            new ProductVariant { Id = 20, ProductId = 19, Price = 5.99m, Stock = 1000, CreatedAt = now },
            new ProductVariant { Id = 21, ProductId = 20, Price = 199.99m, Stock = 60, CreatedAt = now },
            new ProductVariant { Id = 22, ProductId = 21, Price = 2199.00m, Stock = 8, CreatedAt = now },
            new ProductVariant { Id = 23, ProductId = 22, Price = 29.99m, Stock = 150, CreatedAt = now },
            new ProductVariant { Id = 24, ProductId = 23, Price = 27.99m, Stock = 90, CreatedAt = now },
            new ProductVariant { Id = 25, ProductId = 24, Price = 299.99m, Stock = 25, CreatedAt = now }
        );
    }
}


public class TagSeedConfig : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasData(
            new { Key = "running" },
            new { Key = "men" },
            new { Key = "sports" },
            new { Key = "casual" },
            new { Key = "electronics" },
            new { Key = "mobile" },
            new { Key = "new" },
            new { Key = "women" },
            new { Key = "kids" },
            new { Key = "smart" },
            new { Key = "luxury" },
            new { Key = "gaming" },
            new { Key = "home" },
            new { Key = "kitchen" },
            new { Key = "fitness" },
            new { Key = "outdoor" },
            new { Key = "beauty" },
            new { Key = "book" },
            new { Key = "toy" },
            new { Key = "auto" },
            new { Key = "office" },
            new { Key = "bag" },
            new { Key = "watch" },
            new { Key = "headphones" },
            new { Key = "laptop" },
            new { Key = "formal" },
            new { Key = "charger" }
        );
    }
}
public class ProductTagSeedConfig : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
        builder.HasData(
            new { ProductId = 1, Tag = "running" },
            new { ProductId = 1, Tag = "men" },
            new { ProductId = 1, Tag = "sports" },
            new { ProductId = 2, Tag = "casual" },
            new { ProductId = 2, Tag = "men" },
            new { ProductId = 3, Tag = "mobile" },
            new { ProductId = 3, Tag = "new" },
            new { ProductId = 4, Tag = "running" },
            new { ProductId = 4, Tag = "outdoor" },
            new { ProductId = 5, Tag = "formal" },
            new { ProductId = 5, Tag = "men" },
            new { ProductId = 6, Tag = "women" },
            new { ProductId = 6, Tag = "luxury" },
            new { ProductId = 7, Tag = "kids" },
            new { ProductId = 8, Tag = "laptop" },
            new { ProductId = 8, Tag = "gaming" },
            new { ProductId = 9, Tag = "headphones" },
            new { ProductId = 9, Tag = "electronics" },
            new { ProductId = 10, Tag = "charger" },
            new { ProductId = 11, Tag = "kitchen" },
            new { ProductId = 12, Tag = "fitness" },
            new { ProductId = 13, Tag = "beauty" },
            new { ProductId = 14, Tag = "toy" },
            new { ProductId = 15, Tag = "book" },
            new { ProductId = 16, Tag = "auto" },
            new { ProductId = 17, Tag = "luxury" },
            new { ProductId = 18, Tag = "bag" },
            new { ProductId = 19, Tag = "office" },
            new { ProductId = 20, Tag = "watch" },
            new { ProductId = 21, Tag = "gaming" },
            new { ProductId = 21, Tag = "laptop" },
            new { ProductId = 22, Tag = "office" },
            new { ProductId = 23, Tag = "book" },
            new { ProductId = 24, Tag = "electronics" },
            new { ProductId = 24, Tag = "outdoor" }
        );
    }
}

public class AttributeSeedConfig : IEntityTypeConfiguration<Anazon.Models.Attribute>
{
    public void Configure(EntityTypeBuilder<Anazon.Models.Attribute> builder)
    {
        var now = new DateTime(2025, 11, 22, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            new Anazon.Models.Attribute { Id = 1, Name = "Size", CategoryId = 2, CreatedAt = now }, // Shoes
            new Anazon.Models.Attribute { Id = 2, Name = "Size", CategoryId = 3, CreatedAt = now }, // Shirts
            new Anazon.Models.Attribute { Id = 3, Name = "Color", CategoryId = 1, CreatedAt = now } // Clothing
        );
    }
}

public class AttributeValueSeedConfig : IEntityTypeConfiguration<AttributeValue>
{
    public void Configure(EntityTypeBuilder<AttributeValue> builder)
    {
        var now = new DateTime(2025, 11, 22, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            // Shoe sizes
            new AttributeValue { Id = 1, AttributeId = 1, Value = "8", CreatedAt = now },
            new AttributeValue { Id = 2, AttributeId = 1, Value = "9", CreatedAt = now },
            new AttributeValue { Id = 3, AttributeId = 1, Value = "10", CreatedAt = now },
            new AttributeValue { Id = 4, AttributeId = 1, Value = "11", CreatedAt = now },
            // Shirt sizes
            new AttributeValue { Id = 5, AttributeId = 2, Value = "S", CreatedAt = now },
            new AttributeValue { Id = 6, AttributeId = 2, Value = "M", CreatedAt = now },
            new AttributeValue { Id = 7, AttributeId = 2, Value = "L", CreatedAt = now },
            new AttributeValue { Id = 8, AttributeId = 2, Value = "XL", CreatedAt = now },
            // Colors
            new AttributeValue { Id = 9, AttributeId = 3, Value = "Black", CreatedAt = now },
            new AttributeValue { Id = 10, AttributeId = 3, Value = "White", CreatedAt = now },
            new AttributeValue { Id = 11, AttributeId = 3, Value = "Blue", CreatedAt = now },
            new AttributeValue { Id = 12, AttributeId = 3, Value = "Red", CreatedAt = now }
        );
    }
}

public class ProductVariantAttributeValueSeedConfig : IEntityTypeConfiguration<ProductVariantAttributeValue>
{
    public void Configure(EntityTypeBuilder<ProductVariantAttributeValue> builder)
    {
        var now = new DateTime(2025, 11, 22, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            new ProductVariantAttributeValue { Id = 1, ProductVariantId = 1, AttributeValueId = 2, CreatedAt = now }, // variant1 size 9
            new ProductVariantAttributeValue { Id = 2, ProductVariantId = 1, AttributeValueId = 9, CreatedAt = now }, // variant1 color Black
            new ProductVariantAttributeValue { Id = 3, ProductVariantId = 2, AttributeValueId = 3, CreatedAt = now }, // variant2 size 10
            new ProductVariantAttributeValue { Id = 4, ProductVariantId = 2, AttributeValueId = 10, CreatedAt = now }, // variant2 color White
            new ProductVariantAttributeValue { Id = 5, ProductVariantId = 3, AttributeValueId = 6, CreatedAt = now }, // variant3 size M
            new ProductVariantAttributeValue { Id = 6, ProductVariantId = 3, AttributeValueId = 11, CreatedAt = now }, // variant3 color Blue
            new ProductVariantAttributeValue { Id = 7, ProductVariantId = 5, AttributeValueId = 3, CreatedAt = now }, // variant5 size 10
            new ProductVariantAttributeValue { Id = 8, ProductVariantId = 5, AttributeValueId = 12, CreatedAt = now }, // variant5 color Red
            new ProductVariantAttributeValue { Id = 9, ProductVariantId = 6, AttributeValueId = 7, CreatedAt = now }, // variant6 size L
            new ProductVariantAttributeValue { Id = 10, ProductVariantId = 6, AttributeValueId = 10, CreatedAt = now } // variant6 color White
        );
    }
}
