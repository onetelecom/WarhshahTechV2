using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class fdsf7890 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DiscountPoint",
                table: "ServiceInvoices",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DiscPoint",
                table: "RepairOrders",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPoint",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DiscountPoint",
                table: "InspectionWarshahReports",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DiscountPoint",
                table: "DebitAndCreditors",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPoint",
                table: "ServiceInvoices");

            migrationBuilder.DropColumn(
                name: "DiscPoint",
                table: "RepairOrders");

            migrationBuilder.DropColumn(
                name: "DiscountPoint",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DiscountPoint",
                table: "InspectionWarshahReports");

            migrationBuilder.DropColumn(
                name: "DiscountPoint",
                table: "DebitAndCreditors");
        }
    }
}
