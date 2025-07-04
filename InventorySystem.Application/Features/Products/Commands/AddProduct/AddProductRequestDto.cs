namespace InventorySystem.Application.Features.Products.Commands.AddProduct;

public sealed record AddProductRequestDto(
    int ProductId,
    string Name,
    string Barcode,
    decimal Price,
    decimal StockQty,
    string Category,
    bool Status
);