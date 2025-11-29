using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anazon.Migrations
{
    /// <inheritdoc />
    public partial class productsfts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE PRODUCTS 
                ADD  FULLTEXT IX_FT_Name_Description (Name, Description)
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
               ALTER TABLE PRODUCTS 
               DROP INDEX IX_FT_Name_Description
            """);
        }
    }
}
