using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Controllers;

[AutoValidateAntiforgeryToken]
[Authorize(Roles = CargoAppConstants.AdminRole)]
public class AdminController : Controller
{
    public IActionResult Requests()
    {
        return View();
    }

    public IActionResult Users()
    {
        return View();
    }
}
