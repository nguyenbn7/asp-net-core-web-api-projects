using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Users.Models;

public class ApplicationUserRole : IdentityUserRole<string>
{
    public required ApplicationUser User { get; set; }
    public required ApplicationRole Role { get; set; }
}
