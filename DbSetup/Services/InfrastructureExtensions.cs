using DbSetup.Data.Context;
using DbSetup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace DbSetup.Services;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>();
        
        services.AddScoped<SeedService>();
        
        return services;
    }
}