namespace CargoApp.ViewModels;

public class RegisterViewModel
{
    [Phone]
    public string PhoneNumber { get; set; } = null!;

    [MaxLength(64)]
    public string Name { get; set; } = null!;

    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = null!;

    public RegisterViewModel() { }

    public RegisterViewModel(string phoneNumber, string userName, string password, string confirmPassword)
    {
        PhoneNumber = phoneNumber;
        Name = userName;
        Password = password;
        ConfirmPassword = confirmPassword;
    }
}
