

namespace Anazon.Configs;


public record JWTConfig
{

    public string SecretKey { get; set; } = default!;
    public int ExpiryInMinutes { get; set; }
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;

    public int RefreshExpiryInMinutes { get; set; }


}


