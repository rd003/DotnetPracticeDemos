using AzureBlobDemo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AzureBlobDemo.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View(new LoginModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        try
        {
            bool isAuthenticated = model.Username == "ravindra" && model.Password == "Devrani*123*";
            if (!isAuthenticated)
            {
                model.Errors.Add("Invalid username or password");
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,model.Username),
            new Claim(ClaimTypes.NameIdentifier, model.Username)
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = false, // Session cookie
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
                    });


            return RedirectToAction("Index", "Person");
        }
        catch (Exception ex)
        {
            model.Errors.Add("An error occurred during login. Please try again.");
            return View(model);
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
