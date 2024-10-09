﻿namespace CargoApp.Models;

public class User : IdentityUser
{
    public UserInfo UserInfo { get; set; }

    public User()
    {
        UserInfo = new(Id);
    }

    public void SetInfo(string name)
    {
        UserInfo.Name = name;
        UserInfo.PhoneNumber = PhoneNumber;
    }

    public void UpdateUserInfo()
    {
        UserInfo.PhoneNumber = PhoneNumber;
    }
}