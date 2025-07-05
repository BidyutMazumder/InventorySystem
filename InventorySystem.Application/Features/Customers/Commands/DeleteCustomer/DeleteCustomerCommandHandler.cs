using MediatR;

namespace InventorySystem.Application.Features.Customers.Commands.DeleteCustomer;

public sealed record DeleteCustomerCommand(DeleteCustomerRequestDto request)
    :IRequest<Response<Unit>>;
public class DeleteCustomerCommandHandler
    : IRequestHandler<DeleteCustomerCommand, Response<Unit>>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        this._customerRepository = customerRepository;
    }
    public async Task<Response<Unit>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(command.request.CustomerId);
        if (customer == null)
        {
            return Response<Unit>.FailureResponse(
                message: $"Customer with Id {command.request.CustomerId} not found.",
                statusCode: 404);
        }
        var isDeleted = await _customerRepository.DeleteAsync(customer);
        if (!isDeleted)
        {
            return Response<Unit>.FailureResponse(
                message: "Failed to delete the Customer due to internal error.",
                statusCode: 500);
        }
        return Response<Unit>.SuccessResponse(
            message: "Customer deleted successfully.",
            statusCode: 200,
            data: Unit.Value);
    }
}
