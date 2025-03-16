using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class branchsub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBranch",
                table: "Subscribtions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBranch",
                table: "Subscribtions");
        }
    }
}
