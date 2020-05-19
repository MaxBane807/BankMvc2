using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Web.Data.Migrations
{
    public partial class FirstTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestColumn",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestColumn",
                table: "Customers");
        }
    }
}
