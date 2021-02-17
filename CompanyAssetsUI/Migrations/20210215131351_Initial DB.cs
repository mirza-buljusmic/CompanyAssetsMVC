using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyAssetsUI.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CountryDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CurrencyDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyDefault = table.Column<bool>(type: "bit", nullable: false),
                    CurrencyActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyID);
                });

            migrationBuilder.CreateTable(
                name: "Depreciations",
                columns: table => new
                {
                    DepreciationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepreciationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepreciationDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depreciations", x => x.DepreciationID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SupplierDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SupplierEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SupplierContactFristName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SupplierContactLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SupplierContactEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SupplierCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    SupplierAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                    table.ForeignKey(
                        name: "FK_Suppliers_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    OfficeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    OfficeActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.OfficeID);
                    table.ForeignKey(
                        name: "FK_Offices_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offices_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryEOLMonths = table.Column<int>(type: "int", nullable: false),
                    CategoryActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryComment = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DepreciationMethod = table.Column<int>(type: "int", nullable: false),
                    DepreciationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Depreciations_DepreciationID",
                        column: x => x.DepreciationID,
                        principalTable: "Depreciations",
                        principalColumn: "DepreciationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Catalogues",
                columns: table => new
                {
                    CatalogueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    CataloguePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierProductID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    Obsolete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogues", x => x.CatalogueID);
                    table.ForeignKey(
                        name: "FK_Catalogues_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catalogues_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogueID = table.Column<int>(type: "int", nullable: false),
                    AssetPurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssetExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssetActive = table.Column<bool>(type: "bit", nullable: false),
                    OfficeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetID);
                    table.ForeignKey(
                        name: "FK_Assets_Catalogues_CatalogueID",
                        column: x => x.CatalogueID,
                        principalTable: "Catalogues",
                        principalColumn: "CatalogueID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Assets_Offices_OfficeID",
                        column: x => x.OfficeID,
                        principalTable: "Offices",
                        principalColumn: "OfficeID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Replacements",
                columns: table => new
                {
                    ReplacementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogueID = table.Column<int>(type: "int", nullable: false),
                    ReplacingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replacements", x => x.ReplacementID);
                    table.ForeignKey(
                        name: "FK_Replacements_Catalogues_CatalogueID",
                        column: x => x.CatalogueID,
                        principalTable: "Catalogues",
                        principalColumn: "CatalogueID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Replacements_Catalogues_ReplacingID",
                        column: x => x.ReplacingID,
                        principalTable: "Catalogues",
                        principalColumn: "CatalogueID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_CatalogueID",
                table: "Assets",
                column: "CatalogueID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OfficeID",
                table: "Assets",
                column: "OfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogues_CategoryID",
                table: "Catalogues",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogues_SupplierID",
                table: "Catalogues",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DepreciationID",
                table: "Categories",
                column: "DepreciationID");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_CountryID",
                table: "Offices",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_CurrencyID",
                table: "Offices",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Replacements_CatalogueID",
                table: "Replacements",
                column: "CatalogueID");

            migrationBuilder.CreateIndex(
                name: "IX_Replacements_ReplacingID",
                table: "Replacements",
                column: "ReplacingID");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CountryID",
                table: "Suppliers",
                column: "CountryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Replacements");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Catalogues");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Depreciations");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
