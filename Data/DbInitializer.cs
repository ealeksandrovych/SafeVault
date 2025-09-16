using SafeVault.Models;

namespace SafeVault.Data;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Users.Any()) return;

        var users = new List<User>
        {
            new User
            {
                Username = "admin",
                Email = "admin@safevault.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = "Admin"
            },
            new User
            {
                Username = "user",
                Email = "user@safevault.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                Role = "User"
            }
        };

        context.Users.AddRange(users);
        context.SaveChanges();
    }
}