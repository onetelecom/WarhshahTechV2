using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class sdeer2344 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WarshahIdentifier",
                table: "Warshah",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarshahIdentifier",
                table: "Warshah");
        }
    }
}
