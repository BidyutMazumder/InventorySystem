namespace InventorySystem.Application.Features.Products.Query.ProductList;

public sealed record ProductListQuery(ProductListRequestDto Request)
    : IRequest<Response<IEnumerable<ProductListResponseDto>>>;
public class ProductListQueryHandler : IRequestHandler<ProductListQuery, Response<IEnumerable<ProductListResponseDto>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<ProductListRequestDto> _validator;
    private readonly IMapper _mapper;

    public ProductListQueryHandler(
        IProductRepository productRepository,
        IValidator<ProductListRequestDto> validator,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<ProductListResponseDto>>> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Response<IEnumerable<ProductListResponseDto>>.FailureResponse(
                message: "Validation failed",
                errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                statusCode: 400);
        }

        var products = await _productRepository.GetAllAsync(
            request.Request.Page,
            request.Request.PageSize);

        var mapped = _mapper.Map<IEnumerable<ProductListResponseDto>>(products);

        return Response<IEnumerable<ProductListResponseDto>>.SuccessResponse(
            data: mapped,
            message: "Products fetched successfully"
        );
    }
}