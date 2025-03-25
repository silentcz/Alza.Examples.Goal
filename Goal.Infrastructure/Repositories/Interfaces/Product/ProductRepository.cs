using Goal.Shared.DTOs.Product;
using Goal.Shared.Models.Base;

namespace Goal.Infrastructure.Repositories.Interfaces.Product;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PagedResult<ProductDto>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<ProductDto?> UpdateAsync(int id, string newDescription, CancellationToken cancellationToken = default);
}