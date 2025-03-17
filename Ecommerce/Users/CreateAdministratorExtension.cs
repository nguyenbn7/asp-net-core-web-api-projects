using Ecommerce.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Users;

public static class CreateAdministratorExtension
{
    public static async Task CreateAdminUserAsync(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<Program>>();
        var configuration = services.GetRequiredService<IConfiguration>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

        try
        {
            var admin = await userManager.FindByNameAsync("admin");
            if (admin != null) return;

            var adminPassword = configuration.GetValue<string>("Password:Admin") ?? throw new Exception("Password:Admin is not set");

            admin = new ApplicationUser
            {
                FullName = "Administrator",
                UserName = "admin",
                DisplayName = "Admin"
            };

            await userManager.CreateAsync(admin, adminPassword);
            await userManager.AddToRoleAsync(admin, "Administrator");
        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during create system admin: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }
}
