using Anazon.Database;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Shared.Services;
public class UserDuplicateChecker(AppDbContext db)
{
    public async Task<Result> CheckAsync(string email, string phone, CancellationToken ct)
    {
        var exists = await db.Users
            .Select(u => new { u.Email, u.Phone })
            .FirstOrDefaultAsync(u => u.Email == email || u.Phone == phone, ct);

        if (exists is null)
            return Result.Success();

        if (exists.Email == email)
            return Result.Failure(Error.EmailAlreadyInUse);

        return Result.Failure(Error.PhoneAlreadyInUse);
    }
}
