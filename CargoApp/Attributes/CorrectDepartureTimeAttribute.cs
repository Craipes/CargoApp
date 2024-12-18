﻿using Microsoft.Extensions.Localization;

namespace CargoApp.Attributes;

public class CorrectDepartureTimeAttribute : ValidationAttribute
{
    private readonly double minHoursOffset;
    private readonly double maxHoursOffset;

    public CorrectDepartureTimeAttribute(double minHoursOffset, double maxHoursOffset = CargoAppConstants.MaxRequestTimeInHours)
    {
        this.minHoursOffset = minHoursOffset;
        this.maxHoursOffset = maxHoursOffset;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;
        if (value is DateTime time)
        {
            if (time < DateTime.UtcNow.AddHours(minHoursOffset)) return GetLocalizedError("Too early", validationContext);
            if (time > DateTime.UtcNow.AddHours(maxHoursOffset)) return GetLocalizedError("Too late", validationContext);
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
