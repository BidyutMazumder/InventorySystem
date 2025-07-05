
namespace InventorySystem.Infrastructure.Persistence.Repository.Customers;

public class CustomerRepository : ICustomerRepository
{
    private readonly InventorySystemDbContext _context;
    private readonly DapperContext _dapper;

    public CustomerRepository(InventorySystemDbContext context, DapperContext dapper)
    {
        _context = context;
        _dapper = dapper;
    }
    public async Task<IEnumerable<Customer>> GetAllAsync(int page, int pageSize)
    {
        var offset = (page - 1) * pageSize;

        const string sql = @"
        SELECT * FROM Customers
        ORDER BY CustomerId
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY;";

        using var connection = _dapper.CreateConnection();

        var customers = await connection.QueryAsync<Customer>(sql, new
        {
            Offset = offset,
            PageSize = pageSize
        });

        return customers;
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        const string sql = "SELECT * FROM Customers WHERE CustomerId = @Id";
        using var connection = _dapper.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
    }

    public async Task<bool> ExistsAsync(int id)
    {
        const string sql = "SELECT COUNT(1) FROM Customers WHERE CustomerId = @Id";
        using var connection = _dapper.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
        return count > 0;
    }

    public async Task<bool> AddAsync(Customer customer)
    {
        await _context.customers.AddAsync(customer);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        _context.customers.Update(customer);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Customer customer)
    {
        _context.customers.Remove(customer);
        return await _context.SaveChangesAsync() > 0;
    }
}
