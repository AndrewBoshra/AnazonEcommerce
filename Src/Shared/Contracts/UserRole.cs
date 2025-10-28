
namespace Anazon.Shared.Contracts;


public record class UserRole
{
    public string Name { get; set; } = default!;
    public string Key { get; set; } = default!;
    public string? Description { get; set; } 
    public IEnumerable<string>? Permissions { get; set; } = [];
}

