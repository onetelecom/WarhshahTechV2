using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class fom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceNumber",
                table: "TransactionsTodays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "TransactionsTodays");
        }
    }
}
