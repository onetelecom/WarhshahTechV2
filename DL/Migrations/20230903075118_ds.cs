using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class ds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IPAN",
                table: "WarshahBanks",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IPAN",
                table: "WarshahBanks",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24,
                oldNullable: true);
        }
    }
}
