using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class bonus255 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarshahId",
                table: "RecordBonusTechnicals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "FixingPrice",
                table: "DebitAndCreditors",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecordBonusTechnicals_WarshahId",
                table: "RecordBonusTechnicals",
                column: "WarshahId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordBonusTechnicals_Warshah_WarshahId",
                table: "RecordBonusTechnicals",
                column: "WarshahId",
                principalTable: "Warshah",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordBonusTechnicals_Warshah_WarshahId",
                table: "RecordBonusTechnicals");

            migrationBuilder.DropIndex(
                name: "IX_RecordBonusTechnicals_WarshahId",
                table: "RecordBonusTechnicals");

            migrationBuilder.DropColumn(
                name: "WarshahId",
                table: "RecordBonusTechnicals");

            migrationBuilder.DropColumn(
                name: "FixingPrice",
                table: "DebitAndCreditors");
        }
    }
}
