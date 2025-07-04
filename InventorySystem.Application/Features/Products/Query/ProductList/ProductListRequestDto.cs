namespace InventorySystem.Application.Features.Products.Query.ProductList;

public sealed record ProductListRequestDto(int Page = 1, int PageSize = 10);

