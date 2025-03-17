namespace Ecommerce.Orders.Models;

public class OrderItem
{
    public OrderItem()
    {

    }

    public OrderItem(int quantity, OrderedProduct orderedProduct)
    {
        Quantity = quantity;
        OrderedProduct = orderedProduct;
    }

    public int Id { get; set; }
    public int Quantity { get; set; }
    public required OrderedProduct OrderedProduct { get; set; }
    public Order Order { get; set; } = null!;
}
