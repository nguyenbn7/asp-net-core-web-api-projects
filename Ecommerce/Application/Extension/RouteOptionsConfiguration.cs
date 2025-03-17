namespace Ecommerce.Application.Extension;

public static class RouteOptionsConfiguration
{
    public static IServiceCollection ApplyRouteOptionsSettings(this IServiceCollection services)
    {
        return services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
    }
}
