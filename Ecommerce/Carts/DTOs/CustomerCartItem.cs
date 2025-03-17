using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Carts.DTOs;

public class CustomerCartItem
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(1, 99, ErrorMessage = "Quantity must be at least 1 and less than 100")]
    public int Quantity { get; set; }
}
