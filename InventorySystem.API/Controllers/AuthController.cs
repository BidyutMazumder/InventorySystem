using InventorySystem.Application.Features.Auth.Commands.LoginCommand;
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
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequestDto request)
    {
        var response = await _sender.Send(new RegisterUserCommand(request));
        return StatusCode(response.StatusCode, response);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequestDto request)
    {
        var response = await _sender.Send(new LoginUserCommand(request));
        return StatusCode(response.StatusCode, response);
    }
}
