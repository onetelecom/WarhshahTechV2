using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class yy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OthersAllowances",
                table: "ItemSalaries",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OthersAllowances",
                table: "ItemSalaries");
        }
    }
}
