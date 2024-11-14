using CargoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly RequestsService requestsService;
    private readonly CargoAppContext db;
    private readonly ReviewsService reviewsService;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RequestsService requestsService, CargoAppContext db, ReviewsService reviewsService)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.requestsService = requestsService;
        this.db = db;
        this.reviewsService = reviewsService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            User user = new() { Email = model.Email, UserName = model.Email, Name = model.Name };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await signInManager.PasswordSignInAsync(user, model.Password, true, false);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Search", "Home");
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Search", "Home");
                }
            }
        }
        return View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Search", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Profile(string? id)
    {
        id ??= userManager.GetUserId(User);
        var currentId = userManager.GetUserId(User);
        if (id == null) return NotFound();

        var user = await db.Users
            .Include(s => s.CarRequests)
            .Include(s => s.CargoRequests)
            .Include(s => s.CarResponses)
            .Include(s => s.CargoResponses)
            .Include(s => s.ReviewsReceived)
                .ThenInclude(s => s.Sender)
            .Include(s => s.ReviewsSent)
            .Select(s => new UserProfileViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email!,
                Phone = s.PhoneNumber,
                Rating = s.ReviewsReceived.Count == 0 ? 0 : s.ReviewsReceived.Average(r => r.Points),
                ReviewsReceivedCount = s.ReviewsReceived.Count,
                ReviewsSentCount = s.ReviewsSent.Count,
                CarRequestsCount = s.CarRequests.Count,
                CargoRequestsCount = s.CargoRequests.Count,
                CarResponsesCount = s.CarResponses.Count,
                CargoResponsesCount = s.CargoResponses.Count
            })
            .FirstOrDefaultAsync(s => s.Id == id);

        if (user != null)
        {
            var carRequests = await requestsService.LatestCarRequestsAsync(id);
            var cargoRequests = await requestsService.LatestCargoRequestsAsync(id);
            user.CarRequests = carRequests;
            user.CargoRequests = cargoRequests;
            user.AllowEditing = id == currentId || User.IsInRole(CargoAppConstants.AdminRole);
            user.CanCreateReview = currentId != null && currentId != id;
            if (user.CanCreateReview)
            {
                user.WasReviewCreated = !await reviewsService.CanCreateReviewAsync(currentId!, id);
            }
            return View(user);
        }
        return RedirectToAction("Search", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile(UserProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.GetUserAsync(User);
            var updateUser = await userManager.FindByIdAsync(model.Id);
            if (user == null || updateUser == null)
            {
                return RedirectToAction(nameof(Profile));
            }
            if (user.Id != updateUser.Id && !User.IsInRole(CargoAppConstants.AdminRole))
            {
                return RedirectToAction(nameof(Profile));
            }

            updateUser.Name = model.Name;
            updateUser.PhoneNumber = model.Phone;
            await userManager.UpdateAsync(updateUser);
            return RedirectToAction(nameof(Profile), new { id = model.Id });
        }
        model.AllowEditing = true;
        return View(nameof(Profile), model);
    }

    [HttpGet]
    public async Task<IActionResult> ReceivedReviews(string id)
    {
        var user = await db.Users.FindAsync(id);
        if (user == null) return NotFound();

        var reviews = await reviewsService.NoTrackingReceivedReviewsAsync(id);

        ReviewsViewModel viewModel = new()
        {
            CurrentId = userManager.GetUserId(User),
            IsAdmin = User.IsInRole(CargoAppConstants.AdminRole),
            UserId = id,
            UserName = user.Name,
            Reviews = reviews
        };
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> SentReviews(string id)
    {
        var user = await db.Users.FindAsync(id);
        if (user == null) return NotFound();

        var reviews = await reviewsService.NoTrackingSentReviewsAsync(id);

        ReviewsViewModel viewModel = new()
        {
            CurrentId = userManager.GetUserId(User),
            IsAdmin = User.IsInRole(CargoAppConstants.AdminRole),
            UserId = id,
            UserName = user.Name,
            Reviews = reviews
        };
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> CreateReview(string id, CreateUpdateReviewViewModel? viewModel)
    {
        var userId = userManager.GetUserId(User);
        if (userId == null) return Forbid();
        if (id == userId) return RedirectToAction(nameof(Profile));
        var receiver = await db.Users.FindAsync(id);
        if (receiver == null) return NotFound();

        if (!await reviewsService.CanCreateReviewAsync(userId, id))
        {
            return RedirectToAction(nameof(Profile), new { id });
        }

        if (viewModel == null || viewModel.ReceiverId == null)
        {
            viewModel = new(new Review() { ReceiverId = id, SenderId = userId }, id, receiver.Name);
        }

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview(CreateUpdateReviewViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Forbid();
            viewModel.Review.ReceiverId = viewModel.ReceiverId;
            if (await reviewsService.CreateReviewAsync(viewModel.Review))
            {
                return RedirectToAction(nameof(Profile), new { id = viewModel.ReceiverId });
            }
        }
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteReview(string senderId, string receiverId, string? returnUrl)
    {
        var userId = userManager.GetUserId(User);
        if (userId == null) return Forbid();
        if (senderId != userId && !User.IsInRole(CargoAppConstants.AdminRole)) return Forbid();
        await reviewsService.DeleteReviewAsync(senderId, receiverId);

        if (returnUrl != null && Url.IsLocalUrl(returnUrl)) return LocalRedirect(returnUrl);
        return RedirectToAction(nameof(ReceivedReviews), new { id = receiverId });
    }
}