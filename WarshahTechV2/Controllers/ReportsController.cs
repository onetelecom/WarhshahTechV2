using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.Dashboard;
using DL.DTOs.Reports;
using DL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using DL.MailModels;
using Hangfire;
using Helper;
using HELPER;
using System.Security.Claims;
using WarshahTechV2.Models;
using System.Runtime.InteropServices;
using DL.Migrations;
using DL.DTOs.InvoiceDTOs;
using DocumentFormat.OpenXml.Office2010.Excel;
using PagedList;
using MessagePack;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]
    //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
    public class ReportsController : ControllerBase
    {
        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public ReportsController(IUnitOfWork uow, IMapper mapper, IMailService MailService , INotificationService NotificationService)
        {
            _mapper = mapper;
            _uow = uow;
            _MailService = MailService;
            _NotificationService = NotificationService;
        }

        private readonly IMailService _MailService;
        private readonly INotificationService _NotificationService;


        #region Net Income and Net Vat    (حساب صافى الدخل و حساب الضريبة المضافة)

        [HttpGet, Route("NetIncomeReport")]
        public IActionResult NetIncomeReport(DateTime FromDate, DateTime ToDate, int warshahid)
        {
            IncomeAndVatReportDTO IncomeVatDto = new IncomeAndVatReportDTO();

            var warshah = _uow.WarshahRepository.GetById(warshahid);

           

            List<DL.Entities.Invoice> sales = new List<DL.Entities.Invoice>();

            #region Sales and Invoice and Debit     لحساب الفواتير التى تم سدادها و إشعارات المدين التى تم سدادها إلى الورشة

            // حساب إجمالى الفواتير و الضريبة المضافة للفواتير    ( InvoiceStatusId == 2   is   Invouce Paid ) 
            if(warshahid == 4)
            {
                //sales = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.InvoiceStatusId == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate && s.InvoiceSerial != "IN-4-808").ToList();
                sales = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.InvoiceStatusId == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToList();

            }

            else
            {
                sales = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.InvoiceStatusId == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToList();
            }
            decimal tSalaes = sales.Sum(s => s.BeforeDiscount);
            decimal tvatSales = sales.Sum(s => s.VatMoney);
            //

            // الفواتير القديمة


            var OldTotalinvoices = _uow.OldInvoicesRepository.GetMany(i => i.WarshahId == warshah.OldWarshahId && i.InvoiceStatusId == 3 && i.OldCreatedon >= FromDate && i.OldCreatedon <= ToDate)?.ToHashSet();

            decimal OldtotalInvoice = (decimal)OldTotalinvoices.Sum(s => s.Total);
            decimal tvatOld = OldTotalinvoices.Sum(s => s.VatMoney);

            // حساب إجمالى إشعارات المدين و الضريبة المضافة لإشعارات    ( Flag == 1   is  Debit Notice )
            var debits = _uow.DebitAndCreditorRepository.GetMany(s => s.WarshahId == warshahid && s.Flag == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal tDebits = debits.Sum(s => s.TotalWithoutVat); 
            decimal tvatDebits = debits.Sum(s => s.Vat);
            //

            // حساب المبيعات الكلى و الضريبة الكلية للمبيعات
            IncomeVatDto.TotalSales = tSalaes + tDebits;
            IncomeVatDto.VatSales = tvatSales + tvatDebits;
            //

            #endregion


            #region Purchases  and Creditors                       لحساب المشتريات التى تم سدادها و إشعارات الدائن التى تم سدادها من الورشة

            // حساب إجمالى المشتريات و الضريبة المضافة للمشتريات
            // 
            decimal TDebits = 0;
            decimal TInvoices = 0;
            var OrderList = new List<DL.Entities.SparePart>();

            if (sales.Count > 0)
            {
                foreach (var Rorders in sales)
                {
                    var parts = _uow.RepairOrderSparePartRepository.GetMany(p => p.RepairOrderId == Rorders.RepairOrderId).ToHashSet();

                    foreach (var part in parts)
                    {
                        var Qty = part.Quantity;
                        var usedPart = _uow.SparePartRepository.GetById(part.SparePartId);
                        decimal buyPrice = usedPart.BuyingPrice;
                        decimal Cost = Qty * buyPrice;
                        TInvoices += Cost;

                    }

                }
            }

            if (debits.Count > 0)
            {
                foreach (var Rorders in debits)
                {
                    var parts = _uow.NoticeProductRepository.GetMany(p => p.DebitAndCreditorId == Rorders.Id).ToHashSet();

                    foreach (var part in parts)
                    {
                        if(part.PartId != 0 )
                        {
                            var Qty = part.Quantity;
                            var usedPart = _uow.SparePartRepository.GetById(part.PartId);
                            decimal buyPrice = usedPart.BuyingPrice;
                            decimal Cost = Qty * buyPrice;
                            TDebits += Cost;

                        }

                    }

                }
            }


          
            IncomeVatDto.TotalPurchases = TDebits + TInvoices;
                  
            decimal tPurchases = IncomeVatDto.TotalPurchases;
            decimal Vat = 0;
            var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == warshah.Id).FirstOrDefault();
            if (VAT == null)
            {

                Vat = 0;
            }
            else
            {
                Vat = (((decimal)VAT.GetVAT) / (100));
            }
            decimal tvatPurchases = tPurchases * Vat;
            //

            //    حساب إجمالى إشعارات الدائن و الضريبة المضافة للإشعارات   ( Flag == 2   is  creditor Notice )
            var creditors = _uow.DebitAndCreditorRepository.GetMany(s => s.WarshahId == warshahid && s.Flag == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal tcreditors = 0;
            decimal TCreditor = 0;
            if (creditors.Count > 0)
            {
                foreach (var Rorders in creditors)
                {
                    var parts = _uow.NoticeProductRepository.GetMany(p => p.DebitAndCreditorId == Rorders.Id).ToHashSet();

                    foreach (var part in parts)
                    {
                        if (part.PartId != 0)
                        {
                            var Qty = part.Quantity;
                            var usedPart = _uow.SparePartRepository.GetById(part.PartId);
                            decimal buyPrice = usedPart.BuyingPrice;
                            decimal Cost = Qty * buyPrice;
                            tcreditors += Cost;
                        }

                    }

                   


                }
            }

            TCreditor = creditors.Sum(s => s.TotalWithoutVat);
            decimal tvatcreditors = creditors.Sum(s => s.Vat);
            //

            // حساب المشتريات الكلى ( المشتريات + المرتجعات ( إشعارات الدائن)   و الضريبة الكلية للمشتريات
            if(warshahid != 4)
            {
                IncomeVatDto.TotalPurchases = tPurchases + TCreditor;
                IncomeVatDto.VatPurchases = tvatPurchases + tvatcreditors;
            }
            else
            {
                IncomeVatDto.TotalPurchases =  TCreditor;
                IncomeVatDto.VatPurchases =  tvatcreditors;
            }
            
          
            //

            #endregion


            #region Expenses Vat  and Expenses Without Vat    لحساب المصاريف الخاضعة والغير خاضعة للضريبة

            // حساب إجمالى المصاريف الخاضعة و الضريبة المضافة للمصاريف    ( Category ID == 1   is   With Vat ) 
            var Vexpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == warshahid && s.ExpensesCategoryId == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal tVexpenses = Vexpenses.Sum(s => s.TotalWithoutVat);
            decimal tVatexpenses = Vexpenses.Sum(s => s.Vat);
            //

            // حساب إجمالى المصاريف الغير الخاضعة     ( Category ID == 2   is   Without Vat )
            var NVexpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == warshahid && s.ExpensesCategoryId == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal tNVexpenses = NVexpenses.Sum(s => s.Total);
            decimal tNVatexpenses = 0;
            //

            // حساب المصاريف و الضريبة 
            IncomeVatDto.TotalExpenses = tVexpenses;
            IncomeVatDto.VatExpenses = tVatexpenses;
            IncomeVatDto.TotalExpensesWithoutVat = tNVexpenses;
            IncomeVatDto.ZeroVatExpenses = tNVatexpenses;
            //

            #endregion


            #region Calculte Net Income and Net Vat for hayqa el zakka

            IncomeVatDto.NetIncome = (IncomeVatDto.TotalSales + OldtotalInvoice) - (IncomeVatDto.TotalPurchases + IncomeVatDto.TotalExpenses + IncomeVatDto.TotalExpensesWithoutVat);
            IncomeVatDto.NetVat = (IncomeVatDto.VatSales + tvatOld) - (IncomeVatDto.VatPurchases + IncomeVatDto.VatExpenses);

            #endregion

            return Ok(IncomeVatDto);
        }

        #endregion



        #region Net Income and Net Vat  for all branches   (حساب صافى الدخل و حساب الضريبة المضافة  لكل الفروع)

        [HttpGet, Route("TaxReportAllBranches")]
        public IActionResult TaxReportAllBranches(DateTime FromDate, DateTime ToDate, int warshahid)
        {
            IncomeAndVatReportDTO IncomeVatDto = new IncomeAndVatReportDTO();

            var warshah = _uow.WarshahRepository.GetById(warshahid);

            var branches = _uow.WarshahRepository.GetMany(a=>a.ParentWarshahId == warshahid).ToHashSet();

            #region Sales and Invoice and Debit     لحساب الفواتير التى تم سدادها و إشعارات المدين التى تم سدادها إلى الورشة

            // حساب إجمالى الفواتير و الضريبة المضافة للفواتير    ( InvoiceStatusId == 2   is   Invouce Paid ) 
            var sales = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.InvoiceStatusId == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal tSalaes = sales.Sum(s => s.BeforeDiscount);
            decimal tvatSales = sales.Sum(s => s.VatMoney);


            var OldTotalinvoices = _uow.OldInvoicesRepository.GetMany(i => i.WarshahId == warshah.OldWarshahId && i.InvoiceStatusId == 3 && i.OldCreatedon >= FromDate && i.OldCreatedon <= ToDate)?.ToHashSet();

            decimal OldtotalInvoice = (decimal)OldTotalinvoices.Sum(s => s.Total);
            decimal tvatOld = OldTotalinvoices.Sum(s => s.VatMoney);


            decimal salesbranches = 0;
            decimal vatbranches = 0;
             
            if(branches != null)
            {
                foreach( var branch in branches)
                {

                    if(branch.TaxNumber == warshah.TaxNumber)
                    {
                    var branchInvoice = _uow.InvoiceRepository.GetMany(s => s.WarshahId == branch.Id && s.InvoiceStatusId == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();

                    decimal tbranch = branchInvoice.Sum(s => s.BeforeDiscount);
                    
                    decimal tvatbranch = branchInvoice.Sum(s=>s.VatMoney);

                    salesbranches += tbranch;
                    vatbranches += tvatbranch;
                    }
                   

                }
            }
           



            //

            // حساب إجمالى إشعارات المدين و الضريبة المضافة لإشعارات    ( Flag == 1   is  Debit Notice )
            var debits = _uow.DebitAndCreditorRepository.GetMany(s => s.WarshahId == warshahid && s.Flag == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal tDebits = debits.Sum(s => s.TotalWithoutVat);
            decimal tvatDebits = debits.Sum(s => s.Vat);

            decimal tDebitsbranches = 0;
            decimal vatDebitsbranches = 0;

            if (branches != null)
            {
                foreach (var branch in branches)
                {
                    if (branch.TaxNumber == warshah.TaxNumber)
                    {
                    var debitsbranch = _uow.DebitAndCreditorRepository.GetMany(s => s.WarshahId == branch.Id && s.Flag == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
                    decimal tDebitsbranch = debitsbranch.Sum(s => s.TotalWithoutVat);
                    decimal tvatDebitsbranch = debitsbranch.Sum(s => s.Vat);

                    tDebitsbranches += tDebitsbranch;
                    vatDebitsbranches += tvatDebitsbranch;
                    }
                

                }
            }




            //

            // حساب المبيعات الكلى و الضريبة الكلية للمبيعات
            IncomeVatDto.TotalSales = tSalaes + salesbranches + tDebits + tDebitsbranches;
            IncomeVatDto.VatSales = tvatSales + vatbranches + tvatDebits + vatDebitsbranches;
            //

            #endregion









            #region Purchases  and Creditors                       لحساب المشتريات التى تم سدادها و إشعارات الدائن التى تم سدادها من الورشة

            // حساب إجمالى المشتريات و الضريبة المضافة للمشتريات
            // 
            decimal TDebits = 0;
            decimal TInvoices = 0;
            var OrderList = new List<DL.Entities.SparePart>();

            if (sales.Count > 0)
            {
                foreach (var Rorders in sales)
                {
                    var parts = _uow.RepairOrderSparePartRepository.GetMany(p => p.RepairOrderId == Rorders.RepairOrderId).ToHashSet();

                    foreach (var part in parts)
                    {
                        var Qty = part.Quantity;
                        var usedPart = _uow.SparePartRepository.GetById(part.SparePartId);
                        decimal buyPrice = usedPart.BuyingPrice;
                        decimal Cost = Qty * buyPrice;
                        TInvoices += Cost;

                    }

                }
            }

            if (debits.Count > 0)
            {
                foreach (var Rorders in debits)
                {
                    var parts = _uow.NoticeProductRepository.GetMany(p => p.DebitAndCreditorId == Rorders.Id).ToHashSet();

                    foreach (var part in parts)
                    {
                        var Qty = part.Quantity;
                        var usedPart = _uow.SparePartRepository.GetById(part.PartId);
                        decimal buyPrice = usedPart.BuyingPrice;
                        decimal Cost = Qty * buyPrice;
                        TDebits += Cost;

                    }

                }
            }


            decimal PurchasesBranches = 0;
            decimal DebitssBranch = 0;

            if (branches != null)
            {
                foreach (var branch in branches)
                {
                    if (branch.TaxNumber == warshah.TaxNumber)
                    {
            var branchInvoice = _uow.InvoiceRepository.GetMany(s => s.WarshahId == branch.Id && s.InvoiceStatusId == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();

                    if (branchInvoice.Count > 0)
                    {
                        foreach (var Rorders in branchInvoice)
                        {
                            var parts = _uow.RepairOrderSparePartRepository.GetMany(p => p.RepairOrderId == Rorders.RepairOrderId).ToHashSet();

                            foreach (var part in parts)
                            {
                                var Qty = part.Quantity;
                                var usedPart = _uow.SparePartRepository.GetById(part.SparePartId);
                                decimal buyPrice = usedPart.BuyingPrice;
                                decimal Cost = Qty * buyPrice;
                                PurchasesBranches += Cost;

                            }

                        }
                    }
                    }

                   



                }
            }

            if (branches != null)
            {
                foreach (var branch in branches)
                {

                    if (branch.TaxNumber == warshah.TaxNumber)
                    {
  var debitsBranches = _uow.DebitAndCreditorRepository.GetMany(s => s.WarshahId == branch.Id && s.Flag == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();

                    if (debitsBranches.Count > 0)
                    {
                        foreach (var Rorders in debitsBranches)
                        {
                            var parts = _uow.NoticeProductRepository.GetMany(p => p.DebitAndCreditorId == Rorders.Id).ToHashSet();

                            foreach (var part in parts)
                            {
                                var Qty = part.Quantity;
                                var usedPart = _uow.SparePartRepository.GetById(part.PartId);
                                decimal buyPrice = usedPart.BuyingPrice;
                                decimal Cost = Qty * buyPrice;
                                DebitssBranch += Cost;

                            }

                        }
                    }
                    }
                  



                }
            }



            IncomeVatDto.TotalPurchases = TDebits + TInvoices + PurchasesBranches + DebitssBranch;
            decimal tPurchases = IncomeVatDto.TotalPurchases;
            decimal Vat = 0;
            var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == warshah.Id).FirstOrDefault();
            if (VAT == null)
            {

                Vat = 0;

            }
            else
            {
                Vat = (((decimal)VAT.GetVAT) / (100));
            }
            decimal tvatPurchases = tPurchases * Vat;
            //

            //    حساب إجمالى إشعارات الدائن و الضريبة المضافة للإشعارات   ( Flag == 2   is  creditor Notice )
            var creditors = _uow.DebitAndCreditorRepository.GetMany(s => s.WarshahId == warshahid && s.Flag == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal tcreditors = 0;
            decimal TCreditor = 0; 
            if (creditors.Count > 0)
            {
                foreach (var Rorders in creditors)
                {
                    var parts = _uow.NoticeProductRepository.GetMany(p => p.DebitAndCreditorId == Rorders.Id).ToHashSet();

                    foreach (var part in parts)
                    {
                        var Qty = part.Quantity;
                        var usedPart = _uow.SparePartRepository.GetById(part.PartId);
                        decimal buyPrice = usedPart.BuyingPrice;
                        decimal Cost = Qty * buyPrice;
                        tcreditors += Cost;

                    }

                }
            }


            decimal tcreditorsBranches = 0;
            decimal tvatcreditorbranches = 0;

            if (branches != null)
            {
                foreach (var branch in branches)
                {

                    if (branch.TaxNumber == warshah.TaxNumber)
                    {
 var creditorsBranches = _uow.DebitAndCreditorRepository.GetMany(s => s.WarshahId == branch.Id && s.Flag == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();

                    if (creditorsBranches.Count > 0)
                    {
                        foreach (var Rorders in creditorsBranches)
                        {
                            var parts = _uow.NoticeProductRepository.GetMany(p => p.DebitAndCreditorId == Rorders.Id).ToHashSet();

                            foreach (var part in parts)
                            {
                                var Qty = part.Quantity;
                                var usedPart = _uow.SparePartRepository.GetById(part.PartId);
                                decimal buyPrice = usedPart.BuyingPrice;
                                decimal Cost = Qty * buyPrice;
                                tcreditorsBranches += Cost;

                            }

                        }
                    }
                    }
                   
                }
            }

            tvatcreditorbranches = tcreditorsBranches * Vat; 


            TCreditor = creditors.Sum(s => s.TotalWithoutVat);
            decimal tvatcreditors = creditors.Sum(s => s.Vat);
            //

            // حساب المشتريات الكلى ( المشتريات + المرتجعات ( إشعارات الدائن)   و الضريبة الكلية للمشتريات
            IncomeVatDto.TotalPurchases = tPurchases + TCreditor + tcreditorsBranches ;
            IncomeVatDto.VatPurchases = tvatPurchases + tvatcreditors + tvatcreditorbranches;
            //

            #endregion

 









            #region Expenses Vat  and Expenses Without Vat    لحساب المصاريف الخاضعة والغير خاضعة للضريبة

            // حساب إجمالى المصاريف الخاضعة و الضريبة المضافة للمصاريف    ( Category ID == 1   is   With Vat ) 
            var Vexpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == warshahid && s.ExpensesCategoryId == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal tVexpenses = Vexpenses.Sum(s => s.TotalWithoutVat);
            decimal tVatexpenses = Vexpenses.Sum(s => s.Vat);


            decimal Expensesbranches = 0;
            decimal vatExpensesbranches = 0;

            if (branches != null)
            {
                foreach (var branch in branches)
                {
                    if (branch.TaxNumber == warshah.TaxNumber)
                    {

                    var branchExpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == branch.Id && s.ExpensesCategoryId == 1 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();

                    decimal tbranch = branchExpenses.Sum(s => s.TotalWithoutVat);

                    decimal tvatbranch = branchExpenses.Sum(s => s.Vat);

                    Expensesbranches += tbranch;
                    vatExpensesbranches += tvatbranch;
                    }


                }
            }




            //

            // حساب إجمالى المصاريف الغير الخاضعة     ( Category ID == 2   is   Without Vat )
            var NVexpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == warshahid && s.ExpensesCategoryId == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            decimal tNVexpenses = NVexpenses.Sum(s => s.Total);
            decimal tNVatexpenses = 0;


            decimal TNVExpensesbranches = 0;
            decimal vatwithoutExpensesbranches = 0;

            if (branches != null)
            {
                foreach (var branch in branches)
                {
                    if (branch.TaxNumber == warshah.TaxNumber)
                    {
 var branchExpenses = _uow.ExpensesTransactionRepository.GetMany(s => s.WarshahId == branch.Id && s.ExpensesCategoryId == 2 && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();

                    decimal tbranch = branchExpenses.Sum(s => s.Total);

                    decimal tvatbranch = branchExpenses.Sum(s => s.Vat);

                    TNVExpensesbranches += tbranch;
                    vatwithoutExpensesbranches = 0;
                    }

                   

                }
            }



            //

            // حساب المصاريف و الضريبة 
            IncomeVatDto.TotalExpenses = tVexpenses + Expensesbranches;
            IncomeVatDto.VatExpenses = tVatexpenses + vatExpensesbranches;
            IncomeVatDto.TotalExpensesWithoutVat = tNVexpenses + TNVExpensesbranches;
            IncomeVatDto.ZeroVatExpenses = tNVatexpenses + vatwithoutExpensesbranches;
            //

            #endregion


            #region Calculte Net Income and Net Vat for hayqa el zakka

            IncomeVatDto.NetIncome =( IncomeVatDto.TotalSales + OldtotalInvoice) - (IncomeVatDto.TotalPurchases + IncomeVatDto.TotalExpenses + IncomeVatDto.TotalExpensesWithoutVat);
            IncomeVatDto.NetVat = (IncomeVatDto.VatSales + tvatOld) - (IncomeVatDto.VatPurchases + IncomeVatDto.VatExpenses);

            #endregion

            return Ok(IncomeVatDto);
        }

        #endregion


        // Get all branches by warshah id


        [HttpGet, Route("GetBranchesbyWarshahid")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetBranchesbyWarshahid(int warshahId)
        {
            var branches = _uow.WarshahRepository.GetMany(a => a.ParentWarshahId == warshahId).ToHashSet();

            return Ok(branches);
        }

            // number invoices

            [HttpGet, Route("GetChartInvoices")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetChartInvoices(int warshahId)
        {
            var InvoiceNotPaid = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 1).ToHashSet().Count;
            var InvoicePaid = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 2).ToHashSet().Count;
            var InvoiceDelayed = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 4).ToHashSet().Count;


            //var salesrequestorder = _uow.SalesRequestRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet().OrderByDescending(a => a.Id);




            List<ReportChart> list = new List<ReportChart>();

            var open = new ReportChart
            {
                Id = 1,
                Name = "Invoice Open",
                Value = InvoiceNotPaid,

            };

            var Paid = new ReportChart
            {
                Id = 2,
                Name = "Invoice Paid",
                Value = InvoicePaid,

            };

            var Delayed = new ReportChart
            {
                Id = 3,
                Name = "Invoice Delay",
                Value = InvoiceDelayed,

            };




            list.Add(open);
            list.Add(Paid);
            list.Add(Delayed);




            return Ok(list);
        }



        // number invoices

        [HttpGet, Route("ChartInvoicesTotal")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult ChartInvoicesTotal(int warshahId)
        {
            var InvoiceNotPaid = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 1).ToHashSet();
            decimal TotalNotPaid = InvoiceNotPaid.Sum(s => s.Total);
            var InvoicePaid = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 2).ToHashSet();
            decimal TotalPaid = InvoicePaid.Sum(s => s.Total);
            var InvoiceDelayed = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 4).ToHashSet();
            decimal TotalDelayed = InvoiceDelayed.Sum(s => s.Total);

            //var salesrequestorder = _uow.SalesRequestRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet().OrderByDescending(a => a.Id);




            List<ReportChart> list = new List<ReportChart>();

            var open = new ReportChart
            {
                Id = 4,
                Name = "Invoice Open",
                Value = TotalNotPaid,

            };

            var Paid = new ReportChart
            {
                Id = 5,
                Name = "Invoice Paid",
                Value = TotalPaid,

            };

            var Delayed = new ReportChart
            {
                Id = 6,
                Name = "Invoice Delay",
                Value = TotalDelayed,

            };




            list.Add(open);
            list.Add(Paid);
            list.Add(Delayed);




            return Ok(list);
        }





        // reports

        [HttpGet, Route("GetCountAllInvoices")]
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetCountAllInvoices(int year)
        {

            var Invoices = _uow.InvoiceRepository.GetMany(a=>a.CreatedOn.Year == year && a.WarshahId != 53);

            return Ok(Invoices.Count());
        }



        [HttpGet, Route("GetCountAllOldInvoices")]
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetCountAllOldInvoices(int year)
        {

            var Invoices = _uow.OldInvoicesRepository.GetMany(a => a.OldCreatedon.Year == year);

            return Ok(Invoices.Count());
        }


        [HttpGet, Route("GetTotalAllInvoices")]
        public IActionResult GetTotalAllInvoices(int year)
        {

           
            DateTime start = new DateTime(2023, 12, 31);
            DateTime End = new DateTime(2024, 12, 31);


            var Invoices = _uow.InvoiceRepository.GetMany(a => a.CreatedOn >= start && a.CreatedOn <= End && a.WarshahId != 53).ToHashSet();
            //var OldInvoices = _uow.OldInvoicesRepository.GetMany(a => a.OldCreatedon.Year == year);

            var newinv = Invoices.Sum(a => a.Total);

            //var oldinv = OldInvoices.Sum(a => a.Total);

            //var total = newinv + oldinv; 

            return Ok(newinv);
        }



        [HttpGet, Route("GetlAllUsers")]
        public IActionResult GetlAllUsers()
        {

            var users = _uow.UserRepository.GetMany(a => a.RoleId == 2);
          

            return Ok(users.Count());
        }

        [HttpGet, Route("GetlAllMotors")]
        public IActionResult GetlAllMotors()
        {

            var users = _uow.MotorsRepository.GetAll( );


            return Ok(users.Count());
        }






        // number repair order

        [HttpGet, Route("GetChartRepairOrder")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetChartRepairOrder(int warshahId)
        {


            // repair Order  أوامر قيد الإصلاح   (repair order open != 7 || rejected == 9)

            var OpenRepairOrder = _uow.RepairOrderRepository.GetMany(o => o.WarshahId == warshahId && (o.RepairOrderStatus != 7 || o.RepairOrderStatus == 9)).ToHashSet().Count;

            // repair Order  أوامر المغلقة   (repair order closed = 7 )

            var ClosedRepairOrder = _uow.RepairOrderRepository.GetMany(o => o.WarshahId == warshahId && o.RepairOrderStatus == 7).ToHashSet().Count;

            // repair Order  أوامر مرفوضة   (repair order Reject = 9 )

            var RejectRepairOrder = _uow.RepairOrderRepository.GetMany(o => o.WarshahId == warshahId && o.RepairOrderStatus == 9).ToHashSet().Count;


            List<ReportChart> list = new List<ReportChart>();

            var open = new ReportChart
            {
                Id = 7,
                Name = "Open RepairOrder",
                Value = OpenRepairOrder,

            };

            var Closed = new ReportChart
            {
                Id = 8,
                Name = "Closed RepairOrder",
                Value = ClosedRepairOrder,

            };

            var Reject = new ReportChart
            {
                Id = 9,
                Name = "Reject  RepairOrder",
                Value = RejectRepairOrder,

            };




            list.Add(open);
            list.Add(Closed);
            list.Add(Reject);




            return Ok(list);
        }


        // number repair order

        [HttpGet, Route("GetCarOwner")]
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetCarOwner(int warshahId)
        {


            IEnumerable<int> owners = _uow.WarshahCarOwnersRepository.GetMany(o => o.WarshahId == warshahId).Select(a => a.CarOwnerId).ToList();







            decimal OwnerCount = owners.Distinct().Count();


            List<ReportChart> list = new List<ReportChart>();



            var TotalOwnersinWarshsh = new ReportChart
            {
                Id = 1,
                Name = "Total Car Owners",
                Value = OwnerCount,

            };


            list.Add(TotalOwnersinWarshsh);




            return Ok(list);
        }


        // Charts in Period 



        [HttpGet, Route("AllChartPeriod")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult AllChartPeriod(DateTime FromDate, DateTime ToDate, int warshahId)
        {
            var InvoiceNotPaid = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 1 && a.CreatedOn >= FromDate && a.CreatedOn <= ToDate).ToHashSet().Count;
            var InvoicePaid = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 2 && a.CreatedOn >= FromDate && a.CreatedOn <= ToDate).ToHashSet().Count;
            var InvoiceDelayed = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 4 && a.CreatedOn >= FromDate && a.CreatedOn <= ToDate).ToHashSet().Count;


            var InvoiceNotPaid1 = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 1 && a.CreatedOn >= FromDate && a.CreatedOn <= ToDate).ToHashSet();
            decimal TotalNotPaid = InvoiceNotPaid1.Sum(s => s.Total);
            var InvoicePaid1 = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 2 && a.CreatedOn >= FromDate && a.CreatedOn <= ToDate).ToHashSet();
            decimal TotalPaid = InvoicePaid1.Sum(s => s.Total);
            var InvoiceDelayed1 = _uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.InvoiceStatusId == 4 && a.CreatedOn >= FromDate && a.CreatedOn <= ToDate).ToHashSet();
            decimal TotalDelayed = InvoiceDelayed1.Sum(s => s.Total);


            // repair Order  أوامر قيد الإصلاح   (repair order open != 7 || rejected == 9)

            var OpenRepairOrder = _uow.RepairOrderRepository.GetMany(o => o.WarshahId == warshahId && (o.RepairOrderStatus != 7 || o.RepairOrderStatus == 9) && o.CreatedOn >= FromDate && o.CreatedOn <= ToDate).ToHashSet().Count;

            // repair Order  أوامر المغلقة   (repair order closed = 7 )

            var ClosedRepairOrder = _uow.RepairOrderRepository.GetMany(o => o.WarshahId == warshahId && o.RepairOrderStatus == 7 && o.CreatedOn >= FromDate && o.CreatedOn <= ToDate).ToHashSet().Count;

            // repair Order  أوامر مرفوضة   (repair order Reject = 9 )

            var RejectRepairOrder = _uow.RepairOrderRepository.GetMany(o => o.WarshahId == warshahId && o.RepairOrderStatus == 9 && o.CreatedOn >= FromDate && o.CreatedOn <= ToDate).ToHashSet().Count;












            List<ReportChart> list = new List<ReportChart>();

            var open = new ReportChart
            {
                Id = 1,
                Name = "Invoice Open",
                Value = InvoiceNotPaid,

            };

            var Paid = new ReportChart
            {
                Id = 2,
                Name = "Invoice Paid",
                Value = InvoicePaid,

            };

            var Delayed = new ReportChart
            {
                Id = 3,
                Name = "Invoice Delay",
                Value = InvoiceDelayed,

            };

            var TNotPaid = new ReportChart
            {
                Id = 4,
                Name = " Total Invoice Open",
                Value = TotalNotPaid,

            };

            var TPaid = new ReportChart
            {
                Id = 5,
                Name = "Total Invoice Paid",
                Value = TotalPaid,

            };

            var TDelayed = new ReportChart
            {
                Id = 6,
                Name = "Total Invoice Delay",
                Value = TotalDelayed,

            };



            var openorder = new ReportChart
            {
                Id = 7,
                Name = "Open Repair Order",
                Value = OpenRepairOrder,

            };

            var ClosedOrder = new ReportChart
            {
                Id = 8,
                Name = "Closed Repair Order",
                Value = ClosedRepairOrder,

            };

            var RejectOrder = new ReportChart
            {
                Id = 9,
                Name = "Reject Repair Order",
                Value = RejectRepairOrder,

            };





            list.Add(open);
            list.Add(Paid);
            list.Add(Delayed);



            list.Add(TNotPaid);
            list.Add(TPaid);
            list.Add(TDelayed);


            list.Add(openorder);
            list.Add(ClosedOrder);
            list.Add(RejectOrder);


            return Ok(list);
        }





        //public ActionResult ExportExcel(DateTime FromDate, DateTime ToDate, int warshahid)
        //{

        //    var warshah = _uow.WarshahRepository.GetById(warshahid);
        //    var Invoices = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();




        //    //Get the GridView Data from database.
        //    DataTable dt = new DataTable();



        //    dt.Rows.Add(Invoices);
        //    //Set DataTable Name which will be the name of Excel Sheet.
        //    dt.TableName = "الفواتير اليومية";




        //    //Create a New Workbook.
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        //Add the DataTable as Excel Worksheet.
        //        wb.Worksheets.Add(dt);

        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            //Save the Excel Workbook to MemoryStream.
        //            wb.SaveAs(memoryStream);

        //            //Convert MemoryStream to Byte array.
        //            byte[] bytes = memoryStream.ToArray();
        //            memoryStream.Close();





        //            using (MailMessage mm = new MailMessage("engsalahahmed27@gmail.com", "engsalahahmed27@gmail.com"))
        //            {

        //                SmtpClient SmtpServer = new SmtpClient();
        //                //MailMessage mail = new MailMessage();
        //                mm.IsBodyHtml = true;
        //                SmtpServer.UseDefaultCredentials = false;

        //                mm.To.Add("engsalahahmed27@gmail.com".Trim());

        //                mm.Subject = "تقرير القيمة المضافة";
        //                mm.Body = "تقرير القيمة المضافة ";
        //                mm.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(bytes), " " + warshah.WarshahNameAr + ".xlsx"));
        //                mm.IsBodyHtml = true;
        //                mm.BodyEncoding = System.Text.Encoding.UTF8;
        //                mm.SubjectEncoding = System.Text.Encoding.UTF8;
        //                mm.Priority = MailPriority.High;
        //                SmtpServer.EnableSsl = true;

        //                _MailService.Notification("engsalahahmed27@gmail.com", mm);
        //                //SmtpServer.Send(mm);


        //            }
        //        }

        //        return Ok();
        //    }








        [HttpGet, Route("InvoiceToday")]
        public IActionResult InvoiceToday(int warshahid)
        {

           DateTime  FromDate = DateTime.Today.AddDays(-2);
           DateTime  ToDate   = DateTime.Today.AddDays(0);

            var warshah = _uow.WarshahRepository.GetById(warshahid);
            var Invoices = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();

            List<InvoiceTodayDTO> invoicetoday = new List<InvoiceTodayDTO>();

            foreach (var c in Invoices)
            {
                InvoiceTodayDTO invoiceTodayDTO = new InvoiceTodayDTO();

                invoiceTodayDTO.InvoiceNumber = c.InvoiceNumber;
                invoiceTodayDTO.CarOwnerPhone = c.CarOwnerPhone;
                invoiceTodayDTO.CarOwnerName = c.CarOwnerName ;
                invoiceTodayDTO.TotalWithoutVat = c.AfterDiscount;
                invoiceTodayDTO.VAT = c.VatMoney;
                invoiceTodayDTO.Total = c.Total;
                invoiceTodayDTO.CreatedOn = c.CreatedOn;


                invoicetoday.Add(invoiceTodayDTO);

            }


            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Invoices");
                workSheet.Cells.LoadFromCollection(invoicetoday, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $""+ "فواتير ورشة" + warshah.WarshahNameAr + "--" + DateTime.Now + ".xlsx";

            return File(stream, "application/octet-stream", excelName);
            //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);  
        }


        // to Schedule send notification report by daily

        [HttpGet, Route("Schedule")]
        public IActionResult Schedule(int warshahid)
        {

           

            TimeSpan ts = new TimeSpan(23, 59, 00);

            TimeSpan s = new TimeSpan(24, 00, 00);



            var warshah = _uow.WarshahRepository.GetById(warshahid);
            var userid = _uow.UserRepository.GetMany(a => a.WarshahId == warshah.Id && a.RoleId == 1).FirstOrDefault();
            int timeInSeconds = 30;
            if (s > ts)
            {           
                var nitification =  BackgroundJob.Schedule(() => _NotificationService.SetNotificationTaqnyat(userid.Id, "تنبيه : يمكنك تنزيل تقرير الفواتير اليومية اضغط هنا "), TimeSpan.FromSeconds(timeInSeconds));
            }

            return Ok($" Notification will be sent in {timeInSeconds} seconds!");


        }




      
        [HttpGet, Route("DashboardCarOwner")]
        public IActionResult DashboardCarOwner(int CarOwnerId)
        {

            var carowner = _uow.UserRepository.GetById(CarOwnerId);

            // repair Order  أوامر قيد الإصلاح   (repair order open != 7)

            var RepairOrder = _uow.ReciptionOrderRepository.GetMany(o => o.CarOwnerId == CarOwnerId).ToHashSet().Count;

              
                // الفواتير المسددة
               
            
            var Invoices = _uow.InvoiceRepository.GetMany(t => t.CarOwnerID == CarOwnerId.ToString() || t.CarOwnerPhone == carowner.Phone).ToHashSet().Count;

               var motors = _uow.MotorsRepository.GetMany(a=>a.CarOwnerId == carowner.Id).ToHashSet().Count;

             

                return Ok(new
                {
                    RepairOrderCount = RepairOrder,
                    Invoices = Invoices,
                    Motors = motors


                });
            }




        [HttpGet, Route("Dashboard")]
        public IActionResult Dashboard(int warshahId)
        {

            // repair Order  أوامر قيد الإصلاح   (repair order open != 7)

            var RepairOrder = _uow.RepairOrderRepository.GetMany(o => o.WarshahId == warshahId && (o.RepairOrderStatus != 7 || o.RepairOrderStatus == 9)).ToHashSet().Count;

            // كل الموظفين فى الورشة 
            var AllUsers = _uow.UserRepository.GetMany(u => u.WarshahId == warshahId && (u.RoleId == 3 || u.RoleId == 4 || u.RoleId == 5)).ToHashSet().Count;

            // كل الفنيين
            var technicals = _uow.UserRepository.GetMany(u => u.WarshahId == warshahId && u.RoleId == 3).ToHashSet().Count;

            // موظفين الاستقبال
            var receptionists = _uow.UserRepository.GetMany(u => u.WarshahId == warshahId && u.RoleId == 4).ToHashSet().Count;


            // مهندسين الاستقبال
            var engineers = _uow.UserRepository.GetMany(u => u.WarshahId == warshahId && u.RoleId == 5).ToHashSet().Count;

            // الفواتير المسددة
            var InvoicePaid = _uow.InvoiceRepository.GetMany(t => t.WarshahId == warshahId && t.InvoiceStatusId == 2).ToHashSet().Count;

            // الفواتير  الغير مسددة
            var InvoiceNotPaid = _uow.InvoiceRepository.GetMany(t => t.WarshahId == warshahId && t.InvoiceStatusId == 1).ToHashSet().Count;

            // الفواتير  الغير مسددة
            var InvoiceDelaied = _uow.InvoiceRepository.GetMany(t => t.WarshahId == warshahId && t.InvoiceStatusId == 4).ToHashSet().Count;

            // عدد قطع الغيار فى المخزون
            var Parts = _uow.SparePartRepository.GetMany(t => t.WarshahId == warshahId).ToHashSet();
            var TotalQty = Parts.Sum(s => s.Quantity);

            // Cars Ready  مركبات جاهزة للاستلام   (repair order open = 7 && No Invoice)
            var CarReadyCount = 0;
            try
            {
                var RepairOrderClosed = _uow.RepairOrderRepository.GetMany(o => o.WarshahId == warshahId && o.RepairOrderStatus == 7).ToHashSet();

                foreach (var repair in RepairOrderClosed)
                {
                    var inv = _uow.InvoiceRepository.GetMany(i => i.RepairOrderId == repair.Id).FirstOrDefault();

                    if (inv == null || (inv != null && (inv.InvoiceStatusId == 1 || inv.InvoiceStatusId == 4)))
                    {
                        CarReadyCount = CarReadyCount + 1;

                    }
                    else
                    {
                        CarReadyCount = CarReadyCount;
                    }


                }
            }
            catch (Exception)
            {

                CarReadyCount = 0;
            }




            return Ok(new
            {
                RepairOrderCount = RepairOrder,
                UsersCount = AllUsers,
                TechnicalsCount = technicals,
                ReceptionistsCount = receptionists,
                EngineersCount = engineers,
                InvoicePaid = InvoicePaid,
                InvoiceNotPaid = InvoiceNotPaid,
                InvoiceDelaied = InvoiceDelaied,
                TotalQty = TotalQty,
                CarReadyCount = CarReadyCount
            });
        }



        [HttpGet, Route("DetailsCustomerInvoice")]
        public IActionResult DetailsCustomerInvoice(int warshahid , string CarOwnerPhone , int pagenumber, int pagecount , string SearchText)
        {

            var Invoices = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.CarOwnerPhone == CarOwnerPhone).ToHashSet();
            var carowner = _uow.UserRepository.GetMany(a => a.Phone == CarOwnerPhone).FirstOrDefault();
            var Invoicespaid = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.CarOwnerPhone == CarOwnerPhone && s.InvoiceStatusId == 2).ToHashSet();
            decimal TotalPaid = 0;
            decimal TotalNotPaid = 0;
            if (Invoicespaid.Count != 0)
            {
                TotalPaid = Invoicespaid.Sum(a => a.Total);
            }
            var InvoicesNotpaid = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.CarOwnerPhone == CarOwnerPhone && s.InvoiceStatusId != 2).ToHashSet();
            if (InvoicesNotpaid.Count != 0)
            {
                TotalNotPaid = InvoicesNotpaid.Sum(a => a.Total);
            }

            if (SearchText != null)
            {

                Invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == warshahid
                      &&
                (t.CarType.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet();

                 Invoicespaid = _uow.InvoiceRepository.GetMany(t => t.WarshahId == warshahid
                      &&
                (t.CarType.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)) && t.InvoiceStatusId == 2).ToHashSet();

                if (Invoicespaid.Count != 0)
                {
                    TotalPaid = Invoicespaid.Sum(a => a.Total);
                }

                InvoicesNotpaid = _uow.InvoiceRepository.GetMany(t => t.WarshahId == warshahid
                    &&
              (t.CarType.Contains(SearchText)
              || t.InvoiceSerial.Contains(SearchText)) && t.InvoiceStatusId != 2).ToHashSet();

                if (InvoicesNotpaid.Count != 0)
                {
                    TotalNotPaid = InvoicesNotpaid.Sum(a => a.Total);
                }


            }

            List<DetailsInvoiceCustomer> allinvoices = new List<DetailsInvoiceCustomer>();

          
                foreach (var c in Invoices)
                {
                    DetailsInvoiceCustomer invoiceTodayDTO = new DetailsInvoiceCustomer();

                    invoiceTodayDTO.InvoiceNumber = c.InvoiceNumber;
                    invoiceTodayDTO.CarOwnerName = c.CarOwnerName;
                    invoiceTodayDTO.CarType = c.CarType;
                    invoiceTodayDTO.Total = c.Total;

                    if (c.InvoiceStatusId == 2)
                    {
                        invoiceTodayDTO.InvoiceStatus = "مسددة";
                    }
                    else
                    {
                        invoiceTodayDTO.InvoiceStatus = "غير مسددة";
                    }
                    invoiceTodayDTO.CreateOn = c.CreatedOn;


                    allinvoices.Add(invoiceTodayDTO);

                }

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = allinvoices.Count(), Listinvoice = allinvoices.ToPagedList(pagenumber, pagecount) , totalpaid = TotalPaid, totalnotpaid = TotalNotPaid });


       


        }



        [HttpGet, Route("DetailsCustomerInvoiceInTime")]
        public IActionResult DetailsCustomerInvoiceInTime(int warshahid, string CarOwnerPhone, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate, string SearchText)
        {

            var Invoices = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.CarOwnerPhone == CarOwnerPhone && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet();
            var carowner = _uow.UserRepository.GetMany(a => a.Phone == CarOwnerPhone).FirstOrDefault();
            var Invoicespaid = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.CarOwnerPhone == CarOwnerPhone  && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate && s.InvoiceStatusId == 2).ToHashSet();
            decimal TotalPaid = 0;
            decimal TotalNotPaid = 0;
            if (Invoicespaid.Count != 0)
            {
                TotalPaid = Invoicespaid.Sum(a => a.Total);
            }
            var InvoicesNotpaid = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.CarOwnerPhone == CarOwnerPhone && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate && s.InvoiceStatusId != 2).ToHashSet();
            if (InvoicesNotpaid.Count != 0)
            {
                TotalNotPaid = InvoicesNotpaid.Sum(a => a.Total);
            }

            if (SearchText != null)
            {

                Invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == warshahid && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate
                      &&
                (t.CarType.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet();

                Invoicespaid = _uow.InvoiceRepository.GetMany(t => t.WarshahId == warshahid && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate
                     &&
               (t.CarType.Contains(SearchText)
               || t.InvoiceSerial.Contains(SearchText)) && t.InvoiceStatusId == 2).ToHashSet();

                if (Invoicespaid.Count != 0)
                {
                    TotalPaid = Invoicespaid.Sum(a => a.Total);
                }

                InvoicesNotpaid = _uow.InvoiceRepository.GetMany(t => t.WarshahId == warshahid && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate
                    &&
              (t.CarType.Contains(SearchText)
              || t.InvoiceSerial.Contains(SearchText)) && t.InvoiceStatusId != 2).ToHashSet();

                if (InvoicesNotpaid.Count != 0)
                {
                    TotalNotPaid = InvoicesNotpaid.Sum(a => a.Total);
                }


            }

            List<DetailsInvoiceCustomer> allinvoices = new List<DetailsInvoiceCustomer>();


            foreach (var c in Invoices)
            {
                DetailsInvoiceCustomer invoiceTodayDTO = new DetailsInvoiceCustomer();

                invoiceTodayDTO.InvoiceNumber = c.InvoiceNumber;
                invoiceTodayDTO.CarOwnerName = c.CarOwnerName;
                invoiceTodayDTO.CarType = c.CarType;
                invoiceTodayDTO.Total = c.Total;

                if (c.InvoiceStatusId == 2)
                {
                    invoiceTodayDTO.InvoiceStatus = "مسددة";
                }
                else
                {
                    invoiceTodayDTO.InvoiceStatus = "غير مسددة";
                }
                invoiceTodayDTO.CreateOn = c.CreatedOn;


                allinvoices.Add(invoiceTodayDTO);

            }

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = allinvoices.Count(), Listinvoice = allinvoices.ToPagedList(pagenumber, pagecount), totalpaid = TotalPaid, totalnotpaid = TotalNotPaid });





        }
    }
    }





