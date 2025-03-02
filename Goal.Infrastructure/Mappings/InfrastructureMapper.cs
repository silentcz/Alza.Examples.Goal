using Goal.Infrastructure.Models;
using Goal.Shared.DTOs.Product;
using Riok.Mapperly.Abstractions;

namespace Goal.Infrastructure.Mappings;

public interface IInfrastructureMapper
{
    public ProductDto Map(Product input);
}

[Mapper]
public partial class InfrastructureMapper : IInfrastructureMapper
{
    public partial ProductDto Map(Product input);
}