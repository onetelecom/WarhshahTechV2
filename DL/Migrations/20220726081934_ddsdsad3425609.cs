using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class ddsdsad3425609 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Durations_Subscribtions_SubscribtionId",
                table: "Durations");

            migrationBuilder.AlterColumn<int>(
                name: "SubscribtionId",
                table: "Durations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Durations_Subscribtions_SubscribtionId",
                table: "Durations",
                column: "SubscribtionId",
                principalTable: "Subscribtions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Durations_Subscribtions_SubscribtionId",
                table: "Durations");

            migrationBuilder.AlterColumn<int>(
                name: "SubscribtionId",
                table: "Durations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Durations_Subscribtions_SubscribtionId",
                table: "Durations",
                column: "SubscribtionId",
                principalTable: "Subscribtions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
