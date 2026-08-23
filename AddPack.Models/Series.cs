namespace AddPack.Models;

public class Series
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Series? ParentId { get; set; }
    public string Image { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int? SortOrder { get; set; } // auto increment?
    public DateTime CreatedAt { get; set; }

}
