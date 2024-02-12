using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PBAC.DB;
using PBAC.Models;
using PBAC.Models.Db;
using PBAC.Options;
using PBAC.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PBAC.Services;

public class UserService : IUserService
{
    private readonly PbacContext _context;
    private readonly JwtOptions _jwtOptions;

    public UserService(
        PbacContext context, 
        IOptions<JwtOptions> jwtOptions)
    {
        _context = context;
        _jwtOptions = jwtOptions.Value;
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
            AccessToken = GenerateJwtToken(user)
        };
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var token = new JwtSecurityToken(
          _jwtOptions.Issuer,
          _jwtOptions.Audience,
          claims,
          expires: DateTime.Now.AddMinutes(1),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
