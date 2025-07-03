namespace InventorySystem.API.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureServiceRegistration(this IServiceCollection service)
    {
        
        //service.AddMediatR(configuration =>
        //{
        //    configuration.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
        //});


        return service;
    }
}
