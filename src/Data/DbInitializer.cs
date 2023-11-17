using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data;

public static class DbInitializer
{
    public static async Task Initialize(AppDbContext context, UserManager<User> userManager)
    {
        if(!userManager.Users.Any())
        {
            var user = new User {
                UserName = "Bruno",
                Email = "bruno@manutd.com",
                DisplayName = "Bruno"
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Member");

            var user2 = new User {
                UserName = "Rasmus",
                Email = "rasmus@manutd.com",
                DisplayName = "Rasmus"
            };

            await userManager.CreateAsync(user2, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user2, "Member");

            var admin = new User {
                UserName = "Erik",
                Email = "erik@manutd.com",
                DisplayName = "Erik"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new [] {"Admin", "Member"});
        }
    }
}
