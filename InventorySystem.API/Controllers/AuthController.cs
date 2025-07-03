using InventorySystem.Application.Features.Auth.Commands.RegisterCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        this._sender = sender;
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserRequestDto request)
    {
        var result = await _sender.Send(new RegisterUserCommand(request));

        return Ok(result);
    }
}
