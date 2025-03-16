using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class sdeer234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SalesCode",
                table: "Warshah",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesCode",
                table: "Warshah");
        }
    }
}
