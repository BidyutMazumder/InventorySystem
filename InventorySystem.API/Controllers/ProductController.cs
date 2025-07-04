using InventorySystem.Application.Features.Products.Commands.AddProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
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
        public async Task<IActionResult> CreateProduct(AddProductRequestDto request)
        {
            var response = await _sender.Send(new AddProductCommand(request));
            return StatusCode(response.StatusCode, response);
        }
    }
}
