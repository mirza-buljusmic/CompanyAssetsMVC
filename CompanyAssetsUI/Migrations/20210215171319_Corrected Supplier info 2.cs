using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyAssetsUI.Migrations
{
    public partial class CorrectedSupplierinfo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierContactFristName",
                table: "Suppliers",
                newName: "SupplierContactFirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierContactFirstName",
                table: "Suppliers",
                newName: "SupplierContactFristName");
        }
    }
}
