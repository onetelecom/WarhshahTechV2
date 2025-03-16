using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class sfadsf987612 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OpenBalance",
                table: "WarshahBanks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenBalance",
                table: "WarshahBanks");
        }
    }
}
