namespace InventorySystem.Application.Contracts.Persistence.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(ProductSearch productSearch);
    Task<Product?> GetByIdAsync(int productId);
    Task<bool> AddAsync(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(Product product);
    Task<bool> ExistsByBarcodeAsync(string barcode);
}

