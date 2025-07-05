using InventorySystem.Application.Features.Customers.Commands.AddCustomer;
using InventorySystem.Application.Features.Customers.Commands.DeleteCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ISender _sender;

    public CustomerController(ISender sender)
    {
        this._sender = sender;
    }
    [HttpPost]
    public async Task<IActionResult> AddCustomer(AddCustomerRequestDto request)
    {
        var response = await _sender.Send(new AddCustomerCommand(request));
        return StatusCode(response.StatusCode, response);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteCustomer(DeleteCustomerRequestDto request)
    {
        var response = await _sender.Send(new DeleteCustomerCommand(request));
        return StatusCode(response.StatusCode, response);
    }
}
