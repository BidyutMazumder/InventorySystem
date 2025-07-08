namespace InventorySystem.API.Controllers;
[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly ISender _sender;

    public SalesController(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSales(CreateSalesRequestDto request)
    {
        var response = await _sender.Send(new CreateSalesCommand(request));
        await Task.Delay(3000);
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    [ResponseCache(Duration = 5)]
    public async Task<IActionResult> GetSalesList([FromQuery] GetSalesSummaryRequestDto request)
    {
        var response = await _sender.Send(new GetSalesSummaryQuery(request));
        return StatusCode(response.StatusCode, response);
    }
}
