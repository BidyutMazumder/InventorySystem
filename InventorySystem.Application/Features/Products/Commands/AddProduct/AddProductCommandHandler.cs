using InventorySystem.Domain.Products;

namespace InventorySystem.Application.Features.Products.Commands.AddProduct;

public sealed record AddProductCommand(AddProductRequestDto request)
    : IRequest<Response<Unit>>;
public class AddProductCommandHandler
    : IRequestHandler<AddProductCommand, Response<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public AddProductCommandHandler(
        IProductRepository productRepository, 
        IMapper mapper)
    {
        this._productRepository = productRepository;
        this._mapper = mapper;
    }
    public async Task<Response<Unit>> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        bool barcodeExists = await _productRepository.ExistsByBarcodeAsync(command.request.Barcode);
        if (barcodeExists)
        {
            return Response<Unit>.FailureResponse(
                message: $"Product with barcode '{command.request.Barcode}' already exists.",
                statusCode: 409);
        }

        var product = _mapper.Map<Product>(command.request);

        bool productAdded = await _productRepository.AddAsync(product);

        if (!productAdded)
        {
            return Response<Unit>.FailureResponse(
                message: "Failed to add product",
                statusCode: 500);
        }
        return Response<Unit>.SuccessResponse(
            message: "Product created successfully",
            statusCode: 201,
            data: Unit.Value);
    }

}
