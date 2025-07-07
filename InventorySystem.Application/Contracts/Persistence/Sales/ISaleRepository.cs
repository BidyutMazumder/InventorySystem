using InventorySystem.Application.Features.Sales.Query.GetSalesSummary;
using InventorySystem.Domain.Sales;

namespace InventorySystem.Application.Contracts.Persistence.Sales;

public interface ISaleRepository
{
    Task<CreatedSales> AddSalesAsync(Sale sale);

    Task<SalesSummary?> GetSalesSummaryAsync(DateRange dateRange);

    //// Get sale by ID including details
    //Task<Sale?> GetByIdAsync(int saleId);

    //// Get all sales (optional use)
    //Task<IEnumerable<Sale>> GetAllAsync();

    //// Get all sales in a specific date range
    //Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime from, DateTime to);

    //// Calculate total amount of sales (TotalAmount)
    //Task<decimal> GetTotalSalesAmountAsync(DateTime from, DateTime to);

    //// Calculate total revenue (PaidAmount)
    //Task<decimal> GetTotalRevenueAsync(DateTime from, DateTime to);

    //// Count number of transactions
    //Task<int> GetTransactionCountAsync(DateTime from, DateTime to);

    //// SaveChanges (optional if not using Unit of Work)
    //Task SaveChangesAsync();
}


