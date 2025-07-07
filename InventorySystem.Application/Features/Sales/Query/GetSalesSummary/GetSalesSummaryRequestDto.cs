namespace InventorySystem.Application.Features.Sales.Query.GetSalesSummary;

public sealed record GetSalesSummaryRequestDto(DateTime StartDate = new DateTime(), DateTime EndDate = new DateTime());

