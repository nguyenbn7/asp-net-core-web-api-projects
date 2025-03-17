using Ecommerce.Orders.Models;
using Stripe;

namespace Ecommerce.Orders;

public interface IOrderService
{
    Task UpdateOrderStatus(PaymentIntent paymentIntent, OrderStatus status);
    Task<PaymentIntent> CreatePaymentIntentAsync(long amount);
    Task<PaymentIntent> UpdatePaymentIntentAsync(string paymentIntentId, long amount);
}
