using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyAssetsUI.Migrations
{
    public partial class ConnectingcategorytoDepreciation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Depreciations_DepreciationID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DepreciationMethod",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "DepreciationID",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Depreciations_DepreciationID",
                table: "Categories",
                column: "DepreciationID",
                principalTable: "Depreciations",
                principalColumn: "DepreciationID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Depreciations_DepreciationID",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "DepreciationID",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DepreciationMethod",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Depreciations_DepreciationID",
                table: "Categories",
                column: "DepreciationID",
                principalTable: "Depreciations",
                principalColumn: "DepreciationID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
