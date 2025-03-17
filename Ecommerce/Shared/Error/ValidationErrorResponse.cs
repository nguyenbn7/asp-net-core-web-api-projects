using System.Text.Json.Serialization;

namespace Ecommerce.Shared.Error;

public class ValidationError<T> : ErrorResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<T>? Details { get; set; }
}
