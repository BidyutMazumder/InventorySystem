
using InventorySystem.Application.Contracts.Persistence.Products;

namespace InventorySystem.Application.Features.Products.Commands.AddProduct;

public sealed record AddProductCommand(AddProductRequestDto request)
    : IRequest<Response<Unit>>;
public class AddProductCommandHandler
    : IRequestHandler<AddProductCommand, Response<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<AddProductRequestDto> _validator;
    private readonly Mapper _mapper;

    public AddProductCommandHandler(
        IProductRepository productRepository, 
        IValidator<AddProductRequestDto> validator,
        Mapper mapper)
    {
        this._productRepository = productRepository;
        this._validator = validator;
        this._mapper = mapper;
    }
    public async Task<Response<Unit>> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Response<Unit>.FailureResponse(
                message: "Validation failed",
                errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                statusCode: 400);
        }

        bool barcodeExists = await _productRepository.ExistsByBarcodeAsync(command.request.Barcode);
        if (barcodeExists)
        {
            return Response<Unit>.FailureResponse(
                message: $"Product with barcode '{command.request.Barcode}' already exists.",
                statusCode: 409);
        }

        var product = _mapper.Map<Product>(command.request);

        await _productRepository.AddAsync(product);

        return Response<Unit>.SuccessResponse(
            message: "Product created successfully",
            statusCode: 201,
            data: Unit.Value);
    }

}
