namespace Ecommerce.Orders.Models;

public class Order
{
    public int Id { get; set; }
    public required decimal SubTotal { get; set; }
    public required string PaymentIntentId { get; set; }
    public required string ClientSecret { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.PENDING;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public required BillingDetail BillingDetail { get; set; }
    public required List<OrderItem> OrderItems { get; set; }
    public OrderDeliveryMethod DeliveryMethod { get; set; } = null!;
    public decimal Total
    {
        get => SubTotal + DeliveryMethod.Price;
    }
}
