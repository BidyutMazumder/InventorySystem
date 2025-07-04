namespace InventorySystem.Application.Features.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(DeleteProductRequestDto Request) : IRequest<Response<Unit>>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<DeleteProductRequestDto> _validator;

    public DeleteProductCommandHandler(
        IProductRepository productRepository,
        IValidator<DeleteProductRequestDto> validator)
    {
        this._productRepository = productRepository;
        this._validator = validator;
    }

    public async Task<Response<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Response<Unit>.FailureResponse(
                message: "Validation failed",
                errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                statusCode: 400);
        }

        var product = await _productRepository.GetByIdAsync(request.Request.ProductId);
        if (product == null)
        {
            return Response<Unit>.FailureResponse(
                message: $"Product with Id {request.Request.ProductId} not found.",
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
