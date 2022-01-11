using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Data;

public static class UserManagerExtensions
{
    public async static Task<User?> FindByPhoneAsync(this UserManager<User> manager,
        string phoneNumber, CancellationToken cancellation = default)
    {
        return await manager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellation);
    }
}
