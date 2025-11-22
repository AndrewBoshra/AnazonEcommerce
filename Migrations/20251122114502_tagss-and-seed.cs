using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Anazon.Migrations
{
    /// <inheritdoc />
    public partial class tagssandseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Key = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Key);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductTag",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Tag = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTag", x => new { x.ProductId, x.Tag });
                    table.ForeignKey(
                        name: "FK_ProductTag_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTag_Tag_Tag",
                        column: x => x.Tag,
                        principalTable: "Tag",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "ParentCategoryId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "All clothing items", "Clothing", null, null, null },
                    { 4, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Electronic and tech products", "Tech Products", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                column: "Key",
                values: new object[]
                {
                    "casual",
                    "electronics",
                    "men",
                    "mobile",
                    "new",
                    "running",
                    "sports"
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "ParentCategoryId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Footwear and shoes", "Shoes", 1, null, null },
                    { 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Shirts and tops", "Shirts", 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 3, null, 4, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Flagship smartphone", "Smartphone X", null, null });

            migrationBuilder.InsertData(
                table: "ProductTag",
                columns: new[] { "ProductId", "Tag" },
                values: new object[,]
                {
                    { 3, "electronics" },
                    { 3, "mobile" },
                    { 3, "new" }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Price", "ProductId", "Stock", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 4, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 699.00m, 3, 25, null, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, null, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Lightweight running shoes", "Running Sneakers", null, null },
                    { 2, null, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Comfortable casual shirt", "Casual Shirt", null, null }
                });

            migrationBuilder.InsertData(
                table: "ProductTag",
                columns: new[] { "ProductId", "Tag" },
                values: new object[,]
                {
                    { 1, "men" },
                    { 1, "running" },
                    { 1, "sports" },
                    { 2, "casual" },
                    { 2, "men" }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Price", "ProductId", "Stock", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 79.99m, 1, 50, null, null },
                    { 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 89.99m, 1, 30, null, null },
                    { 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 29.99m, 2, 100, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_Tag",
                table: "ProductTag",
                column: "Tag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
