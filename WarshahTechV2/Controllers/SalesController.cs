using AutoMapper;
using BL.Infrastructure;
using DL.DBContext;
using DL.DTOs.InvoiceDTOs;
using DL.DTOs.Sales;
using DL.Entities;
using DL.Entities.HR;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using WarshahTechV2.Models;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDBContext appDBContext;
        private readonly INotificationService _NotificationService;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public SalesController(INotificationService NotificationService, AppDBContext appDBContext, IUnitOfWork uow, IMapper mapper)
        {
            this.appDBContext = appDBContext;
            _NotificationService = NotificationService;
            _uow = uow;
            _mapper = mapper;
        }
        [HttpPost, Route("RequestSparePart")]
        public IActionResult RequestSparePart(TaseerItem taseerItem)
        {

            appDBContext.TaseerItems.Add(taseerItem);
            appDBContext.SaveChanges();
            return Ok(taseerItem);
        }
        [HttpGet, Route("FinishRequest")]
        public IActionResult FinishRequest(int Id)
        {
            var taseerItem = appDBContext.TaseerItems.FirstOrDefault(a => a.Id == Id);
            if (taseerItem != null)
            {

                return Ok();
            }
            return Ok();

        }
        [HttpPost, Route("SetSalesStatus")]
        public IActionResult SetSalesStatus(int ItemId, int Status)
        {

            var Item = appDBContext.TaseerItems.Where(a => a.Id == ItemId).FirstOrDefault();

            Item.Status = Status;

            appDBContext.Update(Item);
            appDBContext.SaveChanges();
            return Ok(Item);
        }
        [HttpGet, Route("GetWarshahRequest")]
        public IActionResult GetWarshahRequest(int warshahId)
        {
            var All = appDBContext.TaseerItems.Where(a => a.Id == warshahId).ToHashSet();

            //return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = All.Count(), Listinvoice = All.ToPagedList(pagenumber, pagecount) });
            return Ok(All);
        }

        [HttpGet, Route("GetAllRequeast")]
        public IActionResult GetAllRequeast()
        {
            var All = appDBContext.TaseerItems.ToHashSet();
            return Ok(All);
        }


        [HttpPost, Route("CreateSalesInvoice")]
        public IActionResult CreateSalesInvoice(SalesInvoiceParametersDTO salesInvoice)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var SalesRequests = _uow.SalesRequestRepository.GetMany(a => a.SalesRequestListId == salesInvoice.SalesRequistListId).ToHashSet();

                    var request = SalesRequests.FirstOrDefault();

                    var invoiceDTO = new SalesInvoiceDTO();

                    var CurrentWarshah = _uow.WarshahRepository.GetMany(s => s.Id == Convert.ToInt16(request.WarshahId)).FirstOrDefault();

                    // Get last invoice number for each warshash
                    var invoicenumber = _uow.SalesInvoiceRepository.GetMany(i => i.WarshahId == CurrentWarshah.Id.ToString())
                        .OrderByDescending(t => t.InvoiceNumber).FirstOrDefault();
                    if (invoicenumber == null)
                    {
                        invoiceDTO.InvoiceNumber = 1;
                    }
                    else
                    {
                        int lastnumber = invoicenumber.InvoiceNumber;
                        invoiceDTO.InvoiceNumber = lastnumber + 1;
                    }
                    var invoice = _mapper.Map<DL.Entities.SalesInvoice>(invoiceDTO);



                    List<SalesInvoiceItem> ItemList = new List<SalesInvoiceItem>();
                    decimal TotalPrice = 0;
                    foreach (var item in SalesRequests)
                    {
                        var ItemExactly = _uow.SparePartTaseerRepository.GetById(item.SparePartTaseerId);

                        var Additem = new SalesInvoiceItem();
                        if (ItemExactly != null)
                        {
                            //Additem.InvoiceId = CurrentInvoice.Id;
                            //Additem.SparePartNameAr = ItemExactly.SparePartName;
                            Additem.Quantity = item.QTY;
                            Additem.PeacePrice = item.BuyPrice;
                            //Additem.CreatedOn = DateTime.Now;
                            //Additem.FixPrice = item.BuyPrice;
                            var Cost = Additem.Quantity * Additem.PeacePrice;

                            TotalPrice += Cost;

                            //ItemList.Add(Additem);
                            //_uow.SalesInvoiceItemRepository.Add(Additem);

                        }

                    }


                    invoice.InvoiceSerial = "IN-" + "Sales" + "-" + invoice.InvoiceNumber;
                    invoice.InvoiceStatusId = 1;   // الفاتورة غير مسددة   
                    invoice.BeforeDiscount = ((decimal)(TotalPrice));
                    invoice.AfterDiscount = invoice.BeforeDiscount - invoice.Deiscount;



                    //var v = 15;
                    //decimal Vat = (((decimal)v) / (100));

                    decimal Vat = 0;
                    var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == CurrentWarshah.Id).FirstOrDefault();
                    if (VAT == null)
                    {

                        Vat = 0;

                    }
                    else
                    {
                        Vat = (((decimal)VAT.GetVAT) / (100));
                    }
                    invoice.VatMoney = invoice.AfterDiscount * Vat;
                    invoice.Total = invoice.AfterDiscount + invoice.VatMoney;

                    //invoice.AdvancePayment = receptionorder.AdvancePayment;
                    invoice.WarshahId = CurrentWarshah.Id.ToString();
                    invoice.IsDeleted = false;
                    invoice.CreatedOn = DateTime.Now;
                    invoice.PaymentTypeInvoiceId = 1;
                    //var paymenttype = _uow.PaymentTypeInvoiceRepository.GetById(invoice.PaymentTypeInvoiceId);
                    //invoice.PaymentTypeName = paymenttype.PaymentTypeNameAr;

                    // New addition
                    invoice.WarshahPhone = CurrentWarshah.LandLineNum;
                    invoice.WarshahCR = CurrentWarshah.CR;
                    invoice.WarshahCity = _uow.CityRepository.GetMany(c => c.Id == CurrentWarshah.CityId)?.FirstOrDefault().CityNameAr;
                    invoice.WarshahAddress = _uow.RegionRepository.GetMany(c => c.Id == CurrentWarshah.RegionId)?.FirstOrDefault().RegionNameAr;
                    invoice.WarshahPostCode = CurrentWarshah.PostalCode.ToString();
                    invoice.WarshahDescrit = CurrentWarshah.Distrect;
                    invoice.WarshahName = CurrentWarshah.WarshahNameAr;
                    invoice.WarshahTaxNumber = CurrentWarshah.TaxNumber;
                    invoice.WarshahStreet = CurrentWarshah.Street;
                    // create and save invoice
                    _uow.SalesInvoiceRepository.Add(invoice);
                    _uow.Save();


                    //var CurrentInvoice = _uow.SalesInvoiceRepository.GetMany(p => p.Id == invoice.Id).FirstOrDefault();

                    List<SalesInvoiceItem> ItemList1 = new List<SalesInvoiceItem>();

                    foreach (var item in SalesRequests)
                    {
                        var ItemExactly = _uow.SparePartTaseerRepository.GetById(item.SparePartTaseerId);

                        var Additem = new SalesInvoiceItem();
                        if (ItemExactly != null)
                        {
                            Additem.InvoiceId = invoice.Id;
                            Additem.SparePartNameAr = ItemExactly.SparePartName;
                            Additem.Quantity = item.QTY;
                            Additem.PeacePrice = item.BuyPrice;
                            Additem.CreatedOn = DateTime.Now;
                            //Additem.FixPrice = item.BuyPrice;

                            ItemList1.Add(Additem);
                            _uow.SalesInvoiceItemRepository.Add(Additem);

                        }

                    }

                    _uow.Save();












                    return Ok(new { CurrentInvoice = invoice });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Invoice");
        }

        [HttpGet, Route("GetSalesInvoiceById")]
        public IActionResult GetSalesInvoiceById(int invoiceid)
        {
            var invoice = _uow.SalesInvoiceRepository.GetMany(p => p.Id == invoiceid).FirstOrDefault();
            var Items = _uow.SalesInvoiceItemRepository.GetMany(a => a.InvoiceId == invoiceid).ToHashSet().OrderByDescending(a => a.Id);


            return Ok(new { invoice = invoice, Items = Items });
        }

        [HttpGet, Route("GetQrCodeSalesInvoice")]
        public IActionResult GetQrCodeSalesInvoice(int InvoiceId)
        {
            var InvoiceItem = _uow.SalesInvoiceRepository.GetById(InvoiceId);

            InvoiceItem.WarshahName = "شركة العربة الدولية لتقنية المعلومات";
            InvoiceItem.WarshahTaxNumber = "310188507200003";

            var FinalQrString = BL.TLVHELPER.GetBase64String(InvoiceItem.WarshahName, InvoiceItem.WarshahTaxNumber, InvoiceItem.CreatedOn.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"), InvoiceItem.Total.ToString(), InvoiceItem.VatMoney.ToString());
            return Ok(new { FinalQrString = FinalQrString });
        }





        [HttpGet, Route("GetAllSalesInvoicesInSales")]
        public IActionResult GetAllSalesInvoicesInSales(int id, int pagenumber, int pagecount, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetAll().ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString() &&
                (t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }



            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }


        [HttpGet, Route("GetAllInvoicesInSalesIdInTime")]
        public IActionResult GetAllInvoicesInSalesIdInTime(int id, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetMany(t => t.CreatedOn >= FromDate && t.CreatedOn <= ToDate).ToHashSet().OrderByDescending(a => a.Id);


            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString()
                && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate && (
                 t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }





            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }


        [HttpGet, Route("GetAllSalesInvoicesWithWarshahId")]
        public IActionResult GetAllInvoicesWithWarshahId(int id, int pagenumber, int pagecount, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString()).ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString() &&
                (t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }



            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }


        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllInvoicesWithWarshahIdInTime")]
        public IActionResult GetAllInvoicesWithWarshahIdInTime(int id, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString() && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate).ToHashSet().OrderByDescending(a => a.Id);


            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString() 
                && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate && (
                 t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }





            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }


        [HttpGet, Route("GetAllNotPaidInvoicesInSales")]
        public IActionResult GetAllNotPaidInvoicesInSales(int id, int pagenumber, int pagecount, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetMany(t=>t.InvoiceStatusId == 1).ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString() &&
                (t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }



            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }




        [HttpGet, Route("GetAllNotPaidInvoicesWithWarshahId")]
        public IActionResult GetAllNotPaidInvoicesWithWarshahId(int id, int pagenumber, int pagecount, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString() &&t.InvoiceStatusId==1).ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString() &&
                (t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }



            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }




        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllNotPaidInvoicesInSalesInTime")]
        public IActionResult GetAllNotPaidInvoicesInSalesInTime(int id, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetMany(t => 
            t.InvoiceStatusId == 1

            && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate).ToHashSet().OrderByDescending(a => a.Id);


            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString()
                && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate && (
                 t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }





            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }






        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllNotPaidInvoicesWithWarshahIdInTime")]
        public IActionResult GetAllNotPaidInvoicesWithWarshahIdInTime(int id, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString()
            && t.InvoiceStatusId == 1

            && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate).ToHashSet().OrderByDescending(a => a.Id);


            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString()
                && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate && (
                 t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }





            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }




        [HttpGet, Route("GetAllPaidInvoicesInsales")]
        public IActionResult GetAllPaidInvoicesInsales(int id, int pagenumber, int pagecount, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetMany(t => t.InvoiceStatusId == 2).ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString() &&
                (t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }



            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }



        [HttpGet, Route("GetAllPaidInvoicesWithWarshahId")]
        public IActionResult GetAllPaidInvoicesWithWarshahId(int id, int pagenumber, int pagecount, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString() && t.InvoiceStatusId == 2).ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString() &&
                (t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }



            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }




        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllPaidInvoicesInSalesInTime")]
        public IActionResult GetAllPaidInvoicesInSalesInTime(int id, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetMany(t => 
            t.InvoiceStatusId == 2

            && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate).ToHashSet().OrderByDescending(a => a.Id);


            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString()
                && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate && (
                 t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }





            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }


        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllPaidInvoicesWithWarshahIdInTime")]
        public IActionResult GetAllPaidInvoicesWithWarshahIdInTime(int id, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate, string SearchText)
        {
            var invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString()
            && t.InvoiceStatusId == 2

            && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate).ToHashSet().OrderByDescending(a => a.Id);


            if (SearchText != null)
            {

                invoices = _uow.SalesInvoiceRepository.GetMany(t => t.WarshahId == id.ToString()
                && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate && (
                 t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }





            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }





        [HttpPost, Route("CreateSalesInvoiceAfterPaid")]
        public IActionResult CreateSalesInvoiceAfterPaid(int invoiceId)
        {



            var invoice = _uow.SalesInvoiceRepository.GetById(invoiceId);
            invoice.UpdatedOn = DateTime.Now;
            invoice.CreatedOn = invoice.CreatedOn;
            // from enum paid invoice
            invoice.InvoiceStatusId = 2;


            _uow.SalesInvoiceRepository.Update(invoice);
            _uow.Save();

            return Ok(invoice);
        }
    }
}
