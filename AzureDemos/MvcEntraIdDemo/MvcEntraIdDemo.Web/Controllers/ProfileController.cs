using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using System.Reflection;
namespace MvcEntraIdDemo.Web.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly GraphServiceClient _graphServiceClient;

    public ProfileController(GraphServiceClient graphServiceClient)
    {
        _graphServiceClient = graphServiceClient;
    }

    public async Task<IActionResult> me()
    {
        try
        {
            var user = await _graphServiceClient.Me
        .GetAsync(requestConfig =>
        {
            requestConfig.QueryParameters.Select = new[] { "id", "displayName", "mail", "city", "state", "country", "mobilePhone", "givenName", "surname", "jobTitle", "companyName" };
        });
            return Ok(user);
        }
        catch (Exception ex)
        {
            // Handle error
            return Ok("Error");
        }

    }

    public async Task<IActionResult> Edit()
    {
        try
        {

            // with this, I am not able to fetch city,state and country
            //var user = await _graphServiceClient.Me
            //    .GetAsync();

            var user = await _graphServiceClient.Me
        .GetAsync(requestConfig =>
        {
            requestConfig.QueryParameters.Select = new[] { "id", "displayName", "mail", "city", "state", "country", "mobilePhone", "givenName", "surname", "jobTitle", "companyName" };
        });

            var model = new EditProfileViewModel
            {
                DisplayName = user.DisplayName,
                FirstName = user.GivenName,
                LastName = user.Surname,
                MobilePhone = user.MobilePhone,
                City = user.City,
                Country = user.Country,
                State = user.State
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
    public async Task<IActionResult> Edit(EditProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _graphServiceClient.Me.GetAsync();
        var userId = user.Id;
        try
        {
            var userUpdate = new User
            {
                DisplayName = model.DisplayName,
                GivenName = model.FirstName,
                Surname = model.LastName,
                City = model.City,
                State = model.State,
                Country = model.Country,
                MobilePhone = model.MobilePhone
            };

            await _graphServiceClient.Users[userId]
                .PatchAsync(userUpdate);

            TempData["message"] = "Profile updated successfully!";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            TempData["message"]= "Failed to update profile";
            return View(model);
        }
    }
}

public class EditProfileViewModel
{
    public string? DisplayName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? MobilePhone { get; set; }
    public string? Email { get; set; }
}