namespace Ecommerce.Application.Extension;

public static class CorsConfiguration
{
    public static IServiceCollection ApplyCorsSettings(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {

        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(policy =>
            {
                if (environment.IsDevelopment())
                {
                    policy.WithOrigins("*")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
                    return;
                }

                var origins = configuration.GetSection("ORIGINS").Get<string[]>() ?? throw new Exception("ORIGINS can not be null");

                policy.WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        return services;
    }
}