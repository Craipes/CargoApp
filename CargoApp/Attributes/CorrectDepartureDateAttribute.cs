using Microsoft.Extensions.Localization;

namespace CargoApp.Attributes;

public class CorrectDepartureDateAttribute : ValidationAttribute
{
    private readonly double maxHoursOffset;

    public CorrectDepartureDateAttribute(double maxHoursOffset = CargoAppConstants.MaxRequestTimeInHours)
    {
        this.maxHoursOffset = maxHoursOffset;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;
        if (value is DateTime time)
        {
            if (time < DateTime.UtcNow.Date) return GetLocalizedError("Too early", validationContext);
            if (time > DateTime.UtcNow.AddHours(maxHoursOffset).Date) return GetLocalizedError("Too late", validationContext);
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
