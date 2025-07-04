namespace InventorySystem.Application.Features.Products.Commands.UpdateProduct;

public sealed record UpdateProductRequestDto(
    int ProductId,
    string Name,
    string Barcode,
    decimal Price,
    decimal StockQty,
    string Category,
    bool Status
);
