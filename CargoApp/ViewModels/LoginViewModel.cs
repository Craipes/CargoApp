namespace CargoApp.ViewModels;

public class LoginViewModel
{
    [Phone]
    public string PhoneNumber { get; set; } = null!;

    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }

    public LoginViewModel() { }

    public LoginViewModel(string phoneNumber, string password)
    {
        PhoneNumber = phoneNumber;
        Password = password;
    }
}
