using Microsoft.EntityFrameworkCore;
using AddPack.Models;

namespace AddPack.DataAccess.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Series> Series { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Series>().HasData(
                new Series
                {
                    Id = 1,
                    Name = "Demo",
                    Slug = "demo",
                    Description = "Demo series for developing the product.",
                    ParentId = null,
                    Image = null,
                    IsActive = true,
                    SortOrder = 1,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 17, 31, DateTimeKind.Utc)
                },
                new Series
                {
                    Id = 2,
                    Name = "Gear",
                    Slug = "gear",
                    Description = "Gear series for outdoor use.",
                    ParentId = null,
                    Image = null,
                    IsActive = true,
                    SortOrder = 2,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 17, 35, DateTimeKind.Utc)
                },
                new Series
                {
                    Id = 3,
                    Name = "Survival",
                    Slug = "survival",
                    Description = "Survival series for emergency situations.",
                    ParentId = null,
                    Image = null,
                    IsActive = true,
                    SortOrder = 3,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 17, 37, DateTimeKind.Utc)
                },
                new Series
                {
                    Id = 4,
                    Name = "EDC",
                    Slug = "edc",
                    Description = "EDC series for everyday carry.",
                    ParentId = null,
                    Image = null,
                    IsActive = true,
                    SortOrder = 4,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 17, 43, DateTimeKind.Utc)
                },
                new Series
                {
                    Id = 5,
                    Name = "Systems",
                    Slug = "systems",
                    Description = "Systems series for comprehensive solutions.",
                    ParentId = null,
                    Image = null,
                    IsActive = true,
                    SortOrder = 5,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 17, 50, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<Series>().HasOne(s => s.SeriesParentId)
                                         .WithMany()
                                         .HasForeignKey(s => s.ParentId)
                                         .OnDelete(DeleteBehavior.SetNull);


        modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    SKU = "AP-OD-GR-CCK-001", // AddPack - Outdoor - Gear - Camping Cooking Kit - 0001 {Marka - Zastosowanie - Seria - Produkt - Numer}
                    Name = "Zestaw do gotowania",
                    Slug = "zestaw-do-gotowania",
                    Description = "Zestaw do gotowania na świeżym powietrzu. Składa się z osłony przeciwwiatrowej pełniącej funkcję uchwytu na garnek, palnika alkoholowego, garnka do gotowania, pokrywek do gotowania i wkładki do garnka na akcesoria. Idealny do biwakowania i turystyki pieszej.",
                    Price = 150.00m,
                    CompareAtPrice = 150.00m,
                    CostPrice = 100.00m,
                    SeriesId = 1,
                    Brand = "AddPack",
                    Weight = 180m,
                    IsActive = true,
                    IsFeatured = true,
                    MetaTitle = "Zestaw do gotowania na świeżym powietrzu - AddPack",
                    MetaDescription = "Zestaw do gotowania na świeżym powietrzu od AddPack. Idealny do biwakowania i turystyki pieszej. Składa się z osłony przeciwwiatrowej, palnika alkoholowego, garnka do gotowania, pokrywek i wkładki na akcesoria.",
                    CreatedAt = new DateTime(2026, 8, 24, 22, 18, 50, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2026, 8, 24, 22, 18, 58, DateTimeKind.Utc)
                }
                );
        }

    }
}
