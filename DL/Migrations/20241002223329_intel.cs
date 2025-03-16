using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class intel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntelCardCode",
                table: "SubscribtionInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntelCardCode",
                table: "SubscribtionInvoices");
        }
    }
}
