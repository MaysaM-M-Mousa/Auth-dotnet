using Microsoft.EntityFrameworkCore;
using PBAC.DB;

namespace PBAC.Extensions;

public static class DbExtensions
{
    public static IServiceCollection AddPbacDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<PbacContext>(x => x.UseSqlServer(configuration.GetConnectionString("AuthPbacAppDb")));
    }
}
