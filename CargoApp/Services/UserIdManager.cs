namespace CargoApp.Services;

public class UserIdManager
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly UserManager<User> userManager;

    public UserIdManager(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.userManager = userManager;
    }

    public string GetOrCreateAnonymousId()
    {
        HttpContext context = GetContext();
        if (!context.Request.Cookies.TryGetValue(CargoAppConstants.AnonymousIdCookie, out string? id) || id == null)
        {
            string newId = Guid.NewGuid().ToString();
            context.Response.Cookies.Append(CargoAppConstants.AnonymousIdCookie, newId, new CookieOptions()
            {
                Expires = DateTime.Today.AddYears(10)
            });
            return newId;
        }
        return id;
    }

    public string GetUserId()
    {
        var user = GetContext().User;
        if (user.Identity != null && user.Identity.IsAuthenticated) return userManager.GetUserId(user);
        return GetOrCreateAnonymousId();
    }

    public bool IsAnonymous()
    {
        HttpRequest request = GetContext().Request;
        return request.Cookies[CargoAppConstants.AnonymousIdCookie] != null;
    }

    public bool TryGetAnonymousId(out string? anonymousId)
    {
        HttpRequest request = GetContext().Request;
        anonymousId = request.Cookies[CargoAppConstants.AnonymousIdCookie];
        return anonymousId != null;
    }

    public void DeleteAnonymous()
    {
        HttpResponse response = GetContext().Response;
        response.Cookies.Delete(CargoAppConstants.AnonymousIdCookie);
    }

    private HttpContext GetContext()
    {
        var context = httpContextAccessor.HttpContext;
        if (context != null)
            return context;
        throw new Exception("No http context found!");
    }
}
