using InventorySystem.Application.Contracts.Persistence.Sales;
using InventorySystem.Application.Features.Sales.Query.GetSalesSummary;

namespace InventorySystem.Infrastructure.Persistence.Repository.Sales;

public class SaleRepository : ISaleRepository
{
    private readonly DapperContext _context;

    public SaleRepository(DapperContext context)
    {
        this._context = context;
    }

    public async Task<CreatedSales> AddSalesAsync(Sale sale)
    {
        var table = new DataTable();
        table.Columns.Add("ProductId", typeof(int));
        table.Columns.Add("Quantity", typeof(decimal));

        foreach (var item in sale.SaleDetails)
        {
            table.Rows.Add(item.ProductId, item.Quantity);
        }

        var parameters = new DynamicParameters();
        parameters.Add("@CustomerId", sale.CustomerId);
        parameters.Add("@PaidAmount", sale.PaidAmount);
        parameters.Add("@SaleDate", sale.SaleDate);
        parameters.Add("@SaleDetails", table.AsTableValuedParameter("SaleDetailType"));

        using var connection = _context.CreateConnection();

        var result = await connection.QuerySingleAsync<CreatedSales>(
            "CreateSaleTransaction",
            parameters,
            commandType: CommandType.StoredProcedure);

        return result;
    }
    public async Task<SalesSummary?> GetSalesSummaryAsync(DateRange dateRange)
    {
        const string sql = @"
            SELECT 
                COUNT(*) AS NumberOfTransactions,
                ISNULL(SUM(TotalAmount), 0) AS TotalSales,
                ISNULL(SUM(PaidAmount), 0) AS TotalRevenue
            FROM Sales
            WHERE cast (SaleDate as date) 
            BETWEEN cast(@StartDate as date) AND cast(@EndDate as date) 
            AND IsDeleted = 0;
        ";

        using var connection = _context.CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<SalesSummary>(sql, new
        {
            StartDate = dateRange.StartDate,
            EndDate = dateRange.EndDate
        });

        return result;
    }
}

