using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class fd345d3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceCategoryId",
                table: "ReciptionOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceCategoryId",
                table: "Invoices",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceCategoryId",
                table: "ReciptionOrders");

            migrationBuilder.DropColumn(
                name: "InvoiceCategoryId",
                table: "Invoices");
        }
    }
}
