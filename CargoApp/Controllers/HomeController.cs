using ApplicationCore.Entities;
using CargoApp.Data;
using CargoApp.Services;
using CargoApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CargoApp.Controllers
{
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> CarRequest(IndexViewModel model)
        {
            if (model.IsCarRequest)
            {
                RemoveFor(ModelState, nameof(model.CargoRequest));
                if (ModelState.IsValid)
                {
                    var requestModel = model.CarRequest;

                    var departurePlace = await db.Settlements
                        .FirstOrDefaultAsync(s => s.NormalizedSettlement == requestModel.DeparturePlace.ToUpper());
                    var destinationPlace = await db.Settlements
                        .FirstOrDefaultAsync(s => s.NormalizedSettlement == requestModel.DestinationPlace.ToUpper());

                    if (departurePlace != null && destinationPlace != null)
                    {
                        string userId = userIdManager.GetUserId();
                        CarRequest request = new(userId, requestModel.ContactPhoneNumber, requestModel.ContactName,
                            departurePlace.Id, destinationPlace.Id, requestModel.Price, requestModel.Details);

                        db.CarRequests.Add(request);
                        await db.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static void RemoveFor(ModelStateDictionary modelState, string valueName)
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
}