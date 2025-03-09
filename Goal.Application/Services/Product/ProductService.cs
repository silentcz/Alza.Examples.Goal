using Goal.Application.Interfaces.Product;
using Goal.Application.Mappings;
using Goal.Infrastructure.Repositories.Interfaces.Product;
using Goal.Shared.Models.Base;
using Goal.Shared.Models.Response.Product;

namespace Goal.Application.Services.Product;

public class ProductService(IProductRepository repository, IApplicationMapper mapper) : IProductService
{
    /// <summary>
    /// Retrieves a list of all available products
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<ProductResponse>?> GetAllProductsAsync()
    {
        var allProducts = await repository.GetAllAsync();
        return allProducts?.Select(mapper.Map);
    }

    /// <summary>
    /// Retrieves a paginated list of all available products
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<PagedResult<ProductResponse>> GetAllProductsPagedAsync(int pageNumber, int pageSize)
    {
        var pagedProducts = await repository.GetPagedAsync(pageNumber, pageSize);

        if (pagedProducts.TotalItems == 0 || pagedProducts.PageNumber == 0 || pagedProducts.PageSize == 0)
        {
            return new PagedResult<ProductResponse>
            {
                Items = [],
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = 0
            };
        }

        return new PagedResult<ProductResponse>
        {
            Items = pagedProducts.Items.Select(mapper.Map),
            PageNumber = pagedProducts.PageNumber,
            PageSize = pagedProducts.PageSize,
            TotalItems = pagedProducts.TotalItems
        };
    }

    /// <summary>
    /// Retrieves the details of a product by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ProductResponse?> GetProductByIdAsync(int id)
    {
        var product = await repository.GetByIdAsync(id);
        return product is null ? null : mapper.Map(product);

    }

    /// <summary>
    /// Updates the description of a specified product
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newDescription"></param>
    /// <returns></returns>
    public async Task<ProductResponse?> UpdateProductDescriptionAsync(int id, string newDescription)
    {
        var updatedProduct = await repository.UpdateAsync(id, newDescription);
        return updatedProduct is not null ? mapper.Map(updatedProduct) : null;
    }
}