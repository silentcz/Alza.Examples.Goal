using Goal.Shared.Models.Base;
using Goal.Shared.Models.Response.Product;

namespace Goal.Application.Interfaces.Product;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>?> GetAllProductsAsync();
    Task<PagedResult<ProductResponse>> GetAllProductsPagedAsync(int pageNumber, int pageSize);
    Task<ProductResponse?> GetProductByIdAsync(int id);
    Task<ProductResponse?> UpdateProductDescriptionAsync(int id, string newDescription);
}