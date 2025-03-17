namespace Ecommerce.Orders.Models;

public class OrderedProduct
{
    public OrderedProduct()
    {
    }

    public OrderedProduct(int id, string name, decimal price, string imageUrl, string brand)
    {
        Id = id;
        Name = name;
        Price = price;
        ImageUrl = imageUrl;
        Brand = brand;
    }

    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public required string Brand { get; set; }
}
