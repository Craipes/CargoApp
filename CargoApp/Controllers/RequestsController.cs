using CargoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Controllers;

[Authorize]
public class RequestsController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly RequestsService requestsService;

    public RequestsController(UserManager<User> userManager, RequestsService requestsService)
    {
        this.userManager = userManager;
        this.requestsService = requestsService;
    }

    [HttpGet]
    [Authorize(Roles = CargoAppConstants.AdminRole)]
    public async Task<IActionResult> AllCarRequestsAdmin(int page = 1)
    {
        int requestsCount = await requestsService.CarRequestsCountAsync(true);
        int pages = (requestsCount - 1) / CargoAppConstants.RequestsPerPage + 1;
        page = Math.Clamp(page, 1, pages);

        var requests = await requestsService.PaginatedCarRequestsAsync(page, true);
        AllCarRequestsViewModel model = new(null, null, page, pages, requests);
        return View(nameof(AllCarRequests), model);
    }

    [HttpGet]
    [Authorize(Roles = CargoAppConstants.AdminRole)]
    public async Task<IActionResult> AllCargoRequestsAdmin(int page = 1)
    {
        int requestsCount = await requestsService.CargoRequestsCountAsync(true);
        int pages = (requestsCount - 1) / CargoAppConstants.RequestsPerPage + 1;
        page = Math.Clamp(page, 1, pages);

        var requests = await requestsService.PaginatedCargoRequestsAsync(page, true);
        AllCargoRequestsViewModel model = new(null, null, page, pages, requests);
        return View(nameof(AllCargoRequests), model);
    }

    [HttpGet]
    public async Task<IActionResult> AllCarRequests(string? id, int page = 1)
    {
        var userId = userManager.GetUserId(User);
        id ??= userId;
        if (id == null) return NotFound();
        var user = await userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        bool canViewHidden = userId == id || User.IsInRole(CargoAppConstants.AdminRole);
        int requestsCount = await requestsService.CarRequestsCountAsync(canViewHidden, id);
        int pages = (requestsCount - 1) / CargoAppConstants.RequestsPerPage + 1;
        page = Math.Clamp(page, 1, pages);

        var requests = await requestsService.PaginatedCarRequestsAsync(page, canViewHidden, id);

        AllCarRequestsViewModel model = new(user.Id, user.Name, page, pages, requests);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> AllCargoRequests(string? id, int page = 1)
    {
        var userId = userManager.GetUserId(User);
        id ??= userId;
        if (id == null) return NotFound();
        var user = await userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        bool canViewHidden = userId == id || User.IsInRole(CargoAppConstants.AdminRole);
        int requestsCount = await requestsService.CargoRequestsCountAsync(canViewHidden, id);
        int pages = (requestsCount - 1) / CargoAppConstants.RequestsPerPage + 1;
        page = Math.Clamp(page, 1, pages);

        var requests = await requestsService.PaginatedCargoRequestsAsync(page, canViewHidden, id);

        AllCargoRequestsViewModel model = new(user.Id, user.Name, page, pages, requests);
        return View(model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> CarRequestDetails(int id)
    {
        var carRequest = await requestsService.NoTrackingCarDetailsAsync(id);

        if (carRequest == null)
        {
            return NotFound();
        }

        var viewModel = new CarRequestViewModel
        {
            CarRequest = carRequest,
            UserName = carRequest.User?.Name ?? "",
            Responses = carRequest.Responses,
            AllowEditing = requestsService.CanEditRequest(carRequest)
        };

        return View(viewModel);
    }

    [AllowAnonymous]
    public async Task<IActionResult> CargoRequestDetails(int id)
    {
        var cargoRequest = await requestsService.NoTrackingCargoDetailsAsync(id);

        if (cargoRequest == null)
        {
            return NotFound();
        }

        var viewModel = new CargoRequestViewModel
        {
            CargoRequest = cargoRequest,
            UserName = cargoRequest.User?.Name ?? "",
            Responses = cargoRequest.Responses,
            AllowEditing = requestsService.CanEditRequest(cargoRequest)
        };

        return View(viewModel);
    }

    public IActionResult CreateCarRequest()
    {
        return View("CarRequest");
    }

    [HttpGet]
    public IActionResult CarRequestFromSearch(SearchViewModel? model)
    {
        if (model == null) return View("CarRequest");

        CarRequest request = new()
        {
            UserId = null!,
            ContactName = null!,
            ContactPhoneNumber = null!,
            DeparturePlace = model.Search.DeparturePlace,
            DestinationPlace = model.Search.DestinationPlace,
            EarlyDepartureDate = (model.CarSearch.EarlyDepartureTime ?? DateTime.UtcNow).Date,
            LateDepartureDate = (model.CarSearch.LateDepartureTime ?? DateTime.UtcNow).Date,
            NeedsGPS = model.CarSearch.NeedsGPS,
            Cargo = new()
            {
                Volume = model.Search.Volume,
                Length = model.Search.Length,
                Width = model.Search.Width,
                Height = model.Search.Height,
                Mass = model.Search.Mass ?? 0,
                TrailerType = model.Search.TrailerType
            }
        };
        ModelState.Clear();
        return View("CarRequest", request);
    }

    public IActionResult CreateCargoRequest()
    {
        return View("CargoRequest");
    }

    [HttpGet]
    public IActionResult CargoRequestFromSearch(SearchViewModel? model)
    {
        if (model == null) return View("CargoRequest");

        CargoRequest request = new()
        {
            UserId = null!,
            ContactName = null!,
            ContactPhoneNumber = null!,
            DeparturePlace = model.Search.DeparturePlace,
            DestinationPlace = model.Search.DestinationPlace,
            DepartureTime = (model.CargoSearch.DepartureTime ?? DateTime.UtcNow).RoundToMinutes(),
            Car = new()
            {
                MaxVolume = model.Search.Volume,
                MaxLength = model.Search.Length,
                MaxWidth = model.Search.Width,
                MaxHeight = model.Search.Height,
                MaxMass = model.Search.Mass ?? 0,
                TrailerType = model.Search.TrailerType,
                AvailableGPS = model.CargoSearch.HasGPS
            }
        };
        ModelState.Clear();
        return View("CargoRequest", request);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCarRequest(CarRequest request)
    {
        requestsService.ValidateVolumeAndDimensions(ModelState, request);
        if (!ModelState.IsValid) return View("CarRequest");

        if (!await requestsService.CreateCarRequestAsync(request))
        {
            ModelState.AddModelError("", "User not found");
            return View("CarRequest");
        }
        return RedirectToAction(nameof(AllCarRequests));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCargoRequest(CargoRequest request)
    {
        requestsService.ValidateVolumeAndDimensions(ModelState, request);
        if (!ModelState.IsValid) return View("CargoRequest");

        if (!await requestsService.CreateCargoRequestAsync(request))
        {
            ModelState.AddModelError("", "User not found");
            return View("CargoRequest");
        }
        return RedirectToAction(nameof(AllCargoRequests));
    }

    [HttpGet]
    public async Task<IActionResult> EditCarRequest(int id)
    {
        var request = await requestsService.NoTrackingCarFindAsync(id);
        if (request == null)
        {
            return NotFound();
        }
        return View("CarRequest", request);
    }

    [HttpGet]
    public async Task<IActionResult> EditCargoRequest(int id)
    {
        var request = await requestsService.NoTrackingCargoFindAsync(id);
        if (request == null)
        {
            return NotFound();
        }
        return View("CargoRequest", request);
    }

    [HttpPost]
    public async Task<IActionResult> EditCarRequest(CarRequest request)
    {
        ModelState.Remove(nameof(CarRequest.DeparturePlace));
        ModelState.Remove(nameof(CarRequest.DestinationPlace));
        ModelState.Remove(nameof(CarRequest.EarlyDepartureDate));
        ModelState.Remove(nameof(CarRequest.LateDepartureDate));

        requestsService.ValidateVolumeAndDimensions(ModelState, request);
        if (!ModelState.IsValid) return View("CarRequest", request);

        var dbRequest = await requestsService.NoTrackingCarFindAsync(request.Id);
        if (dbRequest == null)
        {
            return NotFound();
        }
        if (!requestsService.CanEditRequest(dbRequest))
        {
            return Forbid();
        }
        await requestsService.EditCarRequestAsync(dbRequest, request);
        return RedirectToAction(nameof(AllCarRequests), new { id = request.UserId });
    }

    [HttpPost]
    public async Task<IActionResult> EditCargoRequest(CargoRequest request)
    {
        ModelState.Remove(nameof(CargoRequest.DeparturePlace));
        ModelState.Remove(nameof(CargoRequest.DestinationPlace));
        ModelState.Remove(nameof(CargoRequest.DepartureTime));

        requestsService.ValidateVolumeAndDimensions(ModelState, request);
        if (!ModelState.IsValid) return View("CargoRequest", request);

        var dbRequest = await requestsService.NoTrackingCargoFindAsync(request.Id);
        if (dbRequest == null)
        {
            return NotFound();
        }
        if (!requestsService.CanEditRequest(dbRequest))
        {
            return Forbid();
        }
        await requestsService.EditCargoRequestAsync(dbRequest, request);
        return RedirectToAction(nameof(AllCargoRequests), new { id = request.UserId });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCarRequestVisibility(int id, RequestType visibility)
    {
        var userId = userManager.GetUserId(User);
        if (userId == null) return Forbid();
        await requestsService.UpdateCarRequestVisibilityAsync(id, userId, User.IsInRole(CargoAppConstants.AdminRole), visibility);
        return RedirectToAction(nameof(EditCarRequest), new { id });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCargoRequestVisibility(int id, RequestType visibility)
    {
        var userId = userManager.GetUserId(User);
        if (userId == null) return Forbid();
        await requestsService.UpdateCargoRequestVisibilityAsync(id, userId, User.IsInRole(CargoAppConstants.AdminRole), visibility);
        return RedirectToAction(nameof(EditCargoRequest), new { id });
    }
}
