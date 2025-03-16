using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class df32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ReciptionOrders_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "ReciptionOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_CategorySpareParts_CategorySparePartsId",
                table: "SpareParts");

            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_SubCategoryParts_SubCategoryPartsId",
                table: "SpareParts");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryPartsId",
                table: "SpareParts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategorySparePartsId",
                table: "SpareParts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTypeInvoiceId",
                table: "ReciptionOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTypeInvoiceId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "Invoices",
                column: "PaymentTypeInvoiceId",
                principalTable: "PaymentTypeInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReciptionOrders_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "ReciptionOrders",
                column: "PaymentTypeInvoiceId",
                principalTable: "PaymentTypeInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_CategorySpareParts_CategorySparePartsId",
                table: "SpareParts",
                column: "CategorySparePartsId",
                principalTable: "CategorySpareParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_SubCategoryParts_SubCategoryPartsId",
                table: "SpareParts",
                column: "SubCategoryPartsId",
                principalTable: "SubCategoryParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ReciptionOrders_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "ReciptionOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_CategorySpareParts_CategorySparePartsId",
                table: "SpareParts");

            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_SubCategoryParts_SubCategoryPartsId",
                table: "SpareParts");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryPartsId",
                table: "SpareParts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategorySparePartsId",
                table: "SpareParts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTypeInvoiceId",
                table: "ReciptionOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTypeInvoiceId",
                table: "Invoices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "Invoices",
                column: "PaymentTypeInvoiceId",
                principalTable: "PaymentTypeInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReciptionOrders_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "ReciptionOrders",
                column: "PaymentTypeInvoiceId",
                principalTable: "PaymentTypeInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_CategorySpareParts_CategorySparePartsId",
                table: "SpareParts",
                column: "CategorySparePartsId",
                principalTable: "CategorySpareParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_SubCategoryParts_SubCategoryPartsId",
                table: "SpareParts",
                column: "SubCategoryPartsId",
                principalTable: "SubCategoryParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
