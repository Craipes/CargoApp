namespace ApplicationCore.Attributes;

public class CorrectDepartureTimeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var time = Convert.ToDateTime(value);
        return time > DateTime.Now && time < DateTime.Now.AddHours(Request.DefaultExpirationTimeInHours) ?
            ValidationResult.Success :
            new ValidationResult("Departure time is not correct");
    }
}
