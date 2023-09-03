using AuthApp.Services;

namespace AuthApp.Middlewares;

public class AddUserDataMiddleware : IMiddleware
{
    private readonly IUserService _userService;
    private const string userIdClaimName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

    public AddUserDataMiddleware(IUserService userService)
    {
        _userService = userService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var IsAuthenticated = context.User.Identity?.IsAuthenticated;

        if (IsAuthenticated.HasValue && IsAuthenticated.Value == true)
        {
            var userId = context.User.Claims.FirstOrDefault(c => c.Type == userIdClaimName)?.Value;

            var user = await _userService.GetUser(long.Parse(userId));

            context.Items.Add("UserDate", user);
        }

        await next(context);
    }
}
