namespace InventorySystem.Domain;

public class AppUser
{
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}