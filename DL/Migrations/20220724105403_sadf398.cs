using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class sadf398 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoofPoints",
                table: "LoyalitySettingRevarses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoofPoints",
                table: "LoyalitySettingRevarses");
        }
    }
}
