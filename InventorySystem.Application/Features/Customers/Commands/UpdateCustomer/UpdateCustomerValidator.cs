namespace InventorySystem.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.request.CustomerId)
            .GreaterThan(0).WithMessage("CustomerId must be greater than 0.");

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