namespace InventorySystem.Application.Features.Customers.Query.CustomerList;

public class CustomerListValidator: AbstractValidator<CustomerListQuery>
{
    public CustomerListValidator()
    {
        RuleFor(x => x.request.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber at least greater than or equal to 1."); ;
        RuleFor(x => x.request.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageSize at least greater than or equal to 1."); ;
    }
}
