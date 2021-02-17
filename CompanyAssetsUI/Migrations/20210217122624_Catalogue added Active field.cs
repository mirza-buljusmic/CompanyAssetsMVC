using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyAssetsUI.Migrations
{
    public partial class CatalogueaddedActivefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CatalogueItemActive",
                table: "Catalogues",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatalogueItemActive",
                table: "Catalogues");
        }
    }
}
