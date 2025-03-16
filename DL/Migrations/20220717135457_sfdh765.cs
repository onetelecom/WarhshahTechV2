using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class sfdh765 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionTypeID",
                table: "TransactionsTodays");

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeInvoiceId",
                table: "ExpensesTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeInvoiceId",
                table: "DebitAndCreditors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesTransactions_PaymentTypeInvoiceId",
                table: "ExpensesTransactions",
                column: "PaymentTypeInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DebitAndCreditors_PaymentTypeInvoiceId",
                table: "DebitAndCreditors",
                column: "PaymentTypeInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DebitAndCreditors_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "DebitAndCreditors",
                column: "PaymentTypeInvoiceId",
                principalTable: "PaymentTypeInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesTransactions_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "ExpensesTransactions",
                column: "PaymentTypeInvoiceId",
                principalTable: "PaymentTypeInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebitAndCreditors_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "DebitAndCreditors");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesTransactions_PaymentTypeInvoices_PaymentTypeInvoiceId",
                table: "ExpensesTransactions");

            migrationBuilder.DropIndex(
                name: "IX_ExpensesTransactions_PaymentTypeInvoiceId",
                table: "ExpensesTransactions");

            migrationBuilder.DropIndex(
                name: "IX_DebitAndCreditors_PaymentTypeInvoiceId",
                table: "DebitAndCreditors");

            migrationBuilder.DropColumn(
                name: "PaymentTypeInvoiceId",
                table: "ExpensesTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentTypeInvoiceId",
                table: "DebitAndCreditors");

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeID",
                table: "TransactionsTodays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
