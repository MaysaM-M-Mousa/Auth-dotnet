using JwtAuth.Db;
using JwtAuth.Models;
using JwtAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public HomeController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost("SignUp")]
        public async Task<User> SignUp(User user)
        {
            return await _userService.CreateUser(user);
        }

        [HttpPost("SignIn")]
        public async Task<string> SignIn(UserSignInRequest signInRequest)
        {
            return await _authenticationService.AuthenticateUser(signInRequest);
        }
    }
}
