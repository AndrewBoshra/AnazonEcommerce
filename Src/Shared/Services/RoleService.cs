using Anazon.Configs;
using Anazon.Database;
using Anazon.Models;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Shared.Services;
public class RoleService(AppDbContext db)
{
    public async Task<Role> GetDefaultRole(CancellationToken ct)
    {
        return await db.Roles.FirstAsync(r => r.Key == Roles.DefaultRole, ct)
                    ?? throw new AppException("Default Role is not saved in the database");
    }
}
