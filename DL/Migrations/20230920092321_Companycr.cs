using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class Companycr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Companycr",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Companycr",
                table: "User");
        }
    }
}
