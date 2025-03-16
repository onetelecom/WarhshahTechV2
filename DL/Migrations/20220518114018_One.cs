using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class One : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Car = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategorySpareParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryNameAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CategoryNameEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_CategorySpareParts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReciverId = table.Column<int>(type: "int", nullable: false),
                    RepairOrderId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    GetCustomerAbroval = table.Column<bool>(type: "bit", nullable: false),
                    PeriodDayCustomerApprove = table.Column<int>(type: "int", nullable: false),
                    GetVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Configrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpensesCategoryNameAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExpensesCategoryNameEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_ExpensesCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ColorNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_MotorColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorMakes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakeNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MakeNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_MotorMakes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorYears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", maxLength: 6, nullable: false),
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
                    table.PrimaryKey("PK_MotorYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypeInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentTypeNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaymentTypeNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_PaymentTypeInvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairOrderImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepairOrderId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_RepairOrderImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
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
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarDescribtion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QTY = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    PartName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_SalesRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ServiceCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spicialists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Spicialists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaseerItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VinNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QTY = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DelivaryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoId = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_TaseerItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerfiyCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VirfeyCode = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerfiyCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warshah",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarshahNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Long = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LandLineNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distrect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitNum = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    WarshahLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Warshah", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarshahCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_WarshahCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarshahServiceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_WarshahServiceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarshahType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_WarshahType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoryParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryNameAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SubCategoryNameEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CategorySparePartsId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SubCategoryParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoryParts_CategorySpareParts_CategorySparePartsId",
                        column: x => x.CategorySparePartsId,
                        principalTable: "CategorySpareParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegionNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MotorModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModelNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MotorMakeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_MotorModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotorModels_MotorMakes_MotorMakeId",
                        column: x => x.MotorMakeId,
                        principalTable: "MotorMakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    ServiceCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_ServiceCategories_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "ServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BalanceBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_BalanceBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BalanceBanks_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    TotalIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Boxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boxes_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpensesTypeNameAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExpensesTypeNameEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ExpensesCategoryId = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_ExpensesTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpensesTypes_ExpensesCategories_ExpensesCategoryId",
                        column: x => x.ExpensesCategoryId,
                        principalTable: "ExpensesCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpensesTypes_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InspectionTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectionNameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    InspectionNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: true),
                    IsCommon = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_InspectionTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionTemplates_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentAndReceiptVouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNumber = table.Column<int>(type: "int", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeVoucher = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    AdvancePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentTypeInvoiceId = table.Column<int>(type: "int", nullable: false),
                    Discriptotion = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_PaymentAndReceiptVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentAndReceiptVouchers_PaymentTypeInvoices_PaymentTypeInvoiceId",
                        column: x => x.PaymentTypeInvoiceId,
                        principalTable: "PaymentTypeInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentAndReceiptVouchers_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommerialRegisterar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompany = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPhoneConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IdImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarshahBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_WarshahBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarshahBanks_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarshahParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    WarshahServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    SpicialistsId = table.Column<int>(type: "int", nullable: false),
                    WarshahTypeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_WarshahParams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarshahParams_Spicialists_SpicialistsId",
                        column: x => x.SpicialistsId,
                        principalTable: "Spicialists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarshahParams_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarshahParams_WarshahServiceType_WarshahServiceTypeId",
                        column: x => x.WarshahServiceTypeId,
                        principalTable: "WarshahServiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarshahParams_WarshahType_WarshahTypeId",
                        column: x => x.WarshahTypeId,
                        principalTable: "WarshahType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CityNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpensesCategoryId = table.Column<int>(type: "int", nullable: false),
                    ExpensesTypeId = table.Column<int>(type: "int", nullable: false),
                    TotalWithoutVat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpenseNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ExpensesTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpensesTransactions_ExpensesCategories_ExpensesCategoryId",
                        column: x => x.ExpensesCategoryId,
                        principalTable: "ExpensesCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpensesTransactions_ExpensesTypes_ExpensesTypeId",
                        column: x => x.ExpensesTypeId,
                        principalTable: "ExpensesTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpensesTransactions_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InspectionSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionNameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SectionNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    InspectionTemplateId = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: true),
                    IsCommon = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_InspectionSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionSections_InspectionTemplates_InspectionTemplateId",
                        column: x => x.InspectionTemplateId,
                        principalTable: "InspectionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InspectionSections_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthrizedPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AuthrizedPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthrizedPersons_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Motors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotorMakeId = table.Column<int>(type: "int", nullable: false),
                    MotorModelId = table.Column<int>(type: "int", nullable: false),
                    MotorYearId = table.Column<int>(type: "int", nullable: false),
                    MotorColorId = table.Column<int>(type: "int", nullable: false),
                    ChassisNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlateNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CarOwnerId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Motors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motors_MotorColors_MotorColorId",
                        column: x => x.MotorColorId,
                        principalTable: "MotorColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motors_MotorMakes_MotorMakeId",
                        column: x => x.MotorMakeId,
                        principalTable: "MotorMakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motors_MotorModels_MotorModelId",
                        column: x => x.MotorModelId,
                        principalTable: "MotorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motors_MotorYears_MotorYearId",
                        column: x => x.MotorYearId,
                        principalTable: "MotorYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motors_User_CarOwnerId",
                        column: x => x.CarOwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cheques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChequeNumber = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    WarshahBankId = table.Column<int>(type: "int", nullable: false),
                    ChequeStatus = table.Column<int>(type: "int", nullable: false),
                    TotalMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Cheques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cheques_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cheques_WarshahBanks_WarshahBankId",
                        column: x => x.WarshahBankId,
                        principalTable: "WarshahBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Distrect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suppliers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suppliers_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suppliers_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InspectionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemNameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ItemNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    InspectionSectionId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    WarshahId = table.Column<int>(type: "int", nullable: true),
                    IsCommon = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_InspectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionItems_InspectionSections_InspectionSectionId",
                        column: x => x.InspectionSectionId,
                        principalTable: "InspectionSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InspectionItems_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InspectionWarshahReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    CarOwnerId = table.Column<int>(type: "int", nullable: false),
                    MotorsId = table.Column<int>(type: "int", nullable: false),
                    PaymentInvoiceId = table.Column<int>(type: "int", nullable: false),
                    KM_IN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TechnicalID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_InspectionWarshahReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionWarshahReports_Motors_MotorsId",
                        column: x => x.MotorsId,
                        principalTable: "Motors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InspectionWarshahReports_User_CarOwnerId",
                        column: x => x.CarOwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReciptionOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarOwnerId = table.Column<int>(type: "int", nullable: false),
                    MotorsId = table.Column<int>(type: "int", nullable: false),
                    ReciptionId = table.Column<int>(type: "int", nullable: false),
                    TecnicanId = table.Column<int>(type: "int", nullable: false),
                    KM_In = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdvancePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CheckPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CarOwnerDescribtion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReciptionDescribtion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    warshahId = table.Column<int>(type: "int", nullable: false),
                    IsCheck = table.Column<bool>(type: "bit", nullable: false),
                    PaymentTypeInvoiceId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ReciptionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReciptionOrders_Motors_MotorsId",
                        column: x => x.MotorsId,
                        principalTable: "Motors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReciptionOrders_PaymentTypeInvoices_PaymentTypeInvoiceId",
                        column: x => x.PaymentTypeInvoiceId,
                        principalTable: "PaymentTypeInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReciptionOrders_User_CarOwnerId",
                        column: x => x.CarOwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReciptionOrders_User_ReciptionId",
                        column: x => x.ReciptionId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReciptionOrders_User_TecnicanId",
                        column: x => x.TecnicanId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BeforeDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    afterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MotorsId = table.Column<int>(type: "int", nullable: false),
                    PaymentTypeInvoiceId = table.Column<int>(type: "int", nullable: false),
                    TechId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ServiceInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceInvoices_Motors_MotorsId",
                        column: x => x.MotorsId,
                        principalTable: "Motors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInvoices_User_TechId",
                        column: x => x.TechId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategorySparePartsId = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryPartsId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    MotorYearId = table.Column<int>(type: "int", nullable: false),
                    MotorMakeId = table.Column<int>(type: "int", nullable: false),
                    MotorModelId = table.Column<int>(type: "int", nullable: false),
                    SparePartName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PartDescribtion = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    PartNum = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PeacePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_SpareParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpareParts_CategorySpareParts_CategorySparePartsId",
                        column: x => x.CategorySparePartsId,
                        principalTable: "CategorySpareParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpareParts_MotorMakes_MotorMakeId",
                        column: x => x.MotorMakeId,
                        principalTable: "MotorMakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpareParts_MotorModels_MotorModelId",
                        column: x => x.MotorModelId,
                        principalTable: "MotorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpareParts_MotorYears_MotorYearId",
                        column: x => x.MotorYearId,
                        principalTable: "MotorYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpareParts_SubCategoryParts_SubCategoryPartsId",
                        column: x => x.SubCategoryPartsId,
                        principalTable: "SubCategoryParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpareParts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpareParts_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InspectionReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectionWarshahReportId = table.Column<int>(type: "int", nullable: false),
                    ItemNameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ItemNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Excellent = table.Column<bool>(type: "bit", nullable: true),
                    VeryGood = table.Column<bool>(type: "bit", nullable: true),
                    Good = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_InspectionReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionReports_InspectionWarshahReports_InspectionWarshahReportId",
                        column: x => x.InspectionWarshahReportId,
                        principalTable: "InspectionWarshahReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptVouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNumber = table.Column<int>(type: "int", nullable: false),
                    ReciptionOrderId = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    AdvancePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentTypeInvoiceId = table.Column<int>(type: "int", nullable: false),
                    Discriptotion = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_ReceiptVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_PaymentTypeInvoices_PaymentTypeInvoiceId",
                        column: x => x.PaymentTypeInvoiceId,
                        principalTable: "PaymentTypeInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_ReciptionOrders_ReciptionOrderId",
                        column: x => x.ReciptionOrderId,
                        principalTable: "ReciptionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepairOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KMOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TechReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Garuntee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BeforeDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepairOrderStatus = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    InspectionTemplateId = table.Column<int>(type: "int", nullable: true),
                    TechId = table.Column<int>(type: "int", nullable: false),
                    ReciptionOrderId = table.Column<int>(type: "int", nullable: true),
                    InspectionWarshahReportId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_RepairOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairOrders_InspectionTemplates_InspectionTemplateId",
                        column: x => x.InspectionTemplateId,
                        principalTable: "InspectionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairOrders_InspectionWarshahReports_InspectionWarshahReportId",
                        column: x => x.InspectionWarshahReportId,
                        principalTable: "InspectionWarshahReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairOrders_ReciptionOrders_ReciptionOrderId",
                        column: x => x.ReciptionOrderId,
                        principalTable: "ReciptionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairOrders_User_TechId",
                        column: x => x.TechId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceInvoiceId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_ServiceInvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceItems_ServiceInvoices_ServiceInvoiceId",
                        column: x => x.ServiceInvoiceId,
                        principalTable: "ServiceInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceSerial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    InvoiceStatusId = table.Column<int>(type: "int", nullable: false),
                    InvoiceTypeId = table.Column<int>(type: "int", nullable: false),
                    RepairOrderId = table.Column<int>(type: "int", nullable: true),
                    CarOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarOwnerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarOwnerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarOwnerCivilId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KMOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FastServiceOrderId = table.Column<int>(type: "int", nullable: false),
                    TechReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReciptionOrderId = table.Column<int>(type: "int", nullable: true),
                    KMIn = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FixingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BeforeDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvancePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RemainAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    WarshahName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahCR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahTaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahDescrit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarshahPostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarhshahCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTypeInvoiceId = table.Column<int>(type: "int", nullable: true),
                    PaymentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InspectionWarshahReportId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_InspectionWarshahReports_InspectionWarshahReportId",
                        column: x => x.InspectionWarshahReportId,
                        principalTable: "InspectionWarshahReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_PaymentTypeInvoices_PaymentTypeInvoiceId",
                        column: x => x.PaymentTypeInvoiceId,
                        principalTable: "PaymentTypeInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_ReciptionOrders_ReciptionOrderId",
                        column: x => x.ReciptionOrderId,
                        principalTable: "ReciptionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_RepairOrders_RepairOrderId",
                        column: x => x.RepairOrderId,
                        principalTable: "RepairOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepairOrderSpareParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepairOrderId = table.Column<int>(type: "int", nullable: false),
                    SparePartId = table.Column<int>(type: "int", nullable: false),
                    TechId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PeacePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Garuntee = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_RepairOrderSpareParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairOrderSpareParts_RepairOrders_RepairOrderId",
                        column: x => x.RepairOrderId,
                        principalTable: "RepairOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairOrderSpareParts_SpareParts_SparePartId",
                        column: x => x.SparePartId,
                        principalTable: "SpareParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairOrderSpareParts_User_TechId",
                        column: x => x.TechId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepairOrderId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    HistoryBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_RoHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoHistories_RepairOrders_RepairOrderId",
                        column: x => x.RepairOrderId,
                        principalTable: "RepairOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditorNotices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    ReturnMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReturnVat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_CreditorNotices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditorNotices_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditorNotices_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DebitAndCreditors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    NoticeNumber = table.Column<int>(type: "int", nullable: false),
                    NoticeSerial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    Flag = table.Column<int>(type: "int", nullable: false),
                    TotalWithoutVat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_DebitAndCreditors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DebitAndCreditors_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DebitAndCreditors_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DebitNotices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    WarshahId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_DebitNotices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DebitNotices_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DebitNotices_Warshah_WarshahId",
                        column: x => x.WarshahId,
                        principalTable: "Warshah",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    SparePartNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SparePartNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PeacePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Garuntee = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NoticeProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DebitAndCreditorId = table.Column<int>(type: "int", nullable: false),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    SparePartNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SparePartNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PeacePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_NoticeProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoticeProducts_DebitAndCreditors_DebitAndCreditorId",
                        column: x => x.DebitAndCreditorId,
                        principalTable: "DebitAndCreditors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryNameAr", "CountryNameEn", "CreatedBy", "CreatedOn", "Describtion", "IsActive", "IsDeleted", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, "السعودية", " Saudi Arabia ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, null, null });

            migrationBuilder.InsertData(
                table: "ExpensesCategories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Describtion", "ExpensesCategoryNameAr", "ExpensesCategoryNameEn", "IsActive", "IsDeleted", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "خاضع للضريبة", " Taxable ", true, false, null, null },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "غير خاضع للضريبة", "  Not Taxable ", true, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "InspectionTemplates",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Describtion", "Hours", "InspectionNameAr", "InspectionNameEn", "IsActive", "IsCommon", "IsDeleted", "Price", "UpdatedBy", "UpdatedOn", "WarshahId" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "مجاني", " Free Inspection ", true, true, false, 0m, null, null, null },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "فحص متقدم", "Advance Inspection ", true, true, false, 100m, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypeInvoices",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Describtion", "IsActive", "IsDeleted", "PaymentTypeNameAr", "PaymentTypeNameEn", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "نقدا", " Cash ", null, null },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "بطاقة ائتمان", " Card ", null, null },
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "شيكات", " cheque ", null, null },
                    { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "آخرى", " Other ", null, null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Describtion", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 10, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Sales", null, null },
                    { 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "TaseerAcountant", null, null },
                    { 8, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "TaseerLeader", null, null },
                    { 7, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "TaseerAdmin", null, null },
                    { 6, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "SystemAdmin", null, null },
                    { 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "acountant", null, null },
                    { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Receptionist", null, null },
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Technician", null, null },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "CarOwner", null, null },
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "WarshahOwner", null, null }
                });

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Describtion", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, null },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "tech", null, null },
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Recicp", null, null },
                    { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Accountant", null, null },
                    { 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Common", null, null },
                    { 6, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Sales", null, null },
                    { 7, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "TaseerAdmin", null, null },
                    { 8, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "TaseerLeader", null, null },
                    { 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "TaseerAccountant", null, null }
                });

            migrationBuilder.InsertData(
                table: "InspectionSections",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Describtion", "InspectionTemplateId", "IsActive", "IsCommon", "IsDeleted", "SectionNameAr", "SectionNameEn", "UpdatedBy", "UpdatedOn", "WarshahId" },
                values: new object[,]
                {
                    { 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "الهيكل الخارجي", " Exterior structure ", null, null, null },
                    { 16, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "الدفرنس / الكورونا", " Aldverse / Corona ", null, null, null },
                    { 15, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "نظام الفرامل", " Brake system ", null, null, null },
                    { 14, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "أسفل السيارة", " Under the Car ", null, null, null },
                    { 13, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "السوائل", " Fluids ", null, null, null },
                    { 12, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "نظام الكهرباء", " Electricity system ", null, null, null },
                    { 11, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "الجيربوكس", " Limebox ", null, null, null },
                    { 10, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, " الماكينة", " Machine ", null, null, null },
                    { 17, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "القسم الداخلي", " Internal Section ", null, null, null },
                    { 18, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "الهيكل الخارجي", " Exterior structure ", null, null, null },
                    { 7, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "الدفرنس / الكورونا", " Aldverse / Corona ", null, null, null },
                    { 6, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "نظام الفرامل", " Brake system ", null, null, null },
                    { 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "أسفل السيارة", " Under the Car ", null, null, null },
                    { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "السوائل", " Fluids ", null, null, null },
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "نظام الكهرباء", " Electricity system ", null, null, null },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "الجيربوكس", " Limebox ", null, null, null },
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, " الماكينة", " Machine ", null, null, null },
                    { 8, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "القسم الداخلي", " Internal Section ", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CountryId", "CreatedBy", "CreatedOn", "Describtion", "IsActive", "IsDeleted", "RegionNameAr", "RegionNameEn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "الرياض", " Elryad ", null, null });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityNameAr", "CityNameEn", "CreatedBy", "CreatedOn", "Describtion", "IsActive", "IsDeleted", "RegionId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, "الرياض", " Elryad ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, 1, null, null });

            migrationBuilder.InsertData(
                table: "InspectionItems",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Describtion", "InspectionSectionId", "IsActive", "IsCommon", "IsDeleted", "ItemNameAr", "ItemNameEn", "Status", "UpdatedBy", "UpdatedOn", "WarshahId" },
                values: new object[,]
                {
                    { 59, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6, true, true, false, "علبة الفرامل الرئيسية", "Main brake box", true, null, null, null },
                    { 58, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6, true, true, false, "فلنجات (امامية / خلفية) ", "Flangat (front/back)", true, null, null, null },
                    { 57, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6, true, true, false, "هوب (امامي / خلفي)  ", "Hope (front/back) ", true, null, null, null },
                    { 56, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6, true, true, false, "رامل (امامية / خلفية)", "Ramel (front/back)", true, null, null, null },
                    { 55, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "اليايات ", "The Yayat", true, null, null, null },
                    { 54, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "جلد المقص السفلي", "Leather lower scissors", true, null, null, null },
                    { 53, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "جلد المقص العلوي", "Upper scissor skin", true, null, null, null },
                    { 52, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "صليب عامود الدوران", "Spin column cross", true, null, null, null },
                    { 51, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "مسمام عامود التوازن", "Balance column valve", true, null, null, null },
                    { 50, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "المساعدات الخلفية", "Rear aid", true, null, null, null },
                    { 49, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "المساعدات الامامية / كراسي مساعدات", "Front aid / aid chairs", true, null, null, null },
                    { 48, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "عامود الدركسيون", "Gendarmerie column", true, null, null, null },
                    { 47, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "الدودة", "The worm", true, null, null, null },
                    { 46, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "لبات الدركسيون", "Pat Draconian", true, null, null, null },
                    { 45, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "علبة الدركسيون (طلمبة)", "Gendarmerie box (Talamba)", true, null, null, null },
                    { 44, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "ذراع الدودة ", "Worm arm", true, null, null, null },
                    { 43, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "ذراع شاص", "A chasing arm", true, null, null, null },
                    { 60, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6, true, true, false, "باكم الفرامل ", "bakem - brakes", true, null, null, null },
                    { 42, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "الركبة اليمنى واليسرى ", "Right and left knee", true, null, null, null },
                    { 61, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6, true, true, false, "سلندر (امامي / خلفي) ", "Slender (front/back)", true, null, null, null },
                    { 63, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6, true, true, false, "ليات فرامل", "Brake nights", true, null, null, null },
                    { 80, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9, true, true, false, "الجهة اليمنى", " Right side", true, null, null, null },
                    { 79, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9, true, true, false, "الجهة الخلفية", " Back side", true, null, null, null },
                    { 78, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9, true, true, false, "الجهة الامامية", " Front", true, null, null, null },
                    { 77, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8, true, true, false, "المرايا", " Mirrors", true, null, null, null },
                    { 76, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8, true, true, false, "إمالة / قفل عجلة القيادة", " Tilt/lock steering wheel", true, null, null, null },
                    { 75, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8, true, true, false, "وسائد هوائية", " Airbags", true, null, null, null },
                    { 74, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8, true, true, false, "نظام راديو / موسيقى", " Radio/Music System", true, null, null, null },
                    { 73, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8, true, true, false, "لوحة القيادة وأجهزة القياس", " Dashboard and measuring devices", true, null, null, null },
                    { 72, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8, true, true, false, "عداد الوقود ودرجة الحرارة", " Fuel meter and temperature", true, null, null, null },
                    { 71, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8, true, true, false, "فتحة السقف والنوافذ ", " Sunroof and windows ", true, null, null, null },
                    { 70, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8, true, true, false, "الضوابط والمفاتيح الداخلية", " Internal controls and keys", true, null, null, null },
                    { 69, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8, true, true, false, "مقاعد وأحزمة", " Seats and belts", true, null, null, null },
                    { 68, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 7, true, true, false, "صوفة عكس (امامي / خلفي)", " Reverse wool (front/back)", true, null, null, null },
                    { 67, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 7, true, true, false, "دفرنس امام / خلفي", " Frances in front/back", true, null, null, null },
                    { 66, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 7, true, true, false, "العكوس الدفرنس", " French inverse", true, null, null, null },
                    { 65, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 7, true, true, false, "العكوس الخلفية", " Rear inverse", true, null, null, null },
                    { 64, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 7, true, true, false, "العكوس الامامية ", "Forward inverse", true, null, null, null },
                    { 62, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6, true, true, false, "سلك فرامل اليد (جلنط) ", "Hand brake wire (galent)", true, null, null, null },
                    { 81, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9, true, true, false, "الجهة اليسرى", " Left side", true, null, null, null },
                    { 41, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, true, true, false, "الأذرعة ", " Arms ", true, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "InspectionItems",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Describtion", "InspectionSectionId", "IsActive", "IsCommon", "IsDeleted", "ItemNameAr", "ItemNameEn", "Status", "UpdatedBy", "UpdatedOn", "WarshahId" },
                values: new object[,]
                {
                    { 39, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, true, true, false, "ماء الردياتير", " Water of the radyter", true, null, null, null },
                    { 17, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "وجه طرمبة الزيت", "The face of the oil pump", true, null, null, null },
                    { 16, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "فلتر الهواء  ", " Air filter", true, null, null, null },
                    { 15, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "صفايه البنزين ", "Gas can", true, null, null, null },
                    { 14, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "السيور ", "Belts", true, null, null, null },
                    { 13, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "طرمبة الماء ", "Water pump", true, null, null, null },
                    { 12, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, " وجه صدر الماكينة امام / خلفي ", "Front/rear face of the machine's chest", true, null, null, null },
                    { 11, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "رادياتير الماء ", " Water radiator", true, null, null, null },
                    { 10, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "تهريبات ماء ", " Water smuggling", true, null, null, null },
                    { 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "وجه الثلاجة ", "The face of the fridge", true, null, null, null },
                    { 8, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "قاعدة فلتر الزيت ", " Oil filter base ", true, null, null, null },
                    { 7, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "وجه غطاء البلوف", "The face of the cabbage cover ", true, null, null, null },
                    { 6, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "وجه كرتير الماكينة", " The face of the machine carter ", true, null, null, null },
                    { 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "كرسي الماكينة", " Machine chair ", true, null, null, null },
                    { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "تصفية ماكينة", " Filter machine ", true, null, null, null },
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "الصوفة الخلفية", " Rear wool ", true, null, null, null },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "حالة الماكينة", " Machine condition ", true, null, null, null },
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, true, true, false, "لصوفة الامامية", " Front wool ", true, null, null, null },
                    { 18, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, " الصوفة الامامية", "Front wool", true, null, null, null },
                    { 40, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, true, true, false, "ماء المساحات", " Water spaces ", true, null, null, null },
                    { 19, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "الصوفة الخلفية", "Rear wool", true, null, null, null },
                    { 21, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "وجه كرتير الجير ", "The face of the lime carter.", true, null, null, null },
                    { 38, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, true, true, false, "زيت الفرامل", " Brake oil ", true, null, null, null },
                    { 37, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, true, true, false, "زيت الجير", " Lime oil ", true, null, null, null },
                    { 36, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, true, true, false, "زيت الماكينة", " Machine oil ", true, null, null, null },
                    { 35, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, true, false, "رديتر مكيف", " Redditor air conditioner ", true, null, null, null },
                    { 34, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, true, false, "ثلاجة مكيف", " Air-conditioned refrigerator ", true, null, null, null },
                    { 33, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, true, false, "كودات الكمبيوتر  ", "Computer codes", true, null, null, null },
                    { 32, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, true, false, "شاشة المكيف ", "Air conditioner screen", true, null, null, null },
                    { 31, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, true, false, "المساحات ", "wiper ", true, null, null, null },
                    { 30, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, true, false, "السنتر لوك ", "Center lock", true, null, null, null },
                    { 29, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, true, false, "لمكيف كمبروسر ", "For a kambroser air conditioner.  ", true, null, null, null },
                    { 28, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, true, false, "الانوار ", "Lights", true, null, null, null },
                    { 27, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, true, false, "السلف ", "Predecessor", true, null, null, null },
                    { 26, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, true, false, "الدينمو", "the dynamo", true, null, null, null },
                    { 25, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, true, false, " البطارية ", "Battery", true, null, null, null },
                    { 24, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "حالة الجير ", "Lime case", true, null, null, null },
                    { 23, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "ماسورة مبرد الجير ", "Lime cooler pipe", true, null, null, null },
                    { 22, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "صوف عصا الجير  ", "Lime stick wool", true, null, null, null },
                    { 20, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, true, false, "كرسي الجير بوكس", "Lime Box Chair", true, null, null, null },
                    { 82, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9, true, true, false, "اعلى السيارة ( التنده)", " Top of the car ( Ceiling)", true, null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthrizedPersons_UserId",
                table: "AuthrizedPersons",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceBanks_WarshahId",
                table: "BalanceBanks",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_WarshahId",
                table: "Banks",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_WarshahId",
                table: "Boxes",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_WarshahBankId",
                table: "Cheques",
                column: "WarshahBankId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_WarshahId",
                table: "Cheques",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditorNotices_InvoiceId",
                table: "CreditorNotices",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditorNotices_WarshahId",
                table: "CreditorNotices",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_DebitAndCreditors_InvoiceId",
                table: "DebitAndCreditors",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DebitAndCreditors_WarshahId",
                table: "DebitAndCreditors",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_DebitNotices_InvoiceId",
                table: "DebitNotices",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DebitNotices_WarshahId",
                table: "DebitNotices",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesTransactions_ExpensesCategoryId",
                table: "ExpensesTransactions",
                column: "ExpensesCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesTransactions_ExpensesTypeId",
                table: "ExpensesTransactions",
                column: "ExpensesTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesTransactions_WarshahId",
                table: "ExpensesTransactions",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesTypes_ExpensesCategoryId",
                table: "ExpensesTypes",
                column: "ExpensesCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesTypes_WarshahId",
                table: "ExpensesTypes",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionItems_InspectionSectionId",
                table: "InspectionItems",
                column: "InspectionSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionItems_WarshahId",
                table: "InspectionItems",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionReports_InspectionWarshahReportId",
                table: "InspectionReports",
                column: "InspectionWarshahReportId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionSections_InspectionTemplateId",
                table: "InspectionSections",
                column: "InspectionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionSections_WarshahId",
                table: "InspectionSections",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionTemplates_WarshahId",
                table: "InspectionTemplates",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionWarshahReports_CarOwnerId",
                table: "InspectionWarshahReports",
                column: "CarOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionWarshahReports_MotorsId",
                table: "InspectionWarshahReports",
                column: "MotorsId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InspectionWarshahReportId",
                table: "Invoices",
                column: "InspectionWarshahReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentTypeInvoiceId",
                table: "Invoices",
                column: "PaymentTypeInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReciptionOrderId",
                table: "Invoices",
                column: "ReciptionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_RepairOrderId",
                table: "Invoices",
                column: "RepairOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorModels_MotorMakeId",
                table: "MotorModels",
                column: "MotorMakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Motors_CarOwnerId",
                table: "Motors",
                column: "CarOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Motors_MotorColorId",
                table: "Motors",
                column: "MotorColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Motors_MotorMakeId",
                table: "Motors",
                column: "MotorMakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Motors_MotorModelId",
                table: "Motors",
                column: "MotorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Motors_MotorYearId",
                table: "Motors",
                column: "MotorYearId");

            migrationBuilder.CreateIndex(
                name: "IX_NoticeProducts_DebitAndCreditorId",
                table: "NoticeProducts",
                column: "DebitAndCreditorId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAndReceiptVouchers_PaymentTypeInvoiceId",
                table: "PaymentAndReceiptVouchers",
                column: "PaymentTypeInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAndReceiptVouchers_WarshahId",
                table: "PaymentAndReceiptVouchers",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_PaymentTypeInvoiceId",
                table: "ReceiptVouchers",
                column: "PaymentTypeInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_ReciptionOrderId",
                table: "ReceiptVouchers",
                column: "ReciptionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_WarshahId",
                table: "ReceiptVouchers",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_ReciptionOrders_CarOwnerId",
                table: "ReciptionOrders",
                column: "CarOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReciptionOrders_MotorsId",
                table: "ReciptionOrders",
                column: "MotorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReciptionOrders_PaymentTypeInvoiceId",
                table: "ReciptionOrders",
                column: "PaymentTypeInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReciptionOrders_ReciptionId",
                table: "ReciptionOrders",
                column: "ReciptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReciptionOrders_TecnicanId",
                table: "ReciptionOrders",
                column: "TecnicanId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_InspectionTemplateId",
                table: "RepairOrders",
                column: "InspectionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_InspectionWarshahReportId",
                table: "RepairOrders",
                column: "InspectionWarshahReportId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_ReciptionOrderId",
                table: "RepairOrders",
                column: "ReciptionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_TechId",
                table: "RepairOrders",
                column: "TechId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrderSpareParts_RepairOrderId",
                table: "RepairOrderSpareParts",
                column: "RepairOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrderSpareParts_SparePartId",
                table: "RepairOrderSpareParts",
                column: "SparePartId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrderSpareParts_TechId",
                table: "RepairOrderSpareParts",
                column: "TechId");

            migrationBuilder.CreateIndex(
                name: "IX_RoHistories_RepairOrderId",
                table: "RoHistories",
                column: "RepairOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceItems_ServiceInvoiceId",
                table: "ServiceInvoiceItems",
                column: "ServiceInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoices_MotorsId",
                table: "ServiceInvoices",
                column: "MotorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoices_TechId",
                table: "ServiceInvoices",
                column: "TechId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceCategoryId",
                table: "Services",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_CategorySparePartsId",
                table: "SpareParts",
                column: "CategorySparePartsId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_MotorMakeId",
                table: "SpareParts",
                column: "MotorMakeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_MotorModelId",
                table: "SpareParts",
                column: "MotorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_MotorYearId",
                table: "SpareParts",
                column: "MotorYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_SubCategoryPartsId",
                table: "SpareParts",
                column: "SubCategoryPartsId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_SupplierId",
                table: "SpareParts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_WarshahId",
                table: "SpareParts",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryParts_CategorySparePartsId",
                table: "SubCategoryParts",
                column: "CategorySparePartsId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CityId",
                table: "Suppliers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CountryId",
                table: "Suppliers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_RegionId",
                table: "Suppliers",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_WarshahId",
                table: "Suppliers",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_WarshahId",
                table: "User",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_WarshahBanks_WarshahId",
                table: "WarshahBanks",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_WarshahParams_SpicialistsId",
                table: "WarshahParams",
                column: "SpicialistsId");

            migrationBuilder.CreateIndex(
                name: "IX_WarshahParams_WarshahId",
                table: "WarshahParams",
                column: "WarshahId");

            migrationBuilder.CreateIndex(
                name: "IX_WarshahParams_WarshahServiceTypeId",
                table: "WarshahParams",
                column: "WarshahServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WarshahParams_WarshahTypeId",
                table: "WarshahParams",
                column: "WarshahTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AuthrizedPersons");

            migrationBuilder.DropTable(
                name: "BalanceBanks");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Cheques");

            migrationBuilder.DropTable(
                name: "Configrations");

            migrationBuilder.DropTable(
                name: "CreditorNotices");

            migrationBuilder.DropTable(
                name: "DebitNotices");

            migrationBuilder.DropTable(
                name: "ExpensesTransactions");

            migrationBuilder.DropTable(
                name: "InspectionItems");

            migrationBuilder.DropTable(
                name: "InspectionReports");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "NoticeProducts");

            migrationBuilder.DropTable(
                name: "PaymentAndReceiptVouchers");

            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropTable(
                name: "ReceiptVouchers");

            migrationBuilder.DropTable(
                name: "RepairOrderImages");

            migrationBuilder.DropTable(
                name: "RepairOrderSpareParts");

            migrationBuilder.DropTable(
                name: "RoHistories");

            migrationBuilder.DropTable(
                name: "SalesRequests");

            migrationBuilder.DropTable(
                name: "ServiceInvoiceItems");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "TaseerItems");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "VerfiyCodes");

            migrationBuilder.DropTable(
                name: "WarshahCustomers");

            migrationBuilder.DropTable(
                name: "WarshahParams");

            migrationBuilder.DropTable(
                name: "WarshahBanks");

            migrationBuilder.DropTable(
                name: "ExpensesTypes");

            migrationBuilder.DropTable(
                name: "InspectionSections");

            migrationBuilder.DropTable(
                name: "DebitAndCreditors");

            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropTable(
                name: "ServiceInvoices");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropTable(
                name: "Spicialists");

            migrationBuilder.DropTable(
                name: "WarshahServiceType");

            migrationBuilder.DropTable(
                name: "WarshahType");

            migrationBuilder.DropTable(
                name: "ExpensesCategories");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "SubCategoryParts");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "RepairOrders");

            migrationBuilder.DropTable(
                name: "CategorySpareParts");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "InspectionTemplates");

            migrationBuilder.DropTable(
                name: "InspectionWarshahReports");

            migrationBuilder.DropTable(
                name: "ReciptionOrders");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Motors");

            migrationBuilder.DropTable(
                name: "PaymentTypeInvoices");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "MotorColors");

            migrationBuilder.DropTable(
                name: "MotorModels");

            migrationBuilder.DropTable(
                name: "MotorYears");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "MotorMakes");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Warshah");
        }
    }
}
