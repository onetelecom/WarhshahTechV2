using BL.Infrastructure;
using DL.DTOs.BalanceDTO;
using DL.DTOs.InvoiceDTOs;
using DL.Entities;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
        public interface IBoxNow
    {
        BoxMoneyDTO GetBoxNow(int warshahid);
    }
       
    }
public class BoxNow : IBoxNow
{
    private readonly IUnitOfWork _uow;

    public BoxNow(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public BoxMoneyDTO GetBoxNow(int warshahid)
    {
        var warshah = _uow.WarshahRepository.GetById(warshahid);

        DateTime FromDate = warshah.CreatedOn;

        DateTime ToDate = DateTime.Now;


        // سند قبض دفعة مقدمة

        var TotalReciptVoucher = _uow.ReceiptVouchersRepository.GetMany(s => s.WarshahId == warshahid && s.PaymentTypeInvoiceId == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();

        decimal totalReciptVoucher = TotalReciptVoucher.Sum(s => s.AdvancePayment);


        // فى حساب الفواتير هيتم خصم قيمة الدفعات المقدمة من إجمالى الفاتورة على اعتبار أنه تم سداده الى الورشة فى وقت سابق و لم يدخل مبلغ الفاتورة بالكامل إلى الصندوق

        var Totalinvoices = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.InvoiceStatusId == 2 && s.PaymentTypeInvoiceId == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
        decimal alltotalinvoice = Totalinvoices.Sum(s => s.Total);
        decimal alltotaladvancedpayment = (decimal)Totalinvoices.Sum(s => s.AdvancePayment);

        decimal totalInvoice = alltotalinvoice - alltotaladvancedpayment;

        //

        // حساب إشعارات المدين و الدائن

        var Totaldebit = _uow.DebitAndCreditorRepository.GetMany(i => i.WarshahId == warshahid && i.Flag == 1 && i.PaymentTypeInvoiceId == 1 && i.CreatedOn >= FromDate && i.CreatedOn <= ToDate)?.ToHashSet();

        decimal totaldebit = Totaldebit.Sum(s => s.Total);

        var Totalcreditor = _uow.DebitAndCreditorRepository.GetMany(i => i.WarshahId == warshahid && i.Flag == 2 && i.PaymentTypeInvoiceId == 1 && i.CreatedOn >= FromDate && i.CreatedOn <= ToDate)?.ToHashSet();

        decimal totalcreditor = Totalcreditor.Sum(s => s.Total);

        // سند القبض   العام
        var TotalRecipt = _uow.PaymentAndReceiptVoucherRepository.GetMany(i => i.WarshahId == warshahid && i.TypeVoucher == 1 && i.PaymentTypeInvoiceId == 1 && i.CreatedOn >= FromDate && i.CreatedOn <= ToDate)?.ToHashSet();

        decimal totalRecipt = (decimal)TotalRecipt.Sum(s => s.AdvancePayment);

        // سند الصرف 

        var Totalvoucher = _uow.PaymentAndReceiptVoucherRepository.GetMany(i => i.WarshahId == warshahid && i.TypeVoucher == 2 && i.PaymentTypeInvoiceId == 1 && i.CreatedOn >= FromDate && i.CreatedOn <= ToDate)?.ToHashSet();

        decimal totalvoucher = (decimal)Totalvoucher.Sum(s => s.AdvancePayment);



        // حساب إجمالى المصاريف  الخاضعة     ( Category ID == 1   is   With Vat )

        var Vexpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == warshahid && s.ExpensesCategoryId == 1 && s.PaymentTypeInvoiceId == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
        decimal tVexpenses = Vexpenses.Sum(s => s.Total);



        // حساب إجمالى المصاريف الغير الخاضعة     ( Category ID == 2   is   Without Vat )
        var NVexpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == warshahid && s.ExpensesCategoryId == 2 && s.PaymentTypeInvoiceId == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
        decimal tNVexpenses = NVexpenses.Sum(s => s.TotalWithoutVat);



        var currentBoxIncome = _uow.OpeningBalanceRepository.GetMany(a => a.WarshahId == warshahid).FirstOrDefault();

        decimal currentIncome = 0;
        if (currentBoxIncome != null)
        {
            currentIncome = currentBoxIncome.OpenBalance;
        }

        var Boxbank = _uow.BoxBankRepository.GetMany(s => s.WarshahId == warshahid && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
        decimal BoxbankMoney = 0;
        if (Boxbank != null)
        {
            BoxbankMoney = Boxbank.Sum(s => s.TotalIncome);
        }




        BoxMoneyDTO boxMoneyDTO = new BoxMoneyDTO();
        boxMoneyDTO.WarshahId = (int)warshahid;
        boxMoneyDTO.TotalIncome = (currentIncome + totaldebit + totalInvoice + totalRecipt + totalReciptVoucher) - (totalcreditor + tVexpenses + tNVexpenses + totalvoucher + BoxbankMoney);
        return boxMoneyDTO;
    }
}