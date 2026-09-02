using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddPack.Models;

public class ProductVariant
{
    [Key]
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public required Product Product { get; set; }

    [Required]
    [Display(Name = "Nazwa wariantu produktu")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Slug (link)")]
    public string Slug { get; set; } = string.Empty;

    [Display(Name = "Opis wariantu produktu")]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string SKU { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Cena sprzedaży")]
    public decimal Price { get; set; }

    [Required]
    [Display(Name = "Cena porównawcza")]
    public decimal CompareAtPrice { get; set; }

    [Required]
    [Display(Name = "Cena kosztu")]
    public decimal CostPrice { get; set; }

    [Required]
    [Display(Name = "Waga")]
    public decimal Weight { get; set; }

    [Required]
    [Display(Name = "Dostępna ilość")]
    public int StockQuantity { get; set; }

    [Required]
    [Display(Name = "Aktywny")]
    public bool IsActive { get; set; } = true;

    [Required]
    [Display(Name = "Polecany")]
    public bool IsFeatured { get; set; } = false;

    //public string? ThumbnailImage { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string MetaName { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;

    public ICollection<ProductImage> Images { get; set; } = [];

    //[Key]
    //public Guid Id { get; set; }

    //public int ProductId { get; set; }
    //[ForeignKey(nameof(ProductId))]
    //public required Product Product { get; set; }

    //[Required]
    //public string SKU { get; set; } = string.Empty;

    //[Required]
    //public string Name { get; set; } = string.Empty;
    //public string Slug { get; set; } = string.Empty;
    //public string Description { get; set; } = String.Empty;

    //[Required]
    //public decimal Price { get; set; }
    //public decimal CompareAtPrice { get; set; }

    //[Required]
    //public decimal CostPrice { get; set; }

}
