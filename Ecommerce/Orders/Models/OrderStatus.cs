using System.Runtime.Serialization;

namespace Ecommerce.Orders.Models;

public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    PENDING,
    [EnumMember(Value = "Payment Received")]
    PAYMENT_RECEIVED,
    [EnumMember(Value = "Payment Failed")]
    PAYMENT_FAILED
}
