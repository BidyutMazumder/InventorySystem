namespace InventorySystem.Application;
public static class ApplicationServiceRegistration
{
    public static IServiceCollection ConfigureApplicationService(this IServiceCollection service)
    {
        service.AddAutoMapper(Assembly.GetExecutingAssembly());

        service.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
        });

        // Register all FluentValidation validators in this assembly
        service.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);

        // Enable automatic validation
        service.AddFluentValidationAutoValidation();

        return service;
    }
}

