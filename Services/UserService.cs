using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using SafeVault.Data;
using SafeVault.Models;

namespace SafeVault.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Basic sanitization to prevent SQLi and XSS.
    /// Removes dangerous characters and keywords.
    /// </summary>
    public string SanitizeInput(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        // Remove common XSS/SQLi symbols
        var sanitized = Regex.Replace(input, @"[<>""'%;()&+]", "", RegexOptions.Compiled);

        // Remove common keywords (SQL, script)
        sanitized = sanitized.Replace("DROP", "", StringComparison.OrdinalIgnoreCase);
        sanitized = sanitized.Replace("TABLE", "", StringComparison.OrdinalIgnoreCase);
        sanitized = sanitized.Replace("SCRIPT", "", StringComparison.OrdinalIgnoreCase);
        sanitized = sanitized.Replace("--", "", StringComparison.OrdinalIgnoreCase);

        return sanitized.Trim();
    }

    /// <summary>
    /// Finds user by email using parameterized query (via EF).
    /// </summary>
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var safeEmail = SanitizeInput(email);
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == safeEmail);
    }

    /// <summary>
    /// Verifies login password against hashed password.
    /// </summary>
    public async Task<bool> ValidateLoginAsync(string email, string password)
    {
        var user = await GetUserByEmailAsync(email);
        return user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }
}