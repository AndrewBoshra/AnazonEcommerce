namespace Anazon.Shared.Contracts;


public record class Attribute
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int CategoryId { get; set; }

}


public static class AttributeMappingExtensions
{
    public static Attribute ToAttributeContract(this Models.Attribute attribute)
    {
        return new Attribute
        {
            Id = attribute.Id,
            Name = attribute.Name,
            CategoryId = attribute.CategoryId
        };
    }
}