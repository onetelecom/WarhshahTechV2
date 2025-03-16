using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class salesList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MotorId",
                table: "SalesRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ROID",
                table: "SalesRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesRequestListId",
                table: "SalesRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SalesRequestLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    SalesId = table.Column<int>(type: "int", nullable: true),
                    Describtion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesRequestLists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesRequests_SalesRequestListId",
                table: "SalesRequests",
                column: "SalesRequestListId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequests_SalesRequestLists_SalesRequestListId",
                table: "SalesRequests",
                column: "SalesRequestListId",
                principalTable: "SalesRequestLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequests_SalesRequestLists_SalesRequestListId",
                table: "SalesRequests");

            migrationBuilder.DropTable(
                name: "SalesRequestLists");

            migrationBuilder.DropIndex(
                name: "IX_SalesRequests_SalesRequestListId",
                table: "SalesRequests");

            migrationBuilder.DropColumn(
                name: "MotorId",
                table: "SalesRequests");

            migrationBuilder.DropColumn(
                name: "ROID",
                table: "SalesRequests");

            migrationBuilder.DropColumn(
                name: "SalesRequestListId",
                table: "SalesRequests");
        }
    }
}
