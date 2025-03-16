using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class Two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaseerSuppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_TaseerSuppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SparePartTaseers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategorySparePartsId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryPartsId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    TaseerSupplierId = table.Column<int>(type: "int", nullable: true),
                    MotorYearId = table.Column<int>(type: "int", nullable: false),
                    MotorMakeId = table.Column<int>(type: "int", nullable: false),
                    MotorModelId = table.Column<int>(type: "int", nullable: false),
                    SparePartName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PartDescribtion = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    PartNum = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PeacePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_SparePartTaseers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SparePartTaseers_CategorySpareParts_CategorySparePartsId",
                        column: x => x.CategorySparePartsId,
                        principalTable: "CategorySpareParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SparePartTaseers_MotorMakes_MotorMakeId",
                        column: x => x.MotorMakeId,
                        principalTable: "MotorMakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SparePartTaseers_MotorModels_MotorModelId",
                        column: x => x.MotorModelId,
                        principalTable: "MotorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SparePartTaseers_MotorYears_MotorYearId",
                        column: x => x.MotorYearId,
                        principalTable: "MotorYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SparePartTaseers_SubCategoryParts_SubCategoryPartsId",
                        column: x => x.SubCategoryPartsId,
                        principalTable: "SubCategoryParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SparePartTaseers_TaseerSuppliers_TaseerSupplierId",
                        column: x => x.TaseerSupplierId,
                        principalTable: "TaseerSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SparePartTaseers_CategorySparePartsId",
                table: "SparePartTaseers",
                column: "CategorySparePartsId");

            migrationBuilder.CreateIndex(
                name: "IX_SparePartTaseers_MotorMakeId",
                table: "SparePartTaseers",
                column: "MotorMakeId");

            migrationBuilder.CreateIndex(
                name: "IX_SparePartTaseers_MotorModelId",
                table: "SparePartTaseers",
                column: "MotorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SparePartTaseers_MotorYearId",
                table: "SparePartTaseers",
                column: "MotorYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SparePartTaseers_SubCategoryPartsId",
                table: "SparePartTaseers",
                column: "SubCategoryPartsId");

            migrationBuilder.CreateIndex(
                name: "IX_SparePartTaseers_TaseerSupplierId",
                table: "SparePartTaseers",
                column: "TaseerSupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SparePartTaseers");

            migrationBuilder.DropTable(
                name: "TaseerSuppliers");
        }
    }
}
