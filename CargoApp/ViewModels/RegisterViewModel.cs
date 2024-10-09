namespace CargoApp.ViewModels;

public class RegisterViewModel
{
    [EmailAddress]
    public required string Email { get; set; }

    [MaxLength(64)]
    public required string Name { get; set; }

    [DataType(DataType.Password)]
    [MinLength(6)]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }

    public RegisterViewModel() { }
}
