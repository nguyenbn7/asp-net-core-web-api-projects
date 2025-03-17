#nullable disable

namespace Ecommerce.Orders.DTOs;

public class BillingDetailView
{
    public string FullName { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Address2 { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}
