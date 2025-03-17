#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Orders.DTOs;

public class CustomerBillingDetail
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Address { get; set; }
    public string Address2 { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    public string ZipCode { get; set; }
}
