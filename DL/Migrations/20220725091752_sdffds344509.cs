using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class sdffds344509 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscPoint",
                table: "ServiceInvoices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscPoint",
                table: "DebitAndCreditors",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscPoint",
                table: "ServiceInvoices");

            migrationBuilder.DropColumn(
                name: "DiscPoint",
                table: "DebitAndCreditors");
        }
    }
}
