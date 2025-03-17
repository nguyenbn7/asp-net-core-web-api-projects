using Ecommerce.Carts;
using Ecommerce.Orders;
using Ecommerce.Users;

namespace Ecommerce.Application.Extension;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
