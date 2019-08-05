﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductCatalog.Data.Entities;

namespace ProductCatalog.Data.Migrations
{
    [DbContext(typeof(ProductCatalogContext))]
    [Migration("20190805203809_Removed_Unique_Constraint")]
    partial class Removed_Unique_Constraint
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductCatalog.Data.Entities.PriceHistory", b =>
                {
                    b.Property<int>("PriceHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<DateTime>("PriceChangeDate");

                    b.Property<int>("ProductId");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("PriceHistoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("PriceHistory");
                });

            modelBuilder.Entity("ProductCatalog.Data.Entities.ProductCatalog", b =>
                {
                    b.Property<int>("ProductCatalogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("ProductCatalogId");

                    b.ToTable("ProductCatalog");

                    b.HasData(
                        new
                        {
                            ProductCatalogId = 1,
                            Name = "Stuff"
                        });
                });

            modelBuilder.Entity("ProductCatalog.Data.Entities.Products", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("Money");

                    b.Property<int>("ProductCatalogId");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ProductId");

                    b.HasIndex("ProductCatalogId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductCatalog.Data.Entities.PriceHistory", b =>
                {
                    b.HasOne("ProductCatalog.Data.Entities.Products")
                        .WithMany("PriceHistory")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProductCatalog.Data.Entities.Products", b =>
                {
                    b.HasOne("ProductCatalog.Data.Entities.ProductCatalog")
                        .WithMany("Products")
                        .HasForeignKey("ProductCatalogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
