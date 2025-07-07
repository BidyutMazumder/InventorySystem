using InventorySystem.Domain.Products;

namespace InventorySystem.Infrastructure.Persistence.Context;

public class InventorySystemDbContext: DbContext
{
    public InventorySystemDbContext(DbContextOptions<InventorySystemDbContext> options) : base(options)
    {

    }

    public DbSet<Customer> customers { get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<Sale> sales { get; set; }
    public DbSet<SaleDetail> saleDetails { get; set; }
}
