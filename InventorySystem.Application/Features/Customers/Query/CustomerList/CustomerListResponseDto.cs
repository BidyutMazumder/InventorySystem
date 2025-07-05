namespace InventorySystem.Application.Features.Customers.Query.CustomerList;

public sealed record CustomerListResponseDto(
    int CustomerId,
    string FullName,
    string Phone,
    string Email,
    int LoyaltyPoints
);
