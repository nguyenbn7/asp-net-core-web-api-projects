using System.Text.Json;
using Ecommerce.Orders.Models;
using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Orders;

public static class SeedDemoDataExtension
{
    public static async Task SeedOrderDeliveryMethodsAsync(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        var dbContext = services.GetApplicationDbContext(application.Environment);

        try
        {
            if (await dbContext.OrderDeliveryMethods.AnyAsync()) return;

            var deliveryMethodsData = await File.ReadAllTextAsync("Orders/DemoData/delivery.json");
            var deliveryMethods = JsonSerializer.Deserialize<List<OrderDeliveryMethod>>(deliveryMethodsData);

            if (deliveryMethods == null)
            {
                logger.LogError("Can not get seed data: Delivery Method");
                return;
            }

            dbContext.OrderDeliveryMethods.AddRange(deliveryMethods);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during seed delivery method data: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }
}
