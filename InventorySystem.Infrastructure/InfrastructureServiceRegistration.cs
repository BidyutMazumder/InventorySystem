using InventorySystem.Application.Contracts.Persistence.Sales;
using InventorySystem.Infrastructure.Persistence.Repository.Sales;

namespace InventorySystem.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITokenService, TokenService>();
        services.AddScoped<DapperContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();

        return services;
    }
}
