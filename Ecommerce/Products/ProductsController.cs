using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Products.Models;
using Ecommerce.Products.DTOs;
using Ecommerce.Shared;
using Ecommerce.Shared.Error;

namespace Ecommerce.Products;

[ApiController]
[Route("[controller]")]
public class ProductsController(ApplicationDbContext dbContext, IMapper mapper) : ControllerBase
{
    private readonly ApplicationDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<Page<ProductView>>> GetPageProductAsync([FromQuery] ProductsSearchParams @params)
    {
        var queryable = dbContext.Products.AsNoTracking().AsQueryable();

        if (@params.BrandId.HasValue)
            queryable = queryable.Where(p => p.ProductBrandId == @params.BrandId);


        if (@params.Search is not null)
        {
            if (dbContext.IsPostgreDatabase())
                queryable = queryable.Where(p => EF.Functions.ILike(p.Name, $"%{@params.Search}%"));
            else
                queryable = queryable.Where(p => EF.Functions.Like(p.Name, $"%{@params.Search}%"));
        }

        if (!string.IsNullOrEmpty(@params.Sort))
            queryable = @params.Sort.ToLower() switch
            {
                "price" => queryable.OrderBy(p => p.Price),
                "-price" => queryable.OrderByDescending(p => p.Price),
                _ => queryable.OrderBy(p => p.Name),
            };
        else
            queryable = queryable.OrderBy(p => p.Name);

        var totalItems = await queryable.CountAsync();

        queryable = queryable.Include(p => p.ProductBrand);
        // TODO: move this to extension and use https://docs.automapper.org/en/stable/Queryable-Extensions.html
        queryable = queryable.Skip((@params.PageNumber - 1) * @params.PageSize).Take(@params.PageSize);

        var data = await queryable.ToListAsync();

        return new Page<ProductView>
        {
            PageNumber = @params.PageNumber,
            PageSize = @params.PageSize,
            TotalItems = totalItems,
            Data = mapper.Map<List<Product>, List<ProductView>>(data)
        };
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDetail>> GetProductAsync(int id)
    {
        var product = await dbContext.Products.AsNoTracking()
                                              .Include(p => p.ProductBrand)
                                              .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            return NotFound(new ErrorResponse
            {
                Code = ErrorCodes.ENTITY_DOES_NOT_EXIST,
                Message = "Product not found"
            });

        return mapper.Map<Product, ProductDetail>(product);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductsBrandsAsync()
    {
        return await dbContext.ProductBrands.AsNoTracking().ToListAsync();
    }
}
