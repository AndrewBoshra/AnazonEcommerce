namespace Anazon.Shared.Contracts;


public record class Brand
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

}


public static class BrandMappingExtensions
{
    public static Brand ToBrandContract(this Models.Brand brand)
    {
        return new Brand
        {
            Id = brand.Id,
            Name = brand.Name,
            Description = brand.Description
        };
    }
}