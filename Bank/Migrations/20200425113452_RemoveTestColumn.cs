using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class RemoveTestColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestColumn",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestColumn",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
