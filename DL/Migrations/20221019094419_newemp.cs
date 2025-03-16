using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class newemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Role_RoleId",
                table: "DataEmployees");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Role_RoleId",
                table: "DataEmployees",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Role_RoleId",
                table: "DataEmployees");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Role_RoleId",
                table: "DataEmployees",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
