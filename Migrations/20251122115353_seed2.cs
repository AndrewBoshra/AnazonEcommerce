using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Anazon.Migrations
{
    /// <inheritdoc />
    public partial class seed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 3, "electronics" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "ParentCategoryId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 5, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Mobile phones and smartphones", "Smartphones", 4, null, null },
                    { 6, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Laptops and notebooks", "Laptops", 4, null, null },
                    { 7, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Headphones and earbuds", "Headphones", 4, null, null },
                    { 8, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chargers, cables and accessories", "Tech Accessories", 4, null, null },
                    { 9, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Clothing for women", "Women's Clothing", 1, null, null },
                    { 10, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Clothing for men", "Men's Clothing", 1, null, null },
                    { 11, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Clothing for children", "Kids' Clothing", 1, null, null },
                    { 12, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Home appliances and kitchenware", "Home & Kitchen", null, null, null },
                    { 13, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Sporting goods and outdoor", "Sports & Outdoors", null, null, null },
                    { 14, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cosmetics and personal care", "Beauty & Personal Care", null, null, null },
                    { 15, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Toys and games for children", "Toys & Games", null, null, null },
                    { 16, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Books across genres", "Books", null, null, null },
                    { 17, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Automotive parts and accessories", "Automotive", null, null, null },
                    { 18, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Jewelry and watches", "Jewelry & Watches", null, null, null },
                    { 19, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Bags, backpacks and luggage", "Bags & Luggage", null, null, null },
                    { 20, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Office stationery and supplies", "Office Supplies", null, null, null }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 5);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 4, null, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Durable trail shoes", "Trail Running Shoes", null, null },
                    { 5, null, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Men's formal shirt", "Formal Shirt", null, null },
                    { 24, null, 4, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Waterproof action camera", "Action Camera", null, null }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                column: "Key",
                values: new object[]
                {
                    "auto",
                    "bag",
                    "beauty",
                    "book",
                    "charger",
                    "fitness",
                    "formal",
                    "gaming",
                    "headphones",
                    "home",
                    "kids",
                    "kitchen",
                    "laptop",
                    "luxury",
                    "office",
                    "outdoor",
                    "smart",
                    "toy",
                    "watch",
                    "women"
                });

            migrationBuilder.InsertData(
                table: "ProductTag",
                columns: new[] { "ProductId", "Tag" },
                values: new object[,]
                {
                    { 4, "outdoor" },
                    { 4, "running" },
                    { 5, "formal" },
                    { 5, "men" },
                    { 24, "electronics" },
                    { 24, "outdoor" }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Price", "ProductId", "Stock", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 5, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 109.99m, 4, 40, null, null },
                    { 6, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 39.99m, 5, 80, null, null },
                    { 25, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 299.99m, 24, 25, null, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 6, null, 9, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Elegant evening dress", "Women's Dress", null, null },
                    { 7, null, 11, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Comfortable kids shoes", "Kids Sneakers", null, null },
                    { 8, null, 6, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "High performance laptop", "Laptop Pro 14", null, null },
                    { 9, null, 7, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Noise cancelling headphones", "Wireless Headphones", null, null },
                    { 10, null, 8, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Fast charger 65W", "USB-C Charger", null, null },
                    { 11, null, 12, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Kitchen blender", "Blender 3000", null, null },
                    { 12, null, 13, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Non-slip yoga mat", "Yoga Mat", null, null },
                    { 13, null, 14, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Long lasting lipstick", "Lipstick", null, null },
                    { 14, null, 15, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Educational toy", "Building Blocks Set", null, null },
                    { 15, null, 16, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Top seller fiction book", "Bestselling Novel", null, null },
                    { 16, null, 17, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Vanilla scent", "Car Air Freshener", null, null },
                    { 17, null, 18, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Elegant necklace", "Gold Necklace", null, null },
                    { 18, null, 19, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Water resistant backpack", "Travel Backpack", null, null },
                    { 19, null, 20, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Blue ink pens", "Ballpoint Pens (12)", null, null },
                    { 20, null, 18, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Fitness & notifications", "Smartwatch Z", null, null },
                    { 21, null, 6, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "High-end GPU laptop", "Gaming Laptop", null, null },
                    { 22, null, 8, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Ergonomic mouse", "Wireless Mouse", null, null },
                    { 23, null, 16, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Healthy recipes", "Cookbook", null, null }
                });

            migrationBuilder.InsertData(
                table: "ProductTag",
                columns: new[] { "ProductId", "Tag" },
                values: new object[,]
                {
                    { 6, "luxury" },
                    { 6, "women" },
                    { 7, "kids" },
                    { 8, "gaming" },
                    { 8, "laptop" },
                    { 9, "electronics" },
                    { 9, "headphones" },
                    { 10, "charger" },
                    { 11, "kitchen" },
                    { 12, "fitness" },
                    { 13, "beauty" },
                    { 14, "toy" },
                    { 15, "book" },
                    { 16, "auto" },
                    { 17, "luxury" },
                    { 18, "bag" },
                    { 19, "office" },
                    { 20, "watch" },
                    { 21, "gaming" },
                    { 21, "laptop" },
                    { 22, "office" },
                    { 23, "book" }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Price", "ProductId", "Stock", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 7, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 129.99m, 6, 20, null, null },
                    { 8, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 49.99m, 7, 60, null, null },
                    { 9, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 1499.00m, 8, 10, null, null },
                    { 10, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 199.99m, 9, 45, null, null },
                    { 11, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 29.99m, 10, 200, null, null },
                    { 12, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 89.99m, 11, 35, null, null },
                    { 13, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 24.99m, 12, 120, null, null },
                    { 14, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 14.99m, 13, 150, null, null },
                    { 15, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 59.99m, 14, 70, null, null },
                    { 16, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 19.99m, 15, 500, null, null },
                    { 17, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 7.99m, 16, 300, null, null },
                    { 18, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 249.99m, 17, 5, null, null },
                    { 19, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 79.99m, 18, 80, null, null },
                    { 20, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 5.99m, 19, 1000, null, null },
                    { 21, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 199.99m, 20, 60, null, null },
                    { 22, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 2199.00m, 21, 8, null, null },
                    { 23, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 29.99m, 22, 150, null, null },
                    { 24, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 27.99m, 23, 90, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 4, "outdoor" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 4, "running" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 5, "formal" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 5, "men" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 6, "luxury" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 6, "women" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 7, "kids" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 8, "gaming" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 8, "laptop" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 9, "electronics" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 9, "headphones" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 10, "charger" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 11, "kitchen" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 12, "fitness" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 13, "beauty" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 14, "toy" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 15, "book" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 16, "auto" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 17, "luxury" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 18, "bag" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 19, "office" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 20, "watch" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 21, "gaming" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 21, "laptop" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 22, "office" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 23, "book" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 24, "electronics" });

            migrationBuilder.DeleteData(
                table: "ProductTag",
                keyColumns: new[] { "ProductId", "Tag" },
                keyValues: new object[] { 24, "outdoor" });

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "home");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "smart");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "auto");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "bag");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "beauty");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "book");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "charger");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "fitness");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "formal");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "gaming");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "headphones");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "kids");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "kitchen");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "laptop");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "luxury");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "office");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "outdoor");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "toy");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "watch");

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Key",
                keyValue: "women");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.InsertData(
                table: "ProductTag",
                columns: new[] { "ProductId", "Tag" },
                values: new object[] { 3, "electronics" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 4);
        }
    }
}
