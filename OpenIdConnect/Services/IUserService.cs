using OpenIdConnect.Models;
using System.Security.Claims;

namespace OpenIdConnect.Services;

public interface IUserService
{
    Task EnsureUserCreatedAsync(CreateUserRequest request);

    Task AssignUserRolesAsync(ClaimsPrincipal principal);
}
