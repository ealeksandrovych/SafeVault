using NUnit.Framework;
using SafeVault.Services;

namespace Tests;

[TestFixture]
public class TestInputValidation
{
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _userService = new UserService(null!);
    }

    [Test]
    public void SanitizeInput_Should_Remove_SQLInjection()
    {
        var malicious = "'; DROP TABLE Users; --";
        var clean = _userService.SanitizeInput(malicious);
        Assert.That(clean.Contains(";") || clean.Contains("DROP") || clean.Contains("--"), Is.False);
    }

    [Test]
    public void SanitizeInput_Should_Remove_XSS()
    {
        var malicious = "<script>alert('xss')</script>";
        var clean = _userService.SanitizeInput(malicious);
        Assert.That(clean.Contains("<") || clean.Contains(">") || clean.Contains("script"), Is.False);
    }
}