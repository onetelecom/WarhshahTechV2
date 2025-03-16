using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class olditeminv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OldInvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OldInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SparePartNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SparePartNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PeacePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Garuntee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_OldInvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OldInvoiceItems_OldInvoices_OldInvoiceId",
                        column: x => x.OldInvoiceId,
                        principalTable: "OldInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OldInvoiceItems_OldInvoiceId",
                table: "OldInvoiceItems",
                column: "OldInvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OldInvoiceItems");
        }
    }
}
