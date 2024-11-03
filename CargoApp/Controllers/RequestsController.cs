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
    public async Task<IActionResult> Requests(string? id)
    {
        id ??= userManager.GetUserId(User);
        var currentId = userManager.GetUserId(User);
        if (id == null) return NotFound();
        if (id != currentId && !User.IsInRole(CargoAppConstants.AdminRole)) return Forbid();

        var user = await db.Users
            .Include(s => s.CarRequests)
            .Include(s => s.CargoRequests)
            .Select(s => new
            {
                s.Id,
                s.Name,
                s.CarRequests,
                s.CargoRequests
            })
            .FirstOrDefaultAsync(s => s.Id == id);

        if (user != null)
        {
            var carRequests = user.CarRequests.OrderByDescending(r => r.AddTime);
            var cargoRequests = user.CargoRequests.OrderByDescending(r => r.AddTime);
            var model = new RequestsViewModel(user.Name, carRequests, cargoRequests);

            return View(model);
        }

        return RedirectToAction("Search", "Home");
    }

    [AllowAnonymous]
    public IActionResult CarRequestDetails(int id)
    {
        var carRequest = db.CarRequests.Include(cr => cr.User).FirstOrDefault(cr => cr.Id == id);
        var responses = db.CarResponses.Where(cr => cr.CarRequestId == id).ToList();

        if (carRequest == null)
        {
            return NotFound();
        }

        var viewModel = new CarRequestViewModel
        {
            CarRequest = carRequest,
            UserName = carRequest.User?.Name ?? "",
            Responses = responses
        };

        return View(viewModel);
    }

    [AllowAnonymous]
    public IActionResult CargoRequestDetails(int id)
    {
        var cargoRequest = db.CargoRequests.Include(cr => cr.User).FirstOrDefault(cr => cr.Id == id);
        var responses = db.CargoResponses.Where(cr => cr.CargoRequestId == id).ToList();

        if (cargoRequest == null)
        {
            return NotFound();
        }

        var viewModel = new CargoRequestViewModel
        {
            CargoRequest = cargoRequest,
            UserName = cargoRequest.User?.Name ?? "",
            Responses = responses
        };

        return View(viewModel);
    }

    public IActionResult CreateCarRequest()
    {
        return View("CarRequest");
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
                EarlyDepartureDate = RoundToDays(model.CarSearch.DepartureTime ?? DateTime.UtcNow),
                LateDepartureDate = RoundToDays(model.CarSearch.LateDepartureTime ?? DateTime.UtcNow),
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
            return View("CarRequest", request);
        }
        return View("CarRequest");
    }

    public IActionResult CreateCargoRequest()
    {
        return View("CargoRequest");
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
                DepartureTime = RoundToMinutes(model.CargoSearch.DepartureTime ?? DateTime.UtcNow),
                Car = new()
                {
                    MaxVolume = model.CargoSearch.Volume,
                    MaxLength = model.CargoSearch.Length,
                    MaxWidth = model.CargoSearch.Width,
                    MaxHeight = model.CargoSearch.Height,
                    MaxMass = model.CargoSearch.Mass ?? 0,
                    TrailerType = model.CargoSearch.TrailerType,
                    AvailableGPS = model.CargoSearch.GPS
                }
            };
            ModelState.Clear();
            return View("CargoRequest", request);
        }
        return View("CargoRequest");
    }

    [HttpPost]
    public async Task<IActionResult> CreateCarRequest(CarRequest request)
    {
        if (request.Cargo.Volume == null && (request.Cargo.Length == null || request.Cargo.Width == null || request.Cargo.Height == null))
        {
            ModelState.AddModelError("", "Volume or dimensions must be specified");
        }
        if (!ModelState.IsValid) return View("CarRequest");

        string? userId = userManager.GetUserId(User);
        if (userId == null)
        {
            ModelState.AddModelError("", "User not found");
            return View("CarRequest");
        }
        request.UserId = userId;
        request.EarlyDepartureDate = RoundToDays(request.EarlyDepartureDate);
        request.LateDepartureDate = RoundToDays(request.LateDepartureDate);

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
        if (!ModelState.IsValid) return View("CargoRequest");

        string? userId = userManager.GetUserId(User);
        if (userId == null)
        {
            ModelState.AddModelError("", "User not found");
            return View("CargoRequest");
        }
        request.UserId = userId;
        request.DepartureTime = RoundToMinutes(request.DepartureTime);

        db.CargoRequests.Add(request);
        await db.SaveChangesAsync();
        return RedirectToAction("Search", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> EditCarRequest(int id)
    {
        var request = await db.CarRequests.FindAsync(id);
        if (request == null)
        {
            return NotFound();
        }
        return View("CarRequest", request);
    }

    [HttpGet]
    public async Task<IActionResult> EditCargoRequest(int id)
    {
        var request = await db.CargoRequests.FindAsync(id);
        if (request == null)
        {
            return NotFound();
        }
        return View("CargoRequest", request);
    }

    [HttpPost]
    public async Task<IActionResult> EditCarRequest(CarRequest request)
    {
        if (request.Cargo.Volume == null && (request.Cargo.Length == null || request.Cargo.Width == null || request.Cargo.Height == null))
        {
            ModelState.AddModelError("", "Volume or dimensions must be specified");
        }
        if (!ModelState.IsValid) return View("CarRequest");

        var dbRequest = await db.CarRequests.AsNoTracking().FirstOrDefaultAsync(r => r.Id == request.Id);
        if (dbRequest == null)
        {
            return NotFound();
        }
        if (dbRequest.UserId != userManager.GetUserId(User) && !User.IsInRole(CargoAppConstants.AdminRole))
        {
            return Forbid();
        }
        request.UserId = dbRequest.UserId;
        request.EarlyDepartureDate = RoundToDays(request.EarlyDepartureDate);
        request.LateDepartureDate = RoundToDays(request.LateDepartureDate);

        db.CarRequests.Update(request);
        await db.SaveChangesAsync();
        return RedirectToAction("Requests", new { id = request.UserId });
    }

    [HttpPost]
    public async Task<IActionResult> EditCargoRequest(CargoRequest request)
    {
        if (request.Car.MaxVolume == null && (request.Car.MaxLength == null || request.Car.MaxWidth == null || request.Car.MaxHeight == null))
        {
            ModelState.AddModelError("", "Volume or dimensions must be specified");
        }
        if (!ModelState.IsValid) return View("CargoRequest");

        var dbRequest = await db.CargoRequests.AsNoTracking().FirstOrDefaultAsync(r => r.Id == request.Id);
        if (dbRequest == null)
        {
            return NotFound();
        }
        if (dbRequest.UserId != userManager.GetUserId(User) && !User.IsInRole(CargoAppConstants.AdminRole))
        {
            return Forbid();
        }
        request.UserId = dbRequest.UserId;
        request.DepartureTime = RoundToMinutes(request.DepartureTime);

        db.CargoRequests.Update(request);
        await db.SaveChangesAsync();
        return RedirectToAction("Requests", new { id = request.UserId });
    }

    private static DateTime RoundToMinutes(DateTime datetime)
    {
        return datetime.AddTicks(-(datetime.Ticks % TimeSpan.TicksPerMinute));
    }

    private static DateTime RoundToDays(DateTime datetime)
    {
        return datetime.AddTicks(-(datetime.Ticks % TimeSpan.TicksPerDay));
    }
}
