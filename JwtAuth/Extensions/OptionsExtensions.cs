using JwtAuth.Options;

namespace JwtAuth.Extensions;

public static class OptionsExtensions
{
    public static IServiceCollection AddOptionsClasses(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<HashOptions>().Bind(configuration.GetSection("Options:HashOptions"));
        services.AddOptions<JwtOptions>().Bind(configuration.GetSection("Jwt"));

        return services;
    }
}
