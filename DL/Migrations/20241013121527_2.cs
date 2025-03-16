using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntelCardCode",
                table: "SubscribtionInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceTypeId",
                table: "SubscribtionInvoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntelCardCode",
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
