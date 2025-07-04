namespace InventorySystem.Application.Contracts.Persistence.Sales;

public interface ISaleRepository
{
    Task<Sale?> GetByIdAsync(int saleId);
    Task<IEnumerable<Sale>> GetAllAsync();
    Task AddAsync(Sale sale);
    Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime from, DateTime to);
    Task<decimal> GetTotalRevenueAsync(DateTime from, DateTime to);
    Task<int> GetTotalTransactionCountAsync(DateTime from, DateTime to);
    Task<decimal> GetTotalSalesAmountAsync(DateTime from, DateTime to);
}

