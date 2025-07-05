
using InventorySystem.Application.Contracts.Persistence.Customers;

namespace InventorySystem.Application.Features.Customers.Commands.AddCustomer;

public sealed record AddCustomerCommand(AddCustomerRequestDto Request)
    : IRequest<Response<Unit>>;

public class AddCustomerCommandHandler
    : IRequestHandler<AddCustomerCommand, Response<Unit>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidator<AddCustomerRequestDto> _validator;
    private readonly IMapper _mapper;

    public AddCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IValidator<AddCustomerRequestDto> validator,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Response<Unit>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Response<Unit>.FailureResponse(
                message: "Validation failed",
                errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                statusCode: 400);
        }
        var customer = _mapper.Map<Customer>(request.Request);

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

