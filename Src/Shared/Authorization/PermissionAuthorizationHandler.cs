using System.Security.Claims;
using Anazon.Configs;
using Anazon.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace Anazon.Shared.Authorization;

public sealed class PermissionAuthorizationHandler(AppDbContext dbContext) : AuthorizationHandler<PermissionRequirement>
{


    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var roles = context.User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value!)
            .Append(Roles.Anonymous);


        var hasPermission = await dbContext.Roles
                .Where(r => roles.Contains(r.Key))
                .AnyAsync(r => r.RolePermissions.Any(rp => rp.Permission.Key == requirement.Permission));


        if (hasPermission)
            context.Succeed(requirement);

    }
}