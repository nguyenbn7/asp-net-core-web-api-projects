using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Extension;

public static class ApplicationDbContextConfiguration
{
    public static IServiceCollection ApplyDbContextSettings(
        this IServiceCollection services,
        IWebHostEnvironment environment,
        IConfiguration configuration)
    {
        if (environment.IsDevelopment())
        {
            services.AddDbContext<ApplicationDbContext, SqliteDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlite(configuration.GetConnectionString("sqlite"))
                              .EnableSensitiveDataLogging();
            });
        }
        else
        {
            services.AddDbContext<ApplicationDbContext, PostgreDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("postgre"));
            });
        }
        return services;
    }
}
