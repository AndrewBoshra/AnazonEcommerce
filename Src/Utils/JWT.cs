using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Anazon.Configs;
using Anazon.Shared;

namespace Anazon.Utils;


public class JWT(JWTConfig configuration)
{
    public string GenerateToken(UserClaims claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, claims.UserId.ToString()),
                new Claim(ClaimTypes.Email, claims.Email),
                ..claims.Roles.Select(role => new Claim(ClaimTypes.Role, role))
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.ExpiryInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration.Issuer,
            Audience = configuration.Audience,
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


    
}  