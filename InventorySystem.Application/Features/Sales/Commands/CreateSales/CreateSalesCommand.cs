namespace InventorySystem.Application.Features.Sales.Commands.CreateSales;

public sealed record CreateSalesCommand(CreateSalesRequestDto request) 
    : IRequest<Response<Unit>>;
