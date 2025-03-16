using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class ser5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WarshahName",
                table: "WarshahTechServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarshahPhone",
                table: "WarshahTechServices",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarshahName",
                table: "WarshahTechServices");

            migrationBuilder.DropColumn(
                name: "WarshahPhone",
                table: "WarshahTechServices");
        }
    }
}
