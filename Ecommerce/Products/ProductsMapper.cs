using AutoMapper;
using Ecommerce.Products.DTOs;
using Ecommerce.Products.Models;
using Ecommerce.Products.ValueResolvers;

namespace Ecommerce.Products;

public class ProductsMapper : Profile
{
    public ProductsMapper()
    {
        CreateMap<Product, ProductView>()
            .ForMember(d => d.Brand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ImageUrl, o => o.MapFrom<ProductImageUrl>());

        CreateMap<Product, ProductDetail>()
            .ForMember(d => d.Brand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ImageUrl, o => o.MapFrom<ProductImageUrl>());
    }
}
