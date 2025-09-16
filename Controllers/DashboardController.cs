using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SafeVault.Controllers;

[Authorize]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly()
    {
        return View();
    }
}