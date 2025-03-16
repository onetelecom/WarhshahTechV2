using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class fdfds3243 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionsTodays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    TransactionTypeID = table.Column<int>(type: "int", nullable: false),
                    TransactionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeforeVat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_TransactionsTodays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionsTodays_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsTodays_WarshahId",
                table: "TransactionsTodays",
                column: "WarshahId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionsTodays");
        }
    }
}
