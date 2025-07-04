namespace InventorySystem.Infrastructure.Persistence.Context;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("InventorySystemConnectionString")
                              ?? throw new ArgumentNullException("DefaultConnection not found.");
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}
