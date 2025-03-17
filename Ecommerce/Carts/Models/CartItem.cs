namespace Ecommerce.Carts.Models;

public class CartItem
{
    public required int ProductId { get; set; }
    public required string ProductName { get; set; }
    public required decimal Price { get; set; }
    public required int Quantity { get; set; }
    public required string ProductImageUrl { get; set; }
    public required string Brand { get; set; }
}
