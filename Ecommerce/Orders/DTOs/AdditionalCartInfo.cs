#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Orders.DTOs;

public class AdditionalCartInfo
{
    [Required]
    public int DeliveryMethodId { get; set; }
    [Required]
    public CustomerBillingDetail BillingDetail { get; set; }
}
