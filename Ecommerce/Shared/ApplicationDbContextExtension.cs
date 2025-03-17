using Ecommerce.Application;

namespace Ecommerce.Shared;

public static class ApplicationDbContextExtension
{
    public static ApplicationDbContext GetApplicationDbContext(
        this IServiceProvider serviceProvider,
        IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            return serviceProvider.GetRequiredService<SqliteDbContext>();

        return serviceProvider.GetRequiredService<PostgreDbContext>();
    }

    public static bool IsPostgreDatabase(this ApplicationDbContext dbContext)
    {
        return dbContext is PostgreDbContext;
    }
}
