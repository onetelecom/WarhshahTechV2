using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class emp2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobTitleId",
                table: "DataEmployees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_JobTitleId",
                table: "DataEmployees",
                column: "JobTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_JobTitles_JobTitleId",
                table: "DataEmployees",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_JobTitles_JobTitleId",
                table: "DataEmployees");

            migrationBuilder.DropIndex(
                name: "IX_DataEmployees_JobTitleId",
                table: "DataEmployees");

            migrationBuilder.DropColumn(
                name: "JobTitleId",
                table: "DataEmployees");
        }
    }
}
