using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class dataemployees1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Cities_CityId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_ContractTypes_ContractTypeId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Countries_CountryId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_EmployeeShifts_EmployeeShiftId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Genders_GenderId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_MaritalStatuses_MaritalStatusId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Nationalities_NationalityId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Regions_RegionId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_StatusEmployments_StatusEmploymentId",
                table: "DataEmployees");

            migrationBuilder.AlterColumn<int>(
                name: "WorkingDays",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Transportation",
                table: "DataEmployees",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "StatusEmploymentId",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PassportIssueDate",
                table: "DataEmployees",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PassportEndDate",
                table: "DataEmployees",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherAllowances",
                table: "DataEmployees",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "NationalityId",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatusId",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IdCardStart",
                table: "DataEmployees",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IdCardEnd",
                table: "DataEmployees",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HiringDate",
                table: "DataEmployees",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "ExcludedAttendance",
                table: "DataEmployees",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeShiftId",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Deductions",
                table: "DataEmployees",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContractTypeId",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ContractEndDate",
                table: "DataEmployees",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ChildrerNumber",
                table: "DataEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasicSalary",
                table: "DataEmployees",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Cities_CityId",
                table: "DataEmployees",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_ContractTypes_ContractTypeId",
                table: "DataEmployees",
                column: "ContractTypeId",
                principalTable: "ContractTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Countries_CountryId",
                table: "DataEmployees",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_EmployeeShifts_EmployeeShiftId",
                table: "DataEmployees",
                column: "EmployeeShiftId",
                principalTable: "EmployeeShifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Genders_GenderId",
                table: "DataEmployees",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_MaritalStatuses_MaritalStatusId",
                table: "DataEmployees",
                column: "MaritalStatusId",
                principalTable: "MaritalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Nationalities_NationalityId",
                table: "DataEmployees",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Regions_RegionId",
                table: "DataEmployees",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_StatusEmployments_StatusEmploymentId",
                table: "DataEmployees",
                column: "StatusEmploymentId",
                principalTable: "StatusEmployments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Cities_CityId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_ContractTypes_ContractTypeId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Countries_CountryId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_EmployeeShifts_EmployeeShiftId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Genders_GenderId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_MaritalStatuses_MaritalStatusId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Nationalities_NationalityId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_Regions_RegionId",
                table: "DataEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEmployees_StatusEmployments_StatusEmploymentId",
                table: "DataEmployees");

            migrationBuilder.AlterColumn<int>(
                name: "WorkingDays",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Transportation",
                table: "DataEmployees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusEmploymentId",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PassportIssueDate",
                table: "DataEmployees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PassportEndDate",
                table: "DataEmployees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherAllowances",
                table: "DataEmployees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NationalityId",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatusId",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "IdCardStart",
                table: "DataEmployees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "IdCardEnd",
                table: "DataEmployees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HiringDate",
                table: "DataEmployees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ExcludedAttendance",
                table: "DataEmployees",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeShiftId",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Deductions",
                table: "DataEmployees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContractTypeId",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ContractEndDate",
                table: "DataEmployees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChildrerNumber",
                table: "DataEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BasicSalary",
                table: "DataEmployees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Cities_CityId",
                table: "DataEmployees",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_ContractTypes_ContractTypeId",
                table: "DataEmployees",
                column: "ContractTypeId",
                principalTable: "ContractTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Countries_CountryId",
                table: "DataEmployees",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_EmployeeShifts_EmployeeShiftId",
                table: "DataEmployees",
                column: "EmployeeShiftId",
                principalTable: "EmployeeShifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Genders_GenderId",
                table: "DataEmployees",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_MaritalStatuses_MaritalStatusId",
                table: "DataEmployees",
                column: "MaritalStatusId",
                principalTable: "MaritalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Nationalities_NationalityId",
                table: "DataEmployees",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_Regions_RegionId",
                table: "DataEmployees",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEmployees_StatusEmployments_StatusEmploymentId",
                table: "DataEmployees",
                column: "StatusEmploymentId",
                principalTable: "StatusEmployments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
