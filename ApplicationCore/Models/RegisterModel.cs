using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class RegisterModel
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

    public RegisterModel() { }

    public RegisterModel(string phoneNumber, string userName, string password, string confirmPassword)
    {
        PhoneNumber = phoneNumber;
        Name = userName;
        Password = password;
        ConfirmPassword = confirmPassword;
    }
}
