using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace CargoApp.Services;

public class RequestsService : ServiceBase
{
    private readonly CargoAppContext _context;
    private readonly IStringLocalizer<AnnotationsSharedResource> _stringLocalizer;

    public RequestsService(IHttpContextAccessor contextAccessor, UserManager<User> userManager, CargoAppContext context,
        IStringLocalizer<AnnotationsSharedResource> stringLocalizer) : base(contextAccessor, userManager)
    {
        _context = context;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<CarRequest?> NoTrackingCarDetailsAsync(int id)
    {
        return await _context.CarRequests
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Responses)
            .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<CargoRequest?> NoTrackingCargoDetailsAsync(int id)
    {
        return await _context.CargoRequests
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Responses)
            .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<bool> CreateCarRequestAsync(CarRequest request)
    {
        if (!TryGetUserId(out var userId)) return false;
        request.UserId = userId!;
        request.EarlyDepartureDate = request.EarlyDepartureDate.Date;
        request.LateDepartureDate = request.LateDepartureDate.Date;

        _context.CarRequests.Add(request);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CreateCargoRequestAsync(CargoRequest request)
    {
        if (!TryGetUserId(out var userId)) return false;
        request.UserId = userId!;
        request.DepartureTime = request.DepartureTime.RoundToMinutes();

        _context.CargoRequests.Add(request);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task EditCarRequestAsync(CarRequest initial, CarRequest updated)
    {
        updated.UserId = initial.UserId;
        updated.EarlyDepartureDate = initial.EarlyDepartureDate;
        updated.LateDepartureDate = initial.LateDepartureDate;

        _context.CarRequests.Update(updated);
        await _context.SaveChangesAsync();
    }

    public async Task EditCargoRequestAsync(CargoRequest initial, CargoRequest updated)
    {
        updated.UserId = initial.UserId;
        updated.DepartureTime = initial.DepartureTime;

        _context.CargoRequests.Update(updated);
        await _context.SaveChangesAsync();
    }

    public async Task<CarRequest?> NoTrackingCarFindAsync(int id)
    {
        return await _context.CarRequests.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<CargoRequest?> NoTrackingCargoFindAsync(int id)
    {
        return await _context.CargoRequests.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<int> CarRequestsCountAsync(bool includeHidden, string? userId = null)
    {
        if (userId == null)
        {
            if (includeHidden) return await _context.CarRequests.CountAsync();
            return await _context.CarRequests.Where(r => r.RequestType < CargoAppConstants.REQUEST_TYPE_MAX_VISIBLE).CountAsync();
        }
        if (includeHidden) return await _context.CarRequests.Where(r => r.UserId == userId).CountAsync();
        return await _context.CarRequests.Where(r => r.UserId == userId && r.RequestType < CargoAppConstants.REQUEST_TYPE_MAX_VISIBLE).CountAsync();
    }

    public async Task<int> CargoRequestsCountAsync(bool includeHidden, string? userId = null)
    {
        if (userId == null)
        {
            if (includeHidden) return await _context.CargoRequests.CountAsync();
            return await _context.CargoRequests.Where(r => r.RequestType < CargoAppConstants.REQUEST_TYPE_MAX_VISIBLE).CountAsync();
        }
        if (includeHidden) return await _context.CargoRequests.Where(r => r.UserId == userId).CountAsync();
        return await _context.CargoRequests.Where(r => r.UserId == userId && r.RequestType < CargoAppConstants.REQUEST_TYPE_MAX_VISIBLE).CountAsync();
    }

    public async Task<List<CarRequest>> PaginatedCarRequestsAsync(int page, bool includeHidden, string? userId = null)
    {
        if (includeHidden) return await GetCarRequestsNoTrackingQuery(userId)
            .Skip((page - 1) * CargoAppConstants.RequestsPerPage)
            .Take(CargoAppConstants.RequestsPerPage)
            .ToListAsync();
        return await GetCarRequestsNoTrackingQuery(userId)
            .Where(r => r.RequestType < CargoAppConstants.REQUEST_TYPE_MAX_VISIBLE)
            .Skip((page - 1) * CargoAppConstants.RequestsPerPage)
            .Take(CargoAppConstants.RequestsPerPage)
            .ToListAsync();
    }

    public async Task<List<CargoRequest>> PaginatedCargoRequestsAsync(int page, bool includeHidden, string? userId = null)
    {
        if (includeHidden) return await GetCargoRequestsNoTrackingQuery(userId)
            .Skip((page - 1) * CargoAppConstants.RequestsPerPage)
            .Take(CargoAppConstants.RequestsPerPage)
            .ToListAsync();
        return await GetCargoRequestsNoTrackingQuery(userId)
            .Where(r => r.RequestType < CargoAppConstants.REQUEST_TYPE_MAX_VISIBLE)
            .Skip((page - 1) * CargoAppConstants.RequestsPerPage)
            .Take(CargoAppConstants.RequestsPerPage)
            .ToListAsync();
    }

    public async Task<List<CarRequest>> LatestCarRequestsAsync(string? userId = null)
    {
        return await GetCarRequestsNoTrackingQuery(userId)
            .Where(r => r.LateDepartureDate >= DateTime.UtcNow.AddDays(-CargoAppConstants.LatestRequestsMaxDays))
            .Take(CargoAppConstants.LatestRequestsMaxCount)
            .ToListAsync();
    }

    public async Task<List<CargoRequest>> LatestCargoRequestsAsync(string? userId = null)
    {
        return await GetCargoRequestsNoTrackingQuery(userId)
            .Where(r => r.DepartureTime >= DateTime.UtcNow.AddDays(-CargoAppConstants.LatestRequestsMaxDays))
            .Take(CargoAppConstants.LatestRequestsMaxCount)
            .ToListAsync();
    }

    public async Task<bool> UpdateCarRequestVisibilityAsync(int id, string userId, bool isAdmin, RequestType requestType)
    {
        var request = await _context.CarRequests.FindAsync(id);
        return await UpdateRequestVisibilityAsync(request, userId, isAdmin, requestType);
    }

    public async Task<bool> UpdateCargoRequestVisibilityAsync(int id, string userId, bool isAdmin, RequestType requestType)
    {
        var request = await _context.CargoRequests.FindAsync(id);
        return await UpdateRequestVisibilityAsync(request, userId, isAdmin, requestType);
    }

    private async Task<bool> UpdateRequestVisibilityAsync(BaseRequest? request, string userId, bool isAdmin, RequestType requestType)
    {
        if (request == null) return false;
        if (request.UserId != userId && !isAdmin) return false;
        if (request.RequestType == RequestType.HiddenByAdmin && !isAdmin) return false;
        request.RequestType = requestType;
        await _context.SaveChangesAsync();
        return true;
    }

    public bool CanEditRequest(BaseRequest request)
    {
        var user = _contextAccessor.HttpContext?.User;
        if (user == null) return false;
        return request.UserId == _userManager.GetUserId(user) || user.IsInRole(CargoAppConstants.AdminRole);
    }

    public void ValidateVolumeAndDimensions(ModelStateDictionary modelState, CarRequest request)
    {
        if (request.Cargo.Volume == null && (request.Cargo.Length == null || request.Cargo.Width == null || request.Cargo.Height == null))
        {
            modelState.AddModelError<CarRequest>(r => r.Cargo.Volume, _stringLocalizer["Volume Or Dimensions Error"]);
        }
    }

    public void ValidateVolumeAndDimensions(ModelStateDictionary modelState, CargoRequest request)
    {
        if (request.Car.MaxVolume == null && (request.Car.MaxLength == null || request.Car.MaxWidth == null || request.Car.MaxHeight == null))
        {
            modelState.AddModelError<CargoRequest>(r => r.Car.MaxVolume, _stringLocalizer["Volume Or Dimensions Error"]);
        }
    }

    private IQueryable<CarRequest> GetCarRequestsNoTrackingQuery(string? userId)
    {
        var minDateTime = DateTime.UtcNow.Date.AddHours(CargoAppConstants.MinResponseTimeInHours);
        IQueryable<CarRequest> query = _context.CarRequests
            .AsNoTracking()
            .OrderByDescending(r => r.LateDepartureDate >= minDateTime)
            .ThenBy(r => r.EarlyDepartureDate)
            .ThenBy(r => r.LateDepartureDate);
        if (userId != null) query = query.Where(r => r.UserId == userId);
        return query;
    }

    private IQueryable<CargoRequest> GetCargoRequestsNoTrackingQuery(string? userId)
    {
        var minDateTime = DateTime.UtcNow.AddHours(CargoAppConstants.MinResponseTimeInHours);
        IQueryable<CargoRequest> query = _context.CargoRequests
            .AsNoTracking()
            .OrderByDescending(r => r.DepartureTime >= minDateTime)
            .ThenBy(r => r.DepartureTime);
        if (userId != null) query = query.Where(r => r.UserId == userId);
        return query;
    }
}
