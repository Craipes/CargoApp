using Microsoft.AspNetCore.Authorization;
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
    public IActionResult Search()
    {
        return View();
    }

    [AjaxOnly]
    [HttpPost]
    public async Task<PartialViewResult> Search(SearchViewModel model)
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

                    return PartialView("SearchResultsPartial", model);
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

                    return PartialView("SearchResultsPartial", model);
                }
            }
        }
        return PartialView("SearchResultsPartial", model);
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