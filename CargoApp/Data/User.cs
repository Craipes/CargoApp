using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace CargoApp.Data;

public class User : IdentityUser
{
    public UserInfo UserInfo { get; set; }

    public User()
    {
        UserInfo = new(Id);
    }
}
