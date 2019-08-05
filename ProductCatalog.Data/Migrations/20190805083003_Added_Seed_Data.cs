using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductCatalog.Data.Migrations
{
    public partial class Added_Seed_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCatalog",
                columns: new[] { "ProductCatalogId", "Name" },
                values: new object[] { 1, "Stuff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "ProductCatalogId",
                keyValue: 1);
        }
    }
}
