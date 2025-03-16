using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class g : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusNotificationId",
                table: "NotificationSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusNotificationId",
                table: "NotificationSettings");
        }
    }
}
