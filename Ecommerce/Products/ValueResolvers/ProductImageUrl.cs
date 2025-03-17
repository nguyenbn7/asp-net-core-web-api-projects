using AutoMapper;
using Ecommerce.Products.DTOs;
using Ecommerce.Products.Models;

namespace Ecommerce.Products.ValueResolvers;

public class ProductImageUrl(IConfiguration configuration) : IValueResolver<Product, ProductView, string>
{
    private readonly IConfiguration configuration = configuration;

    public string Resolve(
        Product source,
        ProductView destination,
        string? destMember,
        ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.ImageUrl))
            return string.Empty;

        var baseUriString = configuration["BASE_IMAGE_URL"];
        if (baseUriString != null)
        {
            var uri = new Uri(new Uri(baseUriString), new Uri(source.ImageUrl, UriKind.Relative));
            return uri.AbsoluteUri;
        }
        return source.ImageUrl;
    }
}
