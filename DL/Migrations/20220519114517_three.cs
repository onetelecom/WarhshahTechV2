using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "SparePartTaseers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "SparePartTaseers",
                type: "int",
                nullable: true);
        }
    }
}
