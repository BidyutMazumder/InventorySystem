namespace InventorySystem.Application.Features.Auth.Commands.LoginCommand;

public sealed record LoginUserCommand(LoginUserRequestDto request)
    :IRequest<Response<string?>>;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<string?>>
{
    private readonly ITokenService _tokenService;
    private readonly IValidator<LoginUserRequestDto> _validator;
    private readonly UserManager<IdentityUser> _userManager;

    public LoginUserCommandHandler(
        ITokenService tokenService, 
        IValidator<LoginUserRequestDto> validator,
        UserManager<IdentityUser> userManager
        )
    {
        this._tokenService = tokenService;
        this._validator = validator;
        this._userManager = userManager;
    }
    public async Task<Response<string?>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.request);
        if (!validationResult.IsValid)
        {
            return Response<string?>.FailureResponse(
                message: "Validation failed",
                errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                statusCode: 400);
        }
        var user = await _userManager.FindByNameAsync(command.request.Username);
        if(user is null)
        {
            return Response<string?>.FailureResponse(
                message: "User not found",
                statusCode: 404);
        }
        var result = await _userManager.CheckPasswordAsync(user, command.request.Password);
        if (!result)
        {
            return Response<string?>.FailureResponse(
                message: "Invalid Password",
                statusCode: 400);
        }
        string token = await _tokenService.CreateJWTToken(user);

        return Response<string?>.SuccessResponse(
            data: token,
            message: "Login successful",
            statusCode: 200);
    }
}
