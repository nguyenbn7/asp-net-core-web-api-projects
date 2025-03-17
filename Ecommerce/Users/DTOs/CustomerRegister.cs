#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Users.DTOs;

public class CustomerRegister
{
    [Required]
    [MaxLength(256)]
    public string FullName { get; set; }

    [Required]
    [MaxLength(55)]
    public string DisplayName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$",
    ErrorMessage = "Password must have 1 uppercase, 1 lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]
    public string Password { get; set; }

    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
