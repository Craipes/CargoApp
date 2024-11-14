using Azure.Core;
using CargoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Controllers;

[Authorize]
public class RequestsController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly IRequestsService requestsService;
    private readonly IResponsesService responsesService;

    public RequestsController(UserManager<User> userManager, IRequestsService requestsService, IResponsesService responsesService)
    {
        this.userManager = userManager;
        this.requestsService = requestsService;
        this.responsesService = responsesService;
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
        var viewModel = await GetCarRequestViewModel(id);
        if (viewModel == null)
        {
            return NotFound();
        }
        return View(viewModel);
    }

    [AllowAnonymous]
    public async Task<IActionResult> CargoRequestDetails(int id)
    {
        var viewModel = await GetCargoRequestViewModel(id);
        if (viewModel == null)
        {
            return NotFound();
        }
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
            return RedirectToAction("Error", "Home");
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
            return RedirectToAction("Error", "Home");
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

    [HttpPost]
    public async Task<IActionResult> CreateCarResponse(CarRequestViewModel request)
    {
        responsesService.ValidateVolumeAndDimensions(ModelState, request);
        if (!ModelState.IsValid)
        {
            var viewModel = await GetCarRequestViewModel(request.Response.CarRequestId);
            if (viewModel == null)
            {
                return NotFound();
            }
            viewModel.Response = request.Response;
            return View(nameof(CarRequestDetails), viewModel);
        }

        if (!await responsesService.CreateCarResponseAsync(request.Response))
        {
            return RedirectToAction("Error", "Home");
        }

        return RedirectToAction(nameof(CarRequestDetails), new { id = request.Response.CarRequestId });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCargoResponse(int requestId, int responseId)
    {
        var userId = userManager.GetUserId(User);
        if (userId == null) return Forbid();
        var response = await responsesService.NoTrackingCargoFindAsync(responseId);
        if (response == null) return NotFound();
        if (!responsesService.CanDeleteResponse(response)) return Forbid();

        await responsesService.DeleteCargoResponseAsync(response);
        return RedirectToAction(nameof(CargoRequestDetails), new { id = requestId });
    }

    [HttpPost]
    public async Task<IActionResult> CreateCargoResponse(CargoRequestViewModel request)
    {
        responsesService.ValidateVolumeAndDimensions(ModelState, request);
        if (!ModelState.IsValid)
        {
            var viewModel = await GetCargoRequestViewModel(request.Response.CargoRequestId);
            if (viewModel == null)
            {
                return NotFound();
            }
            viewModel.Response = request.Response;
            return View(nameof(CargoRequestDetails), viewModel);
        }

        if (!await responsesService.CreateCargoResponseAsync(request.Response))
        {
            return RedirectToAction("Error", "Home");
        }

        return RedirectToAction(nameof(CargoRequestDetails), new { id = request.Response.CargoRequestId });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCarResponse(int requestId, int responseId)
    {
        var userId = userManager.GetUserId(User);
        if (userId == null) return Forbid();
        var response = await responsesService.NoTrackingCarFindAsync(responseId);
        if (response == null) return NotFound();
        if (!responsesService.CanDeleteResponse(response)) return Forbid();

        await responsesService.DeleteCarResponseAsync(response);
        return RedirectToAction(nameof(CarRequestDetails), new { id = requestId });
    }

    private async Task<CarRequestViewModel?> GetCarRequestViewModel(int id)
    {
        var carRequest = await requestsService.NoTrackingCarDetailsAsync(id);

        if (carRequest == null)
        {
            return null;
        }

        string? userId = userManager.GetUserId(User);
        bool allowEditing = requestsService.CanEditRequest(carRequest);
        List<CarResponse> responses;
        if (allowEditing)
        {
            responses = carRequest.Responses;
        }
        else if (carRequest.Responses.Any(r => r.UserId == userId))
        {
            responses = carRequest.Responses.Where(r => r.UserId == userId).ToList();
        }
        else
        {
            responses = [];
        }
        return new CarRequestViewModel
        {
            CarRequest = carRequest,
            UserName = carRequest.User?.Name ?? "",
            Responses = responses,
            AllowEditing = allowEditing,
            AllowResponding = carRequest.CanBeResponded && carRequest.RequestType == RequestType.Visible && userId != null && carRequest.UserId != userId && !carRequest.Responses.Any(r => r.UserId == userId),
            UserId = userId
        };
    }

    private async Task<CargoRequestViewModel?> GetCargoRequestViewModel(int id)
    {
        var cargoRequest = await requestsService.NoTrackingCargoDetailsAsync(id);

        if (cargoRequest == null)
        {
            return null;
        }

        string? userId = userManager.GetUserId(User);
        bool allowEditing = requestsService.CanEditRequest(cargoRequest);
        List<CargoResponse> responses;
        if (allowEditing)
        {
            responses = cargoRequest.Responses;
        }
        else if (cargoRequest.Responses.Any(r => r.UserId == userId))
        {
            responses = cargoRequest.Responses.Where(r => r.UserId == userId).ToList();
        }
        else
        {
            responses = [];
        }
        return new CargoRequestViewModel
        {
            CargoRequest = cargoRequest,
            UserName = cargoRequest.User?.Name ?? "",
            Responses = responses,
            AllowEditing = allowEditing,
            AllowResponding = cargoRequest.CanBeResponded && cargoRequest.RequestType == RequestType.Visible && userId != null && cargoRequest.UserId != userId && !cargoRequest.Responses.Any(r => r.UserId == userId),
            UserId = userId
        };
    }
}
