using AutoMapper;
using Ecommerce.Orders.DTOs;
using Ecommerce.Orders.Models;
using Ecommerce.Orders.ValueResolvers;
using Ecommerce.Products.Models;

namespace Ecommerce.Orders;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<CustomerBillingDetail, BillingDetail>();
        CreateMap<CustomerOrderItem, OrderItem>();
        CreateMap<Product, OrderedProduct>()
            .ForMember(d => d.Brand, o => o.MapFrom(s => s.ProductBrand.Name));

        CreateMap<OrderDeliveryMethod, DeliveryMethodView>();
        CreateMap<BillingDetail, BillingDetailView>();
        CreateMap<OrderItem, OrderedProductView>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.OrderedProduct.Id))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.OrderedProduct.Name))
            .ForMember(d => d.ProductPrice, o => o.MapFrom(s => s.OrderedProduct.Price))
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.OrderedProduct.Brand))
            .ForMember(d => d.ProductImageUrl, o => o.MapFrom<ProductImageUrl>());

        CreateMap<Order, OrderView>()
            .ForMember(d => d.Items, o => o.MapFrom(s => s.OrderItems));
    }
}
