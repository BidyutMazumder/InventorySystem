namespace InventorySystem.Application.Features.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(DeleteProductRequestDto request) : IRequest<Response<Unit>>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<Unit>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(
        IProductRepository productRepository
        )
    {
        this._productRepository = productRepository;
    }

    public async Task<Response<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.request.ProductId);
        if (product == null)
        {
            return Response<Unit>.FailureResponse(
                message: $"Product with Id {request.request.ProductId} not found.",
                statusCode: 404);
        }

        var isDeleted = await _productRepository.Delete(product);
        if (!isDeleted)
        {
            return Response<Unit>.FailureResponse(
                message: "Failed to delete the product due to internal error.",
                statusCode: 500);
        }

        return Response<Unit>.SuccessResponse(
            message: "Product deleted successfully.",
            statusCode: 200,
            data: Unit.Value);
    }
}
