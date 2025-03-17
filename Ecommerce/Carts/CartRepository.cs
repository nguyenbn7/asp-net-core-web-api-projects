using System.Text.Json;
using Ecommerce.Carts.Models;
using StackExchange.Redis;

namespace Ecommerce.Carts;

public class CartRepository(IConnectionMultiplexer redis) : ICartRepository
{
    private readonly IDatabase redis = redis.GetDatabase();

    public async Task<Cart?> SaveAsync(Cart cart)
    {
        var created = await redis.StringSetAsync(
            cart.Id,
            JsonSerializer.Serialize(cart),
            TimeSpan.FromDays(30));

        return created ? await GetAsync(cart.Id) : null;
    }

    public Task<bool> DeleteAsync(string id)
    {
        return redis.KeyDeleteAsync(id);
    }

    public async Task<Cart?> GetAsync(string id)
    {
        var data = await redis.StringGetAsync(id);

        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>(data.ToString());
    }
}
