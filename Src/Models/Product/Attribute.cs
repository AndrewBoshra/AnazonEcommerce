namespace Anazon.Models;


public class Attribute : AuditableEntity
{
    public string Name { get; set; } = default!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public IEnumerable<AttributeValue> Values { get; set; } = new List<AttributeValue>();
}