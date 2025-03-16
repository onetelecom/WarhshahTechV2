using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class oldcre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OldCreatedon",
                table: "OldInvoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldCreatedon",
                table: "OldInvoices");
        }
    }
}
