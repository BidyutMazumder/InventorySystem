namespace InventorySystem.Application.Features.Products.Query.ProductList;

public sealed record ProductListResponseDto
(
    int ProductId,
    string Name,
    string Barcode,
    decimal Price,
    decimal StockQty,
    string Category,
    bool Status
);
