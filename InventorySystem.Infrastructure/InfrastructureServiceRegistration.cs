using InventorySystem.Application.Contracts.Persistence.Sales;
using InventorySystem.Infrastructure.Persistence.Repository.Sales;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace InventorySystem.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("InventorySystem")
            .AddEntityFrameworkStores<InventorySystemAuthDbContext>()
            .AddDefaultTokenProviders();

        // Configure Identity password options
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
        });

        // Configure JWT authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });
        services.AddDbContext<InventorySystemDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("InventorySystemConnectionString")));

        services.AddDbContext<InventorySystemAuthDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("InventorySystemAuthConnectionString")));
        services.AddTransient<ITokenService, TokenService>();
        services.AddScoped<DapperContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();

        return services;
    }
}
