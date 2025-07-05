namespace InventorySystem.Application.Features.Products.Query.ProductList;

public sealed record ProductListQuery(ProductListRequestDto request)
    : IRequest<Response<IEnumerable<ProductListResponseDto>>>;
public class ProductListQueryHandler : IRequestHandler<ProductListQuery, Response<IEnumerable<ProductListResponseDto>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductListQueryHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        this._productRepository = productRepository;
        this._mapper = mapper;
    }

    public async Task<Response<IEnumerable<ProductListResponseDto>>> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(
            request.request.Page,
            request.request.PageSize);

        var mapped = _mapper.Map<IEnumerable<ProductListResponseDto>>(products);

        return Response<IEnumerable<ProductListResponseDto>>.SuccessResponse(
            data: mapped,
            message: "Products fetched successfully"
        );
    }
}