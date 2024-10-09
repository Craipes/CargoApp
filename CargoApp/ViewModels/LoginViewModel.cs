namespace CargoApp.ViewModels;

public class LoginViewModel
{
    [EmailAddress]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    [MinLength(6)]
    public required string Password { get; set; }

    public required bool RememberMe { get; set; }

    public LoginViewModel() { }
}
