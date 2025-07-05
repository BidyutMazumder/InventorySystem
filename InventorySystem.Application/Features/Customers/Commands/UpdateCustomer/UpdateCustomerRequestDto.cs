namespace InventorySystem.Application.Features.Customers.Commands.UpdateCustomer;

public sealed record UpdateCustomerRequestDto(
    int CustomerId,
    string FullName,
    string Phone,
    string Email,
    int LoyaltyPoints
);

