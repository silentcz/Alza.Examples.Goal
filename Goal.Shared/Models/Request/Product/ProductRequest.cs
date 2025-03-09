using Goal.Shared.Models.Base.Interfaces.Product;

namespace Goal.Shared.Models.Request.Product
{
    // TOBE: Reserved for future use as part of extended functionality
    public class ProductRequest : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImgUri { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}