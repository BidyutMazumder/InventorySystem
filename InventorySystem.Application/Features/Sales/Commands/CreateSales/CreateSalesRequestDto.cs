namespace InventorySystem.Application.Features.Sales.Commands.CreateSales;

public class CreateSalesRequestDto
{
    public int? CustomerId { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime SaleDate { get; set; }
    public List<CreateSalesItem> Items { get; set; } = new();
}

public class CreateSalesItem
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
}

