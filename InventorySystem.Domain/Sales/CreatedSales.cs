namespace InventorySystem.Domain.Sales;

public class CreatedSales
{
    public bool IsSuccessful { get; set; }
    public int SaleId { get; set; }
    public string Message { get; set; } = string.Empty;
    public int DueAmount { get; set; } = 0;
}