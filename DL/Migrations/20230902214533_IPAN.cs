using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class IPAN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IPAN",
                table: "WarshahBanks",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IPAN",
                table: "WarshahBanks");
        }
    }
}
