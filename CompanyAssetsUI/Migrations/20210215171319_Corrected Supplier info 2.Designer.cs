﻿// <auto-generated />
using System;
using CompanyAssetsUI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CompanyAssetsUI.Migrations
{
    [DbContext(typeof(AssetContext))]
    [Migration("20210215171319_Corrected Supplier info 2")]
    partial class CorrectedSupplierinfo2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CompanyAssetsUI.Models.Asset", b =>
                {
                    b.Property<int>("AssetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AssetActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("AssetExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("AssetPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("AssetPurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("AssetValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CatalogueID")
                        .HasColumnType("int");

                    b.Property<int>("OfficeID")
                        .HasColumnType("int");

                    b.HasKey("AssetID");

                    b.HasIndex("CatalogueID");

                    b.HasIndex("OfficeID");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Catalogue", b =>
                {
                    b.Property<int>("CatalogueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CataloguePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<bool>("Obsolete")
                        .HasColumnType("bit");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<int>("SupplierProductID")
                        .HasColumnType("int");

                    b.HasKey("CatalogueID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Catalogues");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CategoryActive")
                        .HasColumnType("bit");

                    b.Property<string>("CategoryComment")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("CategoryEOLMonths")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("DepreciationID")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.HasIndex("DepreciationID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("CountryVisible")
                        .HasColumnType("bit");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Currency", b =>
                {
                    b.Property<int>("CurrencyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CurrencyActive")
                        .HasColumnType("bit");

                    b.Property<bool>("CurrencyDefault")
                        .HasColumnType("bit");

                    b.Property<string>("CurrencyDescription")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("CurrencyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("ExchangeRate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CurrencyID");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Depreciation", b =>
                {
                    b.Property<int>("DepreciationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepreciationDescription")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("DepreciationName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DepreciationID");

                    b.ToTable("Depreciations");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Office", b =>
                {
                    b.Property<int>("OfficeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyID")
                        .HasColumnType("int");

                    b.Property<bool>("OfficeActive")
                        .HasColumnType("bit");

                    b.Property<string>("OfficeDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("OfficeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OfficeID");

                    b.HasIndex("CountryID");

                    b.HasIndex("CurrencyID");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Replacement", b =>
                {
                    b.Property<int>("ReplacementID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CatalogueID")
                        .HasColumnType("int");

                    b.Property<int>("ReplacingID")
                        .HasColumnType("int");

                    b.HasKey("ReplacementID");

                    b.HasIndex("CatalogueID");

                    b.HasIndex("ReplacingID");

                    b.ToTable("Replacements");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<bool>("SupplierActive")
                        .HasColumnType("bit");

                    b.Property<string>("SupplierAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("SupplierCity")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SupplierContactEmail")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("SupplierContactFirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SupplierContactLastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SupplierDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("SupplierEmail")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SupplierID");

                    b.HasIndex("CountryID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Asset", b =>
                {
                    b.HasOne("CompanyAssetsUI.Models.Catalogue", "Catalogue")
                        .WithMany("Assets")
                        .HasForeignKey("CatalogueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanyAssetsUI.Models.Office", "Office")
                        .WithMany("Assets")
                        .HasForeignKey("OfficeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalogue");

                    b.Navigation("Office");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Catalogue", b =>
                {
                    b.HasOne("CompanyAssetsUI.Models.Category", "Category")
                        .WithMany("Catalogues")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanyAssetsUI.Models.Supplier", "Supplier")
                        .WithMany("Catalogues")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Category", b =>
                {
                    b.HasOne("CompanyAssetsUI.Models.Depreciation", "Depreciation")
                        .WithMany("Categories")
                        .HasForeignKey("DepreciationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Depreciation");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Office", b =>
                {
                    b.HasOne("CompanyAssetsUI.Models.Country", "Country")
                        .WithMany("Offices")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanyAssetsUI.Models.Currency", "Currency")
                        .WithMany("Offices")
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Replacement", b =>
                {
                    b.HasOne("CompanyAssetsUI.Models.Catalogue", "Catalogue")
                        .WithMany()
                        .HasForeignKey("CatalogueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanyAssetsUI.Models.Catalogue", "Replacing")
                        .WithMany()
                        .HasForeignKey("ReplacingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalogue");

                    b.Navigation("Replacing");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Supplier", b =>
                {
                    b.HasOne("CompanyAssetsUI.Models.Country", "Country")
                        .WithMany("Suppliers")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Catalogue", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Category", b =>
                {
                    b.Navigation("Catalogues");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Country", b =>
                {
                    b.Navigation("Offices");

                    b.Navigation("Suppliers");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Currency", b =>
                {
                    b.Navigation("Offices");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Depreciation", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Office", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("CompanyAssetsUI.Models.Supplier", b =>
                {
                    b.Navigation("Catalogues");
                });
#pragma warning restore 612, 618
        }
    }
}
