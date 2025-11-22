namespace Anazon.Models;


public class Product : AuditableEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int? BrandId { get; set; }
    public Brand? Brand { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public IEnumerable<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

}