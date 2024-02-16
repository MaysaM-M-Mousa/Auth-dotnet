using Microsoft.EntityFrameworkCore;
using PBAC.DB;
using PBAC.Models.Db;
using PBAC.Services.Contracts;

namespace PBAC.Services;

public class PermissionService : IPermissionService
{
    private readonly PbacContext _context;

    public PermissionService(PbacContext context)
    {
        _context = context;
    }

    // Permissions CRUD: [create, delete, get all]
    public async Task CreatePermission(Permission permission)
    {
        _context.Add(permission);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePermission(int permissionId)
    {
        _context.Remove(new Permission() { Id = permissionId });
        await _context.SaveChangesAsync();
    }

    public async Task<List<Permission>> GetAllSystemPermissions()
    {
        return await _context
            .Permissions
            .ToListAsync();
    }

    // Roels CRUD: [create, delete, get all]
    public async Task CreateRole(string roleName)
    {
        _context.Add(new Role() { Name = roleName });
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRole(int roleId)
    {
        _context.Remove(new Role() { Id = roleId });
        await _context.SaveChangesAsync();
    }

    public async Task<List<Role>> GetAllSystemRoles()
    {
        return await _context
            .Roles
            .ToListAsync();
    }

    // Roles Actions: [addUserToRole, RemoveUserFromRole]
    public async Task AddUserToRole(Guid userId, int roleId)
    {
        var userRoleRecord = new UserRole()
        {
            UserId = userId,
            RoleId = roleId
        };
        _context.Add(userRoleRecord);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveUserFromRole(Guid userId, int roleId)
    {
        var userRoleRecord = new UserRole()
        {
            UserId = userId,
            RoleId = roleId
        };
        _context.Remove(userRoleRecord);
        await _context.SaveChangesAsync();
    }

    // Roles Actions: [addPermissionToRole, removePermissionFromRole]
    public async Task AddPermissionToRole(int permissionId, int roleId)
    {
        var permissionRoleRecord = new RolePermission()
        {
            RoleId = roleId,
            PermissionId = permissionId
        };
        _context.Add(permissionRoleRecord);
        await _context.SaveChangesAsync();
    }

    public async Task RemovePermissionFromRole(int permissionId, int roleId)
    {
        var permissionRoleRecord = new RolePermission()
        {
            RoleId = roleId,
            PermissionId = permissionId
        };
        _context.Remove(permissionRoleRecord);
        await _context.SaveChangesAsync();
    }

    // General Retrievals: [getAllUserPermissions, getAllRolePermissions, getAllUserRoles]
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
    
    public async Task<List<Permission>> GetAllRolePermissions(int roleId)
    {
        return await (from rolePermissions in _context.RolesPermissions
                    join permissions in _context.Permissions
                    on rolePermissions.PermissionId equals permissions.Id
                    where rolePermissions.RoleId == roleId
                    select permissions).ToListAsync();
    }

    public async Task<List<Role>> GetAllUserRoles(Guid userId)
    {
        return await (from userRoles in _context.UsersRoles
                      join roles in _context.Roles
                      on userRoles.RoleId equals roles.Id
                      where userRoles.UserId == userId
                      select roles).ToListAsync();
    }
}
