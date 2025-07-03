namespace InventorySystem.Infrastructure.Persistence.DbContext;

public class InventorySystemAuthDbContext: IdentityDbContext
{
    public InventorySystemAuthDbContext(DbContextOptions<InventorySystemAuthDbContext> options) : base(options)
    {
    }
}
