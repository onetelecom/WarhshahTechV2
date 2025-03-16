using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class fd345d : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayFrom",
                table: "WarshahShift");

            migrationBuilder.DropColumn(
                name: "DayTo",
                table: "WarshahShift");

            migrationBuilder.DropColumn(
                name: "HourFrom",
                table: "WarshahShift");

            migrationBuilder.RenameColumn(
                name: "HourTo",
                table: "WarshahShift",
                newName: "ShiftName");

            migrationBuilder.CreateTable(
                name: "WorkTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    HourFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahShiftId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_WorkTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkTimes_WarshahShift_WarshahShiftId",
                        column: x => x.WarshahShiftId,
                        principalTable: "WarshahShift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkTimes_WarshahShiftId",
                table: "WorkTimes",
                column: "WarshahShiftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkTimes");

            migrationBuilder.RenameColumn(
                name: "ShiftName",
                table: "WarshahShift",
                newName: "HourTo");

            migrationBuilder.AddColumn<string>(
                name: "DayFrom",
                table: "WarshahShift",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DayTo",
                table: "WarshahShift",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HourFrom",
                table: "WarshahShift",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
