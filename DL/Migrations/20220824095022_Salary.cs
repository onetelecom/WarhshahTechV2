using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class Salary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtherAllowances",
                table: "DataEmployees",
                newName: "Installment");

            migrationBuilder.RenameColumn(
                name: "Deductions",
                table: "DataEmployees",
                newName: "HouseAllowances");

            migrationBuilder.AddColumn<decimal>(
                name: "Absence",
                table: "DataEmployees",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BonusTechnicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    BonusPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_BonusTechnicals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemSalaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    DataEmployeeId = table.Column<int>(type: "int", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Transportation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HouseAllowances = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalBenifites = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Absence = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Installment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OtherDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    table.PrimaryKey("PK_ItemSalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSalaries_DataEmployees_DataEmployeeId",
                        column: x => x.DataEmployeeId,
                        principalTable: "DataEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSalaries_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemSalaries_DataEmployeeId",
                table: "ItemSalaries",
                column: "DataEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSalaries_WarshahId",
                table: "ItemSalaries",
                column: "WarshahId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusTechnicals");

            migrationBuilder.DropTable(
                name: "ItemSalaries");

            migrationBuilder.DropColumn(
                name: "Absence",
                table: "DataEmployees");

            migrationBuilder.RenameColumn(
                name: "Installment",
                table: "DataEmployees",
                newName: "OtherAllowances");

            migrationBuilder.RenameColumn(
                name: "HouseAllowances",
                table: "DataEmployees",
                newName: "Deductions");
        }
    }
}
