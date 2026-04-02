using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Infrastructure.Database;
using SarvashresthaCMS.Infrastructure.Repositories;

namespace SarvashresthaCMS.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDbConnectionFactory, SqlDbConnectionFactory>();
        services.AddScoped<IResortRepository, ResortRepository>();
        return services;
    }
}
