using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace youtubetuto.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodMenu",
                columns: table => new
                {
                    FoodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodMenu", x => x.FoodID);
                });

            migrationBuilder.CreateTable(
                name: "FoodMapping",
                columns: table => new
                {
                    MappingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodID = table.Column<int>(type: "int", nullable: true),
                    FoodQuantity = table.Column<double>(type: "float", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    ItemQuantity = table.Column<double>(type: "float", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodMapping", x => x.MappingID);
                    table.ForeignKey(
                        name: "FK_FoodMapping_FoodMenu_FoodID",
                        column: x => x.FoodID,
                        principalTable: "FoodMenu",
                        principalColumn: "FoodID");
                    table.ForeignKey(
                        name: "FK_FoodMapping_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ItemCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodMapping_FoodID",
                table: "FoodMapping",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodMapping_ItemId",
                table: "FoodMapping",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodMapping");

            migrationBuilder.DropTable(
                name: "FoodMenu");
        }
    }
}
