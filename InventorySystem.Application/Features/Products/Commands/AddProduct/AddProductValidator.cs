﻿namespace InventorySystem.Application.Features.Products.Commands.AddProduct;

public class AddProductValidator : AbstractValidator<AddProductCommand>
{
    public AddProductValidator()
    {
        RuleFor(x => x.request.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

        RuleFor(x => x.request.Barcode)
            .NotEmpty().WithMessage("Barcode is required.")
            .Length(6, 20).WithMessage("Barcode must be between 6 and 20 characters.");

        RuleFor(x => x.request.Price)
            .NotEmpty().WithMessage("Price is required.")
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.request.StockQty)
            .NotEmpty().WithMessage("Stock quantity is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");

        RuleFor(x => x.request.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(50).WithMessage("Category cannot exceed 50 characters.");

        RuleFor(x => x.request.Status)
            .NotNull().WithMessage("Status is required (true/false).");
    }
}
