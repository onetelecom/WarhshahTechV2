using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class salesinv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceSerial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    InvoiceStatusId = table.Column<int>(type: "int", nullable: false),
                    SalesRequestListId = table.Column<int>(type: "int", nullable: true),
                    SparePartTaseerId = table.Column<int>(type: "int", nullable: true),
                    FixingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BeforeDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WarshahId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahCR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahTaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahDescrit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahPostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTypeInvoiceId = table.Column<int>(type: "int", nullable: false),
                    PaymentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceCategoryId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_SalesInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoices_PaymentTypeInvoices_PaymentTypeInvoiceId",
                        column: x => x.PaymentTypeInvoiceId,
                        principalTable: "PaymentTypeInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesInvoices_SalesRequestLists_SalesRequestListId",
                        column: x => x.SalesRequestListId,
                        principalTable: "SalesRequestLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesInvoices_SparePartTaseers_SparePartTaseerId",
                        column: x => x.SparePartTaseerId,
                        principalTable: "SparePartTaseers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_PaymentTypeInvoiceId",
                table: "SalesInvoices",
                column: "PaymentTypeInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_SalesRequestListId",
                table: "SalesInvoices",
                column: "SalesRequestListId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_SparePartTaseerId",
                table: "SalesInvoices",
                column: "SparePartTaseerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesInvoices");
        }
    }
}
