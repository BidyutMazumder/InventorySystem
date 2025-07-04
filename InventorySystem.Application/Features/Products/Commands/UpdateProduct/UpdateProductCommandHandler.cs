namespace InventorySystem.Application.Features.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(UpdateProductRequestDto Request)
    : IRequest<Response<Unit>>;

public class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand, Response<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<UpdateProductRequestDto> _validator;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IValidator<UpdateProductRequestDto> validator,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Response<Unit>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        // Validate DTO
        var validationResult = await _validator.ValidateAsync(command.Request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Response<Unit>.FailureResponse(
                message: "Validation failed",
                errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                statusCode: 400);
        }

        // Check if product exists by ProductId
        var existingProducts = await _productRepository.GetAllAsync(1, int.MaxValue); // or add a GetById method in repo if possible
        var product = existingProducts.FirstOrDefault(p => p.ProductId == command.Request.ProductId);

        if (product == null)
        {
            return Response<Unit>.FailureResponse(
                message: $"Product with Id '{command.Request.ProductId}' not found.",
                statusCode: 404);
        }

        // Optionally: Check if barcode is unique (if barcode can be updated)
        if (product.Barcode != command.Request.Barcode)
        {
            var barcodeExists = await _productRepository.ExistsByBarcodeAsync(command.Request.Barcode);
            if (barcodeExists)
            {
                return Response<Unit>.FailureResponse(
                    message: $"Product with barcode '{command.Request.Barcode}' already exists.",
                    statusCode: 409);
            }
        }

        // Map updated fields from DTO to existing product
        _mapper.Map(command.Request, product);

        // Save update
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
