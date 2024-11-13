using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace CargoApp.Services;

public class ResponsesService : ServiceBase
{
    private readonly CargoAppContext _context;
    private readonly IStringLocalizer<AnnotationsSharedResource> _stringLocalizer;

    public ResponsesService(IHttpContextAccessor contextAccessor, UserManager<User> userManager, CargoAppContext context,
        IStringLocalizer<AnnotationsSharedResource> stringLocalizer) : base(contextAccessor, userManager)
    {
        _context = context;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<CargoResponse?> NoTrackingCargoFindAsync(int id)
    {
        return await _context.CargoResponses
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<bool> CreateCargoResponseAsync(CargoResponse response)
    {
        if (!TryGetUserId(out var userId)) return false;

        var request = await _context.CargoRequests.Select(r => new { r.Id, r.UserId }).FirstOrDefaultAsync(r => r.Id == response.CargoRequestId);
        if (request == null) return false;
        if (request.UserId == userId) return false;
        if (await _context.CargoResponses.AnyAsync(r => r.CargoRequestId == response.CargoRequestId && r.UserId == userId)) return false;
        response.UserId = userId!;

        _context.CargoResponses.Add(response);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCargoResponseAsync(CargoResponse response)
    {
        _context.CargoResponses.Remove(response);
        await _context.SaveChangesAsync();
        return true;
    }

    public void ValidateVolumeAndDimensions(ModelStateDictionary modelState, CargoRequestViewModel request)
    {
        if (request.Response.Cargo.Volume == null && (request.Response.Cargo.Length == null || request.Response.Cargo.Width == null || request.Response.Cargo.Height == null))
        {
            modelState.AddModelError<CargoRequestViewModel>(r => r.Response.Cargo.Volume, _stringLocalizer["Volume Or Dimensions Error"]);
        }
    }

    public bool CanDeleteResponse(BaseResponse response)
    {
        var user = _contextAccessor.HttpContext?.User;
        if (user == null) return false;
        return response.UserId == _userManager.GetUserId(user) || user.IsInRole(CargoAppConstants.AdminRole);
    }
}
