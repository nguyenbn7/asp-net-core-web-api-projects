#nullable disable

namespace Ecommerce.Orders.DTOs;

public class DeliveryMethodView
{
    public string ShortName { get; set; }
    public string DeliveryTime { get; set; }
    public decimal Price { get; set; }
}
