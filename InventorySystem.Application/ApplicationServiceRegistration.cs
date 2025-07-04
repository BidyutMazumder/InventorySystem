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
        service.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);

        return service;
    }
}

