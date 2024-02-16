using Microsoft.AspNetCore.Authorization;

namespace PBAC.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    const string POLICY_PREFIX = "CustomPermission:";

    public HasPermissionAttribute(string permission): 
        base(policy: $"{POLICY_PREFIX}{permission}")
    {
    }
}
