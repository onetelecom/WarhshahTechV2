using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class ba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarshahBankId",
                table: "ChequeBankAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChequeBankAccounts_WarshahBankId",
                table: "ChequeBankAccounts",
                column: "WarshahBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChequeBankAccounts_WarshahBanks_WarshahBankId",
                table: "ChequeBankAccounts",
                column: "WarshahBankId",
                principalTable: "WarshahBanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChequeBankAccounts_WarshahBanks_WarshahBankId",
                table: "ChequeBankAccounts");

            migrationBuilder.DropIndex(
                name: "IX_ChequeBankAccounts_WarshahBankId",
                table: "ChequeBankAccounts");

            migrationBuilder.DropColumn(
                name: "WarshahBankId",
                table: "ChequeBankAccounts");
        }
    }
}
