using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class dafe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SparePartTaseerId",
                table: "SalesRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalesRequests_SparePartTaseerId",
                table: "SalesRequests",
                column: "SparePartTaseerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequests_SparePartTaseers_SparePartTaseerId",
                table: "SalesRequests",
                column: "SparePartTaseerId",
                principalTable: "SparePartTaseers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequests_SparePartTaseers_SparePartTaseerId",
                table: "SalesRequests");

            migrationBuilder.DropIndex(
                name: "IX_SalesRequests_SparePartTaseerId",
                table: "SalesRequests");

            migrationBuilder.DropColumn(
                name: "SparePartTaseerId",
                table: "SalesRequests");
        }
    }
}
