using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class afddujbank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankNameAr",
                table: "WarshahBanks");

            migrationBuilder.RenameColumn(
                name: "BankNameEn",
                table: "WarshahBanks",
                newName: "AccountName");

            migrationBuilder.AddColumn<int>(
                name: "FixedBankId",
                table: "WarshahBanks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "KMOut",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "KMIn",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "FixedBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_FixedBanks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarshahBanks_FixedBankId",
                table: "WarshahBanks",
                column: "FixedBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarshahBanks_FixedBanks_FixedBankId",
                table: "WarshahBanks",
                column: "FixedBankId",
                principalTable: "FixedBanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarshahBanks_FixedBanks_FixedBankId",
                table: "WarshahBanks");

            migrationBuilder.DropTable(
                name: "FixedBanks");

            migrationBuilder.DropIndex(
                name: "IX_WarshahBanks_FixedBankId",
                table: "WarshahBanks");

            migrationBuilder.DropColumn(
                name: "FixedBankId",
                table: "WarshahBanks");

            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "WarshahBanks",
                newName: "BankNameEn");

            migrationBuilder.AddColumn<string>(
                name: "BankNameAr",
                table: "WarshahBanks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "KMOut",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "KMIn",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }
    }
}
