using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Infrastructure.Database;
using SarvashresthaCMS.Infrastructure.Repositories;
using SarvashresthaCMS.Infrastructure.Authentication;
using SarvashresthaCMS.Application.Services;
using Dapper;

namespace SarvashresthaCMS.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        services.AddTransient<IDbConnectionFactory, SqlDbConnectionFactory>();
        services.AddScoped<IResortRepository, ResortRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
