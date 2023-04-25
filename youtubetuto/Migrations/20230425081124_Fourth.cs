using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace youtubetuto.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KitchenFood",
                columns: table => new
                {
                    KitchenFoodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodID = table.Column<int>(type: "int", nullable: true),
                    QuantityPrepared = table.Column<double>(type: "float", nullable: true),
                    PreparedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitchenFood", x => x.KitchenFoodID);
                    table.ForeignKey(
                        name: "FK_KitchenFood_FoodMenu_FoodID",
                        column: x => x.FoodID,
                        principalTable: "FoodMenu",
                        principalColumn: "FoodID");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SalesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KitchenFoodID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Cash = table.Column<double>(type: "float", nullable: true),
                    CreditCardNo = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Credit = table.Column<double>(type: "float", nullable: true),
                    UPI = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SalesID);
                    table.ForeignKey(
                        name: "FK_Sales_KitchenFood_KitchenFoodID",
                        column: x => x.KitchenFoodID,
                        principalTable: "KitchenFood",
                        principalColumn: "KitchenFoodID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KitchenFood_FoodID",
                table: "KitchenFood",
                column: "FoodID",
                unique: true,
                filter: "[FoodID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_KitchenFoodID",
                table: "Sales",
                column: "KitchenFoodID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "KitchenFood");
        }
    }
}
