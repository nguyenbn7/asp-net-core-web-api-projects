using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Carts.Models;
using Ecommerce.Carts.DTOs;
using Ecommerce.Shared;
using Ecommerce.Shared.Error;

namespace Ecommerce.Carts;

[ApiController]
[Route("[controller]")]
public class CartsController(
    ICartRepository cartRepository,
    ApplicationDbContext dbContext) : ControllerBase
{
    private readonly ICartRepository cartRepository = cartRepository;
    private readonly ApplicationDbContext dbContext = dbContext;
    private readonly ErrorResponse cartNotFoundErrorResponse = new()
    {
        Code = ErrorCodes.ENTITY_DOES_NOT_EXIST,
        Message = "Cart not found"
    };

    [HttpGet("{id}")]
    public async Task<ActionResult<Cart>> GetAsync(string id)
    {
        var cart = await cartRepository.GetAsync(id);

        return cart is null ? NotFound(cartNotFoundErrorResponse) : cart;
    }

    [HttpPost]
    public async Task<ActionResult<Cart>> CreateAsync(CustomerCart customerCart)
    {
        var productIds = customerCart.Items.Select(cci => cci.ProductId);

        var productTables = await dbContext.Products.AsNoTracking()
            .Include(p => p.ProductBrand)
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

        List<CartItem> cartItems = [];

        foreach (var item in customerCart.Items)
        {
            if (!productTables.TryGetValue(item.ProductId, out var product) || product is null)
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.ENTITY_DOES_NOT_EXIST,
                    Message = $"Product with id `{item.ProductId}` not found"
                });

            var cartItem = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = item.Quantity,
                ProductImageUrl = product.ImageUrl,
                Brand = product.ProductBrand.Name
            };

            cartItems.Add(cartItem);
        };

        var cart = new Cart
        {
            Id = Guid.NewGuid().ToString(),
            Items = cartItems
        };

        var newCart = await cartRepository.SaveAsync(cart);

        if (newCart is null)
            return BadRequest(new ErrorResponse
            {
                Code = ErrorCodes.UNKNOWN_ERROR,
                Message = "Can not create or save cart"
            });

        return newCart;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Cart>> UpdateAsync(string id, CustomerCart customerCart)
    {
        var cart = await cartRepository.GetAsync(id);
        if (cart is null) return BadRequest(cartNotFoundErrorResponse);

        var productIds = customerCart.Items.Select(cci => cci.ProductId);

        var productTables = await dbContext.Products.AsNoTracking()
            .Include(p => p.ProductBrand)
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

        List<CartItem> cartItems = [];

        foreach (var item in customerCart.Items)
        {
            if (!productTables.TryGetValue(item.ProductId, out var product) || product is null)
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.ENTITY_DOES_NOT_EXIST,
                    Message = $"Product with id `{item.ProductId}` not found"
                });

            var cartItem = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = item.Quantity,
                ProductImageUrl = product.ImageUrl,
                Brand = product.ProductBrand.Name
            };

            cartItems.Add(cartItem);
        };

        cart.Items = cartItems;

        var newCart = await cartRepository.SaveAsync(cart);

        if (newCart is null)
            return BadRequest(new ErrorResponse
            {
                Code = ErrorCodes.UNKNOWN_ERROR,
                Message = "Can not create or save cart"
            });

        return newCart;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var cart = await cartRepository.GetAsync(id);
        if (cart is null)
            return NotFound(cartNotFoundErrorResponse);

        await cartRepository.DeleteAsync(id);
        return Ok();
    }
}
