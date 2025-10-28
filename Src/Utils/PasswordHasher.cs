using Microsoft.AspNetCore.Identity;

namespace Anazon.Utils;

public class PasswordHasher
{
	public static string HashPassword(string password)
	{
		var hasher = new PasswordHasher<object>();
		return hasher.HashPassword(null, password);
	}

	public static bool VerifyHash(string password, string hashedPassword)
	{
		var hasher = new PasswordHasher<object>();
		var result = hasher.VerifyHashedPassword(null, hashedPassword, password);
		return result == PasswordVerificationResult.Success;
	}

}