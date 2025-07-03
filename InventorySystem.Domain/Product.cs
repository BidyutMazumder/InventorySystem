namespace InventorySystem.Domain;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = default!;
    public string Barcode { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal StockQty { get; set; }
    public string Category { get; set; } = default!;
    public bool Status { get; set; }
    public bool IsDeleted { get; set; }
}
