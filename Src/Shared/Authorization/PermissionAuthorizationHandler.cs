using System.Security.Claims;
using Anazon.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace Anazon.Shared.Authorization;

public sealed class PermissionAuthorizationHandler(AppDbContext dbContext) : AuthorizationHandler<PermissionRequirement>
{


    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userIdClaim = context.User.Claims.FirstOrDefault(
            c => c.Type == ClaimTypes.NameIdentifier
        )?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return;


        var hasPermission = await dbContext.Roles
                .Where(r => r.Members.Any(m => m.UserId == userId))
                .AnyAsync(r => r.RolePermissions.Any(rp => rp.Permission.Key == requirement.Permission));


        if (hasPermission)
            context.Succeed(requirement);


    }
}