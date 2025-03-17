using Ecommerce.Carts.Models;

namespace Ecommerce.Carts;

public interface ICartRepository
{
    Task<Cart?> GetAsync(string id);
    Task<Cart?> SaveAsync(Cart cart);
    Task<bool> DeleteAsync(string id);
}
