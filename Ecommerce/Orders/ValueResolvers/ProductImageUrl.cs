using AutoMapper;
using Ecommerce.Orders.DTOs;
using Ecommerce.Orders.Models;

namespace Ecommerce.Orders.ValueResolvers;

public class ProductImageUrl(IConfiguration configuration) : IValueResolver<OrderItem, OrderedProductView, string>
{
    private readonly IConfiguration configuration = configuration;

    public string Resolve(
        OrderItem source,
        OrderedProductView destination,
        string? destMember,
        ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.OrderedProduct.ImageUrl))
            return string.Empty;

        var baseUriString = configuration["BASE_IMAGE_URL"];
        if (baseUriString != null)
        {
            var uri = new Uri(new Uri(baseUriString), new Uri(source.OrderedProduct.ImageUrl, UriKind.Relative));
            return uri.AbsoluteUri;
        }
        return source.OrderedProduct.ImageUrl;
    }
}
