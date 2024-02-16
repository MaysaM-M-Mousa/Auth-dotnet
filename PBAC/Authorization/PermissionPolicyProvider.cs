using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace PBAC.Authorization;

public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    const string POLICY_PREFIX = "CustomPermission:";

    public DefaultAuthorizationPolicyProvider defaultPolicyProvider { get; }

    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        defaultPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return defaultPolicyProvider.GetDefaultPolicyAsync();
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return defaultPolicyProvider.GetFallbackPolicyAsync();
    }

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.StartsWith(POLICY_PREFIX))
        {
            return defaultPolicyProvider.GetPolicyAsync(policyName);
        }

        var permission = policyName.Substring(POLICY_PREFIX.Length);

        var policy = new AuthorizationPolicyBuilder();
        policy.AddRequirements(new PermissionRequirement(permission));

        return Task.FromResult(policy.Build());
    }
}
