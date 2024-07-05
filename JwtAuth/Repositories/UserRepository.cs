using JwtAuth.Db;
using Microsoft.EntityFrameworkCore;

namespace JwtAuth.Repositories;

public class UserRepository : IUserRepository
{
    private readonly JwtAuthDb _context;
    public UserRepository(JwtAuthDb context)
    {
        _context = context;
    }
    public async Task<User?> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetUser(long userId)
    {
        return await _context
            .Users
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUser(string email)
    {
        return await _context
            .Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}
