using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class Disocunt222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountPercentage",
                table: "PersonDiscounts",
                newName: "DiscountPercentageForSpareParts");

            migrationBuilder.AddColumn<decimal>(
                name: "MarginPercent",
                table: "SpareParts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercentageForFixingPrice",
                table: "PersonDiscounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarginPercent",
                table: "SpareParts");

            migrationBuilder.DropColumn(
                name: "DiscountPercentageForFixingPrice",
                table: "PersonDiscounts");

            migrationBuilder.RenameColumn(
                name: "DiscountPercentageForSpareParts",
                table: "PersonDiscounts",
                newName: "DiscountPercentage");
        }
    }
}
