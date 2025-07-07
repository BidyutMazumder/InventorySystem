namespace InventorySystem.Application.Features.Auth.Commands.LoginCommand;

public record LoginUserResponseDto
{
    public string Username { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

