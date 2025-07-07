using InventorySystem.Application.Features.Products.Commands.AddProduct;
using InventorySystem.Application.Features.Products.Commands.DeleteProduct;
using InventorySystem.Application.Features.Products.Commands.UpdateProduct;
using InventorySystem.Application.Features.Products.Query.ProductList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers;
[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        this._sender = sender;
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct(AddProductRequestDto request)
    {
        var response = await _sender.Send(new AddProductCommand(request));
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductRequestDto request)
    {
        var response = await _sender.Send(new UpdateProductCommand(request));
        return StatusCode(response.StatusCode, response);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(DeleteProductRequestDto request)
    {
        var response = await _sender.Send(new DeleteProductCommand(request));
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    [ResponseCache(Duration = 5)]
    public async Task<IActionResult> GetProductsList([FromQuery] ProductListRequestDto request)
    {
        var response = await _sender.Send(new ProductListQuery(request));

        return StatusCode(response.StatusCode, response);
    }
}
