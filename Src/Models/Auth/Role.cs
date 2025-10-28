namespace Anazon.Models;


public class Role : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Key { get; set; } = default!;
    public string? Description { get; set; }

    public IEnumerable<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public IEnumerable<UserRole> Members { get; set; } = new List<UserRole>();
}