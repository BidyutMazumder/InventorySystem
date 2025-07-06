namespace InventorySystem.Infrastructure.Persistence.Repository.Products;

public class ProductRepository : IProductRepository
{
    private readonly InventorySystemDbContext _context;
    private readonly DapperContext _dapperContext;

    public ProductRepository(InventorySystemDbContext context, DapperContext dapperContext)
    {
        this._context = context;
        this._dapperContext = dapperContext;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(int page, int pageSize)
    {
        var offset = (page - 1) * pageSize;

        var sql = @"
            SELECT * FROM Products
            WHERE IsDeleted = 0
            ORDER BY ProductId
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY;";

        using var connection = _dapperContext.CreateConnection();
        var result = await connection.QueryAsync<Product>(sql, new { Offset = offset, PageSize = pageSize });

        return result;
    }


    public async Task<bool> AddAsync(Product product)
    {
        await _context.products.AddAsync(product);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Product product)
    {
        _context.products.Update(product);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Product product)
    {
        product.IsDeleted = true;
        _context.products.Update(product);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> ExistsByBarcodeAsync(string barcode)
    {
        const string sql = "SELECT COUNT(1) FROM Products WHERE Barcode = @Barcode And IsDeleted = 0";

        using var connection = _dapperContext.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { Barcode = barcode });

        return count > 0;
    }

    public async Task<Product?> GetByIdAsync(int productId)
    {
        const string sql = @"
        SELECT * FROM Products
        WHERE ProductId = @ProductId And IsDeleted = 0";

        using var connection = _dapperContext.CreateConnection();
        var product = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { ProductId = productId });
        return product;
    }
}
