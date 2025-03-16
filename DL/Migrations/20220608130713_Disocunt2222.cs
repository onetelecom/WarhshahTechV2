using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class Disocunt2222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddedDiscount",
                table: "RepairOrders",
                newName: "AddedDiscountForSpareParts");

            migrationBuilder.AddColumn<bool>(
                name: "AddedDiscountForFixingPrice",
                table: "RepairOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SparePartsTotal",
                table: "RepairOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDiscountForFixingPrice",
                table: "RepairOrders");

            migrationBuilder.DropColumn(
                name: "SparePartsTotal",
                table: "RepairOrders");

            migrationBuilder.RenameColumn(
                name: "AddedDiscountForSpareParts",
                table: "RepairOrders",
                newName: "AddedDiscount");
        }
    }
}
