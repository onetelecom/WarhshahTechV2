using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class Disocunt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionRef",
                table: "SubscribtionInvoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionRef",
                table: "SubscribtionInvoices");
        }
    }
}
