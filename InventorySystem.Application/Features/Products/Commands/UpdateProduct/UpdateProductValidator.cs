namespace InventorySystem.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.request.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be greater than zero.");

        RuleFor(x => x.request.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100);

        RuleFor(x => x.request.Name)
            .NotEmpty().WithMessage("Barcode is required.")
            .MaximumLength(50);

        RuleFor(x => x.request.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.request.StockQty)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.request.Category)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.request.Status)
            .NotNull();
    }
}
