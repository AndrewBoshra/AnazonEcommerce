namespace Anazon.Shared;

public record UserClaims
{
    public int UserId { get; init; }
    public string Email { get; init; } = default!;
    public IEnumerable<string> Roles { get; init; } = [];

}   

