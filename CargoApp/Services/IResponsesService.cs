using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CargoApp.Services;

public interface IResponsesService
{
    public Task<CarResponse?> NoTrackingCarFindAsync(int id);

    public Task<bool> CreateCarResponseAsync(CarResponse response);

    public Task DeleteCarResponseAsync(CarResponse response);

    public Task<CargoResponse?> NoTrackingCargoFindAsync(int id);

    public Task<bool> CreateCargoResponseAsync(CargoResponse response);

    public Task DeleteCargoResponseAsync(CargoResponse response);

    public void ValidateVolumeAndDimensions(ModelStateDictionary modelState, CarRequestViewModel request);

    public void ValidateVolumeAndDimensions(ModelStateDictionary modelState, CargoRequestViewModel request);

    public bool CanDeleteResponse(BaseResponse response);
}
