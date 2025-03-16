using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class ASdk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimumRecordQuantity",
                table: "SpareParts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumRecordQuantity",
                table: "SpareParts");
        }
    }
}
