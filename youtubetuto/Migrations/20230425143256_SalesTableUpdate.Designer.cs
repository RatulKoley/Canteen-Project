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
    [Migration("20230425143256_SalesTableUpdate")]
    partial class SalesTableUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FoodMapping", b =>
                {
                    b.Property<int>("MappingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MappingID"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("FoodID")
                        .HasColumnType("int");

                    b.Property<double?>("FoodQuantity")
                        .HasColumnType("float");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<double?>("ItemQuantity")
                        .HasColumnType("float");

                    b.HasKey("MappingID");

                    b.HasIndex("FoodID");

                    b.HasIndex("ItemId");

                    b.ToTable("FoodMapping");
                });

            modelBuilder.Entity("FoodMenu", b =>
                {
                    b.Property<int>("FoodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodID"), 1L, 1);

                    b.Property<string>("FoodName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.HasKey("FoodID");

                    b.ToTable("FoodMenu");
                });

            modelBuilder.Entity("Item", b =>
                {
                    b.Property<int>("ItemCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemCode"), 1L, 1);

                    b.Property<string>("Image")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("KitchenFood", b =>
                {
                    b.Property<int>("KitchenFoodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KitchenFoodID"), 1L, 1);

                    b.Property<int?>("FoodID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PreparedDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("QuantityPrepared")
                        .HasColumnType("float");

                    b.HasKey("KitchenFoodID");

                    b.HasIndex("FoodID")
                        .IsUnique()
                        .HasFilter("[FoodID] IS NOT NULL");

                    b.ToTable("KitchenFood");
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

            modelBuilder.Entity("Sales", b =>
                {
                    b.Property<int>("SalesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalesID"), 1L, 1);

                    b.Property<double?>("Cash")
                        .HasColumnType("float");

                    b.Property<double?>("Credit")
                        .HasColumnType("float");

                    b.Property<int?>("CreditCardNo")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CustomerType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KitchenFoodID")
                        .HasColumnType("int");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<double?>("Quantity")
                        .HasColumnType("float");

                    b.Property<double?>("UPI")
                        .HasColumnType("float");

                    b.HasKey("SalesID");

                    b.HasIndex("KitchenFoodID");

                    b.ToTable("Sales");
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

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

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

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("FoodMapping", b =>
                {
                    b.HasOne("FoodMenu", "FoodMenu")
                        .WithMany("FoodMapping")
                        .HasForeignKey("FoodID");

                    b.HasOne("Item", "Item")
                        .WithMany("FoodMapping")
                        .HasForeignKey("ItemId");

                    b.Navigation("FoodMenu");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Item", b =>
                {
                    b.HasOne("Unit", "Unit")
                        .WithMany("Item")
                        .HasForeignKey("UnitId");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("KitchenFood", b =>
                {
                    b.HasOne("FoodMenu", "FoodMenu")
                        .WithOne("KitchenFood")
                        .HasForeignKey("KitchenFood", "FoodID");

                    b.Navigation("FoodMenu");
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

            modelBuilder.Entity("Sales", b =>
                {
                    b.HasOne("KitchenFood", "KitchenFood")
                        .WithMany("Sales")
                        .HasForeignKey("KitchenFoodID");

                    b.Navigation("KitchenFood");
                });

            modelBuilder.Entity("Stock", b =>
                {
                    b.HasOne("Item", "Item")
                        .WithOne("Stock")
                        .HasForeignKey("Stock", "ItemId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("FoodMenu", b =>
                {
                    b.Navigation("FoodMapping");

                    b.Navigation("KitchenFood");
                });

            modelBuilder.Entity("Item", b =>
                {
                    b.Navigation("FoodMapping");

                    b.Navigation("Purchase");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("KitchenFood", b =>
                {
                    b.Navigation("Sales");
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