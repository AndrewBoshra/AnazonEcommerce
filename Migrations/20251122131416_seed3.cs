using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Anazon.Migrations
{
    /// <inheritdoc />
    public partial class seed3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tag_Tag",
                table: "ProductTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Key");

            migrationBuilder.InsertData(
                table: "Attributes",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CreatedBy", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Size", null, null },
                    { 2, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Size", null, null },
                    { 3, 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Color", null, null }
                });

            migrationBuilder.InsertData(
                table: "AttributeValues",
                columns: new[] { "Id", "AttributeId", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "8" },
                    { 2, 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "9" },
                    { 3, 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "10" },
                    { 4, 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "11" },
                    { 5, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "S" },
                    { 6, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "M" },
                    { 7, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "L" },
                    { 8, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "XL" },
                    { 9, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Black" },
                    { 10, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "White" },
                    { 11, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Blue" },
                    { 12, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Red" }
                });

            migrationBuilder.InsertData(
                table: "ProductVariantAttributeValues",
                columns: new[] { "Id", "AttributeValueId", "CreatedAt", "CreatedBy", "ProductVariantId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, null, null },
                    { 2, 9, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, null, null },
                    { 3, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, null, null },
                    { 4, 10, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, null, null },
                    { 5, 6, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 3, null, null },
                    { 6, 11, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 3, null, null },
                    { 7, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 5, null, null },
                    { 8, 12, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 5, null, null },
                    { 9, 7, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 6, null, null },
                    { 10, 10, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, 6, null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tags_Tag",
                table: "ProductTag",
                column: "Tag",
                principalTable: "Tags",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tags_Tag",
                table: "ProductTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductVariantAttributeValues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductVariantAttributeValues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductVariantAttributeValues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductVariantAttributeValues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductVariantAttributeValues",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductVariantAttributeValues",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductVariantAttributeValues",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductVariantAttributeValues",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductVariantAttributeValues",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductVariantAttributeValues",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AttributeValues",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Attributes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attributes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Attributes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tag_Tag",
                table: "ProductTag",
                column: "Tag",
                principalTable: "Tag",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
