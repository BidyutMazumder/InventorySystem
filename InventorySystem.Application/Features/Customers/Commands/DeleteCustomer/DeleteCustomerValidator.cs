namespace InventorySystem.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerValidator: AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerValidator()
    {
        RuleFor(x => x.request.CustomerId)
            .GreaterThan(0).WithMessage("ProductId must be a positive integer.");
    }
}
