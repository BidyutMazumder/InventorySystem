namespace InventorySystem.Application.Features.Auth.Commands.RegisterCommand;

public sealed record RegisterUserCommand(RegisterUserRequestDto request)
    : IRequest<Response<Unit>>;

public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, Response<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IValidator<RegisterUserRequestDto> _validator;

    public RegisterUserCommandHandler(
        UserManager<IdentityUser> userManager,
        IValidator<RegisterUserRequestDto> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<Response<Unit>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.request);
        if (!validationResult.IsValid)
        {
            return Response<Unit>.FailureResponse(
                message: "Validation failed",
                errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                statusCode: 400);
        }

        if (await _userManager.FindByNameAsync(command.request.Username) != null)
        {
            return Response<Unit>.FailureResponse(
                message: "Username taken",
                statusCode: 409);
        }

        var result = await _userManager.CreateAsync(
            new IdentityUser { UserName = command.request.Username },
            command.request.Password);

        if (!result.Succeeded)
        {
            return Response<Unit>.FailureResponse(
                message: "User creation failed",
                errors: result.Errors.Select(e => e.Description).ToList(),
                statusCode: 422); 
        }

        return Response<Unit>.SuccessResponse(
            data: Unit.Value,
            message: "User created",
            statusCode: 201); 
    }
}
