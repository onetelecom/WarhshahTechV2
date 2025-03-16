using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class WarshahSpecialist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "WarshahType");

            migrationBuilder.AddColumn<int>(
                name: "WarshahFixedTypeId",
                table: "WarshahType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServiceWarshahs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    warshahServiceTypeID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ServiceWarshahs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceWarshahs_WarshahServiceType_warshahServiceTypeID",
                        column: x => x.warshahServiceTypeID,
                        principalTable: "WarshahServiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarshahFixedTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameType = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_WarshahFixedTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarshahModelsCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    MotorModelId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_WarshahModelsCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarshahModelsCars_MotorModels_MotorModelId",
                        column: x => x.MotorModelId,
                        principalTable: "MotorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarshahType_WarshahFixedTypeId",
                table: "WarshahType",
                column: "WarshahFixedTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceWarshahs_warshahServiceTypeID",
                table: "ServiceWarshahs",
                column: "warshahServiceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_WarshahModelsCars_MotorModelId",
                table: "WarshahModelsCars",
                column: "MotorModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarshahType_WarshahFixedTypes_WarshahFixedTypeId",
                table: "WarshahType",
                column: "WarshahFixedTypeId",
                principalTable: "WarshahFixedTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarshahType_WarshahFixedTypes_WarshahFixedTypeId",
                table: "WarshahType");

            migrationBuilder.DropTable(
                name: "ServiceWarshahs");

            migrationBuilder.DropTable(
                name: "WarshahFixedTypes");

            migrationBuilder.DropTable(
                name: "WarshahModelsCars");

            migrationBuilder.DropIndex(
                name: "IX_WarshahType_WarshahFixedTypeId",
                table: "WarshahType");

            migrationBuilder.DropColumn(
                name: "WarshahFixedTypeId",
                table: "WarshahType");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WarshahType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
