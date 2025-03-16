using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class warshahParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentWarshahId",
                table: "Warshah",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentWarshahId",
                table: "Warshah");
        }
    }
}
