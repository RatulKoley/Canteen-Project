using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace youtubetuto.Migrations
{
    public partial class SalesTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CreditCardNo",
                table: "Sales",
                type: "int",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreditCardNo",
                table: "Sales",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 4,
                oldNullable: true);
        }
    }
}
