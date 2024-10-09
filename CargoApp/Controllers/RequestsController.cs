using CargoApp.Models;
using CargoApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
                    .ThenInclude(s => s.DeparturePlace)
                .Include(s => s.CarRequests)
                    .ThenInclude(s => s.DestinationPlace)
                .Include(s => s.CargoRequests)
                    .ThenInclude(s => s.DeparturePlace)
                .Include(s => s.CargoRequests)
                    .ThenInclude(s => s.DestinationPlace)
                .Select(s => new
                {
                    s.Id,
                    s.CarRequests,
                    s.CargoRequests
                })
                .FirstOrDefaultAsync(s => s.Id == userId);

            if (user != null)
            {
                var carRequests = user.CarRequests.Select(r => CarRequestViewModel.FromRequest(r)).OrderByDescending(r => r.AddTime).ThenBy(r => r.IsExpired);
                var cargoRequests = user.CargoRequests.Select(r => CargoRequestViewModel.FromRequest(r)).OrderByDescending(r => r.AddTime).ThenBy(r => r.IsExpired);
                var model = new RequestsViewModel(carRequests, cargoRequests);

                return View(model);
            }
        }
        return RedirectToAction("Search", "Home");
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddCarRequest(IndexViewModel model)
    {
        if (model.IsCarRequest)
        {
            RemoveFor(ModelState, nameof(model.CargoRequest));
            if (ModelState.IsValid)
            {
                var requestModel = model.CarRequest;

                var places = await db.SearchPlaces(requestModel.DeparturePlace.ToUpper(), requestModel.DestinationPlace.ToUpper());

                if (places != null)
                {
                    (var departurePlace, var destinationPlace) = places.Value;

                    string? userId = userManager.GetUserId(User);
                    if (userId == null)
                    {
                        ModelState.AddModelError("", "User not found");
                        return View(nameof(Create));
                    }
                    CarRequest request = requestModel.CreateRequest(userId, departurePlace.Id, destinationPlace.Id);

                    db.CarRequests.Add(request);
                    await db.SaveChangesAsync();
                }
            }
        }
        return View(nameof(Create));
    }

    [HttpPost]
    public async Task<IActionResult> AddCargoRequest(IndexViewModel model)
    {
        if (model.IsCargoRequest)
        {
            RemoveFor(ModelState, nameof(model.CarRequest));
            if (ModelState.IsValid)
            {
                var requestModel = model.CargoRequest;

                var places = await db.SearchPlaces(requestModel.DeparturePlace.ToUpper(), requestModel.DestinationPlace.ToUpper());

                if (places != null)
                {
                    (var departurePlace, var destinationPlace) = places.Value;

                    string? userId = userManager.GetUserId(User);
                    if (userId == null)
                    {
                        ModelState.AddModelError("", "User not found");
                        return View(nameof(Create));
                    }
                    CargoRequest request = requestModel.CreateRequest(userId, departurePlace.Id, destinationPlace.Id);

                    db.CargoRequests.Add(request);
                    await db.SaveChangesAsync();
                }
            }
        }
        return View(nameof(Create));
    }

    private static void RemoveFor(ModelStateDictionary modelState, string valueName)
    {
        foreach (var ms in modelState.ToArray())
        {
            if (ms.Key.StartsWith(valueName + ".") || ms.Key == valueName)
            {
                modelState.Remove(ms.Key);
            }
        }
    }
}
