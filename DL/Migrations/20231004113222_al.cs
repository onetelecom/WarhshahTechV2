using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class al : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarhshahCondition",
                table: "PriceOffers");

            migrationBuilder.DropColumn(
                name: "WarshahAddress",
                table: "PriceOffers");

            migrationBuilder.DropColumn(
                name: "WarshahCR",
                table: "PriceOffers");

            migrationBuilder.DropColumn(
                name: "WarshahCity",
                table: "PriceOffers");

            migrationBuilder.DropColumn(
                name: "WarshahDescrit",
                table: "PriceOffers");

            migrationBuilder.DropColumn(
                name: "WarshahName",
                table: "PriceOffers");

            migrationBuilder.DropColumn(
                name: "WarshahPhone",
                table: "PriceOffers");

            migrationBuilder.DropColumn(
                name: "WarshahPostCode",
                table: "PriceOffers");

            migrationBuilder.DropColumn(
                name: "WarshahStreet",
                table: "PriceOffers");

            migrationBuilder.DropColumn(
                name: "WarshahTaxNumber",
                table: "PriceOffers");

            migrationBuilder.CreateIndex(
                name: "IX_PriceOffers_WarshahId",
                table: "PriceOffers",
                column: "WarshahId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceOffers_Warshah_WarshahId",
                table: "PriceOffers",
                column: "WarshahId",
                principalTable: "Warshah",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceOffers_Warshah_WarshahId",
                table: "PriceOffers");

            migrationBuilder.DropIndex(
                name: "IX_PriceOffers_WarshahId",
                table: "PriceOffers");

            migrationBuilder.AddColumn<string>(
                name: "WarhshahCondition",
                table: "PriceOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarshahAddress",
                table: "PriceOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarshahCR",
                table: "PriceOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarshahCity",
                table: "PriceOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarshahDescrit",
                table: "PriceOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarshahName",
                table: "PriceOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarshahPhone",
                table: "PriceOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarshahPostCode",
                table: "PriceOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarshahStreet",
                table: "PriceOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarshahTaxNumber",
                table: "PriceOffers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
