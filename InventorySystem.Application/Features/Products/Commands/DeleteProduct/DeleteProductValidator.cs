namespace InventorySystem.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductRequestDto>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be a positive integer.");
    }
}
