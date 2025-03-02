using Goal.Shared.Models.Base.Interfaces.Product;

namespace Goal.Shared.Models.Request.Product
{
    public class ProductRequest : IProduct
    {
        public int Id { get; set; } // Request může obsahovat všechna pole pro validaci
        public string Name { get; set; } = null!;
        public string ImgUri { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}