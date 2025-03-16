using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class smsfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SMSHistorys_User_UserId",
                table: "SMSHistorys");

            migrationBuilder.DropIndex(
                name: "IX_SMSHistorys_UserId",
                table: "SMSHistorys");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SMSHistorys");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "SMSHistorys",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "SMSHistorys");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SMSHistorys",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SMSHistorys_UserId",
                table: "SMSHistorys",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SMSHistorys_User_UserId",
                table: "SMSHistorys",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
