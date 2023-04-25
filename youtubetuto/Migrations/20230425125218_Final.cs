using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace youtubetuto.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Unit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Supply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Item",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FoodMenu",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Supply");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FoodMenu");
        }
    }
}
