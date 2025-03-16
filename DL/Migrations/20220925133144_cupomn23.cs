using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class cupomn23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubId",
                table: "Cupons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubId",
                table: "Cupons");
        }
    }
}
