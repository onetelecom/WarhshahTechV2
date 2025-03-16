using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class dafdsfg54 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscFixingMoney",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscSpareMoney",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatFixingMoney",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatSpareMoney",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscFixingMoney",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DiscSpareMoney",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "VatFixingMoney",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "VatSpareMoney",
                table: "Invoices");
        }
    }
}
