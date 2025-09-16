using SafeVault.Models;

namespace SafeVault.Services;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> ValidateLoginAsync(string email, string password);
    string SanitizeInput(string input);
}