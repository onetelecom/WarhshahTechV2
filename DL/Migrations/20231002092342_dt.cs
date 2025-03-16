using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class dt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Companycr",
                table: "User");

            migrationBuilder.CreateTable(
                name: "PriceOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferNumber = table.Column<int>(type: "int", nullable: false),
                    CarOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarOwnerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarOwnerTaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarOwnerCR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarOwnerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarOwnerID = table.Column<int>(type: "int", nullable: true),
                    CarType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarPlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotorColorId = table.Column<int>(type: "int", nullable: true),
                    MotorMakeId = table.Column<int>(type: "int", nullable: true),
                    MotorModelId = table.Column<int>(type: "int", nullable: true),
                    MotorYearId = table.Column<int>(type: "int", nullable: true),
                    FixingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BeforeDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    WarshahName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahCR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahTaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahDescrit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahPostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarhshahCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_PriceOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceOffers_MotorColors_MotorColorId",
                        column: x => x.MotorColorId,
                        principalTable: "MotorColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceOffers_MotorMakes_MotorMakeId",
                        column: x => x.MotorMakeId,
                        principalTable: "MotorMakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceOffers_MotorModels_MotorModelId",
                        column: x => x.MotorModelId,
                        principalTable: "MotorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceOffers_MotorYears_MotorYearId",
                        column: x => x.MotorYearId,
                        principalTable: "MotorYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceOffers_MotorColorId",
                table: "PriceOffers",
                column: "MotorColorId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceOffers_MotorMakeId",
                table: "PriceOffers",
                column: "MotorMakeId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceOffers_MotorModelId",
                table: "PriceOffers",
                column: "MotorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceOffers_MotorYearId",
                table: "PriceOffers",
                column: "MotorYearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceOffers");

            migrationBuilder.AddColumn<string>(
                name: "Companycr",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
