using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class SalesRequstAllNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MotorId",
                table: "SalesRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "FromWarshah",
                table: "SalesRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Make",
                table: "SalesRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "SalesRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "SalesRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromWarshah",
                table: "SalesRequests");

            migrationBuilder.DropColumn(
                name: "Make",
                table: "SalesRequests");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "SalesRequests");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "SalesRequests");

            migrationBuilder.AlterColumn<int>(
                name: "MotorId",
                table: "SalesRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
