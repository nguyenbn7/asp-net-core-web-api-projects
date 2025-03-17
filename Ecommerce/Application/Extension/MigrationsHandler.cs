using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Extension;

public static class MigrationsHandler
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<Program>>();
        var dbContext = services.GetApplicationDbContext(app.Environment);

        try
        {
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during migration: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }
}
