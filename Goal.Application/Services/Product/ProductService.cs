using Goal.Application.Interfaces.Product;
using Goal.Application.Mappings;
using Goal.Infrastructure.Repositories.Interfaces.Product;
using Goal.Shared.Models.Base;
using Goal.Shared.Models.Response.Product;

namespace Goal.Application.Services.Product;

public class ProductService(IProductRepository repository, IApplicationMapper mapper) : IProductService
{
    public async Task<IEnumerable<ProductResponse>?> GetAllProductsAsync()
    {
        var allProducts = await repository.GetAllAsync();
        return allProducts?.Select(mapper.Map);
    }

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

    public async Task<ProductResponse?> GetProductByIdAsync(int id)
    {
        var product = await repository.GetByIdAsync(id);
        return product is null ? null : mapper.Map(product);

    }

    public async Task<ProductResponse?> UpdateProductDescriptionAsync(int id, string newDescription)
    {
        var updatedProduct = await repository.UpdateAsync(id, newDescription);
        return updatedProduct is not null ? mapper.Map(updatedProduct) : null;
    }
}