using Microsoft.AspNetCore.Authorization;
using NUnit.Framework;
using SafeVault.Controllers;
using System.Linq;

namespace Tests;

[TestFixture]
public class TestAuthentication
{
    [Test]
    public void AdminOnly_Action_Should_Have_AuthorizeAttribute_With_AdminRole()
    {
        // Arrange
        var method = typeof(DashboardController).GetMethod("AdminOnly");
        var attribute = (AuthorizeAttribute?)method?
            .GetCustomAttributes(typeof(AuthorizeAttribute), true)
            .FirstOrDefault();

        // Assert
        Assert.That(attribute, Is.Not.Null);
        Assert.That(attribute!.Roles, Is.EqualTo("Admin"));
    }
}