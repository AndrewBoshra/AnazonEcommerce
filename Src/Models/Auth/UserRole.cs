namespace Anazon.Models;


public class UserRole : BaseEntity
{
    public int RoleId { get; set; }
    public Role Role { get; set; } = default!;

    public int UserId { get; set; }
    public User User { get; set; } = default!;
}