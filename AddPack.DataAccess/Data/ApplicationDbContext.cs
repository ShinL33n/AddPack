using Microsoft.EntityFrameworkCore;
using AddPack.Models;

namespace AddPack.DataAccess.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // SEED INITIAL DATA FOR SERIES
            var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Kategorie nadrzędne
            var inneId      = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var namiotyId   = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var plecakiId   = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var odziezId    = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var kuchniaId   = Guid.Parse("44444444-4444-4444-4444-444444444444");

            modelBuilder.Entity<Category>().HasData(
                // --- Kategorie główne ---
                new Category
                {
                    Id = inneId,
                    ParentId = null,
                    Name = "Inne",
                    Description = "Inne.",
                    Slug = "inne",
                    IsActive = true,
                    SortOrder = 0,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = namiotyId,
                    ParentId = null,
                    Name = "Namioty",
                    Description = "Namioty turystyczne i bushcraftowe na każdą pogodę.",
                    Slug = "namioty",
                    IsActive = true,
                    SortOrder = 1,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = plecakiId,
                    ParentId = null,
                    Name = "Plecaki",
                    Description = "Plecaki trekkingowe i miejskie w budżetowych cenach.",
                    Slug = "plecaki",
                    IsActive = true,
                    SortOrder = 2,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = odziezId,
                    ParentId = null,
                    Name = "Odzież outdoorowa",
                    Description = "Odzież funkcjonalna do aktywności na świeżym powietrzu.",
                    Slug = "odziez-outdoorowa",
                    IsActive = true,
                    SortOrder = 3,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = kuchniaId,
                    ParentId = null,
                    Name = "Kuchnia turystyczna",
                    Description = "Kuchenki, naczynia i akcesoria do gotowania w terenie.",
                    Slug = "kuchnia-turystyczna",
                    IsActive = true,
                    SortOrder = 4,
                    CreatedAt = seedDate
                },

                // --- Subkategorie: Namioty ---
                new Category
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    ParentId = namiotyId,
                    Name = "Namioty 1-osobowe",
                    Description = "Lekkie namioty solo do szybkich wypraw.",
                    Slug = "namioty-1-osobowe",
                    IsActive = true,
                    SortOrder = 1,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111113"),
                    ParentId = namiotyId,
                    Name = "Namioty rodzinne",
                    Description = "Przestronne namioty dla 4-6 osób.",
                    Slug = "namioty-rodzinne",
                    IsActive = true,
                    SortOrder = 2,
                    CreatedAt = seedDate
                },

                // --- Subkategorie: Plecaki ---
                new Category
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222223"),
                    ParentId = plecakiId,
                    Name = "Plecaki trekkingowe",
                    Description = "Plecaki 40-65L na wielodniowe wyprawy.",
                    Slug = "plecaki-trekkingowe",
                    IsActive = true,
                    SortOrder = 1,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222224"),
                    ParentId = plecakiId,
                    Name = "Plecaki miejskie",
                    Description = "Codzienne plecaki do miasta i na uczelnię.",
                    Slug = "plecaki-miejskie",
                    IsActive = false, // przykład nieaktywnej kategorii
                    SortOrder = 2,
                    CreatedAt = seedDate
                },

                // --- Subkategorie: Odzież ---
                new Category
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333334"),
                    ParentId = odziezId,
                    Name = "Kurtki przeciwdeszczowe",
                    Description = "Kurtki membranowe odporne na deszcz i wiatr.",
                    Slug = "kurtki-przeciwdeszczowe",
                    IsActive = true,
                    SortOrder = 1,
                    CreatedAt = seedDate
                },

                // --- Subkategorie: Kuchnia turystyczna ---
                new Category
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444445"),
                    ParentId = kuchniaId,
                    Name = "Kuchenki gazowe",
                    Description = "Kompaktowe kuchenki turystyczne na kartusze gazowe.",
                    Slug = "kuchenki-gazowe",
                    IsActive = true,
                    SortOrder = 1,
                    CreatedAt = seedDate
                },

                // - Subsubkategorie: Plecaki trekkingowe -
                new Category
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222235"),
                    ParentId = Guid.Parse("22222222-2222-2222-2222-222222222223"),
                    Name = "Plecaki klasyczne",
                    Description = "Plecaki o klasycznej konstrukcji",
                    Slug = "plecaki-klasyczne",
                    IsActive = true,
                    SortOrder = 2,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222236"),
                    ParentId = Guid.Parse("22222222-2222-2222-2222-222222222223"),
                    Name = "Plecaki ultralekkie",
                    Description = "Plecaki z ultralekkich materiałów.",
                    Slug = "plecaki-ultralekkie",
                    IsActive = true,
                    SortOrder = 1,
                    CreatedAt = seedDate
                }
            );

            modelBuilder.Entity<Category>()
                .HasIndex(s => s.Slug)
                .IsUnique();

            // configure the self-referencing relationship for Category
            modelBuilder.Entity<Category>()
                        .HasOne(c => c.CategoryParent)
                        .WithMany(c => c.Subcategories)
                        .HasForeignKey(k => k.ParentId)
                        .OnDelete(DeleteBehavior.Restrict);


            // SEED INITIAL DATA FOR SERIES
            modelBuilder.Entity<Series>().HasData(
                new Series
                {
                    Id = Guid.Parse("3f2504e0-4f89-41d3-9a0c-0305e82c3301"),
                    Name = "Demo",
                    Slug = "demo",
                    Description = "Demo series for developing the product.",
                    Image = null,
                    IsActive = true,
                    SortOrder = 1,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 17, 31, DateTimeKind.Utc)
                },
                new Series
                {
                    Id = Guid.Parse("7b9f5e7c-6d2a-4f18-9a4c-8e2c5d7f1234"),
                    Name = "Gear",
                    Slug = "gear",
                    Description = "Gear series for outdoor use.",
                    Image = null,
                    IsActive = true,
                    SortOrder = 2,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 17, 35, DateTimeKind.Utc)
                },
                new Series
                {
                    Id = Guid.Parse("a3f5c2d1-91e4-4c7a-8b23-5d9e1f6a42c8"),
                    Name = "Survival",
                    Slug = "survival",
                    Description = "Survival series for emergency situations.",
                    Image = null,
                    IsActive = true,
                    SortOrder = 3,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 17, 37, DateTimeKind.Utc)
                },
                new Series
                {
                    Id = Guid.Parse("d81e6b42-7c95-4a13-bf68-2e5d9a7c3140"),
                    Name = "EDC",
                    Slug = "edc",
                    Description = "EDC series for everyday carry.",
                    Image = null,
                    IsActive = true,
                    SortOrder = 4,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 17, 43, DateTimeKind.Utc)
                },
                new Series
                {
                    Id = Guid.Parse("f4c82a19-35d7-46e0-91ab-6c3f8e2d7501"),
                    Name = "Systems",
                    Slug = "systems",
                    Description = "Systems series for comprehensive solutions.",
                    Image = null,
                    IsActive = true,
                    SortOrder = 5,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 17, 50, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<Series>()
                .HasIndex(s => s.Slug)
                .IsUnique();


            //// SEED INITIAL DATA FOR CATEGORIES
            //modelBuilder.Entity<Category>().HasData(
            //    new Category
            //    {
            //        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            //        Name = "Inne",
            //        Slug = "inne",
            //        Description = "Kategoria dla produktów, które nie pasują do innych kategorii.",
            //        ParentId = null,
            //        IsActive = true,
            //        SortOrder = 1,
            //        CreatedAt = new DateTime(2026, 8, 24, 22, 18, 10, DateTimeKind.Utc)
            //    },
            //    new Category
            //    {
            //        Id = Guid.Parse("1c6e8a42-93f5-4d71-b208-5a3f9c1e7642"),
            //        Name = "Zestawy",
            //        Slug = "zestawy",
            //        Description = "Zestawy tworzące kompletną ofertę produktów.",
            //        ParentId = null,
            //        IsActive = true,
            //        SortOrder = 2,
            //        CreatedAt = new DateTime(2026, 8, 24, 22, 18, 15, DateTimeKind.Utc)
            //    },
            //    new Category
            //    {
            //        Id = Guid.Parse("b72d4f19-6a83-41c5-9e07-3d8b2a6f5410"),
            //        Name = "Palniki",
            //        Slug = "palniki",
            //        Description = "Palniki do gotowania na świeżym powietrzu.",
            //        ParentId = null,
            //        IsActive = true,
            //        SortOrder = 3,
            //        CreatedAt = new DateTime(2026, 8, 24, 22, 18, 20, DateTimeKind.Utc)
            //    },
            //    new Category
            //    {
            //        Id = Guid.Parse("e4a91c37-52f8-4b06-a913-7c2d5e8f6041"),
            //        Name = "Palniki alkoholowe",
            //        Slug = "palniki-alkoholowe",
            //        Description = "Palniki do gotowania na świeżym powietrzu.",
            //        ParentId = Guid.Parse("b72d4f19-6a83-41c5-9e07-3d8b2a6f5410"),
            //        IsActive = true,
            //        SortOrder = 4,
            //        CreatedAt = new DateTime(2026, 8, 24, 22, 18, 20, DateTimeKind.Utc),
            //    }
            //);

            //// configure the self-referencing relationship for Category
            //modelBuilder.Entity<Category>()
            //            .HasOne(c => c.CategoryParent)
            //            .WithMany(c => c.Subcategories)
            //            .HasForeignKey(k => k.ParentId)
            //            .OnDelete(DeleteBehavior.Restrict);


            // SEED INITIAL DATA FOR PRODUCTS
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("8f3b2d65-17c9-4a82-be31-6d5e7c9042fa"),
                    SeriesId = Guid.Parse("3f2504e0-4f89-41d3-9a0c-0305e82c3301"),
                    //CategoryId = Guid.Parse("1c6e8a42-93f5-4d71-b208-5a3f9c1e7642"),
                    CategoryId = inneId,
                    Name = "Zestaw do gotowania",
                    Slug = "zestaw-do-gotowania",
                    Description = "Zestaw do gotowania na świeżym powietrzu. Składa się z osłony przeciwwiatrowej pełniącej funkcję uchwytu na garnek, palnika alkoholowego, garnka do gotowania, pokrywek do gotowania i wkładki do garnka na akcesoria. Idealny do biwakowania i turystyki pieszej.",
                    Brand = "AddPack",
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 18, 50, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2026, 8, 24, 22, 18, 58, DateTimeKind.Utc),
                    Series = null!,
                    Category = null!,
                }
            );


            // SEED INITIAL DATA FOR PRODUCTS VARIANTS
            modelBuilder.Entity<ProductVariant>().HasData(
                new ProductVariant
                {
                    Id = Guid.Parse("c51a7e29-84d3-46f0-9b62-1e5c8a734d09"),
                    ProductId = Guid.Parse("8f3b2d65-17c9-4a82-be31-6d5e7c9042fa"),
                    Name = "Zestaw do gotowania - Wersja podstawowa",
                    Slug = "wersja-podstawowa",
                    Description = "Wersja podstawowa zestawu do gotowania na świeżym powietrzu. Składa się z osłony przeciwwiatrowej pełniącej funkcję uchwytu na garnek, palnika alkoholowego 30ml, garnka do gotowania 1l + 0,25l, pokrywek do gotowania i wkładki do garnka na akcesoria.",
                    SKU = "AP-OD-GR-CCK-001", // AddPack - Outdoor - Gear - Camping Cooking Kit - 0001 {Marka - Zastosowanie - Seria - Produkt - Wariant}
                    Price = 150.00m,
                    CompareAtPrice = 150.00m,
                    CostPrice = 100.00m,
                    Weight = 180m,
                    StockQuantity = 5,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 18, 50, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2026, 8, 24, 22, 18, 58, DateTimeKind.Utc),
                    MetaName = "Zestaw do gotowania na świeżym powietrzu podstawowy - AddPack",
                    MetaDescription = "Zestaw do gotowania na świeżym powietrzu od AddPack. Idealny do biwakowania i turystyki pieszej. Składa się z osłony przeciwwiatrowej, palnika alkoholowego 30ml, garnka do gotowania 1l + 0,25l, pokrywek i wkładki na akcesoria.",
                    Product = null!,
                }
            );


            // SEED INITIAL DATA FOR PRODUCT IMAGES
            modelBuilder.Entity<ProductImage>().HasData(
                new ProductImage
                {
                    Id = Guid.Parse("6d92f4b8-3e17-4c65-a081-9f2b7d5e4361"),
                    ProductVariantId = Guid.Parse("c51a7e29-84d3-46f0-9b62-1e5c8a734d09"),
                    ImageUrl = "wwroot/images/products/c51a7e29-84d3-46f0-9b62-1e5c8a734d09/6d92f4b8-3e17-4c65-a081-9f2b7d5e4361_image.jpg",
                    IsPrimary = true,
                    AltText = "Zestaw do gotowania na świeżym powietrzu - Wersja podstawowa, widok z przodu",
                    SortOrder = 1,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 18, 50, DateTimeKind.Utc),
                    ProductVariant = null!,
                },
                new ProductImage
                {
                    Id = Guid.Parse("a84c1f73-6b29-45d8-90e4-3f7a5c2d8169"),
                    ProductVariantId = Guid.Parse("c51a7e29-84d3-46f0-9b62-1e5c8a734d09"),
                    ImageUrl = "wwroot/images/products/c51a7e29-84d3-46f0-9b62-1e5c8a734d09/a84c1f73-6b29-45d8-90e4-3f7a5c2d8169_image.jpg",
                    IsPrimary = false,
                    AltText = "Zestaw do gotowania na świeżym powietrzu - Wersja podstawowa, widok z góry",
                    SortOrder = 2,
                    CreatedAt = new DateTime(2026, 8, 24, 22, 18, 58, DateTimeKind.Utc),
                    ProductVariant = null!,
                }
            );

        }

    }
}   