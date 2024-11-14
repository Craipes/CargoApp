using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CargoApp.Services;

public abstract class ServiceBase
{
    protected readonly IHttpContextAccessor _contextAccessor;
    protected readonly UserManager<User> _userManager;
    protected readonly CargoAppContext _context;
    protected readonly IStringLocalizer<AnnotationsSharedResource> _stringLocalizer;

    public ServiceBase(IHttpContextAccessor contextAccessor, UserManager<User> userManager, CargoAppContext context,
        IStringLocalizer<AnnotationsSharedResource> stringLocalizer)
    {
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _context = context;
        _stringLocalizer = stringLocalizer;
    }

    protected bool TryGetUserId(out string? userId)
    {
        var user = _contextAccessor.HttpContext?.User;
        if (user == null)
        {
            userId = null;
            return false;
        }
        userId = _userManager.GetUserId(user);
        return userId != null;
    }
}
