using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.ExpensesDTOs;
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

    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]

    public class ExpensesTransactionsController : ControllerBase
    {
        private readonly INotificationService _NotificationService;
        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

       

        private readonly IBoxNow _boxNow;

   


        // Constractor for controller 
        public ExpensesTransactionsController(INotificationService NotificationService, IBoxNow boxNow, IUnitOfWork uow, IMapper mapper)
        {
            _boxNow = boxNow;
            _NotificationService = NotificationService;
            _mapper = mapper;
            _uow = uow;
        }


        #region ExpensesTransactionsCRUD

        //Create ExpensesTransactions

        [HttpPost, Route("CreateExpensesTransactions")]
        public IActionResult CreateExpensesTransactions(ExpensesTransactionDTO expensesTransactionDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var expensesTransaction = _mapper.Map<DL.Entities.ExpensesTransaction>(expensesTransactionDTO);
                    if(expensesTransaction.ExpensesCategoryId == 1)
                    {
                        var v = 15;
                        decimal Vat = (((decimal)v) / (100));
                        decimal V = (decimal)System.Convert.ToDouble(1.15);

                        expensesTransaction.TotalWithoutVat = expensesTransaction.Total / V;
                        expensesTransaction.Vat = Vat * expensesTransaction.TotalWithoutVat;
                      
                    }
                    else
                    {
                        expensesTransaction.Vat = 0;
                        expensesTransaction.TotalWithoutVat = expensesTransaction.Total;
                    }
                    expensesTransaction.IsDeleted = false;
                    expensesTransaction.CreatedOn = expensesTransaction.CreatedOn;
                    expensesTransaction.UpdatedOn = expensesTransaction.UpdatedOn; 
                    _uow.ExpensesTransactionRepository.Add(expensesTransaction);


                    // Add To Transaction TODAY bOX when cash
                    if (expensesTransaction.PaymentTypeInvoiceId == 1)
                    {

                        TransactionsToday transactionsToday = new TransactionsToday();
                        transactionsToday.WarshahId = (int)expensesTransaction.WarshahId;
                        transactionsToday.InvoiceNumber = Convert.ToInt32(expensesTransaction.InvoiceNumber);
                        transactionsToday.Vat = expensesTransaction.Vat;
                        transactionsToday.Total = expensesTransaction.Total;
                        transactionsToday.CreatedOn = DateTime.Now;
                        transactionsToday.BeforeVat = expensesTransaction.TotalWithoutVat;
                        transactionsToday.TransactionReason = expensesTransaction.ExpenseNameAr;
                        transactionsToday.TransactionName = "مصاريف";
                        var boxnow = _boxNow.GetBoxNow((int)expensesTransaction.WarshahId);
                        decimal previousmoney = boxnow.TotalIncome;

                        var currenttotal = _uow.TransactionTodayRepository.GetMany(a => a.WarshahId == transactionsToday.WarshahId).OrderByDescending(t => t.Id).FirstOrDefault();
                        if (currenttotal != null)
                        {
                            transactionsToday.PreviousBalance = currenttotal.CurrentBalance;
                            transactionsToday.CurrentBalance = currenttotal.CurrentBalance - transactionsToday.Total;
                        }
                      
                        else
                        {
                            transactionsToday.PreviousBalance = previousmoney;
                            transactionsToday.CurrentBalance = previousmoney - transactionsToday.Total;

                        }




                        _uow.TransactionTodayRepository.Add(transactionsToday);




                    }

                    _uow.Save();
                    var warshah = _uow.WarshahRepository.GetById(expensesTransaction.WarshahId);
                

                    var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == expensesTransaction.WarshahId & a.NameNotificationId == 20 & a.StatusNotificationId ==1 ).FirstOrDefault();

                    if (notificationActive != null)
                    {
                        var userid = _uow.UserRepository.GetMany(a => a.WarshahId == warshah.Id && a.RoleId == 1).FirstOrDefault();
                        if (userid != null)
                        {
                            _NotificationService.SetNotificationTaqnyat(userid.Id, "تم انشاء مصروف جديد  ");

                        }

                    }



                    return Ok(expensesTransaction);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Expenses Transaction");
        }

        // Get  Expenses Transactions  fro warshah to select category

        
        [HttpGet, Route("GetExpensesTransactionByCategoryId")]
        public IActionResult GetExpensesTransactionByCategoryId(int warshahid , int categoryid)
        {
            var expensesTransaction = _uow.ExpensesTransactionRepository.GetMany(t => t.WarshahId == warshahid && t.ExpensesCategoryId == categoryid).Include(t => t.ExpensesCategory).Include(t => t.PaymentTypeInvoice).ToHashSet();

            return Ok(expensesTransaction);
        }


        // Get  ExpensesType for (Id)


        [HttpGet, Route("GetExpenseTransactionByID")]
        public IActionResult GetExpenseTransactionByID(int ExpenseTransactionId)
        {
            var CurrentTransaction = _uow.ExpensesTransactionRepository.GetMany(t => t.Id == ExpenseTransactionId).Include(t => t.ExpensesCategory).Include(t => t.PaymentTypeInvoice).ToHashSet();
            return Ok(CurrentTransaction);
        }




        // Get All Expenses Transaction for warshah
        [HttpGet, Route("GetAllExpensesTransaction")]
        public IActionResult GetAllExpensesTransaction(int warshahid, int pagenumber, int pagecount)
        {
            var expensesTransaction = _uow.ExpensesTransactionRepository.GetMany(t => t.WarshahId == warshahid).Include(t => t.ExpensesCategory).Include(t=>t.PaymentTypeInvoice).ToHashSet();
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = expensesTransaction.Count(), Listinvoice = expensesTransaction.ToPagedList(pagenumber, pagecount) });

        }


        // Update Expenses Transaction

        [HttpPost, Route("EditExpensesTransaction")]
        public IActionResult EditExpensesTransaction(EditExpensesTransactionDTO editExpensesTransactionDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var expensesTransaction = _mapper.Map<DL.Entities.ExpensesTransaction>(editExpensesTransactionDTO);
                    if (expensesTransaction.ExpensesCategoryId == 1)
                    {
                        var v = 15;
                        decimal Vat = (((decimal)v) / (100));
                        decimal V = (decimal)System.Convert.ToDouble(1.15);

                        expensesTransaction.TotalWithoutVat = expensesTransaction.Total / V;
                        expensesTransaction.Vat = Vat * expensesTransaction.TotalWithoutVat;

                    }
                    else
                    {
                        expensesTransaction.Vat = 0;
                        expensesTransaction.TotalWithoutVat = expensesTransaction.Total;
                    }
                    expensesTransaction.UpdatedOn = DateTime.Now;
                    _uow.ExpensesTransactionRepository.Update(expensesTransaction);

                   
                    _uow.Save();
                    return Ok(expensesTransaction);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Expenses Transaction");


        }


        // Delete Expenses Transaction

        [HttpDelete, Route("DeleteExpensesTransaction")]
        public IActionResult DeleteExpensesTransaction(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.ExpensesTransactionRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Expenses Transaction");


        }

        #endregion
    }

}
