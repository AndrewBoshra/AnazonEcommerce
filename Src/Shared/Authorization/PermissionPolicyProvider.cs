using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Anazon.Shared.Authorization;


internal class PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {

        var policy = await base.GetPolicyAsync(policyName);

        if (policy is not null) return policy;

        string permission = policyName.Split(Configs.Config.PERMISSION_POLICY_PREFIX).Last();
        return  new AuthorizationPolicyBuilder()
        .AddRequirements(new PermissionRequirement(permission))
        .Build();        
    }
}