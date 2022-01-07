using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class LoginModel
{
    [Phone]
    public string PhoneNumber { get; set; } = null!;

    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }

    public LoginModel() { }

    public LoginModel(string phoneNumber, string password)
    {
        PhoneNumber = phoneNumber;
        Password = password;
    }
}
