using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class fd345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PartNum",
                table: "SpareParts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<decimal>(
                name: "BuyBeforeVat",
                table: "SpareParts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SellBeforeVat",
                table: "SpareParts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VatBuy",
                table: "SpareParts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VatSell",
                table: "SpareParts",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyBeforeVat",
                table: "SpareParts");

            migrationBuilder.DropColumn(
                name: "SellBeforeVat",
                table: "SpareParts");

            migrationBuilder.DropColumn(
                name: "VatBuy",
                table: "SpareParts");

            migrationBuilder.DropColumn(
                name: "VatSell",
                table: "SpareParts");

            migrationBuilder.AlterColumn<string>(
                name: "PartNum",
                table: "SpareParts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
