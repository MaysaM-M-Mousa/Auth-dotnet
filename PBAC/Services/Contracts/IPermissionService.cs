using PBAC.Models.Db;

namespace PBAC.Services.Contracts;

public interface IPermissionService
{
    Task CreatePermission(Permission permission);

    Task DeletePermission(int permissionId);

    Task<List<Permission>> GetAllSystemPermissions();

    Task CreateRole(string roleName);

    Task DeleteRole(int roleId);

    Task<List<Role>> GetAllSystemRoles();

    public Task AddUserToRole(Guid userId, int roleId);

    public Task RemoveUserFromRole(Guid userId, int roleId);

    public Task AddPermissionToRole(int permissionId, int roleId);    

    Task RemovePermissionFromRole(int permissionId, int roleId);

    Task<List<string>> GetUserPermissions(Guid userId);

    Task<List<Permission>> GetAllRolePermissions(int roleId);

    Task<List<Role>> GetAllUserRoles(Guid userId);
}
