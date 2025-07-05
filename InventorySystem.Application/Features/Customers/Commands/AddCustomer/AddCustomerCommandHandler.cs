
using InventorySystem.Application.Contracts.Persistence.Customers;

namespace InventorySystem.Application.Features.Customers.Commands.AddCustomer;

public sealed record AddCustomerCommand(AddCustomerRequestDto request)
    : IRequest<Response<Unit>>;

public class AddCustomerCommandHandler
    : IRequestHandler<AddCustomerCommand, Response<Unit>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public AddCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Response<Unit>> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(command.request);

        bool success = await _customerRepository.AddAsync(customer);

        if (!success)
        {
            return Response<Unit>.FailureResponse(
                message: "Failed to add customer",
                statusCode: 500);
        }
        return Response<Unit>.SuccessResponse(
            data: Unit.Value,
            message: "Customer added successfully",
            statusCode: 201);
    }
}

