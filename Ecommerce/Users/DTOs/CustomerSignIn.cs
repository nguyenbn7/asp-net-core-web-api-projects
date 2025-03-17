#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Users.DTOs;

public class CustomerSignIn
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
