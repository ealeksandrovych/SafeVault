using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SafeVault.Controllers;

namespace Tests;

[TestFixture]
public class TestAuthentication
{
    [Test]
    public void AdminOnly_Should_Be_Forbidden_For_NonAdmin()
    {
        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Role, "User")
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var context = new DefaultHttpContext { User = principal };
        var controller = new DashboardController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = context
            }
        };

        var result = controller.AdminOnly();

        Assert.That(result, Is.InstanceOf<ForbidResult>());
    }
}