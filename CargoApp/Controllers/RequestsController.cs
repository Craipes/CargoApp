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
                var carRequests = user.CarRequests.OrderByDescending(r => r.AddTime);
                var cargoRequests = user.CargoRequests.OrderByDescending(r => r.AddTime);
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

    [HttpPost]
    public IActionResult CarRequestFromSearch(SearchViewModel? model)
    {
        if (model != null)
        {
            CarRequest request = new()
            {
                UserId = null!,
                ContactName = null!,
                ContactPhoneNumber = null!,
                DeparturePlace = model.CarSearch.DeparturePlace,
                DestinationPlace = model.CarSearch.DestinationPlace,
                EarlyDepartureDate = model.CarSearch.DepartureTime ?? DateTime.UtcNow,
                LateDepartureDate = model.CarSearch.LateDepartureTime ?? DateTime.UtcNow,
                NeedsGPS = model.CarSearch.GPS,
                Cargo = new()
                {
                    Volume = model.CarSearch.Volume,
                    Length = model.CarSearch.Length,
                    Width = model.CarSearch.Width,
                    Height = model.CarSearch.Height,
                    Mass = model.CarSearch.Mass ?? 0,
                    TrailerType = model.CarSearch.TrailerType
                }
            };
            ModelState.Clear();
            return View(nameof(CreateCarRequest), request);
        }
        return View(nameof(CreateCarRequest));
    }

    public IActionResult CreateCargoRequest()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CargoRequestFromSearch(SearchViewModel? model)
    {
        if (model != null)
        {
            CargoRequest request = new()
            {
                UserId = null!,
                ContactName = null!,
                ContactPhoneNumber = null!,
                DeparturePlace = model.CargoSearch.DeparturePlace,
                DestinationPlace = model.CargoSearch.DestinationPlace,
                DepartureTime = model.CargoSearch.DepartureTime ?? DateTime.UtcNow,
                Car = new()
                {
                    MaxVolume = model.CargoSearch.Volume,
                    MaxLength = model.CargoSearch.Length,
                    MaxWidth = model.CargoSearch.Width,
                    MaxHeight = model.CargoSearch.Height,
                    MaxMass = model.CargoSearch.Mass ?? 0,
                    TrailerType = model.CargoSearch.TrailerType,
                    Type = model.CargoSearch.CarType,
                    AvailableGPS = model.CargoSearch.GPS
                }
            };
            ModelState.Clear();
            return View(nameof(CreateCargoRequest), request);
        }
        return View(nameof(CreateCargoRequest));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCarRequest(CarRequest request)
    {
        if (request.Cargo.Volume == null && (request.Cargo.Length == null || request.Cargo.Width == null || request.Cargo.Height == null))
        {
            ModelState.AddModelError("", "Volume or dimensions must be specified");
        }
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
        if (request.Car.MaxVolume == null && (request.Car.MaxLength == null || request.Car.MaxWidth == null || request.Car.MaxHeight == null))
        {
            ModelState.AddModelError("", "Volume or dimensions must be specified");
        }
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
