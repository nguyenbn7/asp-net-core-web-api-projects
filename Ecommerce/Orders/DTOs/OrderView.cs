#nullable disable

namespace Ecommerce.Orders.DTOs;

public class OrderView
{
    public int Id { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public DeliveryMethodView DeliveryMethod { get; set; }
    public List<OrderedProductView> Items { get; set; }
    public BillingDetailView BillingDetail { get; set; }
}
