namespace CargoApp.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "Email")][EmailAddress][Required(ErrorMessage = "Required Error")] public required string Email { get; set; }

    [Display(Name = "Name")][MaxLength(64)][Required(ErrorMessage = "Required Error")] public required string Name { get; set; }

    [Display(Name = "Password")][DataType(DataType.Password)][MinLength(6, ErrorMessage = "Min Length Error")][Required(ErrorMessage = "Required Error")] public required string Password { get; set; }

    [Display(Name = "Confirm password")][DataType(DataType.Password)][Compare("Password", ErrorMessage = "Compare Error")][Required(ErrorMessage = "Required Error")] public required string ConfirmPassword { get; set; }

    public RegisterViewModel() { }
}
