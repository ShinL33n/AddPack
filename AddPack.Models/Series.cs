using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddPack.Models;

public class Series
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ParentId { get; set; }
    [ForeignKey(nameof(ParentId))]
    public Series? SeriesParentId { get; set; }
    public string? Image { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public int? SortOrder { get; set; } // auto increment?
    public DateTime CreatedAt { get; set; }

}
