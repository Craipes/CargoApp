using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CargoApp.Services;

public interface IRequestsService
{
    public Task<CarRequest?> NoTrackingCarDetailsAsync(int id);

    public Task<CargoRequest?> NoTrackingCargoDetailsAsync(int id);

    public Task<bool> CreateCarRequestAsync(CarRequest request);

    public Task<bool> CreateCargoRequestAsync(CargoRequest request);

    public Task EditCarRequestAsync(CarRequest initial, CarRequest updated);

    public Task EditCargoRequestAsync(CargoRequest initial, CargoRequest updated);

    public Task<CarRequest?> NoTrackingCarFindAsync(int id);

    public Task<CargoRequest?> NoTrackingCargoFindAsync(int id);

    public Task<int> CarRequestsCountAsync(bool includeHidden, string? userId = null);

    public Task<int> CargoRequestsCountAsync(bool includeHidden, string? userId = null);

    public Task<List<CarRequest>> PaginatedCarRequestsAsync(int page, bool includeHidden, string? userId = null);

    public Task<List<CargoRequest>> PaginatedCargoRequestsAsync(int page, bool includeHidden, string? userId = null);

    public Task<List<CarRequest>> LatestCarRequestsAsync(string? userId = null);

    public Task<List<CargoRequest>> LatestCargoRequestsAsync(string? userId = null);

    public Task<bool> UpdateCarRequestVisibilityAsync(int id, string userId, bool isAdmin, RequestType requestType);

    public Task<bool> UpdateCargoRequestVisibilityAsync(int id, string userId, bool isAdmin, RequestType requestType);

    public bool CanEditRequest(BaseRequest request);

    public void ValidateVolumeAndDimensions(ModelStateDictionary modelState, CarRequest request);

    public void ValidateVolumeAndDimensions(ModelStateDictionary modelState, CargoRequest request);
}
