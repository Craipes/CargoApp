using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace CargoApp.Attributes;

public class CorrectDepartureTimeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;
        if (value is DateTime time)
        {
            if (time < DateTime.Now.AddHours(CargoAppConstants.MinRequestTimeInHours)) return GetLocalizedError("Too early", validationContext);
            if (time > DateTime.Now.AddHours(CargoAppConstants.MaxRequestTimeInHours)) return GetLocalizedError("Too late", validationContext);
            return ValidationResult.Success;
        }
        return GetLocalizedError("Invalid data format", validationContext);
    }

    protected ValidationResult GetLocalizedError(string error, ValidationContext validationContext)
    {
        var localizationService = validationContext.GetService<IStringLocalizer<AnnotationsSharedResource>>();
        return new ValidationResult(localizationService?[error].Value ?? error);
    }
}
