using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class CarOwnerCR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarOwnerCR",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarOwnerCR",
                table: "Invoices");
        }
    }
}
