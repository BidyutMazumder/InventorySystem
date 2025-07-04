using InventorySystem.Infrastructure.Persistence.Context;
using InventorySystem.Infrastructure.Persistence.Repository.Products;

namespace InventorySystem.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITokenService, TokenService>();
        services.AddScoped<DapperContext>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
