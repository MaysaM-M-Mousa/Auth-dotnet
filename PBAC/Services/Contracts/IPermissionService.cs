namespace PBAC.Services.Contracts;

public interface IPermissionService
{
    Task<List<string>> GetUserPermissions(Guid userId);
}
