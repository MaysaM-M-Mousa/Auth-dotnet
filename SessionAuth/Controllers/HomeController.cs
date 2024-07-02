using Microsoft.AspNetCore.Mvc;
using SessionAuth.Models;
using SessionAuth.Services;

namespace SessionAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public HomeController(IAuthenticationService authenticationServic)
        {
            _authenticationService = authenticationServic;
        }

        [HttpPost("sign-in")]
        public async Task SignInUser(SignInRequest request)
        {
            await _authenticationService.SignIn(request);
        }

        [HttpPost("sign-out")]
        public async Task SingOutUser()
        {
            await _authenticationService.SingOut();
        }
    }
}
