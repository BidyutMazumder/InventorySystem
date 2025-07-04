using InventorySystem.Domain;

namespace InventorySystem.Infrastructure.Persistence.Context;

public class InventorySystemAuthDbContext: IdentityDbContext
{
    public InventorySystemAuthDbContext(DbContextOptions<InventorySystemAuthDbContext> options) : base(options)
    {
    }
    public DbSet<Customer> customers { get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<Sale> sales { get; set; }
    public DbSet<SaleDetail> saleDetails { get; set; }

}
