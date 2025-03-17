using System.Text.Json;
using Ecommerce.Products.Models;
using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Products;

public static class SeedDemoDataExtension
{
    public static async Task SeedProductsAsync(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        var dbContext = services.GetApplicationDbContext(application.Environment);

        if (await dbContext.Products.AnyAsync()) return;

        var productsData = await File.ReadAllTextAsync("Products/DemoData/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
        if (products == null)
        {
            logger.LogError("Can not read file `products.json`");
            return;
        }

        dbContext.Products.AddRange(products);
        await dbContext.SaveChangesAsync();
    }

    public static async Task SeedProductBrandsAsync(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        var dbContext = services.GetApplicationDbContext(application.Environment);

        if (await dbContext.ProductBrands.AnyAsync()) return;

        var brandsData = await File.ReadAllTextAsync("Products/DemoData/brands.json");
        var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
        if (productBrands == null)
        {
            logger.LogError("Can not read file `brands.json`");
            return;
        }

        dbContext.ProductBrands.AddRange(productBrands);
        await dbContext.SaveChangesAsync();
    }
}
