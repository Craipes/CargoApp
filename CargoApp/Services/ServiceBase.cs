using Microsoft.AspNetCore.Identity;

namespace CargoApp.Services;

public abstract class ServiceBase
{
    protected readonly IHttpContextAccessor _contextAccessor;
    protected readonly UserManager<User> _userManager;

    public ServiceBase(IHttpContextAccessor contextAccessor, UserManager<User> userManager)
    {
        _contextAccessor = contextAccessor;
        _userManager = userManager;
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
