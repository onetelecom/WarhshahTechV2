using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.InvoiceDTOs;
using DL.Entities;
using DocumentFormat.OpenXml.Wordprocessing;
using Helper;
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
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
    public class ReceiptVoucherController : ControllerBase
    {

        private readonly IBoxNow _boxNow;

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public ReceiptVoucherController(IUnitOfWork uow, IBoxNow boxNow,
 IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
            _boxNow = boxNow;
        }


        #region ReceiptCRUD

        //Create Receipt 

        [HttpPost, Route("CreateReceipt")]
        public IActionResult CreateReceipt(ReceiptVoucherDTO receiptVoucher)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var Receipt = _mapper.Map<DL.Entities.ReceiptVoucher>(receiptVoucher);
                    var receptionOrder = _uow.ReciptionOrderRepository.GetMany(t => t.Id == Receipt.ReciptionOrderId).FirstOrDefault();
                    var CurrentReceipt = _uow.ReceiptVouchersRepository.GetMany(t => t.ReciptionOrderId == Receipt.ReciptionOrderId).FirstOrDefault();
                    if (CurrentReceipt == null)
                    {
                        var docnumber = _uow.ReceiptVouchersRepository.GetMany(i => i.WarshahId == receptionOrder.warshahId).OrderByDescending(t => t.DocNumber).FirstOrDefault();
                        if (docnumber == null)
                        {
                            Receipt.DocNumber = 1;
                        }
                        else
                        {
                            int lastnumber = docnumber.DocNumber;
                            Receipt.DocNumber = lastnumber + 1;
                        }
                        Receipt.AdvancePayment = (decimal)receptionOrder.AdvancePayment;
                        Receipt.PaymentTypeInvoiceId = (int)receptionOrder.PaymentTypeInvoiceId;
                        Receipt.WarshahId = receptionOrder.warshahId;
                        Receipt.IsDeleted = false;
                        _uow.ReceiptVouchersRepository.Add(Receipt);

                        // Add To Transaction TODAY bOX when cash
                        if (Receipt.PaymentTypeInvoiceId == 1)
                        {

                            TransactionsToday transactionsToday = new TransactionsToday();

                            transactionsToday.WarshahId = Receipt.WarshahId;
                            transactionsToday.InvoiceNumber = Receipt.DocNumber;
                            transactionsToday.Vat = 0;
                            transactionsToday.Total = Receipt.AdvancePayment;
                            transactionsToday.CreatedOn = DateTime.Now;
                            transactionsToday.BeforeVat = 0;
                            transactionsToday.TransactionReason = Receipt.Discriptotion;
                            transactionsToday.TransactionName = "دفعة مقدمة";
                            var boxnow = _boxNow.GetBoxNow((int)transactionsToday.WarshahId);
                            decimal previousmoney = boxnow.TotalIncome;

                            var currenttotal = _uow.TransactionTodayRepository.GetMany(a => a.WarshahId == transactionsToday.WarshahId).OrderByDescending(t => t.Id).FirstOrDefault();
                            if (currenttotal != null)
                            {
                                transactionsToday.PreviousBalance = currenttotal.CurrentBalance;
                                transactionsToday.CurrentBalance = currenttotal.CurrentBalance + transactionsToday.Total;
                            }

                            else
                            {
                                transactionsToday.PreviousBalance = previousmoney;
                                transactionsToday.CurrentBalance = previousmoney + transactionsToday.Total;

                            }
                           

                            _uow.TransactionTodayRepository.Add(transactionsToday);

                        }

                        _uow.Save();
                        return Ok(Receipt);
                    }

                    else
                    {
                        return Ok("this Reception Order have created a Receipt Voucher");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Receipt");
        }



        // Get  ReceptionOrder by id


        [HttpGet, Route("GetReceptionOrderById")]
        public IActionResult GetReceptionOrderById(int? id)
        {

            var GetReceptionOrder = _uow.ReciptionOrderRepository.GetMany(t => t.Id == id).Include(t=>t.CarOwner).FirstOrDefault();

            return Ok(GetReceptionOrder);
        }




        // Get  Receipt by id


        [HttpGet, Route("GetReceipt")]
        public IActionResult GetReceipt(int? id)
        {

            var GetReceipt = _uow.ReceiptVouchersRepository.GetMany(t => t.Id == id).Include(t => t.ReciptionOrder.CarOwner).Include(t=>t.Warshah);

            return Ok(GetReceipt);
        }




        //Get All Receipts


       [HttpGet, Route("GetAllReceiptsByWarshah")]
        public IActionResult GetAllReceiptsByWarshah(int? id , int pagenumber , int pagecount)
        {
            var Receipts = _uow.ReceiptVouchersRepository.GetMany(t => t.WarshahId == id).ToHashSet();
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = Receipts.Count(), Listinvoice = Receipts.ToPagedList(pagenumber, pagecount) });
        }


        // Delete Receipt

        [HttpDelete, Route("DeleteReceipts")]
        public IActionResult DeleteReceipts(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.ReceiptVouchersRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Receipt");


        }


        #endregion




    }
}
