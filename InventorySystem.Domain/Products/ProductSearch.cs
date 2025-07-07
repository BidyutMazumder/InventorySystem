namespace InventorySystem.Domain.Products;

public class ProductSearch
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    public bool? Status { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
