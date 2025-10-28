namespace Anazon.Models;


public class RefreshToken : BaseEntity
{
    public int UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public string Token { get; set; } = default!;
    public DateTime ExpirationDate { get; set; } = default!;

    public bool IsExpired => ExpirationDate < DateTime.UtcNow;
}