namespace Anazon.Shared.Contracts;


public record class Category
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

}


public static class CategoryMappingExtensions
{
    public static Category ToCategoryContract(this Models.Category Category)
    {
        return new Category
        {
            Id = Category.Id,
            Name = Category.Name,
            Description = Category.Description
        };
    }
}