namespace InventorySystem.Application.Features.Products.Query.ProductList;

public sealed record ProductListRequestDto(
    string? Name,
    string? Category,
    bool? Status,
    int Page = 1, 
    int PageSize = 10
);

