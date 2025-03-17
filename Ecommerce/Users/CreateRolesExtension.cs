
using Ecommerce.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Users;

public static class CreateRolesExtension
{
    public static async Task CreateRolesAsync(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<Program>>();
        var configuration = services.GetRequiredService<IConfiguration>();
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

        if ((await roleManager.FindByNameAsync("Administrator")) == null)
        {
            await roleManager.CreateAsync(new ApplicationRole
            {
                Name = "Administrator"
            });
        }

        if ((await roleManager.FindByNameAsync("Customer")) == null)
        {
            await roleManager.CreateAsync(new ApplicationRole
            {
                Name = "Customer"
            });
        }
    }
}
