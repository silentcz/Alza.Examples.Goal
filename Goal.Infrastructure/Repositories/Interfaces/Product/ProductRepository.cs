using Goal.Domain.Entities.Product;
using Goal.Shared.DTOs.Product;
using Goal.Shared.Models.Base;

namespace Goal.Infrastructure.Repositories.Interfaces.Product;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>?> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<PagedResult<ProductDto>> GetPagedAsync(int pageNumber, int pageSize);
    Task<ProductDto?> UpdateAsync(int id, string newDescription);
}