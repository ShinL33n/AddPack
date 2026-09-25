using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AddPack.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAnotherLevelToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "ParentId", "Slug", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222235"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Plecaki o klasycznej konstrukcji", true, "Plecaki klasyczne", new Guid("22222222-2222-2222-2222-222222222223"), "plecaki-klasyczne", 1 },
                    { new Guid("22222222-2222-2222-2222-222222222236"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Plecaki z ultralekkich materiałów.", true, "Plecaki ultralekkie", new Guid("22222222-2222-2222-2222-222222222223"), "plecaki-ultralekkie", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222235"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222236"));
        }
    }
}
