using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Ecommerce.Orders.DTOs;

public class CustomerOrder
{
    [Required]
    [MinLength(1)]
    [MaxLength(99)]
    public List<CustomerOrderItem> Items { get; set; }
    [Required]
    public int DeliveryMethodId { get; set; }
    [Required]
    public CustomerBillingDetail Billing { get; set; }
}
