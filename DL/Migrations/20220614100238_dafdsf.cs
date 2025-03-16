using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class dafdsf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscFixingMoney",
                table: "RepairOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscSpareMoney",
                table: "RepairOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatFixingMoney",
                table: "RepairOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatSpareMoney",
                table: "RepairOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscFixingMoney",
                table: "RepairOrders");

            migrationBuilder.DropColumn(
                name: "DiscSpareMoney",
                table: "RepairOrders");

            migrationBuilder.DropColumn(
                name: "VatFixingMoney",
                table: "RepairOrders");

            migrationBuilder.DropColumn(
                name: "VatSpareMoney",
                table: "RepairOrders");
        }
    }
}
