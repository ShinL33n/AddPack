namespace AddPack.Models;

public class Product
{
    public int Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal CompareAtPrice { get; set; }
    public decimal CostPrice { get; set; }
    public Series Series { get; set; }
    public string Brand { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public bool IsActive { get; set; }
    public bool IsFeatured { get; set; }
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

