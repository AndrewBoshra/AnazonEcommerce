using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Anazon.Migrations
{
    /// <inheritdoc />
    public partial class AttributesPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Key" },
                values: new object[,]
                {
                    { 11, null, "permissions.attributes.list" },
                    { 12, null, "permissions.attributes.view" },
                    { 13, null, "permissions.attributes.create" },
                    { 14, null, "permissions.attributes.update" },
                    { 15, null, "permissions.attributes.delete" }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 11, 11, 1 },
                    { 12, 12, 1 },
                    { 13, 13, 1 },
                    { 14, 14, 1 },
                    { 15, 15, 1 },
                    { 1005, 11, 3 },
                    { 1006, 12, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
