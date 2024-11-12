using Microsoft.Extensions.Localization;

namespace CargoApp.Attributes;

public class LaterThanAttribute : ValidationAttribute
{
    private readonly string comparePropertyName;

    public LaterThanAttribute(string comparePropertyName)
    {
        this.comparePropertyName = comparePropertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;
        if (value is DateTime time)
        {
            var compareValue = validationContext.ObjectInstance.GetType().GetProperty(comparePropertyName)?.GetValue(validationContext.ObjectInstance);
            if (compareValue != null && compareValue is DateTime compareTime)
            {
                return time >= compareTime ?
                    ValidationResult.Success :
                    GetLocalizedError("Should be later than early date", validationContext);
            }
        }
        return GetLocalizedError("Invalid data format", validationContext);
    }

    protected ValidationResult GetLocalizedError(string error, ValidationContext validationContext)
    {
        var localizationService = validationContext.GetService<IStringLocalizer<AnnotationsSharedResource>>();
        return new ValidationResult(localizationService?[error].Value ?? error);
    }
}
