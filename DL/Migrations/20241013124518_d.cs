using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class d : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntelCardCode",
                table: "SubscribtionInvoices",
                type: "int",
                nullable: false,
                defaultValue: 521);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceTypeId",
                table: "SubscribtionInvoices",
                type: "int",
                nullable: false,
                defaultValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntelCardCode",
                table: "SubscribtionInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceTypeId",
                table: "SubscribtionInvoices");
        }
    }
}
