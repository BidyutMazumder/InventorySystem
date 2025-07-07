using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Domain.Sales;

public class Sale
{
    [Key]
    public int SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public int? CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal DueAmount { get; set; }
    public bool IsDeleted { get; set; } = false;

    public Customer? Customer { get; set; }
    public List<SaleDetail> SaleDetails { get; set; } = new();
}

