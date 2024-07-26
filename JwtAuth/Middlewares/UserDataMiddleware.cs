using JwtAuth.Services;
using System.Security.Claims;

namespace JwtAuth.Middlewares;

public class UserDataMiddleware : IMiddleware
{
    private readonly IUserService _userService;

    public UserDataMiddleware(IUserService userService)
    {
        _userService = userService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var IsAuthenticated = context.User.Identity?.IsAuthenticated;

        if (IsAuthenticated.HasValue && IsAuthenticated.Value == true)
        {
            var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = await _userService.GetUser(long.Parse(userId!));

            context.Items.Add("UserDate", user);
        }

        await next(context);
    }
}
