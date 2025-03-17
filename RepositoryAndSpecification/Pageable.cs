namespace Demo;

public sealed class Pageable
{
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }

    private Pageable() { }

    public static Pageable Of(int pageNumber, int pageSize) => new()
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber,
        PageSize = pageSize < 5 ? 5 : pageSize,
    };
}
