namespace CargoApp.Attributes;

public class CorrectDepartureTimeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var time = Convert.ToDateTime(value);
        return time > DateTime.Now.AddHours(BaseRequest.MinRequestTimeInHours) && time < DateTime.Now.AddHours(BaseRequest.MaxRequestTimeInHours) ?
            ValidationResult.Success :
            new ValidationResult("Departure time is not correct");
    }
}
