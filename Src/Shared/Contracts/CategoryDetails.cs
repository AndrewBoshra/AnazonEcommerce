namespace Anazon.Shared.Contracts;


public record class CategoryDetails
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public IEnumerable<Category> Children { get; set; } = new List<Category>();

}



public static class CategoryDetailsMappingExtensions
{
    public static CategoryDetails ToCategoryDetailsContract(this Models.Category category, IEnumerable<Models.Category> Children) => new()
    {
        Id = category.Id,
        Name = category.Name,
        Description = category.Description,
        Children = Children.Select(c=>c.ToCategoryContract())
    };
} 