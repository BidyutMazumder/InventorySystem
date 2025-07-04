namespace InventorySystem.Infrastructure.Persistence.Context;

public class InventorySystemDbContext: DbContext
{
    public InventorySystemDbContext(DbContextOptions<InventorySystemDbContext> options) : base(options)
    {

    }
}
