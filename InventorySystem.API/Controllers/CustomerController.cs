using InventorySystem.Application.Features.Customers.Commands.AddCustomer;
using InventorySystem.Application.Features.Customers.Commands.DeleteCustomer;
using InventorySystem.Application.Features.Customers.Commands.UpdateCustomer;
using InventorySystem.Application.Features.Customers.Query.CustomerList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers;
[Authorize]
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
    [HttpPut]
    public async Task<IActionResult> UpdateCustomer(UpdateCustomerRequestDto request)
    {
        var response = await _sender.Send(new UpdateCustomerCommand(request));
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    [ResponseCache(Duration = 5)]
    public async Task<IActionResult> GetCustomersList([FromQuery] CustomerListRequestDto request)
    {
        var response = await _sender.Send(new CustomerListQuery(request));
        return StatusCode(response.StatusCode, response);
    }
}
