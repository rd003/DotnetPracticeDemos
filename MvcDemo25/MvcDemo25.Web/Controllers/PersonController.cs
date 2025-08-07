using Microsoft.AspNetCore.Mvc;

namespace MvcDemo25.Web.Controllers;

public class PersonController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Add()
    {
        return View();
    }
}
