namespace Ecommerce.Shared;

public class Page<T> where T : class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public required ICollection<T> Data { get; set; }
}
