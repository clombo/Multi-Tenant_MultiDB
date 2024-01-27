using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MTA.Data.Contexts;

namespace MTA.Data;

public static class Extensions
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        // adding a database service with configuration -- connection string read from appsettings.json
        services
            .AddDbContext<AppDbContext>(
                options => options
                    .UseSqlServer(configuration.GetConnectionString("MTA"))
                );

        services
            .AddDbContext<TenantDbContext>(
                options => options
                    .UseSqlServer(configuration.GetConnectionString("MTA"))
            );
        
        return services;
    }
}