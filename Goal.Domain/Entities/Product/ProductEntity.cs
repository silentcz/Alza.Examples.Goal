using Goal.Shared.Models.Base.Interfaces.Product;

namespace Goal.Domain.Entities.Product;

public class ProductEntity : IProduct
{
    // Vlastnosti
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string ImgUri { get; private set; }
    public decimal Price { get; private set; }
    public string? Description { get; private set; }

    // Konstruktor
    public ProductEntity(int id, string name, string imgUri, decimal price, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be null or empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(imgUri))
            throw new ArgumentException("ImgUri cannot be null or empty.", nameof(imgUri));

        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

        Id = id;
        Name = name;
        ImgUri = imgUri;
        Price = price;
        Description = description;
    }

    // Metody
    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("New name cannot be null or empty.", nameof(newName));

        Name = newName;
    }

    public void UpdateImgUri(string newImgUri)
    {
        if (string.IsNullOrWhiteSpace(newImgUri))
            throw new ArgumentException("New ImgUri cannot be null or empty.", nameof(newImgUri));

        ImgUri = newImgUri;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(newPrice), "Price cannot be negative.");

        Price = newPrice;
    }

    public void UpdateDescription(string? newDescription)
    {
        Description = newDescription; // Popis může být null, takže zde není žádná validace
    }
}
