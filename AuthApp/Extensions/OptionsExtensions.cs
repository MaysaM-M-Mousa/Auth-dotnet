using AuthApp.Options;

namespace AuthApp.Extensions;

public static class OptionsExtensions
{
    public static IServiceCollection AddOptionsClasses(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<HashOptions>().Bind(configuration.GetSection("Options:HashOptions"));

        return services;
    }
}
