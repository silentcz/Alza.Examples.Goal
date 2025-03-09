namespace Goal.Infrastructure.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ImgUri { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }
}
