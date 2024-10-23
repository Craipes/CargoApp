using CargoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly CargoAppContext db;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, CargoAppContext db)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.db = db;
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
        var user = await db.Users
            .Include(s => s.ReviewsReceived)
                .ThenInclude(s => s.Sender)
            .Include(s => s.ReviewsSent)
            .Select(s => new
            {
                s.Id,
                s.Name,
                s.Email,
                s.PhoneNumber,
                Rating = s.ReviewsReceived.Count == 0 ? 0 : s.ReviewsReceived.Average(r => r.Points),
                ReceivedCount = s.ReviewsReceived.Count,
                SentCount = s.ReviewsSent.Count,
                Received = s.ReviewsReceived.Select(r => new ReviewViewModel(r.Sender.Name, r.SenderId, r.Points, r.Content))
            })
            .FirstOrDefaultAsync(s => s.Id == id);

        if (user != null)
        {
            return View(new UserProfileViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email ?? string.Empty,
                Phone = user.PhoneNumber,
                Rating = user.Rating,
                ReviewsReceivedCount = user.ReceivedCount,
                ReviewsSentCount = user.SentCount,
                ReviewsReceived = user.Received,
                AllowEditing = id == currentId || User.IsInRole(CargoAppConstants.AdminRole)
            });
        }
        return RedirectToAction("Home", "Search");
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
                return RedirectToAction("Profile");
            }
            if (user.Id != updateUser.Id && !User.IsInRole(CargoAppConstants.AdminRole))
            {
                return RedirectToAction("Profile");
            }

            updateUser.Name = model.Name;
            updateUser.PhoneNumber = model.Phone;
            await db.SaveChangesAsync();
            return RedirectToAction("Profile", new { id = model.Id });
        }
        model.AllowEditing = true;
        return View(nameof(Profile), model);
    }
}