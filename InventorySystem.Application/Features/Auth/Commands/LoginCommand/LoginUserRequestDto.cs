namespace InventorySystem.Application.Features.Auth.Commands.LoginCommand;

public sealed record LoginUserRequestDto
(
    string Username,
    string Password
);
