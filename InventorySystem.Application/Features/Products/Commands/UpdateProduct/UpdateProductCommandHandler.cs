namespace InventorySystem.Application.Features.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(UpdateProductRequestDto request)
    : IRequest<Response<Unit>>;

public class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand, Response<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Response<Unit>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var existingProducts = await _productRepository.GetAllAsync(1, int.MaxValue); 
        var product = existingProducts.FirstOrDefault(p => p.ProductId == command.request.ProductId);

        if (product == null)
        {
            return Response<Unit>.FailureResponse(
                message: $"Product with Id '{command.request.ProductId}' not found.",
                statusCode: 404);
        }

        if (product.Barcode != command.request.Barcode)
        {
            var barcodeExists = await _productRepository.ExistsByBarcodeAsync(command.request.Barcode);
            if (barcodeExists)
            {
                return Response<Unit>.FailureResponse(
                    message: $"Product with barcode '{command.request.Barcode}' already exists.",
                    statusCode: 409);
            }
        }

        _mapper.Map(command.request, product);

        bool updated = await _productRepository.Update(product);
        if (!updated)
        {
            return Response<Unit>.FailureResponse(
                message: "Failed to update product",
                statusCode: 500);
        }

        return Response<Unit>.SuccessResponse(
            message: "Product updated successfully",
            statusCode: 200,
            data: Unit.Value);
    }
}
