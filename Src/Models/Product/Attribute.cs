namespace Anazon.Models;


public class Attribute : AuditableEntity
{
    public string Name { get; set; } = default!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public AttributeValue[] Values { get; set; } = Array.Empty<AttributeValue>();
}