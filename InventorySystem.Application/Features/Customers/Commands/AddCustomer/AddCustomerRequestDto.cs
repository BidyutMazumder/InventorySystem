namespace InventorySystem.Application.Features.Customers.Commands.AddCustomer;

public sealed record AddCustomerRequestDto(
    string FullName,
    string Phone,
    string Email,
    int LoyaltyPoints
);

