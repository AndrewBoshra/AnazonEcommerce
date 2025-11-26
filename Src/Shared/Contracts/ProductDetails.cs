
namespace Anazon.Shared.Contracts;



public record ProductVariantAttributeDetails(
    int AttributeId,
    string Name,
    string Value
);
public record ProductVariantDetails(
    int Id,
    decimal Price,
    int Stock,
    List<ProductVariantAttributeDetails> Details
);
public record ProductDetails(
    int Id,
    string Name,
    string? Description,
    int? BrandId,
    Brand? Brand,
    int CategoryId,
    Category Category,
    List<string> Tags,
    List<ProductVariantDetails> Variants
);


public static class ProductDetailsMappingExtensions
{


    private static ProductVariantDetails ToProductVariantDetailsContract(this Models.ProductVariant Product) => new ProductVariantDetails(
        Id: Product.Id,
        Price: Product.Price,
        Stock: Product.Stock,
        Details: Product.AttributeValues.Select(av => new ProductVariantAttributeDetails(
            AttributeId: av.AttributeValue.AttributeId,
            Name: av.AttributeValue.Attribute.Name,
            Value: av.AttributeValue.Value
        )).ToList()
    );
    public static ProductDetails ToProductDetailsContract(this Models.Product Product) => new ProductDetails(
        Id: Product.Id,
        Name: Product.Name,
        Description: Product.Description,
        BrandId: Product.BrandId,
        Brand: Product.Brand?.ToBrandContract(),
        CategoryId: Product.CategoryId,
        Category: Product.Category.ToCategoryContract(),
        Tags: Product.ProductTags.Select(t => t.Tag).ToList(),
        Variants: Product.Variants.Select(pv=>pv.ToProductVariantDetailsContract()).ToList()
    );
}