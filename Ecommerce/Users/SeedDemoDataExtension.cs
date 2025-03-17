using Ecommerce.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Users;

public static class SeedDemoDataExtension
{
    public static async Task CreateDemoCustomerAsync(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<Program>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

        try
        {
            var customer = await userManager.FindByEmailAsync("demo.customer@test.com");
            if (customer != null)
                return;

            var demoCustomerPassword = "Pa$$w0rd";

            customer = new ApplicationUser
            {
                FullName = "Demo Customer",
                DisplayName = "Demo Customer",
                Email = "demo.customer@test.com",
                UserName = "demo.customer@test.com"
            };

            await userManager.CreateAsync(customer, demoCustomerPassword);

            await userManager.AddToRoleAsync(customer, "Customer");
            logger.LogInformation("Create {} succesffully", customer.FullName);

        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during create demo users: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }

    public static async Task CreateDemoCustomer1Async(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<Program>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

        try
        {
            var customer1 = await userManager.FindByEmailAsync("demo.customer1@test.com");
            if (customer1 != null)
                return;

            var demoCustomer1Password = "P@ssw0rd";

            customer1 = new ApplicationUser
            {
                FullName = "Demo Customer1",
                DisplayName = "Demo Customer1",
                Email = "demo.customer1@test.com",
                UserName = "demo.customer1@test.com"
            };


            await userManager.CreateAsync(customer1, demoCustomer1Password);

            await userManager.AddToRoleAsync(customer1, "Customer");

            logger.LogInformation("Create {} succesffully", customer1.FullName);

        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during create demo users: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }
}
