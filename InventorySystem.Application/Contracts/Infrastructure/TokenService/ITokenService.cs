namespace InventorySystem.Application.Contracts.Infrastructure.TokenService;

public interface ITokenService
{
    Task<string> CreateJWTToken(IdentityUser user);
}
