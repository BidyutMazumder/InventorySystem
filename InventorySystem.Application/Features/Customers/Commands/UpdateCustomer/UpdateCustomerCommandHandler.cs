namespace InventorySystem.Application.Features.Customers.Commands.UpdateCustomer;

public sealed record UpdateCustomerCommand(UpdateCustomerRequestDto request)
    : IRequest<Response<Unit>>;
public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<Unit>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        this._customerRepository = customerRepository;
        this._mapper = mapper;
    }

    public async Task<Response<Unit>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(command.request.CustomerId);
        if (customer is null)
        {
            return Response<Unit>.FailureResponse(
                message: $"Customer with Id '{command.request.CustomerId}' not found.",
                statusCode: 404);
        }

        _mapper.Map(command.request, customer);

        bool updated = await _customerRepository.UpdateAsync(customer);
        if (!updated)
        {
            return Response<Unit>.FailureResponse(
                message: "Failed to update customer.",
                statusCode: 500);
        }

        return Response<Unit>.SuccessResponse(
            message: "Customer updated successfully.",
            statusCode: 200,
            data: Unit.Value);
    }
}

