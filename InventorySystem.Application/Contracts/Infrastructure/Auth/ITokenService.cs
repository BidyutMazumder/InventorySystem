namespace InventorySystem.Application.Contracts.Infrastructure.Auth;

public interface ITokenService
{
    Task<string> CreateJWTToken(IdentityUser user);
}
