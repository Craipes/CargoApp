using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace CargoApp.Services;

public class ResponsesService : ServiceBase, IResponsesService
{
    public ResponsesService(IHttpContextAccessor contextAccessor, UserManager<User> userManager, CargoAppContext context,
        IStringLocalizer<AnnotationsSharedResource> stringLocalizer) : base(contextAccessor, userManager, context, stringLocalizer)
    {
    }

    public async Task<CarResponse?> NoTrackingCarFindAsync(int id)
    {
        return await _context.CarResponses
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<bool> CreateCarResponseAsync(CarResponse response)
    {
        if (!TryGetUserId(out var userId)) return false;

        var request = await _context.CarRequests.Select(r => new { r.Id, r.UserId }).FirstOrDefaultAsync(r => r.Id == response.CarRequestId);
        if (request == null) return false;
        if (request.UserId == userId) return false;
        if (await _context.CarResponses.AnyAsync(r => r.CarRequestId == response.CarRequestId && r.UserId == userId)) return false;
        response.UserId = userId!;

        _context.CarResponses.Add(response);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteCarResponseAsync(CarResponse response)
    {
        _context.CarResponses.Remove(response);
        await _context.SaveChangesAsync();
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

    public async Task DeleteCargoResponseAsync(CargoResponse response)
    {
        _context.CargoResponses.Remove(response);
        await _context.SaveChangesAsync();
    }

    public void ValidateVolumeAndDimensions(ModelStateDictionary modelState, CarRequestViewModel request)
    {
        if (request.Response.Car.MaxVolume == null && (request.Response.Car.MaxLength == null || request.Response.Car.MaxWidth == null || request.Response.Car.MaxHeight == null))
        {
            modelState.AddModelError<CarRequestViewModel>(r => r.Response.Car.MaxVolume, _stringLocalizer["Volume Or Dimensions Error"]);
        }
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
