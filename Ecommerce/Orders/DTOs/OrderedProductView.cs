#nullable disable

namespace Ecommerce.Orders.DTOs;

public class OrderedProductView
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductImageUrl { get; set; }
    public string ProductBrand { get; set; }
}
