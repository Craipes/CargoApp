using System.Text.RegularExpressions;

namespace CargoApp.Attributes;

public class CorrectPhone : ValidationAttribute
{
    private const string pattern = @"\+380\d{9}";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var phone = Convert.ToString(value);
        return phone != null && Regex.IsMatch(phone, pattern) ?
            ValidationResult.Success :
            new ValidationResult("Phone number is not correct");
    }
}
