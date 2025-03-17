namespace Ecommerce.Orders.Models;

public class OrderDeliveryMethod
{
    public int Id { get; set; }
    public required string ShortName { get; set; }
    public required string DeliveryTime { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
}
