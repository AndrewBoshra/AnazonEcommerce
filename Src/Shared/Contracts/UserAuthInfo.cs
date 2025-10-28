using Anazon.Models;

namespace Anazon.Shared.Contracts;


public record class UserAuthInfo
{
    public int Id { get; init; }
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Phone { get; init; } = default!;
    public string Email { get; init; } = default!;
    public UserStatus Status { get; init; } = UserStatus.Active;
    public IEnumerable<UserRole> Roles { get; init; } = [];



    public string JwtToken { get; init; } = default!;
    public string JwtRefreshToken { get; init; } = default!;

}
