using InventorySystem.Application.Contracts.Persistence.Sales;
using InventorySystem.Domain.Sales;
using System.Data;

namespace InventorySystem.Application.Features.Sales.Commands.CreateSales;

public sealed record CreateSalesCommand(CreateSalesRequestDto request) 
    : IRequest<Response<Unit>>;

public class CreateSalesCommandHandler : IRequestHandler<CreateSalesCommand, Response<Unit>>
{
    private static readonly SemaphoreSlim _semaphore = new(3);
    private readonly ISaleRepository _saleRepository;

    public CreateSalesCommandHandler(ISaleRepository saleRepository)
    {
        this._saleRepository = saleRepository;
    }

    public async Task<Response<Unit>> Handle(CreateSalesCommand command, CancellationToken cancellationToken)
    {
        if (!await _semaphore.WaitAsync(0, cancellationToken))
            return Response<Unit>.FailureResponse(
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
                    // ✅ Price will be determined in DB or repository (set to 0 here)
                    Price = 0
                }).ToList()
            };
            //map dto to entity

            await Task.Delay(3000);
            //var sale = await _saleRepository.AddSalesAsync();

            
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
