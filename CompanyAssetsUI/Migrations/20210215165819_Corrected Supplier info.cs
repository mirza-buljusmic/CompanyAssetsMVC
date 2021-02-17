using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyAssetsUI.Migrations
{
    public partial class CorrectedSupplierinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SupplierActive",
                table: "Suppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierActive",
                table: "Suppliers");
        }
    }
}
