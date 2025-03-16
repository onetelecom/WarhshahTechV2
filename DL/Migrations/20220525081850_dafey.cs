using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class dafey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequests_SparePartTaseers_SparePartTaseerId",
                table: "SalesRequests");

            migrationBuilder.AlterColumn<int>(
                name: "SparePartTaseerId",
                table: "SalesRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequests_SparePartTaseers_SparePartTaseerId",
                table: "SalesRequests",
                column: "SparePartTaseerId",
                principalTable: "SparePartTaseers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequests_SparePartTaseers_SparePartTaseerId",
                table: "SalesRequests");

            migrationBuilder.AlterColumn<int>(
                name: "SparePartTaseerId",
                table: "SalesRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequests_SparePartTaseers_SparePartTaseerId",
                table: "SalesRequests",
                column: "SparePartTaseerId",
                principalTable: "SparePartTaseers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
