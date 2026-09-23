using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AddPack.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompareAtPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariant_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductVariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_ProductVariant_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "ParentId", "Slug", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Inne.", true, "Inne", null, "inne", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Namioty turystyczne i bushcraftowe na każdą pogodę.", true, "Namioty", null, "namioty", 1 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Plecaki trekkingowe i miejskie w budżetowych cenach.", true, "Plecaki", null, "plecaki", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Odzież funkcjonalna do aktywności na świeżym powietrzu.", true, "Odzież outdoorowa", null, "odziez-outdoorowa", 3 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kuchenki, naczynia i akcesoria do gotowania w terenie.", true, "Kuchnia turystyczna", null, "kuchnia-turystyczna", 4 }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "CreatedAt", "Description", "Image", "IsActive", "Name", "Slug", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301"), new DateTime(2026, 8, 24, 22, 17, 31, 0, DateTimeKind.Utc), "Demo series for developing the product.", null, true, "Demo", "demo", 1 },
                    { new Guid("7b9f5e7c-6d2a-4f18-9a4c-8e2c5d7f1234"), new DateTime(2026, 8, 24, 22, 17, 35, 0, DateTimeKind.Utc), "Gear series for outdoor use.", null, true, "Gear", "gear", 2 },
                    { new Guid("a3f5c2d1-91e4-4c7a-8b23-5d9e1f6a42c8"), new DateTime(2026, 8, 24, 22, 17, 37, 0, DateTimeKind.Utc), "Survival series for emergency situations.", null, true, "Survival", "survival", 3 },
                    { new Guid("d81e6b42-7c95-4a13-bf68-2e5d9a7c3140"), new DateTime(2026, 8, 24, 22, 17, 43, 0, DateTimeKind.Utc), "EDC series for everyday carry.", null, true, "EDC", "edc", 4 },
                    { new Guid("f4c82a19-35d7-46e0-91ab-6c3f8e2d7501"), new DateTime(2026, 8, 24, 22, 17, 50, 0, DateTimeKind.Utc), "Systems series for comprehensive solutions.", null, true, "Systems", "systems", 5 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "ParentId", "Slug", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111112"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Lekkie namioty solo do szybkich wypraw.", true, "Namioty 1-osobowe", new Guid("11111111-1111-1111-1111-111111111111"), "namioty-1-osobowe", 1 },
                    { new Guid("11111111-1111-1111-1111-111111111113"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Przestronne namioty dla 4-6 osób.", true, "Namioty rodzinne", new Guid("11111111-1111-1111-1111-111111111111"), "namioty-rodzinne", 2 },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Plecaki 40-65L na wielodniowe wyprawy.", true, "Plecaki trekkingowe", new Guid("22222222-2222-2222-2222-222222222222"), "plecaki-trekkingowe", 1 },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Codzienne plecaki do miasta i na uczelnię.", false, "Plecaki miejskie", new Guid("22222222-2222-2222-2222-222222222222"), "plecaki-miejskie", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333334"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kurtki membranowe odporne na deszcz i wiatr.", true, "Kurtki przeciwdeszczowe", new Guid("33333333-3333-3333-3333-333333333333"), "kurtki-przeciwdeszczowe", 1 },
                    { new Guid("44444444-4444-4444-4444-444444444445"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kompaktowe kuchenki turystyczne na kartusze gazowe.", true, "Kuchenki gazowe", new Guid("44444444-4444-4444-4444-444444444444"), "kuchenki-gazowe", 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "CreatedAt", "Description", "IsActive", "IsFeatured", "Name", "SeriesId", "Slug", "UpdatedAt" },
                values: new object[] { new Guid("8f3b2d65-17c9-4a82-be31-6d5e7c9042fa"), "AddPack", new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 8, 24, 22, 18, 50, 0, DateTimeKind.Utc), "Zestaw do gotowania na świeżym powietrzu. Składa się z osłony przeciwwiatrowej pełniącej funkcję uchwytu na garnek, palnika alkoholowego, garnka do gotowania, pokrywek do gotowania i wkładki do garnka na akcesoria. Idealny do biwakowania i turystyki pieszej.", true, true, "Zestaw do gotowania", new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301"), "zestaw-do-gotowania", new DateTime(2026, 8, 24, 22, 18, 58, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "ProductVariant",
                columns: new[] { "Id", "CompareAtPrice", "CostPrice", "CreatedAt", "Description", "IsActive", "IsFeatured", "MetaDescription", "MetaName", "Name", "Price", "ProductId", "SKU", "Slug", "StockQuantity", "UpdatedAt", "Weight" },
                values: new object[] { new Guid("c51a7e29-84d3-46f0-9b62-1e5c8a734d09"), 150.00m, 100.00m, new DateTime(2026, 8, 24, 22, 18, 50, 0, DateTimeKind.Utc), "Wersja podstawowa zestawu do gotowania na świeżym powietrzu. Składa się z osłony przeciwwiatrowej pełniącej funkcję uchwytu na garnek, palnika alkoholowego 30ml, garnka do gotowania 1l + 0,25l, pokrywek do gotowania i wkładki do garnka na akcesoria.", true, true, "Zestaw do gotowania na świeżym powietrzu od AddPack. Idealny do biwakowania i turystyki pieszej. Składa się z osłony przeciwwiatrowej, palnika alkoholowego 30ml, garnka do gotowania 1l + 0,25l, pokrywek i wkładki na akcesoria.", "Zestaw do gotowania na świeżym powietrzu podstawowy - AddPack", "Zestaw do gotowania - Wersja podstawowa", 150.00m, new Guid("8f3b2d65-17c9-4a82-be31-6d5e7c9042fa"), "AP-OD-GR-CCK-001", "wersja-podstawowa", 5, new DateTime(2026, 8, 24, 22, 18, 58, 0, DateTimeKind.Utc), 180m });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "AltText", "CreatedAt", "ImageUrl", "IsPrimary", "ProductVariantId", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("6d92f4b8-3e17-4c65-a081-9f2b7d5e4361"), "Zestaw do gotowania na świeżym powietrzu - Wersja podstawowa, widok z przodu", new DateTime(2026, 8, 24, 22, 18, 50, 0, DateTimeKind.Utc), "wwroot/images/products/c51a7e29-84d3-46f0-9b62-1e5c8a734d09/6d92f4b8-3e17-4c65-a081-9f2b7d5e4361_image.jpg", true, new Guid("c51a7e29-84d3-46f0-9b62-1e5c8a734d09"), 1 },
                    { new Guid("a84c1f73-6b29-45d8-90e4-3f7a5c2d8169"), "Zestaw do gotowania na świeżym powietrzu - Wersja podstawowa, widok z góry", new DateTime(2026, 8, 24, 22, 18, 58, 0, DateTimeKind.Utc), "wwroot/images/products/c51a7e29-84d3-46f0-9b62-1e5c8a734d09/a84c1f73-6b29-45d8-90e4-3f7a5c2d8169_image.jpg", false, new Guid("c51a7e29-84d3-46f0-9b62-1e5c8a734d09"), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Slug",
                table: "Categories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductVariantId",
                table: "ProductImage",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SeriesId",
                table: "Products",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariant",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_Slug",
                table: "Series",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "ProductVariant");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Series");
        }
    }
}
