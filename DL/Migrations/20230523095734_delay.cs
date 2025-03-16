using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class delay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DelayRepairOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    DelayTime = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_DelayRepairOrders", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "PaymentTypeInvoices",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PaymentTypeNameAr", "PaymentTypeNameEn" },
                values: new object[] { "حوالات", " Transfer " });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelayRepairOrders");

            migrationBuilder.UpdateData(
                table: "PaymentTypeInvoices",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PaymentTypeNameAr", "PaymentTypeNameEn" },
                values: new object[] { "آخرى", " Other " });
        }
    }
}
