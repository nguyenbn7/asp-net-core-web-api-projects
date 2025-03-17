namespace Demo;

public class Page<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public required List<T> Data { get; set; }
}
