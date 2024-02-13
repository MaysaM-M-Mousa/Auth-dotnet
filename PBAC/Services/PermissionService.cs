using Microsoft.EntityFrameworkCore;
using PBAC.DB;
using PBAC.Services.Contracts;

namespace PBAC.Services;

public class PermissionService : IPermissionService
{
    private readonly PbacContext _context;

    public PermissionService(PbacContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetUserPermissions(Guid userId)
    {
        return await (from usersRoles in _context.UsersRoles
                      join rolesPermissions in _context.RolesPermissions
                      on usersRoles.RoleId equals rolesPermissions.RoleId
                      join permissions in _context.Permissions
                      on rolesPermissions.PermissionId equals permissions.Id
                      where usersRoles.UserId == userId
                      select permissions.Name)
                .Distinct()
                .ToListAsync();
    }
}
