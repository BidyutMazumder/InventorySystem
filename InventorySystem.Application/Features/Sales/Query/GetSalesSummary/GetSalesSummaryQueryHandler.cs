using InventorySystem.Application.Contracts.Persistence.Sales;
using InventorySystem.Domain.Sales;

namespace InventorySystem.Application.Features.Sales.Query.GetSalesSummary;

public sealed record GetSalesSummaryQuery(GetSalesSummaryRequestDto request)
    : IRequest<Response<GetSalesSummaryResponseDto>>;
public class GetSalesSummaryQueryHandler : IRequestHandler<GetSalesSummaryQuery, Response<GetSalesSummaryResponseDto>>
{
    private readonly ISaleRepository _saleRepository;

    public GetSalesSummaryQueryHandler(ISaleRepository saleRepository)
    {
        this._saleRepository = saleRepository;
    }

    public async Task<Response<GetSalesSummaryResponseDto>> Handle(GetSalesSummaryQuery query, CancellationToken cancellationToken)
    {
        DateRange dateRange = new DateRange
        {
            StartDate = query.request.StartDate,
            EndDate = query.request.EndDate
        };
        var summary = await _saleRepository.GetSalesSummaryAsync(dateRange);
        if(summary == null)
        {
            return Response<GetSalesSummaryResponseDto>.FailureResponse(
                message:"Sales summary not found",
                statusCode: 404
                );
        }
        GetSalesSummaryResponseDto salesSummary = new GetSalesSummaryResponseDto(
            summary.NumberOfTransactions, 
            summary.TotalSales, 
            summary.TotalRevenue);
        return Response<GetSalesSummaryResponseDto>.SuccessResponse(
            message: "Sales summary retrieved successfully",
            statusCode: 200,
            data: salesSummary
            );
    }
}
