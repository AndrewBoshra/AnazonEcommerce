namespace Anazon.Shared.Contracts;


public record class BrandDetails
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

}



public static class BrandDetailsMappingExtensions
{
    public static BrandDetails ToBrandDetailsContract(this Models.Brand brand) => new()
    {
        Id = brand.Id,
        Name = brand.Name,
        Description = brand.Description
    };
} 