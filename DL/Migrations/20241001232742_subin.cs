using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class subin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocUUID",
                table: "SubscribtionInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrevInvoiceHash",
                table: "SubscribtionInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QRCode",
                table: "SubscribtionInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxErrorCode",
                table: "SubscribtionInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaxStatus",
                table: "SubscribtionInvoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeTaxInvoice",
                table: "SubscribtionInvoices",
                type: "nvarchar(max)",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "DocUUID",
                table: "SubscribtionInvoices");

            migrationBuilder.DropColumn(
                name: "PrevInvoiceHash",
                table: "SubscribtionInvoices");

            migrationBuilder.DropColumn(
                name: "QRCode",
                table: "SubscribtionInvoices");

            migrationBuilder.DropColumn(
                name: "TaxErrorCode",
                table: "SubscribtionInvoices");

            migrationBuilder.DropColumn(
                name: "TaxStatus",
                table: "SubscribtionInvoices");

            migrationBuilder.DropColumn(
                name: "TypeTaxInvoice",
                table: "SubscribtionInvoices");
        }
    }
}
