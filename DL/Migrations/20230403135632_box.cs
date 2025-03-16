using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class box : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionReason",
                table: "TransactionsTodays",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionReason",
                table: "TransactionsTodays");
        }
    }
}
