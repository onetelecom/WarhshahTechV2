using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class oldinv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OldInvoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceSerial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    InvoiceStatusId = table.Column<int>(type: "int", nullable: false),
                    InvoiceTypeId = table.Column<int>(type: "int", nullable: false),
                    RepairOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RepairOrderId1 = table.Column<int>(type: "int", nullable: true),
                    CarOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarOwnerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarOwnerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarOwnerCivilId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KMOut = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FastServiceOrderId = table.Column<int>(type: "int", nullable: false),
                    TechReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReciptionOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReciptionOrderId1 = table.Column<int>(type: "int", nullable: true),
                    KMIn = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CarType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FixingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BeforeDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvancePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RemainAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WarshahId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    CarOwnerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OldInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OldInvoices_ReciptionOrders_ReciptionOrderId1",
                        column: x => x.ReciptionOrderId1,
                        principalTable: "ReciptionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OldInvoices_RepairOrders_RepairOrderId1",
                        column: x => x.RepairOrderId1,
                        principalTable: "RepairOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OldInvoices_ReciptionOrderId1",
                table: "OldInvoices",
                column: "ReciptionOrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_OldInvoices_RepairOrderId1",
                table: "OldInvoices",
                column: "RepairOrderId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OldInvoices");
        }
    }
}
