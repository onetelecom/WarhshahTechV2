using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class rolehlr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Describtion", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 11, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Hr", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
