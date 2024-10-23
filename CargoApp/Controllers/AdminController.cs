using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Controllers;

[AutoValidateAntiforgeryToken]
[Authorize(Roles = CargoAppConstants.AdminRole)]
public class AdminController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly CargoAppContext db;

    public AdminController(UserManager<User> userManager, CargoAppContext context)
    {
        this.userManager = userManager;
        db = context;
    }

    public async Task<IActionResult> Requests()
    {
        var carRequests = await db.CarRequests
            .Include(s => s.User)
            .OrderBy(s => s.EarlyDepartureDate)
            .ThenBy(s => s.AddTime)
            .Select(s => new AdminCarRequestViewModel(s, s.User!.Name))
            .ToListAsync();
        var cargoRequests = await db.CargoRequests
            .Include(s => s.User)
            .OrderBy(s => s.DepartureTime)
            .Select(s => new AdminCargoRequestViewModel(s, s.User!.Name))
            .ToListAsync();

        var viewModel = new AdminRequestsViewModel
        {
            CarRequests = carRequests,
            CargoRequests = cargoRequests
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Users()
    {
        var users = await db.Users
            .Include(s => s.CarRequests)
            .Include(s => s.CargoRequests)
            .Include(s => s.CarResponses)
            .Include(s => s.CargoResponses)
            .Include(s => s.ReviewsReceived)
            .Select(s => new UserProfileAdminViewModel()
            {
                Id = s.Id,
                Email = s.Email!,
                Name = s.Name,
                Rating = s.ReviewsReceived.Count == 0 ? 0 : s.ReviewsReceived.Average(r => r.Points),
                ReviewsReceivedCount = s.ReviewsReceived.Count,
                ReviewsSentCount = s.ReviewsSent.Count,
                CarRequestsCount = s.CarRequests.Count,
                CargoRequestsCount = s.CargoRequests.Count,
                CarResponsesCount = s.CarResponses.Count,
                CargoResponsesCount = s.CargoResponses.Count
            }).ToListAsync();

        return View(users);
    }
}
