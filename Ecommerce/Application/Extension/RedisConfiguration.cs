using StackExchange.Redis;

namespace Ecommerce.Application.Extension;

public static class RedisConfiguration
{
    public static IServiceCollection ApplyRedisSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var connectionString = configuration.GetConnectionString("redis");

            if (string.IsNullOrEmpty(connectionString?.Trim()))
                throw new Exception("Can not find or empty redis connection string `redis`");

            var options = ConfigurationOptions.Parse(connectionString);
            return ConnectionMultiplexer.Connect(options);
        });

        return services;
    }
}
