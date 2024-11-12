namespace CargoApp.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Email")] [EmailAddress] public required string Email { get; set; }

    [Display(Name = "Password")] [DataType(DataType.Password)] [MinLength(6)] public required string Password { get; set; }

    [Display(Name = "Remember me")] public required bool RememberMe { get; set; }

    public LoginViewModel() { }
}
