using Anazon.Configs;
using Anazon.Database;
using Anazon.Models;
using Anazon.Utils;

namespace Anazon.Shared.Services;

public class TokenService(AppDbContext db, JWT jwt, JWTConfig config)
{


    public string GenerateJwt(User user)
    {

        return jwt.GenerateToken(new()
        {
            UserId = user.Id,
            Email = user.Email,
            Roles = user.Roles.Select(ur => ur.Role.Key)
        });

    }
    public async Task<RefreshToken> GenerateRefreshToken(User user, CancellationToken ct= default)
    {
        var refresh = new RefreshToken
        {
            ExpirationDate = DateTime.UtcNow.AddMinutes(config.RefreshExpiryInMinutes),
            Token = RefreshTokenGenerator.GenerateRefreshToken(),
            User = user
        };

        await db.RefreshTokens.AddAsync(refresh,ct);

        return refresh;
    }
    
    public async Task<(string Jwt, RefreshToken Refresh)> GenerateTokens(User user)
    {

        return (GenerateJwt(user), await GenerateRefreshToken(user));
    }
}
