namespace InventorySystem.Application.Features.Customers.Query.CustomerList;

public sealed record CustomerListQuery(CustomerListRequestDto request) 
    : IRequest<Response<IEnumerable<CustomerListResponseDto>>>;
public class CustomerListQueryHandler
    : IRequestHandler<CustomerListQuery, Response<IEnumerable<CustomerListResponseDto>>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerListQueryHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        this._customerRepository = customerRepository;
        this._mapper = mapper;
    }
    public async Task<Response<IEnumerable<CustomerListResponseDto>>> 
        Handle(CustomerListQuery query, CancellationToken cancellationToken)
    {
        var customers = await this._customerRepository.GetAllAsync(
            query.request.PageNumber,
            query.request.PageSize);
        var mapped = this._mapper.Map<IEnumerable<CustomerListResponseDto>>(customers);

        return Response<IEnumerable<CustomerListResponseDto>>.SuccessResponse(
            data: mapped,
            message: "Customers fetched successfully"
        );
    }
}
