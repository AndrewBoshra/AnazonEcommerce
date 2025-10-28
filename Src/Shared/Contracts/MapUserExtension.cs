using Anazon.Models;

namespace Anazon.Shared.Contracts;


public static class MapUserExtension
{
    public static UserAuthInfo ToUserAuthInfo(this User user, string jwtToken, string jwtRefreshToken)
    {
        return new UserAuthInfo
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Email = user.Email,
            Status = user.Status,
            Roles = [.. user.Roles.Select(r => new UserRole
            {
                Name = r.Role.Name,
                Description = r.Role.Description,
                Permissions = [.. r.Role.RolePermissions.Select(rp => rp.Permission.Key)],
            })],
            JwtToken = jwtToken,
            JwtRefreshToken = jwtRefreshToken
        };
    }
}
