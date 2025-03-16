using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.InvoiceDTOs;
using DL.Entities;
using DocumentFormat.OpenXml.Wordprocessing;
using Helper;
using HELPER;
using KSAEinvoice;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using WarshahTechV2.Models;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]

    public class DebitAndCreditorNoticeController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        private readonly IBoxNow _boxNow;

        private readonly ILoyalityService _loyalityService;
        // Constractor for controller 
        public DebitAndCreditorNoticeController(IUnitOfWork uow, IMapper mapper, IBoxNow boxNow, ILoyalityService loyalityService)
        {

            _boxNow = boxNow;
            _mapper = mapper;
            _loyalityService = loyalityService;
            _uow = uow;
        }

        // add Debit or Creditor for invoice 

        [HttpPost, Route("CreateNotice")]
        public async Task<IActionResult> CreateNotice(DebitAndCreaditor debitAndCreaditor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notice = _mapper.Map<DL.Entities.DebitAndCreditor>(debitAndCreaditor.debitAndCreditorDTO);
                    var invoice = _uow.InvoiceRepository.GetMany(r => r.Id == notice.InvoiceId)?.FirstOrDefault();
                    var CheckNotice = _uow.DebitAndCreditorRepository.GetMany(t => t.WarshahId == invoice.WarshahId && t.Flag == notice.Flag).OrderByDescending(t => t.NoticeNumber)?.FirstOrDefault();
                    var boxnow = _boxNow.GetBoxNow((int)invoice.WarshahId);
                    notice.WarshahId = (int)invoice.WarshahId;
                    notice.IsDeleted = false;

                    if (CheckNotice == null)
                    {
                        notice.NoticeNumber = 1;
                    }
                    else
                    {
                        int lastnumber = CheckNotice.NoticeNumber;
                        notice.NoticeNumber = lastnumber + 1;
                    }

                    // flag = 1  إشعار مدين  &   flag = 2  إشعار دائن

                    if (notice.Flag == 1)
                    {
                        notice.NoticeSerial = "DN" + notice.NoticeNumber;
                    }

                    else
                    {
                        notice.NoticeSerial = "CN" + notice.NoticeNumber;
                    }

                    _uow.DebitAndCreditorRepository.Add(notice);
                    _uow.Save();


                    // To add List of Parts (products)
                    var AllProducts = new HashSet<NoticeProduct>();
                    if (debitAndCreaditor.noticeProductDTOs != null)
                    {
                        foreach (var item in debitAndCreaditor.noticeProductDTOs)
                        {
                            var Parts = _mapper.Map<DL.Entities.NoticeProduct>(item);
                            if(Parts.PeacePrice >0)
                            {
                                Parts.CreatedOn = DateTime.Now;
                                Parts.DebitAndCreditorId = notice.Id;
                                AllProducts.Add(Parts);
                                _uow.NoticeProductRepository.Add(Parts);
                            }
                           
                        }
                    }

                    // _uow.Save();


                    // To add fixing price


                    var laborItem = new NoticeProduct();
                    laborItem.DebitAndCreditorId = notice.Id;
                    laborItem.SparePartNameAr = "تصليح / Labor";
                    laborItem.Quantity = 1;

                    if (notice.FixingPrice == null || notice.FixingPrice == 0)
                    {
                        laborItem.PeacePrice = 0;
                    }
                    else
                    {
                        laborItem.PeacePrice = (decimal)notice.FixingPrice;
                    }

                    laborItem.CreatedOn = DateTime.Now;
                    _uow.NoticeProductRepository.Add(laborItem);


                    if (AllProducts != null)

                    {

                        if (notice.Flag == 1)
                        {
                            foreach (var part in AllProducts)
                            {

                                if (part.PartId != 0)
                                {
                                    var UsingPart = _uow.SparePartRepository.Get(m => m.Id == part.PartId);

                                    // add to TransactionInventory

                                    TransactionInventory transactionsToday = new TransactionInventory();
                                    transactionsToday.CreatedOn = DateTime.Now;
                                    transactionsToday.SparePartName = UsingPart.SparePartName;

                                    transactionsToday.WarshahId = notice.WarshahId;
                                    transactionsToday.NoofQuentity = part.Quantity;
                                    transactionsToday.OldQuentity = UsingPart.Quantity;
                                    transactionsToday.CurrentQuentity = transactionsToday.OldQuentity - transactionsToday.NoofQuentity;

                                    transactionsToday.TransactionName = "سحب";

                                    _uow.TransactionInventoryRepository.Add(transactionsToday);
                                    _uow.Save();

                                    var UsingQty = part.Quantity;



                                    UsingPart.Quantity = UsingPart.Quantity - UsingQty;
                                    _uow.SparePartRepository.Update(UsingPart);
                                }


                            }

                        }

                        else
                        {
                            foreach (var part in AllProducts)
                            {

                                var UsingPart = _uow.SparePartRepository.Get(m => m.Id == part.PartId);

                                if (part.PartId != 0)
                                {
                                    TransactionInventory transactionsToday = new TransactionInventory();
                                    transactionsToday.CreatedOn = DateTime.Now;
                                    transactionsToday.SparePartName = UsingPart.SparePartName;
                                    transactionsToday.WarshahId = notice.WarshahId;
                                    transactionsToday.NoofQuentity = part.Quantity;
                                    transactionsToday.OldQuentity = UsingPart.Quantity;
                                    transactionsToday.CurrentQuentity = transactionsToday.OldQuentity + transactionsToday.NoofQuentity;

                                    transactionsToday.TransactionName = "إضافة";

                                    _uow.TransactionInventoryRepository.Add(transactionsToday);
                                    _uow.Save();

                                    var UsingQty = part.Quantity;



                                    UsingPart.Quantity = UsingPart.Quantity + UsingQty;
                                    _uow.SparePartRepository.Update(UsingPart);

                                }
                                // add to TransactionInventory


                            }

                        }


                    }





                    // calculate Total for Notice 


                    if (AllProducts != null)
                    {
                        foreach (var part in AllProducts)
                        {
                            if (part.PartId != 0)
                            {
                                var Qty = part.Quantity;
                                var PeacePrice = part.PeacePrice;
                                var Cost = Qty * PeacePrice;

                                notice.TotalWithoutVat += Cost;
                            }

                        }

                    }

                    decimal Vat = 0;
                    var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == invoice.WarshahId).FirstOrDefault();
                    if (VAT == null)
                    {


                        Vat = 0;

                    }
                    else
                    {
                        Vat = (((decimal)VAT.GetVAT) / (100));
                    }



                    decimal totalparts = notice.TotalWithoutVat;
                    notice.TotalWithoutVat = (decimal)(totalparts + notice.FixingPrice);
                    notice.Vat = notice.TotalWithoutVat * Vat;

                    notice.Total = notice.TotalWithoutVat + notice.Vat;
                    notice.UpdatedOn = DateTime.Now;

                    _uow.DebitAndCreditorRepository.Update(notice);

                    //if(invoice.WarshahId == 4 || invoice.WarshahId == 53)
                    //{
                    //    TaxResponse response = new TaxResponse();


                    //    TaxInv _TaxInv = new TaxInv();
                    //    response = await _TaxInv.CreateXml_and_SendInv(notice.Id, "Id", "DebitAndCreditors", "IQ_KSATaxInvHeaderDebitAndCredit", "IQ_KSATaxInvItemsDebitAndCredit", "IQ_KSATaxInvHeader_PerPaid", "");
                    //    //response = await _TaxInv.CreateXml_and_SendInv(notice.Id, "Id", "Invoices", "IQ_KSATaxInvHeader", "IQ_KSATaxInvItems", "IQ_KSATaxInvHeader_PerPaid", "");
                    //}

                    

                    if (invoice.CarOwnerID != null)
                    {
                        int OwnerCarID = int.Parse(invoice.CarOwnerID);
                        // to Calculate discount from point car owner

                        if (notice.DiscountPoint == true)
                        {

                            var CarOwnerPoints = _uow.LoyalityPointRepository.GetMany(s => s.WarshahId == invoice.WarshahId && s.CarOwnerId == OwnerCarID).FirstOrDefault();
                            var MoneyForPointWarshah = _uow.LoyalitySettingRevarseRepository.GetMany(s => s.WarshahId == invoice.WarshahId).FirstOrDefault();

                            if (MoneyForPointWarshah != null && MoneyForPointWarshah.NoofPoints > 0)
                            {
                                var RateFor1Point = MoneyForPointWarshah.CurrancyPerLoyalityPoints / MoneyForPointWarshah.NoofPoints;

                                invoice.DiscountPoint = RateFor1Point * CarOwnerPoints.Points;

                                invoice.Total = (decimal)(invoice.Total - invoice.DiscountPoint);
                            }
                        }

                        _uow.DebitAndCreditorRepository.Update(notice);

                    }



                    // Add To Transaction TODAY bOX when cash
                    if (notice.PaymentTypeInvoiceId == 1)
                    {





                        TransactionsToday transactionsToday = new TransactionsToday();

                        transactionsToday.WarshahId = notice.WarshahId;
                        transactionsToday.InvoiceNumber = invoice.InvoiceNumber;
                        transactionsToday.Vat = notice.Vat;
                        transactionsToday.Total = notice.Total;
                        transactionsToday.CreatedOn = DateTime.Now;
                        transactionsToday.BeforeVat = notice.TotalWithoutVat;

                        decimal previousmoney = boxnow.TotalIncome;
                        var currenttotal = _uow.TransactionTodayRepository.GetMany(a => a.WarshahId == transactionsToday.WarshahId).OrderByDescending(t => t.Id).FirstOrDefault();


                        if (notice.Flag == 1)
                        {
                            transactionsToday.TransactionName = "إشعار مدين";
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

                        }
                        else
                        {
                            transactionsToday.TransactionName = "إشعار دائن";
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




                        }
                        _uow.TransactionTodayRepository.Add(transactionsToday);
                    }

                    _uow.Save();


                    if (notice.Flag == 1)
                    {

                        //// Add To WarshahTechService  

                        WarshahTechService warshahTechService = new WarshahTechService();
                        warshahTechService.WarshahId = (int)invoice.WarshahId;
                        warshahTechService.InvoiceId = invoice.Id;
                        warshahTechService.InvoiceDate = invoice.CreatedOn;
                        warshahTechService.InvoiceNumber = invoice.InvoiceNumber;
                        warshahTechService.InvoiceSerial = invoice.InvoiceSerial;
                        warshahTechService.WarshahName = invoice.WarshahName;
                        warshahTechService.WarshahPhone = invoice.WarshahPhone;
                        warshahTechService.PaymentTypeInvoiceId = invoice.PaymentTypeInvoiceId;
                        warshahTechService.PaymentTypeName = invoice.PaymentTypeName;
                        warshahTechService.AfterDiscount = invoice.AfterDiscount;

                        //decimal percent = (10 / 100);

                        var v = 10;
                        decimal percent = (((decimal)v) / (100));

                        warshahTechService.WarshahService = warshahTechService.AfterDiscount * percent;

                        _uow.WarshahTechServiceRepository.Add(warshahTechService);
                        _uow.Save();
                    }




                    var dataNotice = _uow.DebitAndCreditorRepository.GetMany(p => p.Id == notice.Id).Include(t => t.Invoice).ToHashSet();


                    if (invoice.CarOwnerID != null)
                    {
                        int OwnerCarID = int.Parse(invoice.CarOwnerID);

                        if (notice.Flag == 1)
                        {
                            _loyalityService.SetLoyalityPoints(notice.WarshahId, OwnerCarID, notice.TotalWithoutVat);
                        }

                    }


                    if (invoice.WarshahId == 4 || invoice.WarshahId == 53)
                    {
                        TaxResponse response = new TaxResponse();


                        TaxInv _TaxInv = new TaxInv();
                        response = await _TaxInv.CreateXml_and_SendInv(notice.Id, "Id", "DebitAndCreditors", "IQ_KSATaxInvHeaderDebitAndCredit", "IQ_KSATaxInvItemsDebitAndCredit", "IQ_KSATaxInvHeader_PerPaid", "");
                        //response = await _TaxInv.CreateXml_and_SendInv(notice.Id, "Id", "Invoices", "IQ_KSATaxInvHeader", "IQ_KSATaxInvItems", "IQ_KSATaxInvHeader_PerPaid", "");
                    }

                    return Ok(dataNotice);

                }



                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Notice");
        }



        // Get all Debits or Creaditor with InvoiceId 

        [HttpGet, Route("GetAllNoticeWithInvoiceId")]
        public IActionResult GetAllNoticeWithInvoiceId(int invoiceId)
        {
            var Notices = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoiceId).OrderByDescending(i => i.Id).ToHashSet();
            return Ok(Notices);
        }

        [HttpGet, Route("GetAllCreditorNoticeWithInvoiceId")]
        public IActionResult GetAllCreditorNoticeWithInvoiceId(int invoiceId)
        {
            var Notices = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoiceId && t.Flag == 2).OrderByDescending(i => i.Id).ToHashSet();
            return Ok(Notices.Sum(a => a.Total));
        }




        // Get all Debits or Creaditor with warshah id and Falg id 

        [HttpGet, Route("GetAllNoticeWithWarshahId")]
        public IActionResult GetAllNoticeWithWarshahId(int WarshahId, int Flag, int pagenumber, int pagecount)
        {
            var Notices = _uow.DebitAndCreditorRepository.GetMany(t => t.WarshahId == WarshahId && t.Flag == Flag).ToHashSet();
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = Notices.Count(), Listinvoice = Notices.ToPagedList(pagenumber, pagecount) });


        }



        // Get  Debit or Creaditor with Notice Id 

        [HttpGet, Route("GetNoticeWithItems")]
        public IActionResult GetNoticeWithItems(int? NoticeId)
        {
            var Notice = _uow.DebitAndCreditorRepository.GetMany(n => n.Id == NoticeId).Include(a => a.Invoice).FirstOrDefault();


            // توليد رمز QR كصورة

            if (( Notice.WarshahId == 4 || Notice.WarshahId == 53 ) && Notice.QRCode != null)
            {
                using (var qrGenerator = new QRCodeGenerator())
                {
                    var qrCodeData = qrGenerator.CreateQrCode(Notice.QRCode, QRCodeGenerator.ECCLevel.Q);
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        using (var qrCodeImage = qrCode.GetGraphic(20))  // حجم الصورة
                        {
                            // تحويل الصورة إلى Base64
                            using (var ms = new MemoryStream())
                            {
                                qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                var base64String = Convert.ToBase64String(ms.ToArray());


                                Notice.QRCode = base64String;





                            }
                        }
                    }
                }
            }

            var DTOResult = new GetDebitAndCreaditor();
            DTOResult.debitAndCreditorDTO = Notice;
            var AllItemNotice = _uow.NoticeProductRepository.GetMany(p => p.DebitAndCreditorId == NoticeId).ToHashSet();
            DTOResult.noticeProductDTOs = AllItemNotice;




            return Ok(DTOResult);
        }


    }


}
