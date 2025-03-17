namespace Ecommerce.Application.Extension;

public static class KestrelCustomization
{
    public static IWebHostBuilder CustomizeKestrelSettings(this IWebHostBuilder webHostBuilder)
    {
        return webHostBuilder.ConfigureKestrel(options =>
        {
            options.AddServerHeader = false;
        });
    }
}
