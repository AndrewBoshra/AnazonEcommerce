namespace Anazon.Models;


public class Brand : AuditableEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }


    public IEnumerable<Product> Products { get; set; } = new List<Product>();

}