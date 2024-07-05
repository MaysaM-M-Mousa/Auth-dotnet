using SessionAuth.Models;

namespace SessionAuth.Services;

public interface IUserService
{
    public Task<User?> GetUser(string email);

    Task<List<string>> GetUserRoles(Guid userId);
}
