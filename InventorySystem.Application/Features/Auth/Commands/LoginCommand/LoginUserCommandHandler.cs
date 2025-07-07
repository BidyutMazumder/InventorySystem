namespace InventorySystem.Application.Features.Auth.Commands.LoginCommand;

public sealed record LoginUserCommand(LoginUserRequestDto request)
    : IRequest<Response<LoginUserResponseDto>>;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<LoginUserResponseDto>>
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<IdentityUser> _userManager;

    public LoginUserCommandHandler(
        ITokenService tokenService,
        UserManager<IdentityUser> userManager)
    {
        this._tokenService = tokenService;
        this._userManager = userManager;
    }

    public async Task<Response<LoginUserResponseDto>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(command.request.Username);
        if (user is null)
        {
            return Response<LoginUserResponseDto>.FailureResponse(
                message: "User not found",
                statusCode: 404);
        }

        var result = await _userManager.CheckPasswordAsync(user, command.request.Password);
        if (!result)
        {
            return Response<LoginUserResponseDto>.FailureResponse(
                message: "Invalid password",
                statusCode: 400);
        }

        string token = await _tokenService.CreateJWTToken(user);

        var response = new LoginUserResponseDto
        {
            Username = user.UserName!,
            Token = token
        };

        return Response<LoginUserResponseDto>.SuccessResponse(
            data: response,
            message: "Login successful",
            statusCode: 200);
    }
}
