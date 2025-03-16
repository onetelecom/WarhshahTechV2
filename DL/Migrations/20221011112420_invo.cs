using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class invo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FixPrice",
                table: "InvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NoDispote",
                table: "BoxBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BoxBanks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BoxBanks_UserId",
                table: "BoxBanks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoxBanks_User_UserId",
                table: "BoxBanks",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoxBanks_User_UserId",
                table: "BoxBanks");

            migrationBuilder.DropIndex(
                name: "IX_BoxBanks_UserId",
                table: "BoxBanks");

            migrationBuilder.DropColumn(
                name: "FixPrice",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "NoDispote",
                table: "BoxBanks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BoxBanks");
        }
    }
}
