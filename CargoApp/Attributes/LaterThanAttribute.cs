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
                    new ValidationResult("Should be later than " + comparePropertyName);
            }
            return new ValidationResult("Compare value is not of type DateTime");
        }
        return new ValidationResult("Value is not of type DateTime");
    }
}
