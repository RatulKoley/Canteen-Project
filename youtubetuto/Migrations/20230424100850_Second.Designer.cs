﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using youtubetuto;

#nullable disable

namespace youtubetuto.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230424100850_Second")]
    partial class Second
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Item", b =>
                {
                    b.Property<int>("ItemCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemCode"), 1L, 1);

                    b.Property<string>("Image")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<double>("ReorderLevel")
                        .HasColumnType("float");

                    b.Property<int?>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("ItemCode");

                    b.HasIndex("UnitId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Purchase", b =>
                {
                    b.Property<int>("PurchaseNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseNo"), 1L, 1);

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("PurchasedDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("PurchasedValue")
                        .HasColumnType("float");

                    b.Property<double?>("Quantity")
                        .HasColumnType("float");

                    b.Property<int?>("SupplyId")
                        .HasColumnType("int");

                    b.HasKey("PurchaseNo");

                    b.HasIndex("ItemId");

                    b.HasIndex("SupplyId");

                    b.ToTable("Purchase");
                });

            modelBuilder.Entity("Stock", b =>
                {
                    b.Property<int>("StockID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockID"), 1L, 1);

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<double?>("Qunatity")
                        .HasColumnType("float");

                    b.HasKey("StockID");

                    b.HasIndex("ItemId")
                        .IsUnique()
                        .HasFilter("[ItemId] IS NOT NULL");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("Supply", b =>
                {
                    b.Property<int>("SupplyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplyID"), 1L, 1);

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("SupplyID");

                    b.ToTable("Supply");
                });

            modelBuilder.Entity("Unit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("Item", b =>
                {
                    b.HasOne("Unit", "Unit")
                        .WithMany("Item")
                        .HasForeignKey("UnitId");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("Purchase", b =>
                {
                    b.HasOne("Item", "Item")
                        .WithMany("Purchase")
                        .HasForeignKey("ItemId");

                    b.HasOne("Supply", "Supply")
                        .WithMany("Purchase")
                        .HasForeignKey("SupplyId");

                    b.Navigation("Item");

                    b.Navigation("Supply");
                });

            modelBuilder.Entity("Stock", b =>
                {
                    b.HasOne("Item", "Item")
                        .WithOne("Stock")
                        .HasForeignKey("Stock", "ItemId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Item", b =>
                {
                    b.Navigation("Purchase");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Supply", b =>
                {
                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("Unit", b =>
                {
                    b.Navigation("Item");
                });
#pragma warning restore 612, 618
        }
    }
}
