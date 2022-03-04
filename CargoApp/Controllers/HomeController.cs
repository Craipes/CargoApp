using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;

namespace CargoApp.Controllers;

public class HomeController : Controller
{
    private readonly CargoAppContext db;
    private readonly UserIdManager userIdManager;
    private readonly ILogger<HomeController> _logger;

    public HomeController(CargoAppContext context, UserIdManager userIdManager, ILogger<HomeController> logger)
    {
        db = context;
        this.userIdManager = userIdManager;
        _logger = logger;
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize]
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

                    string userId = userIdManager.GetUserId();
                    CarRequest request = requestModel.CreateRequest(userId, departurePlace.Id, destinationPlace.Id);

                    db.CarRequests.Add(request);
                    await db.SaveChangesAsync();
                }
            }
        }
        return RedirectToAction("Create", "Home");
    }

    [Authorize]
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

                    string userId = userIdManager.GetUserId();
                    CargoRequest request = requestModel.CreateRequest(userId, departurePlace.Id, destinationPlace.Id);

                    db.CargoRequests.Add(request);
                    await db.SaveChangesAsync();
                }
            }
        }
        return RedirectToAction("Create", "Home");
    }

    [HttpGet]
    public IActionResult Search()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Search(SearchViewModel model)
    {
        if (model.IsCarSearching)
        {
            RemoveFor(ModelState, nameof(model.CargoSearch));
            if (ModelState.IsValid)
            {
                var requests = await db.CargoRequests.Search(db, model.CarSearch);

                if (requests != null)
                {
                    var requestsArr = await requests.ToArrayAsync();

                    model.CargoRequests = requestsArr
                        .Select(r => CargoRequestModel.FromRequest(r))
                        .ToList();

                    return View(model);
                }
            }
        }
        else if (model.IsCargoSearching)
        {
            RemoveFor(ModelState, nameof(model.CarSearch));
            if (ModelState.IsValid)
            {
                var requests = await db.CarRequests.Search(db, model.CargoSearch);

                if (requests != null)
                {
                    var requestsArr = await requests.ToArrayAsync();

                    model.CarRequests = requests
                        .Select(r => CarRequestModel.FromRequest(r))
                        .ToList();

                    return View(model);
                }
            }
        }
        return View(model);
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
                modelState.Remove(ms.Key);
            }
        }
    }
}