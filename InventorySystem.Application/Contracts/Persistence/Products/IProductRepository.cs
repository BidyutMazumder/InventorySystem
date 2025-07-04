namespace InventorySystem.Application.Contracts.Persistence.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(int page, int pageSize);
    Task AddAsync(Product product);
    void Update(Product product);
    void Delete(Product product);
    Task<bool> ExistsByBarcodeAsync(string barcode);
}

