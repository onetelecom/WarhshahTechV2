using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class bo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentBalance",
                table: "TransactionsTodays",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PreviousBalance",
                table: "TransactionsTodays",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBalance",
                table: "TransactionsTodays");

            migrationBuilder.DropColumn(
                name: "PreviousBalance",
                table: "TransactionsTodays");
        }
    }
}
