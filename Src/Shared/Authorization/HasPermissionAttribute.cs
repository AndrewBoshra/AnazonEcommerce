using Anazon.Configs;
using Microsoft.AspNetCore.Authorization;
namespace Anazon.Shared.Authorization;
public sealed class HasPermissionAttribute(string permission) : AuthorizeAttribute(Config.PERMISSION_POLICY_PREFIX + permission)
{
}