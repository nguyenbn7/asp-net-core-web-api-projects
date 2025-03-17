using Ecommerce.Orders;
using Ecommerce.Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Ecommerce.Application;

[ApiController]
[Route("webhook")]
public class WebhookController(
    IConfiguration configuration,
    ILogger<WebhookController> logger,
    IOrderService orderService) : ControllerBase
{
    private readonly string ENDPOINT_SECRET = configuration["Stripe:Endpoint Secret"]
                                              ?? throw new Exception("{\"Stripe\":\n\"Endpoint Secret\": \"....\"} not found");
    private readonly ILogger<WebhookController> logger = logger;
    private readonly IOrderService orderService = orderService;

    [HttpPost("stripe")]
    public async Task<IActionResult> HandleStripeEventAsync()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], ENDPOINT_SECRET);

            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    {
                        var paymentIntent = (stripeEvent.Data.Object as PaymentIntent)!;
                        await orderService.UpdateOrderStatus(paymentIntent, OrderStatus.PAYMENT_RECEIVED);
                    }
                    break;
                case Events.PaymentIntentPaymentFailed:
                    {
                        var paymentIntent = (stripeEvent.Data.Object as PaymentIntent)!;
                        await orderService.UpdateOrderStatus(paymentIntent, OrderStatus.PAYMENT_FAILED);
                    }
                    break;
                default:
                    logger.LogError("Unhandled event type: {}", stripeEvent.Type);
                    break;
            }

            return Ok();
        }
        catch (StripeException ex)
        {
            logger.LogError(ex, "");
            return BadRequest();
        }
    }
}
