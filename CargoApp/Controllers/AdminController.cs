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

    public IActionResult Requests()
    {
        return View();
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
