using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Carts.DTOs;

public class CustomerCart
{
    [Required]
    [MinLength(1)]
    [MaxLength(99)]
    public required List<CustomerCartItem> Items { get; set; }
}
