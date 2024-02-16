using Microsoft.AspNetCore.Mvc;
using PBAC.Authorization;
using PBAC.Services.Contracts;

namespace PBAC.Controllers;

[HasPermission("user.permissions:write")]
[Route("api/[controller]")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    /// <summary>
    /// Creates a permissions
    /// </summary>
    [HttpPost]
    public async Task CreatePermission(PermissionDto permission)
    {
        await _permissionService.CreatePermission(new()
        {
            Id = permission.Id,
            Name = permission.Name,
        });
    }

    /// <summary>
    /// Deletes a permission
    /// </summary>
    [HttpDelete("permissions/{permissionId}")]
    public async Task DeletePermission(int permissionId)
    {
        await _permissionService.DeletePermission(permissionId);
    }

    /// <summary>
    /// Gets All system's permission
    /// </summary>
    [HttpGet]
    public async Task<List<PermissionDto>> GetSystemPermissions()
    {
        return (await _permissionService.GetAllSystemPermissions())
            .Select(p => new PermissionDto() { Id = p.Id, Name = p.Name })
            .ToList();
    }

    /// <summary>
    /// Creates a role
    /// </summary>
    [HttpPost("roles")]
    public async Task CreateRole(RoleDto role)
    {
        await _permissionService.CreateRole(role.Name);
    }

    /// <summary>
    /// Deletes a role
    /// </summary>
    [HttpDelete("roles/{roleId}")]
    public async Task DeleteRole(int roleId)
    {
        await _permissionService.DeleteRole(roleId);
    }

    /// <summary>
    /// Gets All system's roles
    /// </summary>
    [HttpGet("roles")]
    public async Task<List<RoleDto>> GetSystemRoles()
    {
        return (await _permissionService.GetAllSystemRoles())
            .Select(p => new RoleDto() { Id = p.Id, Name = p.Name })
            .ToList();
    }

    /// <summary>
    /// Adds a user to a role
    /// </summary>
    [HttpPost("users/roles")]
    public async Task AddUserToRole(UserRoleRequest request)
    {
        await _permissionService.AddUserToRole(request.UserId, request.RoleId);
    }

    /// <summary>
    /// Removes a user from a role
    /// </summary>
    [HttpDelete("users/roles")]
    public async Task RemoveUserFromRole(UserRoleRequest request)
    {
        await _permissionService.RemoveUserFromRole(request.UserId, request.RoleId);
    }

    /// <summary>
    /// Adds a permission to a role
    /// </summary>
    [HttpPost("roles/permissions")]
    public async Task AddPermissionToRole(RolePermissionRequest request)
    {
        await _permissionService.AddPermissionToRole(request.PermissionId, request.RoleId);
    }

    /// <summary>
    /// Removes a permission from a role
    /// </summary>
    [HttpDelete("roles/permissions")]
    public async Task RemovePermissionFromRole(RolePermissionRequest request)
    {
        await _permissionService.RemovePermissionFromRole(request.PermissionId, request.RoleId);
    }

    /// <summary>
    /// Gets all user's permissions
    /// </summary>
    [HttpGet("users/{userId}")]
    public async Task<List<string>> GetUserPermissions(Guid userId)
    {
        return await _permissionService.GetUserPermissions(userId);
    }
}

public class UserRoleRequest
{
    public Guid UserId { get; set; }

    public int RoleId { get; set; }
}

public class RolePermissionRequest
{
    public int PermissionId { get; set; }

    public int RoleId { get; set; }
}

public class PermissionDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}

public class RoleDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}