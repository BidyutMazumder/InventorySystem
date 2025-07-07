namespace InventorySystem.Application.Features.Sales.Commands.CreateSales;

public class CreateSalesCommandValidator : AbstractValidator<CreateSalesCommand>
{
    public CreateSalesCommandValidator()
    {
        RuleFor(x => x.request)
            .NotNull().WithMessage("Request body cannot be null.");

        When(x => x.request != null, () =>
        {
            RuleFor(x => x.request.SaleDate)
                .NotEmpty().WithMessage("Sale date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

            RuleFor(x => x.request.PaidAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Paid amount cannot be negative.");

            RuleFor(x => x.request.Items)
                .NotEmpty().WithMessage("At least one product must be included in the sale.");

            RuleForEach(x => x.request.Items)
                .SetValidator(new CreateSalesItemDtoValidator());
        });
    }
}

public class CreateSalesItemDtoValidator : AbstractValidator<CreateSalesItem>
{
    public CreateSalesItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}

