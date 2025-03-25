using Goal.Shared.Models.Base;

namespace Goal.Shared.Models.Response.Product;

public class ProductPageResponse : PagedResult<ProductResponse>
{
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
