namespace InventorySystem.Infrastructure.Persistence.Context;

public class InventorySystemAuthDbContext: IdentityDbContext
{
    public InventorySystemAuthDbContext(DbContextOptions<InventorySystemAuthDbContext> options) : base(options)
    {
    }
}
