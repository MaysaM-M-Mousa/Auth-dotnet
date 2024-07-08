using OAuth.Services;

namespace OAuth.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IGithubUserService, GithubUserService>();
        return services;
    }

}
