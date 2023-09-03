using AuthApp.Db;
using AuthApp.Models;
using AuthApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controllers
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
