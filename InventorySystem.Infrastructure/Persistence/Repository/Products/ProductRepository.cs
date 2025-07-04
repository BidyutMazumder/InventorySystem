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


    public async Task AddAsync(Product product)
    {
        await _context.products.AddAsync(product);
    }

    public void Update(Product product)
    {
        _context.products.Update(product);
    }

    public void Delete(Product product)
    {
        _context.products.Remove(product);
    }
    public async Task<bool> ExistsByBarcodeAsync(string barcode)
    {
        const string sql = "SELECT COUNT(1) FROM Products WHERE Barcode = @Barcode AND IsDeleted = 0";

        using var connection = _dapperContext.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { Barcode = barcode });

        return count > 0;
    }
}
