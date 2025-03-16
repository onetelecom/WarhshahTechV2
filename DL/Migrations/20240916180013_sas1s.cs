using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class sas1s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocUUID",
                table: "DebitAndCreditors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrevInvoiceHash",
                table: "DebitAndCreditors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QRCode",
                table: "DebitAndCreditors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxErrorCode",
                table: "DebitAndCreditors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaxStatus",
                table: "DebitAndCreditors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeTaxInvoice",
                table: "DebitAndCreditors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocUUID",
                table: "DebitAndCreditors");

            migrationBuilder.DropColumn(
                name: "PrevInvoiceHash",
                table: "DebitAndCreditors");

            migrationBuilder.DropColumn(
                name: "QRCode",
                table: "DebitAndCreditors");

            migrationBuilder.DropColumn(
                name: "TaxErrorCode",
                table: "DebitAndCreditors");

            migrationBuilder.DropColumn(
                name: "TaxStatus",
                table: "DebitAndCreditors");

            migrationBuilder.DropColumn(
                name: "TypeTaxInvoice",
                table: "DebitAndCreditors");
        }
    }
}
