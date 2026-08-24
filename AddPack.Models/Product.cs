using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddPack.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string SKU { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    public decimal CompareAtPrice { get; set; }
    [Required]
    public decimal CostPrice { get; set; }
    public int SeriesId { get; set; }
    [ForeignKey(nameof(SeriesId))]
    public Series Series { get; set; }
    public string Brand { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public bool IsFeatured { get; set; } = false;
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

