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
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Series_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Series",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompareAtPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SeriesId = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "CreatedAt", "Description", "Image", "IsActive", "Name", "ParentId", "Slug", "SortOrder" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 24, 22, 17, 31, 0, DateTimeKind.Utc), "Demo series for developing the product.", null, true, "Demo", null, "demo", 1 },
                    { 2, new DateTime(2026, 8, 24, 22, 17, 35, 0, DateTimeKind.Utc), "Gear series for outdoor use.", null, true, "Gear", null, "gear", 2 },
                    { 3, new DateTime(2026, 8, 24, 22, 17, 37, 0, DateTimeKind.Utc), "Survival series for emergency situations.", null, true, "Survival", null, "survival", 3 },
                    { 4, new DateTime(2026, 8, 24, 22, 17, 43, 0, DateTimeKind.Utc), "EDC series for everyday carry.", null, true, "EDC", null, "edc", 4 },
                    { 5, new DateTime(2026, 8, 24, 22, 17, 50, 0, DateTimeKind.Utc), "Systems series for comprehensive solutions.", null, true, "Systems", null, "systems", 5 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CompareAtPrice", "CostPrice", "CreatedAt", "Description", "IsActive", "IsFeatured", "MetaDescription", "MetaTitle", "Name", "Price", "SKU", "SeriesId", "Slug", "UpdatedAt", "Weight" },
                values: new object[] { 1, "AddPack", 150.00m, 100.00m, new DateTime(2026, 8, 24, 22, 18, 50, 0, DateTimeKind.Utc), "Zestaw do gotowania na świeżym powietrzu. Składa się z osłony przeciwwiatrowej pełniącej funkcję uchwytu na garnek, palnika alkoholowego, garnka do gotowania, pokrywek do gotowania i wkładki do garnka na akcesoria. Idealny do biwakowania i turystyki pieszej.", true, true, "Zestaw do gotowania na świeżym powietrzu od AddPack. Idealny do biwakowania i turystyki pieszej. Składa się z osłony przeciwwiatrowej, palnika alkoholowego, garnka do gotowania, pokrywek i wkładki na akcesoria.", "Zestaw do gotowania na świeżym powietrzu - AddPack", "Zestaw do gotowania", 150.00m, "AP-OD-GR-CCK-001", 1, "zestaw-do-gotowania", new DateTime(2026, 8, 24, 22, 18, 58, 0, DateTimeKind.Utc), 180m });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SeriesId",
                table: "Products",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_ParentId",
                table: "Series",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Series");
        }
    }
}
