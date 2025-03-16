using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class offer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FixingPrice",
                table: "PriceOfferItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixingPrice",
                table: "PriceOfferItems");
        }
    }
}
