using System;
using System.Collections.Generic;

namespace Goal.Infrastructure.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ImgUri { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }
}
