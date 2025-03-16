using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class ddf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOld",
                table: "Warshah",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "OldWarshahId",
                table: "Warshah",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOld",
                table: "Warshah");

            migrationBuilder.DropColumn(
                name: "OldWarshahId",
                table: "Warshah");
        }
    }
}
