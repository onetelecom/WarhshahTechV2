using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class ststys1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceStatusId",
                table: "SubscribtionInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceTypeId",
                table: "SubscribtionInvoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceStatusId",
                table: "SubscribtionInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceTypeId",
                table: "SubscribtionInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
