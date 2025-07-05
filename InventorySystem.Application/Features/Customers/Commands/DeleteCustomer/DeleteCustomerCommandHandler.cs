
namespace InventorySystem.Application.Features.Customers.Commands.DeleteCustomer;

public sealed record DeleteCustomerCommand(DeleteCustomerRequestDto request)
    :IRequest<Response<Unit>>;
public class DeleteCustomerCommandHandler
    : IRequestHandler<DeleteCustomerCommand, Response<Unit>>
{
    public async Task<Response<Unit>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
