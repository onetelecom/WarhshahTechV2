using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class Disocunt22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AddedDiscount",
                table: "RepairOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDiscount",
                table: "RepairOrders");
        }
    }
}
