using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class requesttypeworktype2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "NameEn",
                value: "General Inquire");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "NameEn",
                value: "general Inquire");

            migrationBuilder.InsertData(
                table: "WorkTypes",
                columns: new[] { "Id", "ActionCode", "CreatedBy", "CreatedOn", "Describtion", "IsActive", "IsDeleted", "NameAr", "NameEn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 3, "generalInquire", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "إستفسار عام", "general Inquire", null, null });
        }
    }
}
