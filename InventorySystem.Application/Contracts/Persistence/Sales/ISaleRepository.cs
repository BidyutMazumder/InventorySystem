using InventorySystem.Domain.Sales;

namespace InventorySystem.Application.Contracts.Persistence.Sales;

public interface ISaleRepository
{
    Task<CreatedSales> AddSalesAsync(Sale sale);

    Task<SalesSummary?> GetSalesSummaryAsync(DateRange dateRange);
}


