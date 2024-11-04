using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CargoApp.Services;

public class RequestsService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<User> _userManager;
    private readonly CargoAppContext _context;

    public RequestsService(IHttpContextAccessor contextAccessor, UserManager<User> userManager, CargoAppContext context)
    {
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _context = context;
    }

    public async Task<CarRequest?> NoTrackingCarDetailsAsync(int id)
    {
        return await _context.CarRequests
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Responses)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<CargoRequest?> NoTrackingCargoDetailsAsync(int id)
    {
        return await _context.CargoRequests
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Responses)
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
        updated.EarlyDepartureDate = updated.EarlyDepartureDate.Date;
        updated.LateDepartureDate = updated.LateDepartureDate.Date;

        _context.CarRequests.Update(updated);
        await _context.SaveChangesAsync();
    }

    public async Task EditCargoRequestAsync(CargoRequest initial, CargoRequest updated)
    {
        updated.UserId = initial.UserId;
        updated.DepartureTime = updated.DepartureTime.RoundToMinutes();

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

    public async Task<int> CarRequestsCountAsync(string userId)
    {
        return await _context.CarRequests.Where(r => r.UserId == userId).CountAsync();
    }

    public async Task<int> CargoRequestsCountAsync(string userId)
    {
        return await _context.CargoRequests.Where(r => r.UserId == userId).CountAsync();
    }

    public async Task<List<CarRequest>> PaginatedCarRequestsAsync(string userId, int page)
    {
        return await GetCarRequestsNoTrackingQuery(userId)
            .Skip((page - 1) * CargoAppConstants.RequestsPerPage)
            .Take(CargoAppConstants.RequestsPerPage)
            .ToListAsync();
    }

    public async Task<List<CargoRequest>> PaginatedCargoRequestsAsync(string userId, int page)
    {
        return await GetCargoRequestsNoTrackingQuery(userId)
            .Skip((page - 1) * CargoAppConstants.RequestsPerPage)
            .Take(CargoAppConstants.RequestsPerPage)
            .ToListAsync();
    }

    public async Task<List<CarRequest>> LatestCarRequestsAsync(string userId)
    {
        return await GetCarRequestsNoTrackingQuery(userId)
            .Take(CargoAppConstants.LatestRequestsMaxCount)
            .ToListAsync();
    }

    public async Task<List<CargoRequest>> LatestCargoRequestsAsync(string userId)
    {
        return await GetCargoRequestsNoTrackingQuery(userId)
            .Take(CargoAppConstants.LatestRequestsMaxCount)
            .ToListAsync();
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
            modelState.AddModelError("", "Volume or dimensions must be specified");
        }
    }

    public void ValidateVolumeAndDimensions(ModelStateDictionary modelState, CargoRequest request)
    {
        if (request.Car.MaxVolume == null && (request.Car.MaxLength == null || request.Car.MaxWidth == null || request.Car.MaxHeight == null))
        {
            modelState.AddModelError("", "Volume or dimensions must be specified");
        }
    }

    private IQueryable<CarRequest> GetCarRequestsNoTrackingQuery(string userId)
    {
        var minDateTime = DateTime.UtcNow.Date.AddHours(CargoAppConstants.MinResponseTimeInHours);
        return _context.CarRequests
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.LateDepartureDate >= minDateTime)
            .ThenBy(r => r.EarlyDepartureDate)
            .ThenBy(r => r.LateDepartureDate);
    }

    private IQueryable<CargoRequest> GetCargoRequestsNoTrackingQuery(string userId)
    {
        var minDateTime = DateTime.UtcNow.AddHours(CargoAppConstants.MinResponseTimeInHours);
        return _context.CargoRequests
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.DepartureTime >= minDateTime)
            .ThenBy(r => r.DepartureTime);
    }

    private bool TryGetUserId(out string? userId)
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
