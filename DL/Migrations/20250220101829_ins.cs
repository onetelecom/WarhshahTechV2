using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class ins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstantParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: true),
                    SparePartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartDescribtion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BuyingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyBeforeVat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VatBuy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PeacePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatSell = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SellBeforeVat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    table.PrimaryKey("PK_InstantParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstantParts_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstantParts_WarshahId",
                table: "InstantParts",
                column: "WarshahId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstantParts");
        }
    }
}
