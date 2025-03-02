namespace Goal.Shared.Models.Base.Interfaces.Product
{
    public interface IProduct
    {
        int Id { get; }
        string Name { get; }
        string ImgUri { get; }
        decimal Price { get; }
        string? Description { get; }
    }
}