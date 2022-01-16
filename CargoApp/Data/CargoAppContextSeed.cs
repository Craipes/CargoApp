using Microsoft.AspNetCore.Identity;
using StreetRegister;

namespace CargoApp.Data;

public static class CargoAppContextSeed
{
    public static async Task SeedAsync(UserManager<User> userManager, CargoAppContext context,
        bool seedLocalities = false, string? localitiesPath = null)
    {
        var defaultUser = new User() { PhoneNumber = "+380989973045", UserName = "Default user" };
        defaultUser.SetInfo(defaultUser.UserName);
        await userManager.CreateAsync(defaultUser, "password");

        if (seedLocalities && localitiesPath != null)
        {
            XmlDataExtractor extractor = new(localitiesPath);
            context.Localities.AddRange(extractor.RunAndGet());
            await context.SaveChangesAsync();
        }
    }
}
