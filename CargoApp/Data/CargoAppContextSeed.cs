using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Data;

public static class CargoAppContextSeed
{
    public static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        var defaultUser = new User() { PhoneNumber = "+380989973045", UserName = "Default user" };
        defaultUser.SetInfo(defaultUser.UserName);
        await userManager.CreateAsync(defaultUser, "password");
    }

    public static async Task RecreateSettlements(CargoAppContext context, string localitiesPath)
    {
        XmlDataExtractor extractor = new(localitiesPath);
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE SETTLEMENTS");
        context.Settlements.RemoveRange(context.Settlements);
        context.Settlements.AddRange(extractor.RunAndGet());
        //extractor.RunAndSave("D://UA_DB_SETTLEMENTS.xml");
        await context.SaveChangesAsync();
        Console.WriteLine($"Settlements count: {context.Settlements.Count()}");
    }
}
