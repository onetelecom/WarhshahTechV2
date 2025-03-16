using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
using DL.DTOs.BalanceDTO;
using DL.Migrations;
using HELPER;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]

    public class BoxMoneyController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public BoxMoneyController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }




        [HttpPost, Route("CreateBox")]
        public IActionResult CreateBox(BoxMoneyDTO boxMoneyDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var box = _mapper.Map<DL.Entities.BoxMoney>(boxMoneyDTO);
                    var currentbox = _uow.BoxRepository.GetMany(s=>s.WarshahId==box.WarshahId).FirstOrDefault();
                    if(currentbox==null)
                    {
                        box.IsDeleted = false;
                        _uow.BoxRepository.Add(box);
                    }
                    else
                    {

                        box.IsDeleted = false;
                        currentbox.WarshahId = box.WarshahId;
                        currentbox.TotalIncome = box.TotalIncome;
                        _uow.BoxRepository.Update(currentbox);

                    }
                    _uow.Save();
              
                    return Ok(box);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Box");
        }

        // الصندوق خاص بالفواتير التى تم تحصيلها كاش فقط و تم سداداها بالكامل

        // Get Box Money All Time

        [HttpGet, Route("GetBox")]
        public IActionResult GetBox(int? warshahid)
        {
            var warshah = _uow.WarshahRepository.GetById(warshahid);

            var Totalinvoices = _uow.InvoiceRepository.GetMany(i=>i.WarshahId == warshahid && i.PaymentTypeInvoiceId == 1 && i.InvoiceStatusId ==2 )?.ToHashSet();

            decimal totalInvoice = Totalinvoices.Sum(s => s.Total);


            var OldTotalinvoices = _uow.OldInvoicesRepository.GetMany(i => i.WarshahId == warshah.OldWarshahId && i.InvoiceStatusId == 3)?.ToHashSet();

            decimal OldtotalInvoice = (decimal)OldTotalinvoices.Sum(s => s.Total);


            var Totalnotice = _uow.DebitAndCreditorRepository.GetMany(i => i.WarshahId == warshahid && i.Flag == 1)?.ToHashSet();

            decimal totaldebit = Totalnotice.Sum(s => s.Total);

            var Totalcreditor = _uow.DebitAndCreditorRepository.GetMany(i => i.WarshahId == warshahid && i.Flag == 2)?.ToHashSet();

            decimal totalcreditor = Totalcreditor.Sum(s => s.Total);


            var currentwarshah = _uow.BoxRepository.GetMany(s=>s.WarshahId == warshahid).FirstOrDefault();

            decimal currentIncome = currentwarshah.TotalIncome;

            var Vexpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == warshahid && s.ExpensesCategoryId == 1 && s.PaymentTypeInvoiceId == 1 ).ToHashSet();
            decimal tVexpenses = Vexpenses.Sum(s => s.TotalWithoutVat);
           
            

            // حساب إجمالى المصاريف الغير الخاضعة     ( Category ID == 2   is   Without Vat )
            var NVexpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == warshahid && s.ExpensesCategoryId == 2 && s.PaymentTypeInvoiceId == 1).ToHashSet();
            decimal tNVexpenses = NVexpenses.Sum(s => s.TotalWithoutVat);
          




            BoxMoneyDTO boxMoneyDTO = new BoxMoneyDTO();
            boxMoneyDTO.WarshahId = (int)warshahid;
            boxMoneyDTO.TotalIncome = (currentIncome + totaldebit + totalInvoice + OldtotalInvoice) - (totalcreditor + tVexpenses + tNVexpenses) ;

            return Ok(boxMoneyDTO);
        }

        // Get Box Money  in Period

        [HttpGet, Route("GetBoxInTime")]
        public IActionResult GetBoxInTime(DateTime FromDate, DateTime ToDate, int warshahid)
        {
            var warshah = _uow.WarshahRepository.GetById(warshahid);
            // سند قبض دفعة مقدمة

            var TotalReciptVoucher = _uow.ReceiptVouchersRepository.GetMany(s => s.WarshahId == warshahid &&   s.PaymentTypeInvoiceId == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();

            decimal totalReciptVoucher = TotalReciptVoucher.Sum(s => s.AdvancePayment);


            // فى حساب الفواتير هيتم خصم قيمة الدفعات المقدمة من إجمالى الفاتورة على اعتبار أنه تم سداده الى الورشة فى وقت سابق و لم يدخل مبلغ الفاتورة بالكامل إلى الصندوق

            var Totalinvoices = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid &&  s.InvoiceStatusId == 2 && s.PaymentTypeInvoiceId == 1   && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal alltotalinvoice = Totalinvoices.Sum(s => s.Total);
            decimal alltotaladvancedpayment = (decimal)Totalinvoices.Sum(s => s.AdvancePayment);

            decimal totalInvoice = alltotalinvoice - alltotaladvancedpayment;

            //



            var OldTotalinvoices = _uow.OldInvoicesRepository.GetMany(i => i.WarshahId == warshah.OldWarshahId && i.InvoiceStatusId == 3 && i.OldCreatedon >= FromDate && i.OldCreatedon <= ToDate)?.ToHashSet();

            decimal OldtotalInvoice = (decimal)OldTotalinvoices.Sum(s => s.Total);


            // حساب إشعارات المدين و الدائن

            var Totaldebit = _uow.DebitAndCreditorRepository.GetMany(i => i.WarshahId == warshahid && i.Flag == 1 && i.PaymentTypeInvoiceId == 1 && i.CreatedOn >= FromDate && i.CreatedOn <= ToDate)?.ToHashSet();

            decimal totaldebit = Totaldebit.Sum(s => s.Total);

            var Totalcreditor = _uow.DebitAndCreditorRepository.GetMany(i => i.WarshahId == warshahid && i.Flag == 2 && i.PaymentTypeInvoiceId == 1 && i.CreatedOn >= FromDate && i.CreatedOn <= ToDate)?.ToHashSet();

            decimal totalcreditor = Totalcreditor.Sum(s => s.Total);

            // سند القبض   العام
            var TotalRecipt = _uow.PaymentAndReceiptVoucherRepository.GetMany(i => i.WarshahId == warshahid && i.TypeVoucher == 1 && i.PaymentTypeInvoiceId == 1 &&  i.CreatedOn >= FromDate && i.CreatedOn <= ToDate)?.ToHashSet();

            decimal totalRecipt = (decimal)TotalRecipt.Sum(s => s.AdvancePayment);

            // سند الصرف 

            var Totalvoucher = _uow.PaymentAndReceiptVoucherRepository.GetMany(i => i.WarshahId == warshahid && i.TypeVoucher == 2 && i.PaymentTypeInvoiceId == 1 && i.CreatedOn >= FromDate && i.CreatedOn <= ToDate)?.ToHashSet();

            decimal totalvoucher = (decimal)Totalvoucher.Sum(s => s.AdvancePayment);



            // حساب إجمالى المصاريف  الخاضعة     ( Category ID == 1   is   With Vat )

            var Vexpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == warshahid && s.ExpensesCategoryId == 1 &&  s.PaymentTypeInvoiceId == 1  && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal tVexpenses = Vexpenses.Sum(s => s.Total);

        

            // حساب إجمالى المصاريف الغير الخاضعة     ( Category ID == 2   is   Without Vat )
            var NVexpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == warshahid && s.ExpensesCategoryId == 2 && s.PaymentTypeInvoiceId == 1  && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal tNVexpenses = NVexpenses.Sum(s => s.TotalWithoutVat);



            var currentBoxIncome = _uow.OpeningBalanceRepository.GetMany(a => a.WarshahId == warshahid).FirstOrDefault();

            decimal currentIncome = 0;
            if (currentBoxIncome != null)
            {
              currentIncome  = currentBoxIncome.OpenBalance;
            }
            
            var Boxbank = _uow.BoxBankRepository.GetMany(s => s.WarshahId == warshahid  && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal BoxbankMoney = 0;
            if (Boxbank != null)
            {
                BoxbankMoney = Boxbank.Sum(s=>s.TotalIncome);
            }




            BoxMoneyDTO boxMoneyDTO = new BoxMoneyDTO();
            boxMoneyDTO.WarshahId = (int)warshahid;
            boxMoneyDTO.TotalIncome = (currentIncome + totaldebit + totalInvoice + totalRecipt + totalReciptVoucher + OldtotalInvoice) - (totalcreditor + tVexpenses + tNVexpenses + totalvoucher + BoxbankMoney);
            return Ok(boxMoneyDTO);
        }



        // Current Box الصندوق حتى الامس 


        [HttpGet, Route("GetBoxCurrent")]
        public IActionResult GetBoxCurrent( int warshahid)
        {

            var warshah = _uow.WarshahRepository.GetById(warshahid);

            DateTime FromDate = warshah.CreatedOn;

            DateTime ToDate = DateTime.Today;

           
            // سند قبض دفعة مقدمة

            var TotalReciptVoucher = _uow.ReceiptVouchersRepository.GetMany(s => s.WarshahId == warshahid && s.PaymentTypeInvoiceId == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();

            decimal totalReciptVoucher = TotalReciptVoucher.Sum(s => s.AdvancePayment);


            // فى حساب الفواتير هيتم خصم قيمة الدفعات المقدمة من إجمالى الفاتورة على اعتبار أنه تم سداده الى الورشة فى وقت سابق و لم يدخل مبلغ الفاتورة بالكامل إلى الصندوق

            var Totalinvoices = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.InvoiceStatusId == 2 && s.PaymentTypeInvoiceId == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal alltotalinvoice = Totalinvoices.Sum(s => s.Total);
            decimal alltotaladvancedpayment = (decimal)Totalinvoices.Sum(s => s.AdvancePayment);

            decimal totalInvoice = alltotalinvoice - alltotaladvancedpayment;

            //


            var OldTotalinvoices = _uow.OldInvoicesRepository.GetMany(i => i.WarshahId == warshah.OldWarshahId && i.InvoiceStatusId == 3 && i.OldCreatedon >= FromDate && i.OldCreatedon <= ToDate)?.ToHashSet();

            decimal OldtotalInvoice = (decimal)OldTotalinvoices.Sum(s => s.Total);
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
            boxMoneyDTO.TotalIncome = (currentIncome + totaldebit + totalInvoice + totalRecipt + totalReciptVoucher + OldtotalInvoice) - (totalcreditor + tVexpenses + tNVexpenses + totalvoucher + BoxbankMoney);
            return Ok(boxMoneyDTO);
        }


        // Current Box الصندوق  الان 


        [HttpGet, Route("GetBoxNow")]
        public IActionResult GetBoxNow(int warshahid)
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


            var OldTotalinvoices = _uow.OldInvoicesRepository.GetMany(i => i.WarshahId == warshah.OldWarshahId && i.InvoiceStatusId == 3 && i.OldCreatedon >= FromDate && i.OldCreatedon <= ToDate)?.ToHashSet();

            decimal OldtotalInvoice = (decimal)OldTotalinvoices.Sum(s => s.Total);

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
            boxMoneyDTO.TotalIncome = (currentIncome + totaldebit + totalInvoice + totalRecipt + totalReciptVoucher + OldtotalInvoice) - (totalcreditor + tVexpenses + tNVexpenses + totalvoucher + BoxbankMoney);
            return Ok(boxMoneyDTO);
        }


    }

}
