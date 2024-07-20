using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenIdConnect.Db;
using OpenIdConnect.Db.Models;
using OpenIdConnect.Models;
using System.Security.Claims;

namespace OpenIdConnect.Services;

public class UserService : IUserService
{
    private readonly OidcDb _context;

    private List<string> DefaultRoles = new List<string>() { "User" };

    public UserService(OidcDb context)
    {
        _context = context;
    }

    public async Task EnsureUserCreatedAsync(CreateUserRequest request)
    {
        var systemUser = await GetUserAsync(request.Email);
        var now = DateTime.UtcNow;
        
        if (systemUser is null)
        {
            systemUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                CreatedAt = now,
            };

            _context.Add(systemUser);
        }

        if (await IsNewExternalLoginAsync(request.ProviderKey, request.Provider))
        {
            var externalLogin = new ExternalLogin()
            {
                Id = Guid.NewGuid(),
                Provider = request.Provider,
                ProviderKey = request.ProviderKey,
                UserId = systemUser.Id,
                Username = request.Username,
                CreatedAt = now
            };

            _context.Add(externalLogin);
        }

        await _context.SaveChangesAsync();
    }

    public async Task AssignUserRolesAsync(ClaimsPrincipal principal)
    {
        var providerKey = principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var userRoles = await GetUserRolesAsync(providerKey);

        if (userRoles.IsNullOrEmpty())
        {
            userRoles = DefaultRoles;
        }

        var identity = principal.Identity as ClaimsIdentity;

        foreach(var role in userRoles!)
        {
            identity!.AddClaim(new(ClaimTypes.Role, role));
        }
    }

    private async Task<List<string>?> GetUserRolesAsync(string providerKey)
    {
        var userRolesQuery = from externalLogins in _context.ExternalLogins
                             join externalLoginsRoles in _context.ExternalLoginRoles on externalLogins.Id equals externalLoginsRoles.ExternalLoginId
                             join roles in _context.Roles on externalLoginsRoles.RoleId equals roles.Id
                             where externalLogins.ProviderKey == providerKey
                             select roles.Name;

        return await userRolesQuery.Distinct().ToListAsync();
    }

    private async Task<User?> GetUserAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ;
    
    private async Task<bool> IsNewExternalLoginAsync(string providerKey, string provider)
        => await _context.ExternalLogins
        .FirstOrDefaultAsync(x => x.ProviderKey == providerKey && x.Provider == provider) is null;
}

