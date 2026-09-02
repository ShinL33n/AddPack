using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddPack.Models;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    public Guid SeriesId { get; set; }
    [ForeignKey(nameof(SeriesId))]
    public required Series Series { get; set; }

    public Guid CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public required Category Category { get; set; }

    [Required]
    [Display(Name = "Nazwa produktu")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Slug (link)")]
    public string Slug { get; set; } = string.Empty;

    [Display(Name = "Opis produktu")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Marka")]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Aktywny")]
    public bool IsActive { get; set; } = true;

    [Required]
    [Display(Name = "Polecany")]
    public bool IsFeatured { get; set; } = false;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<ProductVariant> Variants { get; set; } = [];

    //[Key]
    //public Guid Id { get; set; }

    ////[Required]
    ////public string SKU { get; set; } = string.Empty;

    //[Required]
    //public string Name { get; set; } = string.Empty;
    //public string Slug { get; set; } = string.Empty;
    //public string Description { get; set; } = String.Empty;

    ////[Required]
    ////public decimal Price { get; set; }
    ////public decimal CompareAtPrice { get; set; }

    ////[Required]
    ////public decimal CostPrice { get; set; }

    //public int SeriesId { get; set; }
    //[ForeignKey(nameof(SeriesId))]
    //public required Series Series { get; set; }

    //public int CategoryId { get; set; }
    //[ForeignKey(nameof(CategoryId))]
    //public required Category Category { get; set; }

    //public string Brand { get; set; } = string.Empty;
    ////public decimal Weight { get; set; }
    //public bool IsActive { get; set; } = true;
    ////public bool IsFeatured { get; set; } = false;
    ////public string MetaTitle { get; set; } = string.Empty;
    ////public string MetaDescription { get; set; } = string.Empty;
    //public DateTime CreatedAt { get; set; }
    //public DateTime UpdatedAt { get; set; }
}

