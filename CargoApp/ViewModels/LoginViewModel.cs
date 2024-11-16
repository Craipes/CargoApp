namespace CargoApp.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Email")][EmailAddress][Required(ErrorMessage = "Required Error")] public required string Email { get; set; }

    [Display(Name = "Password")][Required(ErrorMessage = "Required Error")][DataType(DataType.Password)] [MinLength(6, ErrorMessage = "Min Length Error")] public required string Password { get; set; }

    [Display(Name = "Remember me")] public required bool RememberMe { get; set; }

    public LoginViewModel() { }
}
