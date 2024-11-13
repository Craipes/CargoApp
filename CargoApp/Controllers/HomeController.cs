using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;

namespace CargoApp.Controllers;

public class HomeController : Controller
{
    private readonly CargoAppContext db;
    private readonly ILogger<HomeController> _logger;

    public HomeController(CargoAppContext context, ILogger<HomeController> logger)
    {
        db = context;
        _logger = logger;
    }

    public IActionResult About()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Search(SearchViewModel model)
    {
        var search = model.Search;
        if (model.IsCarSearching)
        {
            RemoveFor(ModelState, nameof(model.CargoSearch));
            if (ModelState.IsValid)
            {
                var carSearch = model.CarSearch;
                var requests = db.CargoRequests
                    .AsNoTracking()
                    .Where(r => r.RequestType < CargoAppConstants.REQUEST_TYPE_MAX_OPEN)
                    .Where(r => r.DeparturePlace.ToUpper().Contains(search.DeparturePlace.ToUpper()) && r.DestinationPlace.ToUpper().Contains(search.DestinationPlace.ToUpper()));

                if (search.Mass.HasValue)
                {
                    requests = requests.Where(r => r.Car.MaxMass >= search.Mass);
                }
                if (search.Volume.HasValue)
                {
                    requests = requests.Where(r => r.Car.MaxVolume >= search.Volume);
                }
                if (search.Length.HasValue)
                {
                    requests = requests.Where(r => r.Car.MaxLength >= search.Length);
                }
                if (search.Width.HasValue)
                {
                    requests = requests.Where(r => r.Car.MaxWidth >= search.Width);
                }
                if (search.Height.HasValue)
                {
                    requests = requests.Where(r => r.Car.MaxHeight >= search.Height);
                }
                if (carSearch.NeedsGPS)
                {
                    requests = requests.Where(r => r.Car.AvailableGPS);
                }
                if (search.TrailerType != TrailerType.Any)
                {
                    requests = requests.Where(r => r.Car.TrailerType == search.TrailerType);
                }
                if (carSearch.EarlyDepartureTime.HasValue)
                {
                    requests = requests.Where(r => r.DepartureTime.Date >= carSearch.EarlyDepartureTime.Value.Date);
                }
                else
                {
                    requests = requests.Where(r => r.DepartureTime >= DateTime.UtcNow);
                }
                if (carSearch.LateDepartureTime.HasValue)
                {
                    requests = requests.Where(r => r.DepartureTime.Date <= carSearch.LateDepartureTime.Value);
                }

                requests = requests.OrderBy(r => r.DepartureTime);
                model.CargoRequests = await requests.ToListAsync();
                model.SearchWasPerformed = true;
                return View(model);
            }
        }
        else if (model.IsCargoSearching)
        {
            RemoveFor(ModelState, nameof(model.CarSearch));
            if (ModelState.IsValid)
            {
                var cargoSearch = model.CargoSearch;
                var requests = db.CarRequests
                    .AsNoTracking()
                    .Where(r => r.RequestType < CargoAppConstants.REQUEST_TYPE_MAX_OPEN)
                    .Where(r => r.DeparturePlace.ToUpper().Contains(search.DeparturePlace.ToUpper()) && r.DestinationPlace.ToUpper().Contains(search.DestinationPlace.ToUpper()));

                if (search.Mass.HasValue)
                {
                    requests = requests.Where(r => r.Cargo.Mass <= search.Mass);
                }
                if (search.Volume.HasValue)
                {
                    requests = requests.Where(r => r.Cargo.Volume <= search.Volume);
                }
                if (search.Length.HasValue)
                {
                    requests = requests.Where(r => r.Cargo.Length <= search.Length);
                }
                if (search.Width.HasValue)
                {
                    requests = requests.Where(r => r.Cargo.Width <= search.Width);
                }
                if (search.Height.HasValue)
                {
                    requests = requests.Where(r => r.Cargo.Height <= search.Height);
                }
                if (!cargoSearch.HasGPS)
                {
                    requests = requests.Where(r => !r.NeedsGPS);
                }
                if (search.TrailerType != TrailerType.Any)
                {
                    requests = requests.Where(r => r.Cargo.TrailerType == search.TrailerType || r.Cargo.TrailerType == TrailerType.Any);
                }
                else
                {
                    requests = requests.Where(r => r.Cargo.TrailerType == TrailerType.Any);
                }
                if (cargoSearch.DepartureTime.HasValue)
                {
                    var shift = cargoSearch.DepartureTime.Value.Date.AddHours(-6);
                    requests = requests.Where(r => r.LateDepartureDate >= shift);
                }
                else
                {
                    requests = requests.Where(r => r.LateDepartureDate >= DateTime.UtcNow);
                }

                requests = requests.OrderBy(r => r.EarlyDepartureDate);
                model.CarRequests = await requests.ToListAsync();
                model.SearchWasPerformed = true;
                return View(model);
            }
        }
        model.IsCarSearching = true;
        return View(model);
    }

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(returnUrl);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private static void RemoveFor(ModelStateDictionary modelState, string valueName)
    {
        foreach (var ms in modelState.ToArray())
        {
            if (ms.Key.StartsWith(valueName + ".") || ms.Key == valueName)
            {
                //modelState[ms.Key]?.Errors.Clear();
                modelState.Remove(ms.Key);
            }
        }
    }
}