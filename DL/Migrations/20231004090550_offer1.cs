using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class offer1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarPlate",
                table: "PriceOffers",
                newName: "CarPlateNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarPlateNo",
                table: "PriceOffers",
                newName: "CarPlate");
        }
    }
}
