namespace InventorySystem.Application.Features.Sales.Query.GetSalesSummary;

public sealed record GetSalesSummaryResponseDto(
    int NumberOfTransactions,
    decimal TotalSales,
    decimal TotalRevenue
);

