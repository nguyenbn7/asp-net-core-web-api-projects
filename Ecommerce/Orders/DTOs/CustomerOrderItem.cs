using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Orders.DTOs;

public class CustomerOrderItem
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }
}
