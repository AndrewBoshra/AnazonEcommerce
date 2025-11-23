namespace Anazon.Models;


public class ProductVariant : AuditableEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public IEnumerable<ProductVariantAttributeValue> AttributeValues { get; set; } = new List<ProductVariantAttributeValue>();    
}