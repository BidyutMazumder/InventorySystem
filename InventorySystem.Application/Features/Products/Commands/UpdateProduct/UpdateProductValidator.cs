namespace InventorySystem.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequestDto>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be greater than zero.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Barcode)
            .NotEmpty().WithMessage("Barcode is required.")
            .MaximumLength(50);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.StockQty)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Category)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Status)
            .NotNull();
    }
}
