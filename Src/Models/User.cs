namespace Anazon.Models;


public class User : AuditableEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public UserStatus Status { get; set; } = UserStatus.Active;
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public IEnumerable<UserRole> Roles { get; set; } = new List<UserRole>();

    public bool CanLogin => new[] { UserStatus.Active, UserStatus.PendingVerification }
                            .Contains(Status);
}