using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Web.Data.Migrations
{
    public partial class UniqueId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Customers");
          
        }
    }
}
