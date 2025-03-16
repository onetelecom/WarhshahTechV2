using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class salesListىرخهؤث : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarOwnerAddress",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarOwnerAddress",
                table: "Invoices");
        }
    }
}
