namespace Anazon.Models;


public class AttributeValue : AuditableEntity
{
    public int AttributeId { get; set; } = default!;
    public Attribute Attribute { get; set; } = default!;
    public string Value { get; set; } = default!;

    public IEnumerable<ProductVariantAttributeValue> ProductAttributes { get; set; } = new List<ProductVariantAttributeValue>();
}