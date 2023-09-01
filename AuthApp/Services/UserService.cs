using AuthApp.Db;
using AuthApp.Repositories;

namespace AuthApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;

    public UserService(IUserRepository userRepository, IHashService hashService)
    {
        _userRepository = userRepository;
        _hashService = hashService;
    }

    public async Task<User> CreateUser(User user)
    {
        user.Password = _hashService.HashPassword(user.Password);
        await _userRepository.CreateUser(user);
        return user;
    }

    public async Task<User> GetUser(long userId)
    {
        return await _userRepository.GetUser(userId);
    }
    public async Task<User> GetUser(string email)
    {
        return await _userRepository.GetUser(email);
    }
}
