using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SparePartName",
                table: "TransactionInventories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SparePartName",
                table: "TransactionInventories");
        }
    }
}
