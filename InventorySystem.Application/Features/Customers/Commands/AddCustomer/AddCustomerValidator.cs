namespace InventorySystem.Application.Features.Customers.Commands.AddCustomer;

using FluentValidation;

public class AddCustomerValidator : AbstractValidator<AddCustomerCommand>
{
    public AddCustomerValidator()
    {
        RuleFor(x => x.request.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");

        RuleFor(x => x.request.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^01\d{9}$").WithMessage("Phone number must start with '01' and be 11 digits long.");

        RuleFor(x => x.request.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.");

        RuleFor(x => x.request.LoyaltyPoints)
            .GreaterThanOrEqualTo(0).WithMessage("Loyalty points must be zero or positive.");
    }
}

