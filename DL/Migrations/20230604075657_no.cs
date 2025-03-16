using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class no : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationRepairOrders_NameNotifications_NameNotificationId",
                table: "NotificationRepairOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationRepairOrders_NotificationRepairOrderAddings_NameNotificationId",
                table: "NotificationRepairOrders",
                column: "NameNotificationId",
                principalTable: "NotificationRepairOrderAddings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationRepairOrders_NotificationRepairOrderAddings_NameNotificationId",
                table: "NotificationRepairOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationRepairOrders_NameNotifications_NameNotificationId",
                table: "NotificationRepairOrders",
                column: "NameNotificationId",
                principalTable: "NameNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
