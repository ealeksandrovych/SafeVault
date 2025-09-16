using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SafeVault.Data;
using SafeVault.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC + Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// EF Core + SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=safevault.db"));

builder.Services.AddScoped<IUserService, UserService>();

// Auth + Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // якщо неавторизований
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
