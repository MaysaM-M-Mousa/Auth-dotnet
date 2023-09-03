using AuthApp.Db;
using AuthApp.Models;
using AuthApp.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApp.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly IHashService _hashService;
    private readonly JwtOptions _jwtOptions;

    public AuthenticationService(IUserService userService, IHashService hashService, IOptions<JwtOptions> jwtOptions)
    {
        _userService = userService;
        _hashService = hashService;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<string> AuthenticateUser(UserSignInRequest userRequest)
    {
        var userDb = await _userService.GetUser(userRequest.Email);

        if (userDb is null)
        {
            throw new UnauthorizedAccessException();
        }

        if (_hashService.HashPassword(userRequest.Password) != userDb.Password)
        {
            throw new UnauthorizedAccessException();
        }

        return GenerateJwtToken(userDb);
    }

    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(_jwtOptions.Issuer,
          _jwtOptions.Audience,
          claims,
          expires: DateTime.Now.AddMinutes(1),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
