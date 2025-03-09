using Goal.Shared.Models.Base;
using Goal.Shared.Models.Response.Product;

namespace Goal.Application.Interfaces.Product;

public interface IProductService
{
    // v1: Seznam všech produktů bez stránkování
    Task<IEnumerable<ProductResponse>?> GetAllProductsAsync();

    // v2: Seznam všech produktů s podporou stránkování
    Task<PagedResult<ProductResponse>> GetAllProductsPagedAsync(int pageNumber, int pageSize);

    // Získání produktu podle ID
    Task<ProductResponse?> GetProductByIdAsync(int id);

    // Aktualizace popisu produktu
    Task<ProductResponse?> UpdateProductDescriptionAsync(int id, string newDescription);
}