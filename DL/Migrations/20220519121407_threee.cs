using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class threee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartNumber",
                table: "SalesRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartNumber",
                table: "SalesRequests");
        }
    }
}
