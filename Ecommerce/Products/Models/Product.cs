namespace Ecommerce.Products.Models;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public required string ImageUrl { get; set; }

    public ProductBrand ProductBrand { get; set; } = null!;
    public int ProductBrandId { get; set; }
}
