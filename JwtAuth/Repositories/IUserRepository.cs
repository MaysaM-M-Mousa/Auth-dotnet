using JwtAuth.Db;

namespace JwtAuth.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUser(long userId);

    public Task<User?> CreateUser(User user);

    public Task<User?> GetUser(string email);
}
