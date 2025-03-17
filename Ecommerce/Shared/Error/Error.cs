using System.Text.Json.Serialization;

namespace Ecommerce.Shared.Error;

public class ErrorResponse
{
    public required string Code { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<string>? StackTrace { get; set; }
}
