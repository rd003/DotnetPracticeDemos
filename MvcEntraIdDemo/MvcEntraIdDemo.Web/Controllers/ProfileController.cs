using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using System.Text.Json;

namespace MvcEntraIdDemo.Web.Controllers;

public class ProfileController : Controller
{
    private readonly GraphServiceClient _graphServiceClient;

    public ProfileController(GraphServiceClient graphServiceClient)
    {
        _graphServiceClient = graphServiceClient;
    }

    [Authorize]
    public async Task<IActionResult> Edit()
    {
        try
        {

            var user = await _graphServiceClient.Me
                .GetAsync();

            var model = new EditProfileViewModel
            {
                DisplayName = user.DisplayName,
                GivenName = user.GivenName,
                Surname = user.Surname,
               
            };

            return View(model);
        }
        catch (Exception ex)
        {
            // Handle error
            return View("Error");
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(EditProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var userId = User.FindFirst("oid")?.Value;

        try
        {
            var userUpdate = new User
            {
                DisplayName = model.DisplayName,
                GivenName = model.GivenName,
                Surname = model.Surname
            };

            await _graphServiceClient.Users[userId]
                .PatchAsync(userUpdate);

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Failed to update profile");
            return View(model);
        }
    }
}

public class EditProfileViewModel
{
    public string? DisplayName { get; set; }
    public string? Surname { get; set; }
    public string? GivenName { get; set; }
}