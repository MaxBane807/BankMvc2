using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "PermenentOrder");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Dispositions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
