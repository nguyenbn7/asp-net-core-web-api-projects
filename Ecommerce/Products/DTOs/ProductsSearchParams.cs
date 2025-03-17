namespace Ecommerce.Products.DTOs;

public class ProductsSearchParams
{
    private const int MAX_PAGE_SIZE = 500;
    private const int DEFAULT_PAGE_SIZE = 9;
    private int pageSize = DEFAULT_PAGE_SIZE;
    private int pageNumber = 1;
    private string? search;

    public int PageNumber
    {
        get => pageNumber;
        set => pageNumber = value < 1 ? 1 : value;
    }
    public int PageSize
    {
        get => pageSize;
        set => pageSize = (value < DEFAULT_PAGE_SIZE)
            ? DEFAULT_PAGE_SIZE : (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
    }
    public int? BrandId { get; set; }
    public string? Sort { get; set; }
    public string? Search
    {
        get => search;
        set => search = value?.ToLower();
    }
}
