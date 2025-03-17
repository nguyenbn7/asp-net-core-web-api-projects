using System.Security.Claims;

namespace Ecommerce.Users;

public interface IUserService
{
    string GenerateJWTToken(List<Claim> claims);
}
