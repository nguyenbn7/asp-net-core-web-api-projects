using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Users.Models;

public class ApplicationRole : IdentityRole
{
    public List<ApplicationUserRole> UserRoles { get; set; } = [];
}
