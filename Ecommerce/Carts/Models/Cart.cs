namespace Ecommerce.Carts.Models;

public class Cart
{
    public required string Id { get; set; }
    public required List<CartItem> Items { get; set; }
}
