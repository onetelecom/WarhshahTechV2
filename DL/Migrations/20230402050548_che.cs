using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class che : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileClient",
                table: "Cheques",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayFor",
                table: "Cheques",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileClient",
                table: "Cheques");

            migrationBuilder.DropColumn(
                name: "PayFor",
                table: "Cheques");
        }
    }
}
