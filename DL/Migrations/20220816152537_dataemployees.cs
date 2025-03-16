using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class dataemployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    UserWarshahCode = table.Column<int>(type: "int", nullable: false),
                    EmployeeNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalityId = table.Column<int>(type: "int", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: false),
                    ChildrerNumber = table.Column<int>(type: "int", nullable: false),
                    SocialSecurity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalInsurance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCardStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCardEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportIssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassportEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusEmploymentId = table.Column<int>(type: "int", nullable: false),
                    EmployeeShiftId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    HiringDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkingDays = table.Column<int>(type: "int", nullable: false),
                    ContractTypeId = table.Column<int>(type: "int", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Transportation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherAllowances = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deductions = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AttendanceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExcludedAttendance = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_DataEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataEmployees_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataEmployees_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataEmployees_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataEmployees_EmployeeShifts_EmployeeShiftId",
                        column: x => x.EmployeeShiftId,
                        principalTable: "EmployeeShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataEmployees_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataEmployees_MaritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataEmployees_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataEmployees_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataEmployees_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataEmployees_StatusEmployments_StatusEmploymentId",
                        column: x => x.StatusEmploymentId,
                        principalTable: "StatusEmployments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataEmployees_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_CityId",
                table: "DataEmployees",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_ContractTypeId",
                table: "DataEmployees",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_CountryId",
                table: "DataEmployees",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_EmployeeShiftId",
                table: "DataEmployees",
                column: "EmployeeShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_GenderId",
                table: "DataEmployees",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_MaritalStatusId",
                table: "DataEmployees",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_NationalityId",
                table: "DataEmployees",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_RegionId",
                table: "DataEmployees",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_RoleId",
                table: "DataEmployees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_StatusEmploymentId",
                table: "DataEmployees",
                column: "StatusEmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEmployees_WarshahId",
                table: "DataEmployees",
                column: "WarshahId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataEmployees");
        }
    }
}
