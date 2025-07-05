namespace InventorySystem.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.request.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be a positive integer.");
    }
}
