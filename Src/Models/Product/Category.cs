namespace Anazon.Models;


public class Category : AuditableEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}