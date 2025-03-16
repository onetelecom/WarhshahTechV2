using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class requesttypeworktype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Namear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_RequestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_WorkTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RequestTypes",
                columns: new[] { "Id", "ActionCode", "CreatedBy", "CreatedOn", "Describtion", "IsActive", "IsDeleted", "NameEn", "Namear", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "salesandsubscriptions", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Sales and subscriptions", " مبيعات واشتراكات ", null, null },
                    { 2, "techsupport", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Technical Support", "دعم فني", null, null },
                    { 3, "generalInquire", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "general Inquire", "إستفسار عام", null, null }
                });

            migrationBuilder.InsertData(
                table: "WorkTypes",
                columns: new[] { "Id", "ActionCode", "CreatedBy", "CreatedOn", "Describtion", "IsActive", "IsDeleted", "NameAr", "NameEn", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "WarshaOwner", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "  صاحب ورشة  ", " Warshah Owner", null, null },
                    { 2, "CarOwner", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, " مالك سيارة", " Car Owner", null, null },
                    { 3, "generalInquire", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "إستفسار عام", "general Inquire", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_WarshahId",
                table: "RepairOrders",
                column: "WarshahId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairOrders_Warshah_WarshahId",
                table: "RepairOrders",
                column: "WarshahId",
                principalTable: "Warshah",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairOrders_Warshah_WarshahId",
                table: "RepairOrders");

            migrationBuilder.DropTable(
                name: "RequestTypes");

            migrationBuilder.DropTable(
                name: "WorkTypes");

            migrationBuilder.DropIndex(
                name: "IX_RepairOrders_WarshahId",
                table: "RepairOrders");
        }
    }
}
