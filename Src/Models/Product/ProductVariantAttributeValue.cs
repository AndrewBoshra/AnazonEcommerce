namespace Anazon.Models;


public class ProductVariantAttributeValue : AuditableEntity
{
    public int AttributeValueId { get; set; } = default!;
    public AttributeValue AttributeValue { get; set; } = default!;
    
    public int ProductVariantId { get; set; } = default!;
    public ProductVariant ProductVariant { get; set; } = default!;
}