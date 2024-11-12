namespace CargoApp.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "Email")][EmailAddress] public required string Email { get; set; }

    [Display(Name = "Name")][MaxLength(64)] public required string Name { get; set; }

    [Display(Name = "Password")][DataType(DataType.Password)][MinLength(6)] public required string Password { get; set; }

    [Display(Name = "Confirm password")][DataType(DataType.Password)][Compare("Password")] public required string ConfirmPassword { get; set; }

    public RegisterViewModel() { }
}
