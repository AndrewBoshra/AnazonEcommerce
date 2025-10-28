using Microsoft.AspNetCore.Authorization;
namespace Anazon.Shared.Authorization;

public sealed class PermissionRequirement(string Permission) : IAuthorizationRequirement
{
    public string Permission { get; } = Permission;
}