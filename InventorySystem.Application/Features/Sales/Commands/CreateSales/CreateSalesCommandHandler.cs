using InventorySystem.Application.Contracts.Persistence.Sales;
using InventorySystem.Domain.Sales;
using System.Data;

namespace InventorySystem.Application.Features.Sales.Commands.CreateSales;

public sealed record CreateSalesCommand(CreateSalesRequestDto request) 
    : IRequest<Response<CraeteSalesResponseDto>>;

public class CreateSalesCommandHandler : IRequestHandler<CreateSalesCommand, Response<CraeteSalesResponseDto>>
{
    private static readonly SemaphoreSlim _semaphore = new(3);
    private readonly ISaleRepository _saleRepository;

    public CreateSalesCommandHandler(ISaleRepository saleRepository)
    {
        this._saleRepository = saleRepository;
    }

    public async Task<Response<CraeteSalesResponseDto>> Handle(CreateSalesCommand command, CancellationToken cancellationToken)
    {
        if (!await _semaphore.WaitAsync(0, cancellationToken))
            return Response<CraeteSalesResponseDto>.FailureResponse(
                message:"Too many concurrent sales.", 
                statusCode:429);
        try
        {

            var request = command.request;

            var sale = new Sale
            {
                SaleDate = request.SaleDate,
                CustomerId = request.CustomerId,
                PaidAmount = request.PaidAmount,
                SaleDetails = request.Items.Select(item => new SaleDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = 0
                }).ToList()
            };
            var response = await _saleRepository.AddSalesAsync(sale);
     
            if (!response.IsSuccessful)
            {
                return Response<CraeteSalesResponseDto>.FailureResponse(
                    message: response.Message,
                    statusCode: 400);
            }

            var responseDto = new CraeteSalesResponseDto(
                response.DueAmount,
                response.SaleId
            );
            return Response<CraeteSalesResponseDto>.SuccessResponse(
                message: "Sales Created Successfully",
                statusCode: 200,
                data: responseDto);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
