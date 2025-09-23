using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcEntraIdDemo.Web.Controllers;

[Authorize]
public class TestController : Controller
{
    public IActionResult Claims()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        return Json(claims);
    }

    [Authorize(Roles = "Task.ReadWriteDelete")]
    public IActionResult Index()
    {
        return Content("Admin role");
    }

    [Authorize(Roles = "Manager")]
    public IActionResult Manager()
    {
        return Content("Manager role...");
    }
}
