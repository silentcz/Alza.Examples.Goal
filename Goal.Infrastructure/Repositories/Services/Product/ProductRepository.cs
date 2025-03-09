using Goal.Infrastructure.Mappings;
using Goal.Infrastructure.Persistence;
using Goal.Infrastructure.Repositories.Interfaces.Product;
using Goal.Shared.DTOs.Product;
using Goal.Shared.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infrastructure.Repositories.Services.Product;

public class ProductRepository(GoalDatabaseContext context, IInfrastructureMapper mapper) : IProductRepository
{
    public async Task<IEnumerable<ProductDto>?> GetAllAsync()
    {
        var products = await context.Products.ToListAsync();
        return products.Select(mapper.Map);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product is not null ? mapper.Map(product) : null;
    }

    public async Task<PagedResult<ProductDto>> GetPagedAsync(int pageNumber, int pageSize)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            return new PagedResult<ProductDto> { Items = [] };
        }

        var totalItems = await context.Products.CountAsync();

        var products = await context.Products
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var productDtos = products.Select(mapper.Map);

        return new PagedResult<ProductDto>
        {
            Items = productDtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }

    public async Task<ProductDto?> UpdateAsync(int id, string newDescription)
    {
        var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (existingProduct is null)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }

        existingProduct.Description = newDescription;
        await context.SaveChangesAsync();

        return mapper.Map(existingProduct);
    }
}