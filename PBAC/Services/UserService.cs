using Microsoft.EntityFrameworkCore;
using PBAC.DB;
using PBAC.Models;
using PBAC.Models.Db;
using PBAC.Services.Contracts;

namespace PBAC.Services;

public class UserService : IUserService
{
    private readonly PbacContext _context;

    public UserService(PbacContext context)
    {
        _context = context;
    }

    public async Task RegisterUser(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<AuthenticationResult> AuthenticateAsync(AuthenticationModel model)
    {
        var user = await _context.Users
            .Where(x => x.Email == model.Email)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return new() { IsAuthenticated = false };
        }

        if (user.Password != model.Password)
        {
            return new() { IsAuthenticated = false };
        }

        return new() { 
            IsAuthenticated = true,
            User = user,         
            LastAuthenticated = DateTime.UtcNow,
            AccessToken = ""
        };
    }
}
