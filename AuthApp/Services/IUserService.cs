using AuthApp.Db;

namespace AuthApp.Services;

public interface IUserService
{
    public Task<User> CreateUser(User user);

    public Task<User> GetUser(long userId);

    public Task<User> GetUser(string email);

}
