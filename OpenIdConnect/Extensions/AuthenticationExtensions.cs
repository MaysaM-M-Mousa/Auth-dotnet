using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using OpenIdConnect.Models;
using OpenIdConnect.Services;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace OpenIdConnect.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddGithubOidcConfigurations(
        this IServiceCollection services,
        IConfiguration configurations)
    {
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            // github does not support oidc for social logins, so we have to manually implement it, ref: https://stackoverflow.com/a/73208830/23366465
            .AddOAuth("oidc-github", options => {
                // these are read from user secrets
                options.ClientId = configurations["OpenIdConnect:Github:ClientId"]!;
                options.ClientSecret = configurations["OpenIdConnect:Github:ClientSecret"]!;
                options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                options.CallbackPath = "/oidc/github-cb";
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SaveTokens = true;

                // newly added props
                options.UserInformationEndpoint = "https://api.github.com/user";
                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                options.ClaimActions.MapJsonKey("name", "name");
                options.ClaimActions.MapJsonKey("username", "login");

                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        // extending OAuth to retrieve user information from the user endpoint and append the result to the context
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                        var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();

                        var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

                        context.RunClaimActions(user.RootElement);


                        // Ensure authenticated user from github is created in the system
                        var userRequest = new CreateUserRequest()
                        {
                            Email = context.Principal!.FindFirst(ClaimTypes.Email)!.Value,
                            ProviderKey = context.Principal!.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                            Username = context.Principal!.FindFirst("name")!.Value,
                            Provider = context.Scheme.Name
                        };

                        var userService = context.HttpContext.RequestServices.GetService<IUserService>()!;
                        await userService.EnsureUserCreatedAsync(userRequest);
                        await userService.AssignUserRolesAsync(context.Principal);
                    }
                };
        });

        return services;
    }

    public static IServiceCollection AddGoogleOidcConfigurations(
        this IServiceCollection services,
        IConfiguration configurations)
    {
        services
            .AddAuthentication()
            .AddOpenIdConnect("oidc-google", options =>
            {
                options.ClientId = configurations["OpenIdConnect:Google:ClientId"];
                options.ClientSecret = configurations["OpenIdConnect:Google:ClientSecret"];
                // check: https://accounts.google.com/.well-known/openid-configuration
                options.Authority = "https://accounts.google.com/";
                options.CallbackPath = "/oidc/google-cb";
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SaveTokens = true;
                options.Scope.Add("email");

                // This is to get extra info about the user most likely does not exist in the Id token
                options.GetClaimsFromUserInfoEndpoint = true;

                options.Events = new OpenIdConnectEvents()
                {
                    OnTicketReceived = async context =>
                    {
                        var userRequest = new CreateUserRequest()
                        {
                            Email = context.Principal!.FindFirst(ClaimTypes.Email)!.Value,
                            ProviderKey = context.Principal!.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                            Username = context.Principal!.FindFirst("name")!.Value,
                            Provider = context.Scheme.Name
                        };

                        var userService = context.HttpContext.RequestServices.GetService<IUserService>()!;
                        await userService.EnsureUserCreatedAsync(userRequest);
                        await userService.AssignUserRolesAsync(context.Principal);
                    }
                };
            });

        return services;
    }
}
