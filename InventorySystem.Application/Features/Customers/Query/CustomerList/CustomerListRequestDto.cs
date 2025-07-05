namespace InventorySystem.Application.Features.Customers.Query.CustomerList;

public sealed record CustomerListRequestDto(int PageNumber = 1, int PageSize = 10);
