using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class salesListse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequests_SalesRequestLists_SalesRequestListId",
                table: "SalesRequests");

            migrationBuilder.AlterColumn<int>(
                name: "SalesRequestListId",
                table: "SalesRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequests_SalesRequestLists_SalesRequestListId",
                table: "SalesRequests",
                column: "SalesRequestListId",
                principalTable: "SalesRequestLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequests_SalesRequestLists_SalesRequestListId",
                table: "SalesRequests");

            migrationBuilder.AlterColumn<int>(
                name: "SalesRequestListId",
                table: "SalesRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequests_SalesRequestLists_SalesRequestListId",
                table: "SalesRequests",
                column: "SalesRequestListId",
                principalTable: "SalesRequestLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
