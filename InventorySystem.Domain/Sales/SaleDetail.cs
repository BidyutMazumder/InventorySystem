namespace InventorySystem.Domain.Sales;

public class SaleDetail
{
    public int SaleDetailId { get; set; }
    public int SaleId { get; set; } = 0;
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }

    public Sale? Sale { get; set; }
    public Product? Product { get; set; }
}

