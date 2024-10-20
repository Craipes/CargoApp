using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Controllers;

[Authorize]
public class RequestsController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly CargoAppContext db;

    public RequestsController(UserManager<User> userManager, CargoAppContext context)
    {
        this.userManager = userManager;
        db = context;
    }

    [HttpGet]
    public async Task<IActionResult> Requests()
    {
        var userId = userManager.GetUserId(User);
        if (userId != null)
        {
            var user = await db.Users
                .Include(s => s.CarRequests)
                .Include(s => s.CargoRequests)
                .Select(s => new
                {
                    s.Id,
                    s.CarRequests,
                    s.CargoRequests
                })
                .FirstOrDefaultAsync(s => s.Id == userId);

            if (user != null)
            {
                var carRequests = user.CarRequests.OrderByDescending(r => r.AddTime).ThenBy(r => r.IsExpired);
                var cargoRequests = user.CargoRequests.OrderByDescending(r => r.AddTime).ThenBy(r => r.IsExpired);
                var model = new RequestsViewModel(carRequests, cargoRequests);

                return View(model);
            }
        }
        return RedirectToAction("Search", "Home");
    }

    public IActionResult CreateCarRequest()
    {
        return View();
    }

    public IActionResult CreateCargoRequest()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCarRequest(CarRequest request)
    {
        if (!ModelState.IsValid) return View();

        string? userId = userManager.GetUserId(User);
        if (userId == null)
        {
            ModelState.AddModelError("", "User not found");
            return View();
        }
        request.UserId = userId;

        db.CarRequests.Add(request);
        await db.SaveChangesAsync();
        return RedirectToAction("Search", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> CreateCargoRequest(CargoRequest request)
    {
        if (!ModelState.IsValid) return View();

        string? userId = userManager.GetUserId(User);
        if (userId == null)
        {
            ModelState.AddModelError("", "User not found");
            return View();
        }
        request.UserId = userId;

        db.CargoRequests.Add(request);
        await db.SaveChangesAsync();
        return RedirectToAction("Search", "Home");
    }
}
