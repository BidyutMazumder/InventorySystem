using InventorySystem.Application.Features.Auth.Commands.RegisterCommand;
using InventorySystem.Application.ValidationBehavior;

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
        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return service;
    }
}

