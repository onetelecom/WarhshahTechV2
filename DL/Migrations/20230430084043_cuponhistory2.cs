using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class cuponhistory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "warshahId",
                table: "CuponHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "warshahId",
                table: "CuponHistories");
        }
    }
}
