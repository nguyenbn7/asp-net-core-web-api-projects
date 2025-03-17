#nullable disable

namespace Ecommerce.Products.DTOs;

public class ProductView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Brand { get; set; }
}
