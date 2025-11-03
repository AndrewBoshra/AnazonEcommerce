namespace Anazon.Shared.Contracts;


public record class CategoryDetails
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

}



public static class CategoryDetailsMappingExtensions
{
    public static CategoryDetails ToCategoryDetailsContract(this Models.Category category) => new()
    {
        Id = category.Id,
        Name = category.Name,
        Description = category.Description
    };
} 