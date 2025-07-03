namespace InventorySystem.Application.Features.Auth.Commands.RegisterCommand;

public sealed record RegisterUserCommand(RegisterUserRequestDto request) : IRequest<RegisterUserResponseDto>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponseDto>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IValidator<RegisterUserRequestDto> _validator;

    public RegisterUserCommandHandler(UserManager<IdentityUser> userManager, IValidator<RegisterUserRequestDto> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<RegisterUserResponseDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new RegisterUserResponseDto(false, errorMessage);
        }

        var existingUser = await _userManager.FindByNameAsync(command.request.Username);
        if (existingUser != null)
        {
            return new RegisterUserResponseDto(false, "Username already exists.");
        }

        var identityUser = new IdentityUser
        {
            UserName = command.request.Username
        };

        var result = await _userManager.CreateAsync(identityUser, command.request.Password);

        if (!result.Succeeded)
        {
            var errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
            return new RegisterUserResponseDto(false, errorMessage);
        }

        return new RegisterUserResponseDto(true, "User registered successfully.");
    }
}

