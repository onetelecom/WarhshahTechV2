using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.InvoiceDTOs;
using DL.Entities;
using DocumentFormat.OpenXml.Wordprocessing;
using Helper;
using Helper.Triggers;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WarshahTechV2.Models;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]

    public class PaymentAndReceiptController : ControllerBase
    {

        private readonly IBoxNow _boxNow;

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public PaymentAndReceiptController(IUnitOfWork uow, IBoxNow boxNow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
            _boxNow = boxNow;
        }


        #region PaymentAndReceiptVoucherCRUD

        //Create ReceiptVoucher    (TypeVoucher = 1)  سند القبض   

        [HttpPost, Route("CreateReceiptVoucher")]
        public IActionResult CreateReceiptVoucher(PaymentAndReceiptVoucherDTO paymentAndReceiptVoucherDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var ReceiptVoucher = _mapper.Map<DL.Entities.PaymentAndReceiptVoucher>(paymentAndReceiptVoucherDTO);

                    var docnumber = _uow.PaymentAndReceiptVoucherRepository.GetMany(i => i.WarshahId == ReceiptVoucher.WarshahId && i.TypeVoucher == 1).OrderByDescending(t => t.DocNumber).FirstOrDefault();
                    if (docnumber == null)
                    {
                        ReceiptVoucher.DocNumber = 1;
                    }
                    else
                    {
                        int lastnumber = docnumber.DocNumber;
                        ReceiptVoucher.DocNumber = lastnumber + 1;
                    }

                    ReceiptVoucher.IsDeleted = false;
                    _uow.PaymentAndReceiptVoucherRepository.Add(ReceiptVoucher);
                    _uow.Save();

                    // Add To Transaction TODAY bOX when cash
                    if (ReceiptVoucher.PaymentTypeInvoiceId == 1)
                    {

                        TransactionsToday transactionsToday = new TransactionsToday();

                        transactionsToday.WarshahId = ReceiptVoucher.WarshahId;
                        transactionsToday.InvoiceNumber = ReceiptVoucher.DocNumber;
                        transactionsToday.Vat = 0;
                        transactionsToday.Total = (decimal)ReceiptVoucher.AdvancePayment;
                        transactionsToday.CreatedOn = DateTime.Now;
                        transactionsToday.BeforeVat = 0;
                        transactionsToday.TransactionReason = ReceiptVoucher.Discriptotion;
                        transactionsToday.TransactionName = "سند قبض";
                        var boxnow = _boxNow.GetBoxNow((int)transactionsToday.WarshahId);
                        decimal previousmoney = boxnow.TotalIncome;
                        transactionsToday.PreviousBalance = previousmoney;
                        transactionsToday.CurrentBalance = previousmoney + transactionsToday.Total;


                        _uow.TransactionTodayRepository.Add(transactionsToday);

                    }

                    _uow.Save();
                    return Ok(ReceiptVoucher);
                  
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Receipt");
        }



        //Create PaymentVoucher (TypeVoucher = 2)   سند الصرف

        [HttpPost, Route("CreatePaymentVoucher")]
        public IActionResult CreatePaymentVoucher(PaymentAndReceiptVoucherDTO paymentAndReceiptVoucherDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var PaymentVoucher = _mapper.Map<DL.Entities.PaymentAndReceiptVoucher>(paymentAndReceiptVoucherDTO);

                    var docnumber = _uow.PaymentAndReceiptVoucherRepository.GetMany(i => i.WarshahId == PaymentVoucher.WarshahId && i.TypeVoucher == 2).OrderByDescending(t => t.DocNumber).FirstOrDefault();
                    if (docnumber == null)
                    {
                        PaymentVoucher.DocNumber = 1;
                    }
                    else
                    {
                        int lastnumber = docnumber.DocNumber;
                        PaymentVoucher.DocNumber = lastnumber + 1;
                    }

                    PaymentVoucher.IsDeleted = false;
                    _uow.PaymentAndReceiptVoucherRepository.Add(PaymentVoucher);
                    _uow.Save();
                    // Add To Transaction TODAY bOX when cash
                    if (PaymentVoucher.PaymentTypeInvoiceId == 1)
                    {

                        TransactionsToday transactionsToday = new TransactionsToday();

                        transactionsToday.WarshahId = PaymentVoucher.WarshahId;
                        transactionsToday.InvoiceNumber = PaymentVoucher.DocNumber;
                        transactionsToday.Vat = 0;
                        transactionsToday.Total = (decimal)PaymentVoucher.AdvancePayment;
                        transactionsToday.CreatedOn = DateTime.Now;
                        transactionsToday.BeforeVat = 0;
                        transactionsToday.TransactionReason = PaymentVoucher.Discriptotion;
                            transactionsToday.TransactionName = "سند صرف";
                        var boxnow = _boxNow.GetBoxNow((int)transactionsToday.WarshahId);
                        decimal previousmoney = boxnow.TotalIncome;
                        transactionsToday.PreviousBalance = previousmoney;
                        transactionsToday.CurrentBalance = previousmoney - transactionsToday.Total;

                        _uow.TransactionTodayRepository.Add(transactionsToday);

                    }

                    _uow.Save();
                    return Ok(PaymentVoucher);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Receipt");
        }


        // Get  Receipt or Payment by id


        [HttpGet, Route("GetVoucher")]
        public IActionResult GetVoucher(int? id)
        {

            var GetVoucher = _uow.PaymentAndReceiptVoucherRepository.GetMany(t => t.Id == id).Include(t => t.Warshah).Include(a=>a.PaymentTypeInvoice).FirstOrDefault();

            return Ok(GetVoucher);
        }



        //Get All Receipts  (typevoucherid = 1 ) or Payments (typevoucherid = 2 ) from warshah  


        [HttpGet, Route("GetAllVouchersByWarshah")]
        public IActionResult GetAllVouchersByWarshah(int? id , int? typevoucherid ,  int pagenumber , int pagecount)
        {
            var vouchers = _uow.PaymentAndReceiptVoucherRepository.GetMany(t => t.WarshahId == id && t.TypeVoucher == typevoucherid).Include(a=>a.PaymentTypeInvoice).ToHashSet();
          
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = vouchers.Count(), Listinvoice = vouchers.ToPagedList(pagenumber, pagecount) });

        }



        // Update Voucher

        [HttpPost, Route("EditVoucher")]
        public IActionResult EditVoucher(EditPaymentAndReceiptVoucherDTO editPaymentAndReceiptVoucherDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Voucher = _mapper.Map<DL.Entities.PaymentAndReceiptVoucher>(editPaymentAndReceiptVoucherDTO);
                    var CurrentVoucher = _uow.PaymentAndReceiptVoucherRepository.Get(t => t.Id == Voucher.Id);
                    Voucher.WarshahId = CurrentVoucher.WarshahId;
                    Voucher.TypeVoucher = CurrentVoucher.TypeVoucher;
                    Voucher.DocNumber = CurrentVoucher.DocNumber;
                   
                    _uow.PaymentAndReceiptVoucherRepository.Update(Voucher);
                    _uow.Save();
                    return Ok(Voucher);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Voucher");


        }


        // Delete voucher

        [HttpDelete, Route("Deletevoucher")]
        public IActionResult Deletevoucher(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.PaymentAndReceiptVoucherRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Voucher");


        }


        #endregion




    }

}
