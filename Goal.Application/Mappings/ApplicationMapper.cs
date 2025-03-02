using Goal.Shared.DTOs.Product;
using Goal.Shared.Models.Response.Product;
using Riok.Mapperly.Abstractions;

namespace Goal.Application.Mappings;

public interface IApplicationMapper
{
    public ProductResponse Map(ProductDto input);
}

[Mapper]
public partial class ApplicationMapper : IApplicationMapper
{
    public partial ProductResponse Map(ProductDto input);
}