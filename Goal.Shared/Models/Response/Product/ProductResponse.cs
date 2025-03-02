using Goal.Shared.Models.Base.Interfaces.Product;

namespace Goal.Shared.Models.Response.Product;

public class ProductResponse : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImgUri { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}