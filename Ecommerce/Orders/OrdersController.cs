using AutoMapper;
using Ecommerce.Orders.DTOs;
using Ecommerce.Orders.Models;
using Ecommerce.Shared;
using Ecommerce.Shared.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Orders;

[ApiController]
[Route("[controller]")]
public class OrdersController(
    IMapper mapper,
    ApplicationDbContext dbContext,
    IOrderService orderService) : ControllerBase
{
    private readonly ApplicationDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    private readonly IOrderService orderService = orderService;

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(CustomerOrder customerOrder)
    {
        var deliveryMethod = await dbContext.OrderDeliveryMethods.FirstOrDefaultAsync(sm => sm.Id == customerOrder.DeliveryMethodId);
        if (deliveryMethod == null)
            return NotFound(new ErrorResponse
            {
                Code = ErrorCodes.DELIVERY_METHOD_OPTION_NOT_FOUND,
                Message = $"Can not find delivery method with id: {customerOrder.DeliveryMethodId}"
            });

        var itemTables = new Dictionary<int, CustomerOrderItem>();
        var productIds = new List<int>();

        foreach (var item in customerOrder.Items)
        {
            if (itemTables.TryGetValue(item.ProductId, out CustomerOrderItem? value))
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.DUPLICATE_ORDER_PRODUCT,
                    Message = $"Order can not have same product with difference quantities, found product id '${item.ProductId}' with quantity ${value.Quantity} and quantity ${item.Quantity}"
                });

            itemTables[item.ProductId] = item;
            productIds.Add(item.ProductId);
        }

        var products = await dbContext.Products.AsNoTracking()
                                               .Include(p => p.ProductBrand)
                                               .Where(p => productIds.Contains(p.Id))
                                               .ToListAsync();

        var subTotal = 0m;
        var orderItems = new List<OrderItem>(customerOrder.Items.Count);

        foreach (var product in products)
        {
            var orderProduct = itemTables[product.Id];
            subTotal += product.Price * orderProduct.Quantity;
            var orderItem = new OrderItem
            {
                Quantity = orderProduct.Quantity,
                OrderedProduct = new OrderedProduct
                {
                    Brand = product.ProductBrand.Name,
                    Name = product.Name,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price
                }
            };
            orderItems.Add(orderItem);
        }

        var paymentIntent = await orderService.CreatePaymentIntentAsync(decimal.ToInt64((subTotal + deliveryMethod.Price) * 100));

        var order = new Order
        {
            BillingDetail = mapper.Map<CustomerBillingDetail, BillingDetail>(customerOrder.Billing),
            DeliveryMethod = deliveryMethod,
            SubTotal = subTotal,
            OrderItems = orderItems,
            PaymentIntentId = paymentIntent.Id,
            ClientSecret = paymentIntent.ClientSecret
        };

        dbContext.Orders.Add(order);

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Conflict(new ErrorResponse
            {
                Code = ErrorCodes.CAN_NOT_CREATE_ORDER,
                Message = "Error while creating order. Please try again"
            });
        }

        return Ok(mapper.Map<Order, OrderView>(order));
    }

    [HttpGet("delivery-methods")]
    public async Task<IActionResult> GetDeliveryMethodsAsync()
    {
        return Ok(await dbContext.OrderDeliveryMethods.AsNoTracking().ToListAsync());
    }

}
