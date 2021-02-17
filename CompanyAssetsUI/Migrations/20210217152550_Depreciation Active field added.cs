using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyAssetsUI.Migrations
{
    public partial class DepreciationActivefieldadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DepreciationActive",
                table: "Depreciations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepreciationActive",
                table: "Depreciations");
        }
    }
}
