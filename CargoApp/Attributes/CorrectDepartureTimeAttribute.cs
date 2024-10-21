namespace CargoApp.Attributes;

public class CorrectDepartureTimeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;
        if (value is DateTime time)
        {
            return time > DateTime.Now.AddHours(BaseRequest.MinRequestTimeInHours) && time < DateTime.Now.AddHours(BaseRequest.MaxRequestTimeInHours) ?
                ValidationResult.Success :
                new ValidationResult("Departure time is not correct");
        }
        return new ValidationResult("Departure time is not of type DateTime");
    }
}
