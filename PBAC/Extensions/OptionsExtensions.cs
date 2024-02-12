using PBAC.Options;

namespace PBAC.Extensions;

public static class OptionsExtensions
{
    public static IServiceCollection AddOptionsClasses(this IServiceCollection services, IConfiguration configuration)
    {
        return services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
    }
}
