using Ecommerce.Orders.Models;
using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Ecommerce.Orders;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext dbContext;
    private readonly PaymentIntentService paymentIntentService;
    public OrderService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        this.dbContext = dbContext;
        StripeConfiguration.ApiKey = configuration.GetValue<string>("Stripe:Api key")
                                     ?? throw new Exception("Can not read Stripe 'Api key'");
        paymentIntentService = new();
    }

    public Task<PaymentIntent> CreatePaymentIntentAsync(long amount)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = amount,
            Currency = "usd",
            PaymentMethodTypes = ["card"],
        };
        return paymentIntentService.CreateAsync(options);
    }

    public async Task UpdateOrderStatus(PaymentIntent paymentIntent, OrderStatus status)
    {
        var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.PaymentIntentId == paymentIntent.Id && o.ClientSecret == paymentIntent.ClientSecret);

        if (order is null)
            return;

        order.Status = status;
        dbContext.Update(order);

        await dbContext.SaveChangesAsync();
    }

    public Task<PaymentIntent> UpdatePaymentIntentAsync(string paymentIntentId, long amount)
    {
        var options = new PaymentIntentUpdateOptions
        {
            Amount = amount,
        };
        return paymentIntentService.UpdateAsync(paymentIntentId, options);
    }
}
