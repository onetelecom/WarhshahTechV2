using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class ded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrevInvoiceHash",
                table: "Warshah",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostCodeCompany",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitNum",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrevInvoiceHash",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QRCode",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaxStatus",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeTaxInvoice",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UUID",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_CityId",
                table: "User",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RegionId",
                table: "User",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Cities_CityId",
                table: "User",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Regions_RegionId",
                table: "User",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Cities_CityId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Regions_RegionId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CityId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RegionId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PrevInvoiceHash",
                table: "Warshah");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PostCodeCompany",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UnitNum",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PrevInvoiceHash",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "QRCode",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TaxStatus",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TypeTaxInvoice",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Invoices");
        }
    }
}
