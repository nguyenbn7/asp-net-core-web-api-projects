using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Users.Models;

public class ApplicationUser : IdentityUser
{
    public required string FullName { get; set; }
    public required string DisplayName { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;

    // Relationship
    public List<ApplicationUserRole> UserRoles { get; set; } = [];
}
