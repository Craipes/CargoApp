using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly CargoAppContext db;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, CargoAppContext cargoAppContext)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        db = cargoAppContext;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            User user = new() { PhoneNumber = model.PhoneNumber, UserName = model.Name };
            user.SetInfo(model.Name);
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await signInManager.PasswordSignInAsync(user, model.Password, true, false);
                return RedirectToAction("Index", "Home");
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
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByPhoneAsync(model.PhoneNumber);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
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
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Requests()
    {
        var userId = userManager.GetUserId(User);
        if (userId != null)
        {
            var user = await db.Users
                .Include(s => s.UserInfo.CarRequests)
                    .ThenInclude(s => s.DeparturePlace)
                .Include(s => s.UserInfo.CarRequests)
                    .ThenInclude(s => s.DestinationPlace)
                .Include(s => s.UserInfo.CargoRequests)
                    .ThenInclude(s => s.DeparturePlace)
                .Include(s => s.UserInfo.CargoRequests)
                    .ThenInclude(s => s.DestinationPlace)
                .Select(s => new
                {
                    s.Id,
                    s.UserInfo.CarRequests,
                    s.UserInfo.CargoRequests
                })
                .FirstOrDefaultAsync(s => s.Id == userId);

            if (user != null)
            {
                var carRequests = user.CarRequests.Select(r => CarRequestModel.FromRequest(r)).OrderByDescending(r => r.AddTime).ThenBy(r => r.IsExpired);
                var cargoRequests = user.CargoRequests.Select(r => CargoRequestModel.FromRequest(r)).OrderByDescending(r => r.AddTime).ThenBy(r => r.IsExpired);
                var model = new RequestsViewModel(carRequests, cargoRequests);

                return View(model);
            }
        }
        return RedirectToAction("Index", "Home");
    }
}