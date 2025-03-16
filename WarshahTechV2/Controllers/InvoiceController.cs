using AutoMapper;
using BL.Infrastructure;
using BL.Security;
using DL.DTOs.FastServiceDTO;
using DL.DTOs.InspectionDTOs;
using DL.DTOs.InvoiceDTOs;
using DL.DTOs.Pagination;
using DL.Entities;
using DL.Entities.HR;
using DL.Migrations;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Grpc.Core;
using Helper;
using Helper.Triggers;
using HELPER;
using KSAEinvoice;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
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
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tax.API.Models;
using WarshahTechV2.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Invoice = DL.Entities.Invoice;


namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class InvoiceController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;
      
        private readonly IBoxNow _boxNow;
        // take instance from IMapper
        private readonly IMapper _mapper;
        private readonly ISerialService _serialService;
        private readonly ILoyalityService _loyalityService;
        private readonly INotificationService _NotificationService;

        private static Random random = new Random();
        int length = 24;

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private string ActionLink = "https://warshahtech.sa/";
        // Constractor for controller 
        public InvoiceController(INotificationService NotificationService, IBoxNow boxNow,
                    IUnitOfWork uow, IMapper mapper, ISerialService serialService , ILoyalityService loyalityService)
        {
            _mapper = mapper;
            _serialService = serialService;
            _loyalityService = loyalityService;
            _uow = uow;
            _boxNow = boxNow;
            _NotificationService = NotificationService;
        }


        #region InvoiceCRUD


        //Create Invoice BeforePaid
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateInvoiceChecked")]
        public IActionResult CreateInvoiceChecked(InvoiceDTO invoiceDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var receptionOrder = _uow.ReciptionOrderRepository.GetMany(r => r.Id == invoiceDTO.ReciptionOrderId).FirstOrDefault();
                    var repairorder = _uow.RepairOrderRepository.GetMany(r => r.ReciptionOrderId == invoiceDTO.ReciptionOrderId).FirstOrDefault();
                    // Get last invoice number for each warshash
                    var invoicenumber = _uow.InvoiceRepository.GetMany(i => i.WarshahId == receptionOrder.warshahId).OrderByDescending(t => t.InvoiceNumber).FirstOrDefault();
                    if (invoicenumber == null)
                    {
                        invoiceDTO.InvoiceNumber = 1;
                    }
                    else
                    {
                        int lastnumber = invoicenumber.InvoiceNumber;
                        invoiceDTO.InvoiceNumber = lastnumber + 1;
                    }
                    var invoice = _mapper.Map<DL.Entities.Invoice>(invoiceDTO);
                    invoice.InvoiceSerial = "IN-" + receptionOrder.warshahId + "-" + invoice.InvoiceNumber;
                    // from enum CheckingInvoice  
                    invoice.InvoiceTypeId = 1;
                    // from enum BeforePaid  
                    invoice.InvoiceStatusId = 2;
                    invoice.PaymentTypeInvoiceId = receptionOrder.PaymentTypeInvoiceId;
                    invoice.RepairOrderId = repairorder.Id;
                    invoice.CheckPrice = receptionOrder.CheckPrice;
                    invoice.FixingPrice = 0;
                    invoice.Deiscount = 0;
                    invoice.BeforeDiscount = 0;
                    invoice.AfterDiscount = 0;
                    invoice.VatMoney = 0;
                    invoice.Total = (decimal)receptionOrder.CheckPrice;
                    invoice.AdvancePayment = 0;
                    invoice.RemainAmount = 0;
                    invoice.WarshahId = receptionOrder.warshahId;
                    invoice.IsDeleted = false;
                    invoice.CreatedOn = DateTime.Now;

                    // Closed reception order from enum reception order
                    receptionOrder.StatusId = 2;
                    _uow.ReciptionOrderRepository.Update(receptionOrder);



                    // create and save invoice
                    _uow.InvoiceRepository.Add(invoice);
                    _NotificationService.SetNotificationTaqnyat(Int32.Parse( invoice.CarOwnerID), "تم اصدار فاتورة جديدة ");
                    var user = _uow.UserRepository.GetMany(a => a.WarshahId == repairorder.WarshahId && a.RoleId == 1).FirstOrDefault();

                    string messge2 = "تم اصدار فاتورة  غير مسددة جديدة رقم الفاتورة  " + invoice.InvoiceNumber;

                    _NotificationService.SetNotificationTaqnyat(user.Id, messge2);
                    _uow.Save();
                    return Ok(invoice);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
                
            }

            return BadRequest("Invalid Invoice");
        }

        //Create Invoice BeforePaid
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateInvoice")]
        public IActionResult CreateInvoice(InvoiceDTO invoiceDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var repairorder = _uow.RepairOrderRepository.GetMany(r => r.Id == invoiceDTO.RepairOrderId).FirstOrDefault();

                    var CurrentWarshah = _uow.WarshahRepository.GetMany(s => s.Id == repairorder.WarshahId).FirstOrDefault();

                    var reportInspection = _uow.InspectionWarshahReportRepository.GetMany(r => r.Id == repairorder.InspectionWarshahReportId).Include(a => a.CarOwner).FirstOrDefault();

                    

                    // Get last invoice number for each warshash
                    var invoicenumber = _uow.InvoiceRepository.GetMany(i => i.WarshahId == repairorder.WarshahId).OrderByDescending(t => t.InvoiceNumber).FirstOrDefault();
                    if (invoicenumber == null)
                    {
                        invoiceDTO.InvoiceNumber = 1;
                    }
                    else
                    {
                        int lastnumber = invoicenumber.InvoiceNumber;
                        invoiceDTO.InvoiceNumber = lastnumber + 1;
                    }
                    var invoice = _mapper.Map<DL.Entities.Invoice>(invoiceDTO);
                    invoice.InvoiceSerial = "IN-" + repairorder.WarshahId + "-" + invoice.InvoiceNumber;
                    // from enum FixingInvoice  
                    invoice.InvoiceTypeId = 2;
                    // from enum BeforePaid  
                    invoice.InvoiceStatusId = 1;

                    if (repairorder.ReciptionOrderId != null)
                    {

                        invoice.ReciptionOrderId = repairorder.ReciptionOrderId;
                        var receptionorder = _uow.ReciptionOrderRepository.GetMany(r => r.Id == repairorder.ReciptionOrderId).Include(a => a.CarOwner).FirstOrDefault();
                        var motor = _uow.MotorsRepository.GetMany(m => m.Id == receptionorder.MotorsId).FirstOrDefault();
                        var paymenttype = _uow.PaymentTypeInvoiceRepository.GetById(invoice.PaymentTypeInvoiceId);
                        invoice.PaymentTypeName = paymenttype.PaymentTypeNameAr;


                        invoice.CheckPrice = receptionorder.CheckPrice;
                        invoice.FixingPrice = repairorder.FixingPrice;
                        invoice.Deiscount = repairorder.Deiscount;
                        invoice.BeforeDiscount = repairorder.BeforeDiscount;
                        invoice.AfterDiscount = repairorder.AfterDiscount;
                        invoice.VatMoney = repairorder.VatMoney;
                        invoice.Total = repairorder.Total;
                        var recept = _uow.ReciptionOrderRepository.GetMany(r => r.Id == repairorder.ReciptionOrderId).FirstOrDefault();
                        var carowner = _uow.UserRepository.GetMany(r => r.Id == recept.CarOwnerId).FirstOrDefault();

                        int OwnerCarID = carowner.Id;
                        // to Calculate discount from point car owner

                        if (repairorder.DiscPoint == true)
                        {

                            var CarOwnerPoints = _uow.LoyalityPointRepository.GetMany(s => s.WarshahId == repairorder.WarshahId && s.CarOwnerId == OwnerCarID).FirstOrDefault();
                            var MoneyForPointWarshah = _uow.LoyalitySettingRevarseRepository.GetMany(s => s.WarshahId == repairorder.WarshahId).FirstOrDefault();


                            if (MoneyForPointWarshah != null)
                            {
                                //var RateFor1Point = MoneyForPointWarshah.CurrancyPerLoyalityPoints / MoneyForPointWarshah.NoofPoints;

                                invoice.DiscountPoint =  CarOwnerPoints.Points / MoneyForPointWarshah.CurrancyPerLoyalityPoints;

                                invoice.Total = (decimal)(invoice.Total - invoice.DiscountPoint);

                                CarOwnerPoints.Points = 0;
                                _uow.LoyalityPointRepository.Update(CarOwnerPoints);

                            }

                            
                        }

                        invoice.AdvancePayment = receptionorder.AdvancePayment;
                        invoice.RemainAmount = (invoice.AdvancePayment - invoice.Total);
                        invoice.WarshahId = repairorder.WarshahId;
                        invoice.IsDeleted = false;
                        invoice.CreatedOn = DateTime.Now;
                        invoice.PaymentTypeInvoiceId = invoice.PaymentTypeInvoiceId;


                        // New addition

                        invoice.WarhshahCondition = CurrentWarshah.Terms;
                        invoice.WarshahPhone = CurrentWarshah.LandLineNum;
                        invoice.WarshahCR = CurrentWarshah.CR;
                        invoice.WarshahCity = _uow.CityRepository.GetMany(c => c.Id == CurrentWarshah.CityId)?.FirstOrDefault().CityNameAr;
                        invoice.WarshahAddress = _uow.RegionRepository.GetMany(c => c.Id == CurrentWarshah.RegionId)?.FirstOrDefault().RegionNameAr;
                        invoice.WarshahPostCode = CurrentWarshah.PostalCode.ToString();
                        invoice.WarshahDescrit = CurrentWarshah.Distrect;
                        invoice.WarshahName = CurrentWarshah.WarshahNameAr;
                        invoice.WarshahTaxNumber = CurrentWarshah.TaxNumber;
                        invoice.WarshahStreet = CurrentWarshah.Street;
                        invoice.CarOwnerTaxNumber = carowner.TaxNumber;
                        invoice.CarOwnerCR = carowner.CommerialRegisterar;

                        invoice.CarOwnerID = receptionorder.CarOwner.Id.ToString();

                        if(receptionorder.CarOwner.IsCompany == true)
                        {
                            invoice.CarOwnerName = receptionorder.CarOwner.CompanyName;
                        }
                        else
                        {
                            invoice.CarOwnerName = string.Format(receptionorder.CarOwner.FirstName + " " + receptionorder.CarOwner.LastName);
                        }
                        invoice.CarOwnerPhone = receptionorder.CarOwner.Phone;

                        var model = _uow.MotorModelRepository.GetMany(c => c.Id == motor.MotorModelId)?.FirstOrDefault().ModelNameAr;

                        var make = _uow.MotorMakeRepository.GetMany(c => c.Id == motor.MotorMakeId)?.FirstOrDefault().MakeNameAr;

                        if (motor.PlateNo == null)
                        {
                            invoice.CarType = make  + " - " + model;

                        }
                        else
                        {

                            invoice.CarType = make + " - " + model + " - " + motor.PlateNo;
                        }



                        if (receptionorder.KM_In == null)
                        {
                            invoice.KMIn = 0;
                        }
                        else
                        {
                            invoice.KMIn = (decimal)receptionorder.KM_In;

                        }

                        if (repairorder.KMOut == null)
                        {
                            invoice.KMOut = 0;
                        }
                        else
                        {
                            invoice.KMOut = repairorder.KMOut;

                        }

                        invoice.TechReview = repairorder.TechReview;

                        invoice.InspectionWarshahReportId = repairorder.InspectionWarshahReportId;
                        invoice.CarOwnerAddress = receptionorder.CarOwner.Address;
                        invoice.DiscFixingMoney = repairorder.DiscFixingMoney;
                        invoice.DiscSpareMoney = repairorder.DiscSpareMoney;
                        invoice.VatFixingMoney= repairorder.VatFixingMoney;
                        invoice.VatSpareMoney= repairorder.VatSpareMoney;
                        invoice.InvoiceCategoryId = receptionorder.InvoiceCategoryId;   
                        // Closed repair order from enum repair order
                        repairorder.RepairOrderStatus = 7;
                        _uow.RepairOrderRepository.Update(repairorder);



                        invoice.Describtion = new string(Enumerable.Repeat(chars, length)
                                            .Select(s => s[random.Next(s.Length)]).ToArray());

                        // create and save invoice
                        _uow.InvoiceRepository.Add(invoice);


                        // Invoice Repair Order   إشعار فاتورة أمر إصلاح  
                        var sms = "";

                        var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == invoice.WarshahId & a.NameNotificationId == 1 & a.StatusNotificationId == 1).FirstOrDefault();

                        if (notificationActive != null)
                        {
                            var InvoiceLink = ActionLink + "/GetInvoiceHashById/" + invoice.Describtion;

                            string messge2 = "تم اصدار فاتوره امر إصلاح جديده "
                            + "\n للإطلاع و مراجعة الفاتورة من خلال الرابط التالى  "
                          + "\n  " + InvoiceLink;

                            sms = "تم اصدار فاتوره امر إصلاح جديده "
                            + "\n للإطلاع و مراجعة الفاتورة من خلال الرابط التالى  "
                          + "\n  " + InvoiceLink;


                            _NotificationService.SetNotificationTaqnyat(Int32.Parse(invoice.CarOwnerID), messge2);

                            var user1 = _uow.UserRepository.GetMany(a => a.WarshahId == repairorder.WarshahId && a.RoleId == 1).FirstOrDefault();

                            string messge = "تم اصدار فاتورة  امر اصلاح غير مسددة جديدة رقم الفاتورة  " + invoice.InvoiceNumber;

                            _NotificationService.SetNotificationTaqnyat(user1.Id, messge);

                        }

                       
                    }

                    else
                    {
                        invoice.ReciptionOrderId = repairorder.ReciptionOrderId;
                        //var receptionorder = _uow.ReciptionOrderRepository.GetMany(r => r.Id == repairorder.ReciptionOrderId).Include(a => a.CarOwner).FirstOrDefault();
                        var motor = _uow.MotorsRepository.GetMany(m => m.Id == reportInspection.MotorsId).FirstOrDefault();


                        //invoice.CheckPrice = receptionorder.CheckPrice;
                        invoice.FixingPrice = repairorder.FixingPrice;
                       

                        invoice.Deiscount = repairorder.Deiscount;
                        invoice.BeforeDiscount = repairorder.BeforeDiscount;
                        invoice.AfterDiscount = repairorder.AfterDiscount;
                        invoice.VatMoney = repairorder.VatMoney;
                        invoice.Total = repairorder.Total;

                        int OwnerCarID = reportInspection.CarOwner.Id;
                        // to Calculate discount from point car owner

                        if (repairorder.DiscPoint == true)
                        {

                            var CarOwnerPoints = _uow.LoyalityPointRepository.GetMany(s => s.WarshahId == repairorder.WarshahId && s.CarOwnerId == OwnerCarID).FirstOrDefault();
                            var MoneyForPointWarshah = _uow.LoyalitySettingRevarseRepository.GetMany(s => s.WarshahId == repairorder.WarshahId).FirstOrDefault();
                           
                            if(MoneyForPointWarshah != null)
                            {
                                invoice.DiscountPoint = CarOwnerPoints.Points / MoneyForPointWarshah.CurrancyPerLoyalityPoints;

                                invoice.Total = (decimal)(invoice.Total - invoice.DiscountPoint);

                                CarOwnerPoints.Points = 0;
                                _uow.LoyalityPointRepository.Update(CarOwnerPoints);
                            }
                           
                        }
                        //invoice.AdvancePayment = receptionorder.AdvancePayment;
                        invoice.RemainAmount = (invoice.AdvancePayment - invoice.Total);
                        invoice.WarshahId = repairorder.WarshahId;
                        invoice.IsDeleted = false;
                        invoice.CreatedOn = DateTime.Now;
                        invoice.PaymentTypeInvoiceId = reportInspection.PaymentInvoiceId;

                        var paymenttype = _uow.PaymentTypeInvoiceRepository.GetById(invoice.PaymentTypeInvoiceId);
                        invoice.PaymentTypeName = paymenttype.PaymentTypeNameAr;

                        // New addition

                        invoice.WarhshahCondition = CurrentWarshah.Terms;
                        invoice.WarshahPhone = CurrentWarshah.LandLineNum;
                        invoice.WarshahCR = CurrentWarshah.CR;
                        invoice.WarshahCity = _uow.CityRepository.GetMany(c => c.Id == CurrentWarshah.CityId)?.FirstOrDefault().CityNameAr;
                        invoice.WarshahAddress = _uow.RegionRepository.GetMany(c => c.Id == CurrentWarshah.RegionId)?.FirstOrDefault().RegionNameAr;
                        invoice.WarshahPostCode = CurrentWarshah.PostalCode.ToString();
                        invoice.WarshahDescrit = CurrentWarshah.Distrect;
                        invoice.WarshahName = CurrentWarshah.WarshahNameAr;
                        invoice.WarshahTaxNumber = CurrentWarshah.TaxNumber;
                        invoice.WarshahStreet = CurrentWarshah.Street;
                        invoice.CarOwnerTaxNumber = reportInspection.CarOwner.TaxNumber;
                        invoice.CarOwnerCR = reportInspection.CarOwner.CommerialRegisterar;
                        if (reportInspection.CarOwner.IsCompany == true)
                        {
                            invoice.CarOwnerName = reportInspection.CarOwner.CompanyName;
                        }
                        else
                        {
                            invoice.CarOwnerName = string.Format(reportInspection.CarOwner.FirstName + " " + reportInspection.CarOwner.LastName);
                        }
                        invoice.CarOwnerPhone = reportInspection.CarOwner.Phone;

                        var model = _uow.MotorModelRepository.GetMany(c => c.Id == motor.MotorModelId)?.FirstOrDefault().ModelNameAr;

                        var make = _uow.MotorMakeRepository.GetMany(c => c.Id == motor.MotorMakeId)?.FirstOrDefault().MakeNameAr;


                        if (motor.PlateNo == null)
                        {
                            invoice.CarType = make + " - " + model;

                        }
                        else
                        {

                            invoice.CarType = make + " - " + model + " - " + motor.PlateNo;
                        }



                        if (reportInspection.KM_IN == null)
                        {
                            invoice.KMIn = 0;
                        }
                        else
                        {
                            invoice.KMIn = (decimal)reportInspection.KM_IN;

                        }

                        if (repairorder.KMOut == null)
                        {
                            invoice.KMOut = 0;
                        }
                        else
                        {
                            invoice.KMOut = repairorder.KMOut; 

                        }
                         
                        
                        invoice.TechReview = repairorder.TechReview;

                        invoice.InspectionWarshahReportId = repairorder.InspectionWarshahReportId;
                        invoice.CarOwnerAddress = reportInspection.CarOwner.Address;
                        invoice.DiscFixingMoney = repairorder.DiscFixingMoney;
                        invoice.DiscSpareMoney = repairorder.DiscSpareMoney;
                        invoice.VatFixingMoney = repairorder.VatFixingMoney;
                        invoice.VatSpareMoney = repairorder.VatSpareMoney;
                        invoice.CarOwnerID = reportInspection.CarOwner.Id.ToString();
                        // Closed repair order from enum repair order
                        repairorder.RepairOrderStatus = 7;
                        _uow.RepairOrderRepository.Update(repairorder);



                        invoice.Describtion = new string(Enumerable.Repeat(chars, length)
                                            .Select(s => s[random.Next(s.Length)]).ToArray());
                        // create and save invoice
                        _uow.InvoiceRepository.Add(invoice);

                        //Invoice Repair Order  فاتورة أمر إصلاح
                        var sms = "";

                        var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == invoice.WarshahId & a.NameNotificationId == 1 & a.StatusNotificationId == 1).FirstOrDefault();

                        if (notificationActive != null)
                        {
                           
                            var InvoiceLink = ActionLink + "/GetInvoiceHashById/" + invoice.Describtion;

                            string messge2 = "تم اصدار فاتوره امر إصلاح جديده "
                            + "\n للإطلاع و مراجعة الفاتورة من خلال الرابط التالى  "
                          + "\n  " + InvoiceLink;

                            sms = "تم اصدار فاتوره امر إصلاح جديده "
                            + "\n للإطلاع و مراجعة الفاتورة من خلال الرابط التالى  "
                          + "\n  " + InvoiceLink;


                            _NotificationService.SetNotificationTaqnyat(Int32.Parse(invoice.CarOwnerID), messge2);

                            var userno = _uow.UserRepository.GetMany(a => a.WarshahId == repairorder.WarshahId && a.RoleId == 1).FirstOrDefault();

                            string messge3 = "تم اصدار فاتورة  غير مسددة جديدة رقم الفاتورة  " + invoice.InvoiceNumber;

                            _NotificationService.SetNotificationTaqnyat(userno.Id, messge3);

                        }
                    }


                    if (repairorder != null)
                    {

                        // decrease using Quantity from parts 
                        var Parts2 = _uow.RepairOrderSparePartRepository.GetMany(p => p.RepairOrderId == repairorder.Id);
                        if (Parts2 != null)
                        {
                            foreach (var part in Parts2)
                            {

                                var UsingQty = part.Quantity;

                                var UsingPart = _uow.SparePartRepository.Get(m => m.Id == part.SparePartId);


                                // add to TransactionInventory

                                TransactionInventory transactionsToday = new TransactionInventory();

                                transactionsToday.CreatedOn = DateTime.Now;
                                transactionsToday.SparePartName = UsingPart.SparePartName;
                                transactionsToday.WarshahId = repairorder.WarshahId;
                                transactionsToday.NoofQuentity = part.Quantity;
                                transactionsToday.OldQuentity = UsingPart.Quantity;
                                transactionsToday.CurrentQuentity = transactionsToday.OldQuentity - transactionsToday.NoofQuentity;

                                transactionsToday.TransactionName = "  سحب من المخزون";

                                _uow.TransactionInventoryRepository.Add(transactionsToday);






                                UsingPart.Quantity = UsingPart.Quantity - UsingQty;
                                _uow.SparePartRepository.Update(UsingPart);






                            }

                        }
                        //order.RepairOrderStatus = ROStatusConst.WaitTech;
                        //order.UpdatedOn = DateTime.Now;
                        //_uow.RepairOrderRepository.Update(order);
                        //_uow.Save();
                    }





                    _uow.Save();



                  

                    var CurrentInvoice = _uow.InvoiceRepository.GetMany(p => p.RepairOrderId == invoice.RepairOrderId).FirstOrDefault();


                    //// Add To WarshahTechService  
                    
                    WarshahTechService warshahTechService = new WarshahTechService();
                    warshahTechService.WarshahId = (int)CurrentInvoice.WarshahId;
                    warshahTechService.InvoiceId = CurrentInvoice.Id;
                    warshahTechService.InvoiceDate = CurrentInvoice.CreatedOn;
                    warshahTechService.InvoiceNumber = CurrentInvoice.InvoiceNumber;
                    warshahTechService.InvoiceSerial = CurrentInvoice.InvoiceSerial;
                    warshahTechService.WarshahName = CurrentInvoice.WarshahName;
                    warshahTechService.WarshahPhone = CurrentInvoice.WarshahPhone;
                    warshahTechService.PaymentTypeInvoiceId = CurrentInvoice.PaymentTypeInvoiceId;
                    warshahTechService.PaymentTypeName = CurrentInvoice.PaymentTypeName;
                    warshahTechService.AfterDiscount = CurrentInvoice.AfterDiscount;

                    //decimal percent = (10 / 100);

                    var v = 10;
                    decimal percent = (((decimal)v) / (100));

                    warshahTechService.WarshahService = warshahTechService.AfterDiscount * percent;

                    _uow.WarshahTechServiceRepository.Add(warshahTechService);
                    _uow.Save();




                    var Parts = _uow.RepairOrderSparePartRepository.GetMany(p => p.RepairOrderId == invoice.RepairOrderId);
                    var OrderServices = _uow.RepairOrderServicesRepository.GetMany(a => a.OrderId == invoice.RepairOrderId).ToHashSet();
                    List<InvoiceItem> ItemList = new List<InvoiceItem>();

                    foreach (var item in Parts)
                    {
                        var ItemExactly = _uow.SparePartRepository.GetById(item.SparePartId);

                        var Additem = new InvoiceItem();
                        if (ItemExactly != null)
                        {
                            Additem.InvoiceId = CurrentInvoice.Id;
                            Additem.SparePartNameAr = ItemExactly.SparePartName;
                            Additem.Quantity = item.Quantity;
                            Additem.PeacePrice = item.PeacePrice;
                            Additem.Garuntee = item.Garuntee;
                            Additem.CreatedOn = DateTime.Now;
                            Additem.FixPrice = item.FixPrice;
                            Additem.Describtion = item.SparePartId.ToString();
                            _uow.InvoiceItemRepository.Add(Additem);



                        }

                    }


                    foreach (var item in OrderServices)
                    {
                        var Additem = new InvoiceItem();
                        Additem.PeacePrice = 0;
                        Additem.FixPrice = item.Price;
                        Additem.InvoiceId = CurrentInvoice.Id;
                        Additem.Garuntee = item.Gruntee;
                        Additem.SparePartNameAr = item.Name;
                        Additem.Quantity = 1;                      
                        Additem.CreatedOn = DateTime.Now;

                        _uow.InvoiceItemRepository.Add(Additem);
                    }






                    _uow.Save();

                  
                    // Add bonus to Technical

                    var bonusPercent = _uow.BonusTechnicalRepository.GetMany(b => b.WarshahId == invoice.WarshahId).FirstOrDefault();

                    if (bonusPercent != null)
                    {

                        foreach(var part in Parts)
                        {
                            decimal bonusTotal = part.FixPrice * (bonusPercent.BonusPercent / 100);

                            RecordBonusTechnical bonus = new RecordBonusTechnical();
                            bonus.WarshahId = (int)invoice.WarshahId;
                            bonus.CreatedOn = DateTime.Now;
                            bonus.UserId = part.TechId;
                            bonus.Bonus = bonusTotal;

                            _uow.RecordBonusRepository.Add(bonus);

                        }
                        foreach (var Service in OrderServices)
                        {
                            decimal bonusTotal = Service.Price * (bonusPercent.BonusPercent / 100);

                            RecordBonusTechnical bonus = new RecordBonusTechnical();
                            bonus.WarshahId = (int)invoice.WarshahId;
                            bonus.CreatedOn = DateTime.Now;
                            bonus.UserId = Service.TechId;
                            bonus.Bonus = bonusTotal;

                            _uow.RecordBonusRepository.Add(bonus);

                        }

                    }

                    _uow.Save();


                    return Ok(invoice);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex =ex});
                }

            }

            return BadRequest("Invalid Invoice");
        }

        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpPost, Route("CreateInspectionInvoice")]
        public async Task<IActionResult> CreateInspectionInvoice(int? ReportInspectionId)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var Report = _uow.InspectionWarshahReportRepository.GetById(ReportInspectionId);


                    var invoiceDTO = new InvoiceDTO();

                    //var receptionOrder = _uow.ReciptionOrderRepository.GetMany(r => r.Id == receptionId).FirstOrDefault();
                    //var receptionorder = _uow.ReciptionOrderRepository.GetMany(r => r.Id == receptionOrder.Id).Include(a => a.CarOwner).FirstOrDefault();
                    var CurrentWarshah = _uow.WarshahRepository.GetMany(s => s.Id == Report.WarshahId).FirstOrDefault();

                    // Get last invoice number for each warshash
                    var invoicenumber = _uow.InvoiceRepository.GetMany(i => i.WarshahId == CurrentWarshah.Id).OrderByDescending(t => t.InvoiceNumber).FirstOrDefault();
                    if (invoicenumber == null)
                    {
                        invoiceDTO.InvoiceNumber = 1;
                    }
                    else
                    {
                        int lastnumber = invoicenumber.InvoiceNumber;
                        invoiceDTO.InvoiceNumber = lastnumber + 1;
                    }
                    var invoice = _mapper.Map<DL.Entities.Invoice>(invoiceDTO);
                    invoice.InvoiceSerial = "IN-" + CurrentWarshah.Id + "-" + invoice.InvoiceNumber;
                    // from enum FixingInvoice  
                    invoice.InvoiceTypeId = 2;
                    //invoice.ReciptionOrderId = receptionOrder.Id;
                    var motor = _uow.MotorsRepository.GetMany(m => m.Id == Report.MotorsId).FirstOrDefault();

                    var template = _uow.InspectionTemplateRepository.GetMany(r => r.Id == Report.TemplateId).FirstOrDefault();

                    //invoice.CheckPrice = receptionorder.CheckPrice;
                    invoice.FixingPrice = template.Price;

                    invoice.InvoiceStatusId = 2;   // الفاتورة مسددة مسبقا قبل الفحص


                    invoice.BeforeDiscount = ((decimal)(invoice.FixingPrice));
                    invoice.AfterDiscount = invoice.BeforeDiscount;



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

                    int OwnerCarID = Report.CarOwnerId;
                    // to Calculate discount from point car owner

                    if (Report.DiscountPoint == true)
                    {

                        var CarOwnerPoints = _uow.LoyalityPointRepository.GetMany(s => s.WarshahId == Report.WarshahId && s.CarOwnerId == OwnerCarID).FirstOrDefault();
                        var MoneyForPointWarshah = _uow.LoyalitySettingRevarseRepository.GetMany(s => s.WarshahId == Report.WarshahId).FirstOrDefault();

                        if (MoneyForPointWarshah != null)
                        {
                            invoice.DiscountPoint = CarOwnerPoints.Points / MoneyForPointWarshah.CurrancyPerLoyalityPoints;

                            invoice.Total = (decimal)(invoice.Total - invoice.DiscountPoint);

                            CarOwnerPoints.Points = 0;
                            _uow.LoyalityPointRepository.Update(CarOwnerPoints);
                        }


                    }

                    //invoice.AdvancePayment = receptionorder.AdvancePayment;
                    invoice.RemainAmount = (invoice.AdvancePayment - invoice.Total);
                    invoice.WarshahId = CurrentWarshah.Id;
                    invoice.IsDeleted = false;
                    invoice.CreatedOn = DateTime.Now;
                    invoice.PaymentTypeInvoiceId = Report.PaymentInvoiceId;

                    var paymenttype = _uow.PaymentTypeInvoiceRepository.GetById(invoice.PaymentTypeInvoiceId);
                    invoice.PaymentTypeName = paymenttype.PaymentTypeNameAr;

                    // New addition

                    invoice.WarhshahCondition = CurrentWarshah.Terms;
                    invoice.WarshahPhone = CurrentWarshah.LandLineNum;
                    invoice.WarshahCR = CurrentWarshah.CR;
                    invoice.WarshahCity = _uow.CityRepository.GetMany(c => c.Id == CurrentWarshah.CityId)?.FirstOrDefault().CityNameAr;
                    invoice.WarshahAddress = _uow.RegionRepository.GetMany(c => c.Id == CurrentWarshah.RegionId)?.FirstOrDefault().RegionNameAr;
                    invoice.WarshahPostCode = CurrentWarshah.PostalCode.ToString();
                    invoice.WarshahDescrit = CurrentWarshah.Distrect;
                    invoice.WarshahName = CurrentWarshah.WarshahNameAr;
                    invoice.WarshahTaxNumber = CurrentWarshah.TaxNumber;
                    invoice.WarshahStreet = CurrentWarshah.Street;
                    var currentcarowner = _uow.UserRepository.GetById(motor.CarOwnerId);
                    invoice.CarOwnerAddress = currentcarowner.Address;
                    invoice.CarOwnerTaxNumber = currentcarowner.TaxNumber;
                    invoice.CarOwnerCR = currentcarowner.CommerialRegisterar;
                    if (currentcarowner.IsCompany == true)
                    {
                        invoice.CarOwnerName = currentcarowner.CompanyName;
                    }
                    else
                    {
                        invoice.CarOwnerName = string.Format(currentcarowner.FirstName + " " + currentcarowner.LastName);
                    }

                    invoice.CarOwnerPhone = currentcarowner.Phone;

                    var model = _uow.MotorModelRepository.GetMany(c => c.Id == motor.MotorModelId)?.FirstOrDefault().ModelNameAr;

                    var make = _uow.MotorMakeRepository.GetMany(c => c.Id == motor.MotorMakeId)?.FirstOrDefault().MakeNameAr;

                    if (motor.PlateNo == null)
                    {
                        invoice.CarType = make + " - " + model;

                    }
                    else 
                    {

                        invoice.CarType = make + " - " + model + " - " + motor.PlateNo;
                    }



                    if (Report.KM_IN == null)
                    {
                        invoice.KMIn = 0;
                    }
                    else
                    {
                        invoice.KMIn = (decimal)Report.KM_IN;
                    }
                  
                    invoice.KMOut = 0;
                    invoice.TechReview = "";
                    invoice.CarOwnerID = currentcarowner.Id.ToString();
                    invoice.InspectionWarshahReportId = ReportInspectionId;



                   
                    invoice.Describtion = new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());


                    // create and save invoice
                    _uow.InvoiceRepository.Add(invoice);
                    // Add To Transaction TODAY bOX when cash
                    if (invoice.PaymentTypeInvoiceId == 1)
                    {

                        TransactionsToday transactionsToday = new TransactionsToday();

                        transactionsToday.WarshahId = (int)invoice.WarshahId;
                        transactionsToday.InvoiceNumber = invoice.InvoiceNumber;
                        transactionsToday.Vat = invoice.VatMoney;
                        transactionsToday.Total = invoice.Total;
                        transactionsToday.CreatedOn = DateTime.Now;
                        transactionsToday.BeforeVat = invoice.AfterDiscount;

                        transactionsToday.TransactionName = "فاتورة فحص";

                        var boxnow = _boxNow.GetBoxNow((int)transactionsToday.WarshahId);
                        decimal previousmoney = boxnow.TotalIncome;
                        transactionsToday.PreviousBalance = previousmoney;
                        transactionsToday.CurrentBalance = previousmoney + transactionsToday.Total;


                        _uow.TransactionTodayRepository.Add(transactionsToday);

                    }

                    _uow.Save();

            


                    var CurrentInvoice = _uow.InvoiceRepository.GetMany(p => p.InspectionWarshahReportId == ReportInspectionId).FirstOrDefault();



                    //// Add To WarshahTechService  

                    WarshahTechService warshahTechService = new WarshahTechService();
                    warshahTechService.WarshahId = (int)CurrentInvoice.WarshahId;
                    warshahTechService.InvoiceId = CurrentInvoice.Id;
                    warshahTechService.InvoiceDate = CurrentInvoice.CreatedOn;
                    warshahTechService.InvoiceNumber = CurrentInvoice.InvoiceNumber;
                    warshahTechService.InvoiceSerial = CurrentInvoice.InvoiceSerial;
                    warshahTechService.WarshahName = CurrentInvoice.WarshahName;
                    warshahTechService.WarshahPhone = CurrentInvoice.WarshahPhone;
                    warshahTechService.PaymentTypeInvoiceId = CurrentInvoice.PaymentTypeInvoiceId;
                    warshahTechService.PaymentTypeName = CurrentInvoice.PaymentTypeName;
                    warshahTechService.AfterDiscount = CurrentInvoice.AfterDiscount;

                    //decimal percent = (10 / 100);

                    var v = 10;
                    decimal percent = (((decimal)v) / (100));

                    warshahTechService.WarshahService = warshahTechService.AfterDiscount * percent;

                    _uow.WarshahTechServiceRepository.Add(warshahTechService);
                    _uow.Save();


                    var laborItem = new InvoiceItem();
                    laborItem.InvoiceId = CurrentInvoice.Id;
                    laborItem.SparePartNameAr = "فحص / Inspection";
                    laborItem.Quantity = 1;
                    laborItem.PeacePrice = invoice.FixingPrice;
                    laborItem.CreatedOn = DateTime.Now;
                    laborItem.Garuntee = "";

                    _uow.InvoiceItemRepository.Add(laborItem);
                    //var AddList = _mapper.Map<DL.Entities.InvoiceItem>(ItemList);

                    //Invoice Inspection Order  فاتورة أمر فحص

                    var sms = "";

                    var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == invoice.WarshahId & a.NameNotificationId == 2 & a.StatusNotificationId == 1).FirstOrDefault();

                    if (notificationActive != null)
                    {
                        var InvoiceLink = ActionLink + "/GetInvoiceHashById/" + invoice.Describtion;

                        string messge2 = "تم اصدار فاتوره امر فحص جديده "
                        + "\n للإطلاع و مراجعة الفاتورة من خلال الرابط التالى  "
                      + "\n  " +InvoiceLink  ;

                        sms = "تم اصدار فاتوره امر فحص جديده "
                        + "\n للإطلاع و مراجعة الفاتورة من خلال الرابط التالى  "
                      + "\n  " + InvoiceLink;


                        _NotificationService.SetNotificationTaqnyat(Int32.Parse(invoice.CarOwnerID), messge2);

                        var user1 = _uow.UserRepository.GetMany(a => a.WarshahId == invoice.WarshahId && a.RoleId == 1).FirstOrDefault();

                        string messge = "تم اصدار فاتورة  امر فحص غير مسددة جديدة رقم الفاتورة  " + invoice.InvoiceNumber;

                        _NotificationService.SetNotificationTaqnyat(user1.Id, messge);

                    }

                    
                  
                   
                    _loyalityService.SetLoyalityPoints((int)(invoice.WarshahId), OwnerCarID, invoice.AfterDiscount);

                    // Add bonus to Technical

                    var bonusPercent = _uow.BonusTechnicalRepository.GetMany(b => b.WarshahId == CurrentWarshah.Id).FirstOrDefault();

                    if (bonusPercent != null)
                    {
                        decimal bonusTotal = invoice.AfterDiscount * (bonusPercent.BonusPercent / 100);

                        RecordBonusTechnical bonus = new RecordBonusTechnical();
                        bonus.WarshahId = CurrentWarshah.Id;
                        bonus.CreatedOn = DateTime.Now;
                        bonus.UserId = Report.TechnicalID;
                        bonus.Bonus = bonusTotal;

                        _uow.RecordBonusRepository.Add(bonus);
                    }

                    _uow.Save();


                    if (invoice.CarOwnerID != null)
                    {

                        var customer = _uow.WarshahCarOwnersRepository.GetMany(a => a.WarshahId == invoice.WarshahId && a.CarOwnerId == Convert.ToInt16(invoice.CarOwnerID)).FirstOrDefault();

                        if (customer == null)
                        {
                            var carwarshah = new WarshahWithCarOwner();
                            carwarshah.CarOwnerId = Convert.ToInt16(invoice.CarOwnerID);
                            carwarshah.WarshahId = (int)invoice.WarshahId;
                            _uow.WarshahCarOwnersRepository.Add(carwarshah);
                            _uow.Save();


                        }



                    }

                    if (invoice.WarshahId == 4 || invoice.WarshahId == 53)
                    {
                        TaxResponse response = new TaxResponse();


                        TaxInv _TaxInv = new TaxInv();
                        //response = await _TaxInv.CreateXml_and_SendInv(InvoiceID, "Id", "DebitAndCreditors", "IQ_KSATaxInvHeaderDebitAndCredit", "IQ_KSATaxInvItemsDebitAndCredit", "IQ_KSATaxInvHeader_PerPaid", "");
                        response = await _TaxInv.CreateXml_and_SendInv(invoice.Id, "Id", "Invoices", "IQ_KSATaxInvHeader", "IQ_KSATaxInvItems", "IQ_KSATaxInvHeader_PerPaid", "");
                    }


                    return Ok(new { invoice = invoice , message2 = sms });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Invoice");
        }






        #region OldInvoices



        // Update Invoice after Paid invoice
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateOldInvoiceAfterPaid")]
        public IActionResult CreateOldInvoiceAfterPaid(Guid invoiceId)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var invoice = _uow.OldInvoicesRepository.GetById(invoiceId);
                    var warshah = _uow.WarshahRepository.GetMany(a => a.OldWarshahId == invoice.WarshahId).FirstOrDefault();
                    invoice.OldCreatedon = DateTime.Now;
                    // from enum paid invoice
                    invoice.InvoiceStatusId = 3;
                    _uow.OldInvoicesRepository.Update(invoice);


                    // Add To Transaction TODAY bOX when cash
                   

                        TransactionsToday transactionsToday = new TransactionsToday();

                        transactionsToday.WarshahId = (int)warshah.Id;
                        transactionsToday.InvoiceNumber = invoice.InvoiceNumber;
                        transactionsToday.Vat = invoice.VatMoney;
                        if (invoice.AdvancePayment == null)
                        {
                            transactionsToday.Total = invoice.Total;

                        }
                        else
                        {
                            transactionsToday.Total = (decimal)(invoice.Total - invoice.AdvancePayment);
                        }

                        transactionsToday.CreatedOn = DateTime.Now;
                        transactionsToday.BeforeVat = invoice.AfterDiscount;
                       
                        transactionsToday.TransactionName = "فاتورة أمر إصلاح";
                        transactionsToday.Describtion = "فاتورة من النظام السابق";



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

                    

                    _uow.Save();

                  
                  



                    _uow.Save();
                    return Ok(invoice);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Invoice");


        }

        #endregion

        // Get all OldInvoices with warshah id

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllOldInvoicesByWarshahId")]
        public IActionResult GetAllOldInvoicesByWarshahId(Guid id, int pagenumber, int pagecount , string SearchText)
        {
            var invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id).ToHashSet().OrderByDescending(a => a.Id);


            if (SearchText != null)
            {

                invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id && (t.CarOwnerName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }



            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }



        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllOldInvoicesByWarshahIdInTime")]
        public IActionResult GetAllOldInvoicesByWarshahIdInTime(Guid id, int pagenumber, int pagecount , DateTime FromDate, DateTime ToDate , string SearchText)
        {
            var invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id && t.OldCreatedon >= FromDate && t.OldCreatedon <= ToDate).ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id && t.OldCreatedon >= FromDate && t.OldCreatedon <= ToDate && (t.CarOwnerName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }


            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invoices.ToPagedList(pagenumber, pagecount) });


        }

        // Get all OldInvoices Paid   (  النظام القديم الفواتير المسددة)

        ////[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetPaidOldInvoicesByInvoiceStatus")]
        public IActionResult GetPaidOldInvoicesByInvoiceStatus(Guid id, int pagenumber, int pagecount, string SearchText)
        {
            var invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 3 ).ToHashSet().OrderByDescending(a => a.Id);
            if (SearchText != null)
            {

                invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 3 && (t.CarOwnerName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }

            //return Ok(invoices.OrderByDescending(a => a.OldCreatedon));

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), invoices = invoices.OrderByDescending(a => a.OldCreatedon).ToPagedList(pagenumber, pagecount) });

        }



        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetPaidOldInvoicesByInvoiceStatusInTime")]
        public IActionResult GetPaidOldInvoicesByInvoiceStatusInTime(Guid id, DateTime FromDate, DateTime ToDate, string SearchText)
        {
            var invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 3 && t.OldCreatedon >= FromDate && t.OldCreatedon <= ToDate).ToHashSet().OrderByDescending(a => a.Id);


            if (SearchText != null)
            {

                invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id && t.OldCreatedon >= FromDate && t.OldCreatedon <= ToDate && t.InvoiceStatusId == 1 && (t.CarOwnerName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }

            return Ok(invoices.OrderByDescending(a => a.OldCreatedon));
        }


        // Get all OldInvoices open   (الفواتير  الغير المسددة النظام القديم)

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetOpenOldInvoicesByInvoiceStatus")]
        public IActionResult GetOpenOldInvoicesByInvoiceStatus(Guid id, int pagenumber, int pagecount, string SearchText)
        {
            var invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 1).ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 1 && (t.CarOwnerName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), invoices = invoices.OrderByDescending(a => a.OldCreatedon).ToPagedList(pagenumber, pagecount) });

            return Ok(invoices.OrderByDescending(a => a.OldCreatedon));
        }



        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetOpenOldInvoicesByInvoiceStatusInTime")]
        public IActionResult GetOpenOldInvoicesByInvoiceStatusInTime(Guid id, DateTime FromDate, DateTime ToDate , string SearchText)
        {
            var invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 1 && t.OldCreatedon >= FromDate && t.OldCreatedon <= ToDate).ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.OldInvoicesRepository.GetMany(t => t.WarshahId == id && t.OldCreatedon >= FromDate && t.OldCreatedon <= ToDate && t.InvoiceStatusId == 1 && (t.CarOwnerName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }

            return Ok(invoices.OrderByDescending(a => a.OldCreatedon));
        }




        // Get Oldinvoice by id
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetOldInvoiceById")]
        public IActionResult GetOldInvoiceById(Guid id)
        {
            var invoice = _uow.OldInvoicesRepository.GetMany(p => p.Id == id).FirstOrDefault();
            var Items = _uow.OldItemInvoiceRepository.GetMany(a => a.OldInvoiceId == id).ToHashSet().OrderByDescending(a => a.Id);
          
            return Ok(new { invoice = invoice, Items = Items });
        }



        [HttpGet, Route("GetTotalOldInvoicesBywarshahId")]
        public IActionResult GetTotalOldInvoicesBywarshahId(Guid oldwarshahid)
        {
            var invoice = _uow.OldInvoicesRepository.GetMany(p => p.WarshahId == oldwarshahid).ToHashSet();
            var total = invoice.Sum(a => a.Total);
            var countinvoice = invoice.Count();

            return Ok(new { TotalCount = countinvoice , TotalMoney = total  });
        }


        [HttpGet, Route("GetTotalOldInvoicesAllWarshahs")]
        public IActionResult GetTotalOldInvoicesAllWarshahs( )
        {
            var oldinvoice = _uow.OldInvoicesRepository.GetMany(a=>a.InvoiceStatusId == 3).ToHashSet();
            var total = oldinvoice.Sum(a => a.Total);
            var countinvoice = oldinvoice.Count();

            var newinvoices = _uow.InvoiceRepository.GetMany( t=>t.InvoiceStatusId == 2).ToHashSet();
            var newtotal = newinvoices.Sum(a => a.Total);
            var newcountinvoice = newinvoices.Count();

            var Totalcount = countinvoice + newcountinvoice;
            var Totalinvoice = total + newtotal;

            return Ok(new { TotalCount = Totalcount, TotalMoney = Totalinvoice });
        }





        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetItemsWithInvoiceID")]
        public IActionResult GetItemsWithInvoiceID(int id)
        {
            var Invoice = _uow.InvoiceRepository.GetById(id);


            var DTOResult = new ItemAndInvoice();
            DTOResult.Invoice = Invoice;
            var AllItemInvoice = _uow.InvoiceItemRepository.GetMany(p => p.InvoiceId == id).OrderByDescending(a => a.Id).ToHashSet();

            DTOResult.InvoiceItem = AllItemInvoice;




            return Ok(DTOResult);
        }


        // Get invoice by id
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]


        //public IActionResult GetInvoiceById(int id)
        //{
        //    var invoice = _uow.InvoiceRepository.GetMany(p => p.Id == id).FirstOrDefault();
        //    // Convert text to byte array
        //    byte[] textBytes = Encoding.UTF8.GetBytes(invoice.QRCode);

        //    // Convert byte array to Base64 string
        //    string base64String = Convert.ToBase64String(textBytes);

        //    invoice.QRCode = base64String;
        //    var Items = _uow.InvoiceItemRepository.GetMany(a => a.InvoiceId == id).ToHashSet().OrderByDescending(a => a.Id);
        //    if (invoice.CarOwnerID != null)
        //    {
        //        var Id = int.Parse(invoice.CarOwnerID);
        //        var CarOwner = _uow.UserRepository.GetMany(a => a.Id == Id).FirstOrDefault();
        //        var Auth = _uow.AuthrizedPersonRepository.GetMany(a => a.UserId == Id).FirstOrDefault();
        //        var DebitCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 1).Count();
        //        var CreditCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 2).Count();
        //        return Ok(new { invoice = invoice, Items = Items, CarOwner = CarOwner, Auth = Auth, CreditCount = CreditCount, DebitCount = DebitCount });

        //    }

        //    return Ok(new { invoice = invoice, Items = Items });
        //}


        [HttpGet, Route("GetInvoiceById")]
        public IActionResult GetInvoiceById(int id)
        {
            var invoice = _uow.InvoiceRepository.GetMany(p => p.Id == id).Include(a=>a.RepairOrder).FirstOrDefault();

            if (invoice == null)
            {
                return NotFound();
            }



            // توليد رمز QR كصورة
           
            if((invoice.WarshahId == 4  | invoice.WarshahId == 53 ) && invoice.QRCode != null)
            {
                using (var qrGenerator = new QRCodeGenerator())
                {
                    var qrCodeData = qrGenerator.CreateQrCode(invoice.QRCode, QRCodeGenerator.ECCLevel.Q);
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        using (var qrCodeImage = qrCode.GetGraphic(20))  // حجم الصورة
                        {
                            // تحويل الصورة إلى Base64
                            using (var ms = new MemoryStream())
                            {
                                qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                var base64String = Convert.ToBase64String(ms.ToArray());


                                invoice.QRCode = base64String;





                            }
                        }
                    }
                }
            }
        

            var Items = _uow.InvoiceItemRepository.GetMany(a => a.InvoiceId == id).ToHashSet().OrderByDescending(a => a.Id);

            if (invoice.CarOwnerID != null)
            {
                var Id = int.Parse(invoice.CarOwnerID);
                var CarOwner = _uow.UserRepository.GetMany(a => a.Id == Id).FirstOrDefault();
                var Auth = _uow.AuthrizedPersonRepository.GetMany(a => a.UserId == Id).FirstOrDefault();
                var DebitCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 1).Count();
                var CreditCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 2).Count();

                return Ok(new { invoice = invoice, Items = Items, CarOwner = CarOwner, Auth = Auth, CreditCount = CreditCount, DebitCount = DebitCount });
            }

            return Ok(new { invoice = invoice, Items = Items });
        }




        [HttpGet, Route("GetInvoiceHashById")]
        public IActionResult GetInvoiceHashById(string describtion)
        {
            var invoice = _uow.InvoiceRepository.GetMany(p => p.Describtion == describtion).FirstOrDefault();
            var Items = _uow.InvoiceItemRepository.GetMany(a => a.InvoiceId == invoice.Id).ToHashSet().OrderByDescending(a => a.Id);
   

            return Ok(new { invoice = invoice, Items = Items });
        }


        // Get all Invoices with warshah id

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllInvoicesWithWarshahId")]
        public IActionResult  GetAllInvoicesWithWarshahId(int id, int pagenumber, int pagecount , string SearchText)
        {
            var invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id).ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText !=  null) 
            {

                 invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id && ( t.CarOwnerName.Contains(SearchText)
                 || t.PaymentTypeName.Contains(SearchText)
                 || t.InvoiceSerial.Contains(SearchText) ) )
                 .ToHashSet().OrderByDescending(a => a.Id);

            }


            List<InvocieCreditDTO> invocieCreditDTOs = new List<InvocieCreditDTO>();

            foreach (var invoice in invoices)
            {


                InvocieCreditDTO dTO = new InvocieCreditDTO();
                dTO.Invoice = invoice;
                dTO.AdvancedpaymentCount = _uow.ReceiptVouchersRepository.GetMany(t => t.ReciptionOrderId == invoice.ReciptionOrderId).Count();
                dTO.DebitCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 1).Count();
                dTO.CreditCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 2).Count();
                invocieCreditDTOs.Add(dTO);
            }


            return Ok(new {pagenumber = pagenumber , pagesize = pagecount , totalrow = invoices.Count() , Listinvoice = invocieCreditDTOs.ToPagedList(pagenumber, pagecount) });


        }


        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllInvoicesWithWarshahIdInTime")]
        public IActionResult GetAllInvoicesWithWarshahIdInTime(int id, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate, string SearchText)
        {
            var invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id && t.CreatedOn >= FromDate  && t.CreatedOn <= ToDate).ToHashSet().OrderByDescending(a => a.Id);


            if (SearchText != null)
            {

                invoices = _uow.InvoiceRepository.GetMany(    t => t.WarshahId == id 
                && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate &&
                (t.CarOwnerName.Contains(SearchText)
                || t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }


            List<InvocieCreditDTO> invocieCreditDTOs = new List<InvocieCreditDTO>();

            foreach (var invoice in invoices)
            {


                InvocieCreditDTO dTO = new InvocieCreditDTO();
                dTO.Invoice = invoice;
                dTO.AdvancedpaymentCount = _uow.ReceiptVouchersRepository.GetMany(t => t.ReciptionOrderId == invoice.ReciptionOrderId).Count();
                dTO.DebitCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 1).Count();
                dTO.CreditCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 2).Count();
                invocieCreditDTOs.Add(dTO);
            }


            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invocieCreditDTOs.ToPagedList(pagenumber, pagecount) });


        }

        // Get all Invoices Claims   (عملاء المطالبات مطالبات)

        [HttpGet, Route("GetClaimsInvoicesByWarshahId")]
        public IActionResult GetClaimsInvoicesByWarshahId(int warshahid, int pagenumber, int pagecount)
        {
           var calims = _uow.ClaimInvoiceRepository.GetMany(t => t.WarshahId == warshahid).ToHashSet();


            List<int> lst = new List<int>();
            foreach (var c in calims)
            {
                int ownerid = Convert.ToInt16(c.CarOwnerID);
                lst.Add(ownerid);

            }

            List<int> uniqueLst = lst.Distinct().ToList();

            List <ClaimClientsDTO> owner = new List<ClaimClientsDTO>();

           foreach(var c in uniqueLst)
            {
                ClaimClientsDTO claimClientsDTO = new ClaimClientsDTO();

                var ownerclaim = _uow.UserRepository.GetById(c);

                claimClientsDTO.Id = c;
                claimClientsDTO.CarOwnerPhone = ownerclaim.Phone;
                claimClientsDTO.CarOwnerName = ownerclaim.FirstName + ownerclaim.LastName;

                owner.Add(claimClientsDTO);

            }





            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = owner.Count(), Listinvoice = owner.ToPagedList(pagenumber, pagecount) });

        }


        // Get all Invoices Claims   (الفواتير مطالبات)

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetClaimsInvoicesWithCarOwnerId")]
        public IActionResult GetClaimsInvoicesWithCarOwnerId(string id ,int pagenumber, int pagecount)
        {
            var calims = _uow.ClaimInvoiceRepository.GetMany(t => t.CarOwnerID == id).ToHashSet();
           



            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = calims.Count(), Listinvoice = calims.ToPagedList(pagenumber, pagecount) });


        }


        // Get  Invoices Claims   (مطالبة فاتورة)

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetClaimInvoiceByClaimId")]
        public IActionResult GetClaimInvoiceByClaimId(string id)
        {
            var calim = _uow.ClaimInvoiceRepository.GetById(id);

            return Ok(calim);
        }









        // Get all Invoices Paid   (الفواتير المسددة)

        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetPaidInvoicesWithInvoiceStatus")]
        public IActionResult GetPaidInvoicesWithInvoiceStatus(int id, int pagenumber, int pagecount, string SearchText)
        {
            var invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 2).Include(a=>a.RepairOrder).ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 2 && (t.CarOwnerName.Contains(SearchText)
                || t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText))).Include(a => a.RepairOrder)
                .ToHashSet().OrderByDescending(a => a.Id);

            }

            List<InvocieCreditDTO> invocieCreditDTOs = new List<InvocieCreditDTO>();

            foreach (var invoice in invoices)
            {


                InvocieCreditDTO dTO = new InvocieCreditDTO();
                dTO.Invoice = invoice;
                dTO.AdvancedpaymentCount = _uow.ReceiptVouchersRepository.GetMany(t => t.ReciptionOrderId == invoice.ReciptionOrderId).Count();
                dTO.DebitCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 1).Count();
                dTO.CreditCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 2).Count();
                invocieCreditDTOs.Add(dTO);
            }
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invocieCreditDTOs.Count(), invocieCreditDTOs = invocieCreditDTOs.ToPagedList(pagenumber, pagecount) });
           // return Ok(invocieCreditDTOs.OrderByDescending(a=>a.Invoice.Id));
        }



        ////[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetPaidInvoicesWithInvoiceStatusInTime")]
        public IActionResult GetPaidInvoicesWithInvoiceStatusInTime(int id, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate, string SearchText)
        {
            var invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 2 
            && t.CreatedOn >= Convert.ToDateTime(FromDate) && t.CreatedOn <= Convert.ToDateTime(ToDate)
            
            ).ToHashSet().OrderByDescending(a => a.Id);


            if (SearchText != null)
            {

                invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id
            && t.CreatedOn >= Convert.ToDateTime(FromDate) && t.CreatedOn <= Convert.ToDateTime(ToDate)&&


                t.InvoiceStatusId == 2 && (t.CarOwnerName.Contains(SearchText)
                || t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }

            List<InvocieCreditDTO> invocieCreditDTOs = new List<InvocieCreditDTO>();

            foreach (var invoice in invoices)
            {


                InvocieCreditDTO dTO = new InvocieCreditDTO();
                dTO.Invoice = invoice;
                dTO.AdvancedpaymentCount = _uow.ReceiptVouchersRepository.GetMany(t => t.ReciptionOrderId == invoice.ReciptionOrderId).Count();
                dTO.DebitCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 1).Count();
                dTO.CreditCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 2).Count();
                invocieCreditDTOs.Add(dTO);
            }


            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invocieCreditDTOs.OrderByDescending(a => a.Invoice.Id).ToPagedList(pagenumber, pagecount) });

           // return Ok(invocieCreditDTOs.OrderByDescending(a => a.Invoice.Id));
        }


        // Get all Invoices Open   (الفواتير المفتوحة)

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetOpenInvoicesWithInvoiceStatus")]
        public IActionResult GetOpenInvoicesWithInvoiceStatus(int id , int pagenumber, int pagecount, string SearchText)
        {
            var invoices = _uow.InvoiceRepository
                .GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 1)
                .ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 1 && 
                (t.CarOwnerName.Contains(SearchText)
                || t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }

            List<InvocieCreditDTO> invocieCreditDTOs = new List<InvocieCreditDTO>();

            foreach (var invoice in invoices)
            {


                InvocieCreditDTO dTO = new InvocieCreditDTO();
                dTO.Invoice = invoice;
                dTO.AdvancedpaymentCount = _uow.ReceiptVouchersRepository.GetMany(t => t.ReciptionOrderId == invoice.ReciptionOrderId).Count();
                dTO.DebitCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 1).Count();
                dTO.CreditCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 2).Count();
                invocieCreditDTOs.Add(dTO);
            }


            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invocieCreditDTOs.Count(), invocieCreditDTOs = invocieCreditDTOs.ToPagedList(pagenumber, pagecount) });


            //return Ok(invocieCreditDTOs);
        }



        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetOpenInvoicesWithInvoiceStatusInTime")]
        public IActionResult GetOpenInvoicesWithInvoiceStatusInTime(int id, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate , string SearchText)
        {
            var invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 1 
                            && t.CreatedOn >= Convert.ToDateTime(FromDate) && t.CreatedOn <= Convert.ToDateTime(ToDate) )
            .ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id &&
                t.CreatedOn >= Convert.ToDateTime(FromDate) && t.CreatedOn <= Convert.ToDateTime(ToDate) 
                && t.InvoiceStatusId == 1 && (t.CarOwnerName.Contains(SearchText)
                || t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }


            List<InvocieCreditDTO> invocieCreditDTOs = new List<InvocieCreditDTO>();

            foreach (var invoice in invoices)
            {


                InvocieCreditDTO dTO = new InvocieCreditDTO();
                dTO.Invoice = invoice;
                dTO.AdvancedpaymentCount = _uow.ReceiptVouchersRepository.GetMany(t => t.ReciptionOrderId == invoice.ReciptionOrderId).Count();
                dTO.DebitCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 1).Count();
                dTO.CreditCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 2).Count();
                invocieCreditDTOs.Add(dTO);
            }


            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invocieCreditDTOs.OrderByDescending(a => a.Invoice.Id).ToPagedList(pagenumber, pagecount) });



           // return Ok(invocieCreditDTOs.OrderByDescending(a => a.Invoice.Id));
        }



        // Get all Invoices Delayed   (الفواتير المؤجلة)

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetDelayInvoicesWithInvoiceStatus")]
        public IActionResult GetDelayInvoicesWithInvoiceStatus(int id, int pagenumber, int pagecount, string SearchText)
        {
            var invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 4).ToHashSet().OrderByDescending(a => a.Id);

            if (SearchText != null)
            {

                invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 4 &&  (t.CarOwnerName.Contains(SearchText)
                || t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }

            List<InvocieCreditDTO> invocieCreditDTOs = new List<InvocieCreditDTO>();

            foreach (var invoice in invoices)
            {


                InvocieCreditDTO dTO = new InvocieCreditDTO();
                dTO.Invoice = invoice;
                dTO.AdvancedpaymentCount = _uow.ReceiptVouchersRepository.GetMany(t => t.ReciptionOrderId == invoice.ReciptionOrderId).Count();
                dTO.DebitCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 1).Count();
                dTO.CreditCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 2).Count();
                invocieCreditDTOs.Add(dTO);
            }




            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invocieCreditDTOs.ToPagedList(pagenumber, pagecount) });

        }


        ////[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetDelayInvoicesWithInvoiceStatusInTime")]
        public IActionResult GetDelayInvoicesWithInvoiceStatusInTime(int id, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate , string SearchText)
        {
            var invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id && t.InvoiceStatusId == 4 && 
            t.CreatedOn >= Convert.ToDateTime(FromDate) && t.CreatedOn <= Convert.ToDateTime(ToDate)).ToHashSet().OrderByDescending(a => a.Id);


            if (SearchText != null)
            {

                invoices = _uow.InvoiceRepository.GetMany(t => t.WarshahId == id &&  t.CreatedOn >=Convert.ToDateTime(FromDate)  
                && t.CreatedOn <=Convert.ToDateTime(ToDate)  && t.InvoiceStatusId == 4 && (t.CarOwnerName.Contains(SearchText)
                || t.PaymentTypeName.Contains(SearchText)
                || t.InvoiceSerial.Contains(SearchText)))
                .ToHashSet().OrderByDescending(a => a.Id);

            }



            List<InvocieCreditDTO> invocieCreditDTOs = new List<InvocieCreditDTO>();

            foreach (var invoice in invoices)
            {


                InvocieCreditDTO dTO = new InvocieCreditDTO();
                dTO.Invoice = invoice;
                dTO.AdvancedpaymentCount = _uow.ReceiptVouchersRepository.GetMany(t => t.ReciptionOrderId == invoice.ReciptionOrderId).Count();
                dTO.DebitCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 1).Count();
                dTO.CreditCount = _uow.DebitAndCreditorRepository.GetMany(t => t.InvoiceId == invoice.Id && t.Flag == 2).Count();
                invocieCreditDTOs.Add(dTO);
            }



            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = invoices.Count(), Listinvoice = invocieCreditDTOs.OrderByDescending(a => a.Invoice.Id).ToPagedList(pagenumber, pagecount) });
           // return Ok(invocieCreditDTOs.OrderByDescending(a => a.Invoice.Id));
        }





        // Get Invoice Subscribtion in warshah tech
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetInvoiceSUbscribtionWithWarshahId")]
        public IActionResult GetInvoiceSUbscribtionWithWarshahId(int id)
        {
         
            var invoice = _uow.SubscribtionInvoicerepository.GetMany(a=>a.WarshahId==id);

            return Ok(invoice);


        }


        // Get Invoice Subscribtion in warshah tech
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetInvoiceSUbscribtionByInvoiceId")]
        public IActionResult GetInvoiceSUbscribtionByInvoiceId(int Invoiceid)
        {

            var invoice = _uow.SubscribtionInvoicerepository.GetById(Invoiceid);

            // توليد رمز QR كصورة

            if (invoice.QRCode != null)
            {
                using (var qrGenerator = new QRCodeGenerator())
                {
                    var qrCodeData = qrGenerator.CreateQrCode(invoice.QRCode, QRCodeGenerator.ECCLevel.Q);
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        using (var qrCodeImage = qrCode.GetGraphic(20))  // حجم الصورة
                        {
                            // تحويل الصورة إلى Base64
                            using (var ms = new MemoryStream())
                            {
                                qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                var base64String = Convert.ToBase64String(ms.ToArray());


                                invoice.QRCode = base64String;





                            }
                        }
                    }
                }
            }


            return Ok(invoice);
        }




        // Delayed Invoice  
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("DelayedInvoice")]
        public IActionResult DelayedInvoice(int  InvoiceId)
        {
            if (ModelState.IsValid)
            {
                try
                {

                  var  invoice = _uow.InvoiceRepository.GetById(InvoiceId);
                    invoice.UpdatedOn = DateTime.Now;
                    // from enum Delayed invoice
                    invoice.InvoiceStatusId = 4;
                    _uow.InvoiceRepository.Update(invoice);
                    _uow.Save();

                   //ClaimInvoice claimInvoice = new ClaimInvoice();  
                   // claimInvoice.InvoiceId = InvoiceId;
                   // claimInvoice.CreatedOn = invoice.CreatedOn;
                   // claimInvoice.InvoiceSerial = invoice.InvoiceSerial;
                   // claimInvoice.InvoiceNumber = invoice.InvoiceNumber;
                   // claimInvoice.CarOwnerID = invoice.CarOwnerID;
                   // claimInvoice.CarOwnerName = invoice.CarOwnerName;
                   // claimInvoice.CarOwnerPhone = invoice.CarOwnerPhone;
                   // claimInvoice.AfterDiscount = invoice.AfterDiscount;
                   // claimInvoice.VatMoney = invoice.VatMoney;
                   // claimInvoice.Total  = invoice.Total;
                   // claimInvoice.WarshahId = invoice.WarshahId;
                   // _uow.ClaimInvoiceRepository.Add(claimInvoice);
                   // _uow.Save();

                    return Ok(invoice);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Invoice");


        }



        // Calim Invoice

        // Delayed Invoice  
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CalimInvoice")]
        public IActionResult CalimInvoice(int InvoiceId)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var invoice = _uow.InvoiceRepository.GetById(InvoiceId);
                    invoice.UpdatedOn = DateTime.Now;
                    // from enum Calim invoice
                    invoice.InvoiceStatusId = 5;
                    _uow.InvoiceRepository.Update(invoice);
                    _uow.Save();

                    ClaimInvoice claimInvoice = new ClaimInvoice();
                    claimInvoice.InvoiceId = InvoiceId;
                    claimInvoice.CreatedOn = invoice.CreatedOn;
                    claimInvoice.InvoiceSerial = invoice.InvoiceSerial;
                    claimInvoice.InvoiceNumber = invoice.InvoiceNumber;
                    claimInvoice.CarOwnerID = invoice.CarOwnerID;
                    claimInvoice.CarOwnerName = invoice.CarOwnerName;
                    claimInvoice.CarOwnerPhone = invoice.CarOwnerPhone;
                    claimInvoice.AfterDiscount = invoice.AfterDiscount;
                    claimInvoice.VatMoney = invoice.VatMoney;
                    claimInvoice.Total = invoice.Total;
                    claimInvoice.WarshahId = invoice.WarshahId;
                    _uow.ClaimInvoiceRepository.Add(claimInvoice);
                    _uow.Save();

                    return Ok(invoice);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Invoice");


        }


        // Update Invoice after Paid invoice
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateInvoiceAfterPaid")]
        public async Task<IActionResult>  CreateInvoiceAfterPaid(int invoiceId)
        {
            if (ModelState.IsValid)
            {
                try
                {

                  var  invoice = _uow.InvoiceRepository.GetById(invoiceId);
                    invoice.UpdatedOn = DateTime.Now;
                    invoice.CreatedOn = invoice.CreatedOn;
                    // from enum paid invoice
                    invoice.InvoiceStatusId = 2;

                    // phase 1 


                    //if (invoice.WarshahId != 4)
                    //{


                    //    var FinalQrString = BL.TLVHELPER.GetBase64String(invoice.WarshahName, invoice.WarshahTaxNumber, invoice.CreatedOn.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"), invoice.Total.ToString(), invoice.VatMoney.ToString());

                    //    invoice.QRCode = FinalQrString;
                    //}
                    // رصيد البنك عبارة عن الرصيد الافتتاحى بالإضافة إلى الفواتير المدفوعة عن طريق شيكات أو بطاقة ائتمان (Card = 2 ) 

                    if (invoice.PaymentTypeInvoiceId == 2)
                    {
                        var currentbalance = _uow.BalanceBankRepository.GetMany(b => b.WarshahId == invoice.WarshahId).FirstOrDefault();
                        if (currentbalance != null)
                        
                        { currentbalance.CurrentBalance = currentbalance.CurrentBalance + invoice.Total;
                            _uow.BalanceBankRepository.Update(currentbalance);
                        }
                        else
                        {
                            _uow.BalanceBankRepository.Add(new BalanceBank { WarshahId = (int)invoice.WarshahId, OpeningBalance = 0, CurrentBalance = invoice.Total ,CreatedOn = DateTime.Now });
                      
                        }
                       
                       
                    }

                    // Add To Transaction TODAY bOX when cash
                    if (invoice.PaymentTypeInvoiceId == 1)
                    {

                        TransactionsToday transactionsToday = new TransactionsToday();

                        transactionsToday.WarshahId = (int)invoice.WarshahId;
                        transactionsToday.InvoiceNumber = invoice.InvoiceNumber;
                        transactionsToday.Vat = invoice.VatMoney;
                        if (invoice.AdvancePayment == null)
                        {
                            transactionsToday.Total = invoice.Total;

                        }
                        else
                        {
                            transactionsToday.Total = (decimal)(invoice.Total - invoice.AdvancePayment);
                        }

                        transactionsToday.CreatedOn = DateTime.Now;
                        transactionsToday.BeforeVat = invoice.AfterDiscount;
                        if(invoice.InvoiceTypeId == 2)
                        {
                            transactionsToday.TransactionName = "فاتورة أمر إصلاح";
                        }

                        if (invoice.InvoiceTypeId == 3)
                        {
                            transactionsToday.TransactionName = "فاتورة سريعة ";
                        }

                        if (invoice.InvoiceTypeId == 4)
                        {
                            transactionsToday.TransactionName = "فاتورة فورية ";
                        }


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


                    _uow.InvoiceRepository.Update(invoice);


                    

                    _uow.Save();




                    //Invoice Paid  فاتورة مسددة

                    var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == invoice.WarshahId & a.NameNotificationId == 3 & a.StatusNotificationId == 1).FirstOrDefault();

                    if (notificationActive != null)
                    {
                        //if (invoice.CarOwnerID != null)
                        //{
                        //    _NotificationService.SetNotification(Int32.Parse(invoice.CarOwnerID), "تم اصدار فاتوره جديده ");

                        //}

                        var user = _uow.UserRepository.GetMany(a => a.WarshahId == invoice.WarshahId && a.RoleId == 1).FirstOrDefault();

                        string messge2 = "تم   سداد الفاتورة  رقم   " + invoice.InvoiceNumber;

                        _NotificationService.SetNotificationTaqnyat(user.Id, messge2);

                    }





                    



                   






                    if (invoice.CarOwnerID != null)
                    {
                      
                        int OwnerCarID = int.Parse(invoice.CarOwnerID);
                        _loyalityService.SetLoyalityPoints((int)(invoice.WarshahId), OwnerCarID, invoice.AfterDiscount);
                    }

                    var calimowner = _uow.ClaimInvoiceRepository.GetMany(v => v.InvoiceId == invoice.Id).FirstOrDefault();
                    if(calimowner != null)
                    {
                        _uow.ClaimInvoiceRepository.Delete(calimowner.Id);
                        _uow.Save();
                    }
                   
                   if(invoice.CarOwnerID != null)
                    {

                        var customer = _uow.WarshahCarOwnersRepository.GetMany(a=>a.WarshahId== invoice.WarshahId && a.CarOwnerId == Convert.ToInt16(invoice.CarOwnerID)).FirstOrDefault();

                        if (customer == null) 
                        {
                            var carwarshah = new WarshahWithCarOwner();
                            carwarshah.CarOwnerId = Convert.ToInt16(invoice.CarOwnerID);
                            carwarshah.WarshahId = (int)invoice.WarshahId;
                            _uow.WarshahCarOwnersRepository.Add(carwarshah);
                            _uow.Save();


                        }



                    }

                    if (invoice.WarshahId == 4 || invoice.WarshahId == 53)
                    {
                        TaxResponse response = new TaxResponse();


                        TaxInv _TaxInv = new TaxInv();
                        //response = await _TaxInv.CreateXml_and_SendInv(InvoiceID, "Id", "DebitAndCreditors", "IQ_KSATaxInvHeaderDebitAndCredit", "IQ_KSATaxInvItemsDebitAndCredit", "IQ_KSATaxInvHeader_PerPaid", "");
                        response = await _TaxInv.CreateXml_and_SendInv(invoice.Id, "Id", "Invoices", "IQ_KSATaxInvHeader", "IQ_KSATaxInvItems", "IQ_KSATaxInvHeader_PerPaid", "");
                    }

                    return Ok(invoice);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Invoice");


        }




        [AllowAnonymous]
        [HttpGet, Route("GetQRCode")]
        public IActionResult GetQrCode(int InvoiceId)
        {
            var InvoiceItem = _uow.InvoiceRepository.GetById(InvoiceId);
            var FinalQrString = BL.TLVHELPER.GetBase64String(InvoiceItem.WarshahName, InvoiceItem.WarshahTaxNumber, InvoiceItem.CreatedOn.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"), InvoiceItem.Total.ToString(), InvoiceItem.VatMoney.ToString());
            return Ok(new { FinalQrString = FinalQrString });
        }



        [HttpGet, Route("AddQRCodePhase1")]
        public IActionResult AddQRCodePhase1( )
        {
            var invoices = _uow.InvoiceRepository.GetMany(a => a.QRCode == null && a.Total > 0 && a.WarshahTaxNumber != null ).ToHashSet();
            foreach( var invoice in invoices)
            {
                var InvoiceItem = _uow.InvoiceRepository.GetById(invoice.Id);
                var FinalQrString = BL.TLVHELPER.GetBase64String(InvoiceItem.WarshahName, InvoiceItem.WarshahTaxNumber, InvoiceItem.CreatedOn.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"), InvoiceItem.Total.ToString(), InvoiceItem.VatMoney.ToString());
                InvoiceItem.QRCode = FinalQrString;
                _uow.InvoiceRepository.Update(InvoiceItem);
            }

            _uow.Save();
          
            return Ok( );
        }





        [AllowAnonymous]
        [HttpGet, Route("GetQRCodeInvoiceHash")]
        public IActionResult GetQRCodeInvoiceHash(string Describtion)
        {
            var InvoiceItem = _uow.InvoiceRepository.GetMany(a=>a.Describtion == Describtion).FirstOrDefault();
            var FinalQrString = BL.TLVHELPER.GetBase64String(InvoiceItem.WarshahName, InvoiceItem.WarshahTaxNumber, InvoiceItem.CreatedOn.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"), InvoiceItem.Total.ToString(), InvoiceItem.VatMoney.ToString());
            return Ok(new { FinalQrString = FinalQrString });
        }



        [AllowAnonymous]
        [HttpGet, Route("GetQRCodeOldinvoice")]
        public IActionResult GetQRCodeOldinvoice(Guid InvoiceId)
        {
            var InvoiceItem = _uow.OldInvoicesRepository.GetById(InvoiceId);
            var FinalQrString = BL.TLVHELPER.GetBase64String(InvoiceItem.WarshahName, InvoiceItem.WarshahTaxNumber, InvoiceItem.OldCreatedon.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"), InvoiceItem.Total.ToString(), InvoiceItem.VatMoney.ToString());
            return Ok(new { FinalQrString = FinalQrString });
        }


        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpGet, Route("GetQrCodeSubscribtionInvoice")]
        public IActionResult GetQrCodeSubscribtionInvoice(int InvoiceId)
        {
            var InvoiceItem = _uow.SubscribtionInvoicerepository.GetById(InvoiceId);
            InvoiceItem.WarshahName = "شركة العربة الدولية لتقنية المعلومات";
            InvoiceItem.WarshahTaxNumber = "310188507200003";
            var FinalQrString = BL.TLVHELPER.GetBase64String(InvoiceItem.WarshahName, InvoiceItem.WarshahTaxNumber, InvoiceItem.CreatedOn.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"), InvoiceItem.TotalSubscribtion.ToString(), InvoiceItem.SubscribtionVat.ToString());
            return Ok(new { FinalQrString = FinalQrString });
        }


        [AllowAnonymous]
        [HttpGet, Route("GetQrCodeNotice")]
        public IActionResult GetQrCodeNotice(int NoticeId)
        {
            var NoticeItem = _uow.DebitAndCreditorRepository.GetById(NoticeId);
            var InvoiceItem = _uow.InvoiceRepository.GetById(NoticeItem.InvoiceId);
            var FinalQrString = BL.TLVHELPER.GetBase64String(InvoiceItem.WarshahName, InvoiceItem.WarshahTaxNumber, NoticeItem.CreatedOn.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"), NoticeItem.Total.ToString(), NoticeItem.Vat.ToString());
            return Ok(new { FinalQrString = FinalQrString });
        }



        [AllowAnonymous]
        [HttpGet, Route("GetQRCodeWarshah")]
        public IActionResult GetQRCodeWarshah(int InvoiceId)
        {
            var InvoiceItem = _uow.SubscribtionInvoicerepository.GetById(InvoiceId);
            var FinalQrString = BL.TLVHELPER.GetBase64String(InvoiceItem.WarshahName, InvoiceItem.WarshahTaxNumber, InvoiceItem.CreatedOn.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"), InvoiceItem.TotalSubscribtion.ToString(), InvoiceItem.SubscribtionVat.ToString());
            return Ok(new { FinalQrString = FinalQrString });
        }

        // Add Invoice To Fast Servies 
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("FastServiceInvoic")]
        public IActionResult FastServiceInvoic(IssueInvoiceFastDTO issueInvoiceFastDTO)
        {
            var Result = AddFastInvoice(issueInvoiceFastDTO.Discount,issueInvoiceFastDTO.FastServiceOrderId , issueInvoiceFastDTO.PaymentInvoiceType , issueInvoiceFastDTO.CategoryInvoiceId );
            if (Result != null)
            {
                return Ok(Result);

            }
            return BadRequest(new { status = "Erorr" });
        }


        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("ImediateInvoice")]
        public IActionResult ImediateInvoice(InvoiceDTO invoice)
        {
            if (ModelState.IsValid)
            {


                // Get last invoice number for each warshash
                var invoicenumber = _uow.InvoiceRepository.GetMany(i => i.WarshahId == invoice.WarshahId).OrderByDescending(t => t.InvoiceNumber).FirstOrDefault();
                if (invoicenumber == null)
                {
                    invoice.InvoiceNumber = 1;
                }
                else
                {
                    int lastnumber = invoicenumber.InvoiceNumber;
                    invoice.InvoiceNumber = lastnumber + 1;
                }
                var invoicee = _mapper.Map<DL.Entities.Invoice>(invoice);
                invoicee.InvoiceSerial =   "IN-" + invoice.WarshahId + "-" + invoice.InvoiceNumber;
                invoicee.InvoiceStatusId = 1;
                //invoicee.InvoiceCategoryId = 2;
                var warshah = _uow.WarshahRepository.GetById(invoicee.WarshahId);
                if (warshah != null)
                {
                    invoicee.WarshahName = warshah.WarshahNameAr;
                    invoicee.WarshahCR = warshah.CR;
                    invoicee.WarshahTaxNumber = warshah.TaxNumber;
                    invoicee.WarshahPhone = warshah.LandLineNum;
                    invoicee.WarshahDescrit = warshah.Distrect;
                    invoicee.WarshahStreet = warshah.Street;
                    invoicee.WarshahAddress = _uow.CityRepository.GetById(warshah.CityId)?.CityNameAr + " -" + warshah.Street + "-" + warshah.Distrect;
                    invoicee.WarshahCity = _uow.CityRepository.GetById(warshah.CityId)?.CityNameAr;
                    invoicee.InvoiceTypeId = 4;
                }
                invoicee.CreatedOn = DateTime.Now;
                //var InvSerial = _serialService.GetInvoiceSerial((int)invoice.WarshahId);
                //invoicee.InvoiceNumber = InvSerial.InvoiceNumber;
                //invoicee.InvoiceSerial = InvSerial.InvoiceSerial;
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

                invoicee.BeforeDiscount = invoicee.FixingPrice;
                invoicee.AfterDiscount = invoicee.FixingPrice;
                invoicee.VatMoney = invoicee.FixingPrice * Vat;
                invoicee.Total = invoicee.FixingPrice + invoicee.VatMoney;

                var carowner = _uow.UserRepository.GetMany(a=>a.Phone == invoicee.CarOwnerPhone).FirstOrDefault();

                if(carowner != null)
                {
                    invoicee.CarOwnerID = carowner.Id.ToString();
                    invoice.CarOwnerTaxNumber = carowner.TaxNumber;
                    invoice.CarOwnerCR =carowner.CommerialRegisterar;

                }

                if (carowner == null)
                {
                    //invoicee.CarOwnerID = carowner.Id.ToString();
                    invoice.CarOwnerTaxNumber = invoicee.CarOwnerTaxNumber;
                    invoice.CarOwnerCR =  invoicee.CarOwnerCR;

                }



                invoicee.Describtion = new string(Enumerable.Repeat(chars, length)
                                    .Select(s => s[random.Next(s.Length)]).ToArray());

           

                _uow.InvoiceRepository.Add(invoicee);

                _uow.Save();

                var Additem = new InvoiceItem();
                Additem.PeacePrice = 0;
                Additem.FixPrice = invoicee.FixingPrice;
                Additem.InvoiceId = invoicee.Id;
                Additem.Garuntee = "0";
                Additem.SparePartNameAr = invoicee.TechReview;
                Additem.Quantity = 1;
                Additem.CreatedOn = DateTime.Now;

                _uow.InvoiceItemRepository.Add(Additem);

                //// Add To WarshahTechService  

                WarshahTechService warshahTechService = new WarshahTechService();
                warshahTechService.WarshahId = (int)invoicee.WarshahId;
                warshahTechService.InvoiceId = invoicee.Id;
                warshahTechService.InvoiceDate = invoicee.CreatedOn;
                warshahTechService.InvoiceNumber = invoicee.InvoiceNumber;
                warshahTechService.InvoiceSerial = invoicee.InvoiceSerial;
                warshahTechService.WarshahName = invoicee.WarshahName;
                warshahTechService.WarshahPhone = invoicee.WarshahPhone;
                warshahTechService.PaymentTypeInvoiceId = invoicee.PaymentTypeInvoiceId;
                warshahTechService.PaymentTypeName = invoicee.PaymentTypeName;
                warshahTechService.AfterDiscount = invoicee.AfterDiscount;

                //decimal percent = (10 / 100);

                var v = 10;
                decimal percent = (((decimal)v) / (100));

                warshahTechService.WarshahService = warshahTechService.AfterDiscount * percent;

                _uow.WarshahTechServiceRepository.Add(warshahTechService);

                _uow.Save();


                var sms = "";

                var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == invoicee.WarshahId & a.NameNotificationId == 4 & a.StatusNotificationId == 1).FirstOrDefault();

                if (notificationActive != null)
                {
                    var InvoiceLink = ActionLink + "/GetInvoiceHashById/" + invoicee.Describtion;

                    string messge2 = "تم اصدار فاتوره فورية  جديده "
                    + "\n للإطلاع و مراجعة الفاتورة من خلال الرابط التالى  "
                  + "\n  " + InvoiceLink;

                    sms = "تم اصدار فاتوره فورية  جديده "
                    + "\n للإطلاع و مراجعة الفاتورة من خلال الرابط التالى  "
                  + "\n  " + InvoiceLink;


                    if( invoicee.CarOwnerID != null)
                    {
                        _NotificationService.SetNotificationTaqnyat(Int32.Parse(invoicee.CarOwnerID), messge2);
                    }

                  
                    var user = _uow.UserRepository.GetMany(a => a.WarshahId == invoice.WarshahId && a.RoleId == 1).FirstOrDefault();

                    string messge = "تم   سداد فاتورة فورية  رقم   " + invoicee.InvoiceNumber;

                    _NotificationService.SetNotificationTaqnyat(user.Id, messge);
                }

                //if(warshah.LandLineNum == "535353" && carowner != null)
                //{

                //    _ = MyFatoorahOperation.Program.SendPayment( carowner.FirstName + carowner.LastName , carowner.Phone , carowner.Email , invoicee.Total , 3  );

                //}


                return Ok(invoicee);
            }
            return BadRequest(ModelState);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        private DL.Entities.Invoice AddFastInvoice(bool Discount ,int issueInvoiceFastDTO , int paymenttypeinvoice , int categoryinvoiceId )
        {
            try
            {
                var AllItems = _uow.ServiceInvoiceItemRepository.GetMany(a => a.ServiceInvoiceId == issueInvoiceFastDTO).ToHashSet().OrderByDescending(a => a.Id);
                var Order = _uow.ServiceInvoiceRepository.GetMany(a => a.Id == issueInvoiceFastDTO).Include(a => a.Motors.motorMake).Include(a => a.Motors.motorModel).Include(a => a.Motors.CarOwner).FirstOrDefault();

                DL.Entities.Invoice inv = new DL.Entities.Invoice();
                inv.AdvancePayment = 0;
                inv.AfterDiscount = Order.afterDiscount;
                inv.BeforeDiscount = Order.BeforeDiscount;
                inv.CarOwnerID = Order.Motors.CarOwnerId.ToString();
                inv.CarOwnerAddress = Order.Motors.CarOwner.Address;
                inv.CarOwnerName = Order.Motors.CarOwner.FirstName +" "+ Order.Motors.CarOwner.LastName;
                inv.CarOwnerPhone = Order.Motors.CarOwner.Phone;
                inv.CarOwnerTaxNumber = Order.Motors.CarOwner.TaxNumber;
                inv.CarOwnerCR = Order.Motors.CarOwner.CommerialRegisterar;
                inv.CarType = Order.Motors.motorMake.MakeNameAr + "-" + Order.Motors.motorModel.ModelNameAr + "-" + Order.Motors.PlateNo;
                inv.CheckPrice = 0;
                inv.CarOwnerCivilId = Order.Motors.CarOwner.CivilId;
                inv.CreatedOn = DateTime.Now;
                inv.Deiscount = Order.Discount;
                inv.InvoiceCategoryId = categoryinvoiceId;
                var InvSerial = _serialService.GetInvoiceSerial(Order.WarshahId);
                inv.InvoiceNumber = InvSerial.InvoiceNumber;
                inv.InvoiceSerial = InvSerial.InvoiceSerial;
                inv.InvoiceStatusId = 1;
                inv.InvoiceTypeId = 3;
                inv.KMIn = 0;
                inv.KMOut = 0;
                inv.FastServiceOrderId = issueInvoiceFastDTO;
                inv.PaymentTypeInvoiceId = paymenttypeinvoice;
                inv.PaymentTypeName = _uow.PaymentTypeInvoiceRepository.GetById(Order.PaymentTypeInvoiceId)?.PaymentTypeNameAr;
                inv.RemainAmount = 0;
                inv.TechReview = "Fast Service - خدمات سريعة ";
                inv.Total = Order.Total;
                inv.VatMoney = Order.Vat;
                int OwnerCarID = int.Parse(inv.CarOwnerID);


                // to Calculate discount from point car owner

                if (Discount)
                {
                    
                    var CarOwnerPoints = _uow.LoyalityPointRepository.GetMany(s => s.WarshahId == Order.WarshahId && s.CarOwnerId == OwnerCarID).FirstOrDefault();
                    var MoneyForPointWarshah = _uow.LoyalitySettingRevarseRepository.GetMany(s => s.WarshahId == Order.WarshahId).FirstOrDefault();

                    if (MoneyForPointWarshah != null)
                    {
                        inv.DiscountPoint = CarOwnerPoints.Points / MoneyForPointWarshah.CurrancyPerLoyalityPoints;

                        inv.Total = (decimal)(inv.Total - inv.DiscountPoint);

                        CarOwnerPoints.Points = 0;
                        _uow.LoyalityPointRepository.Update(CarOwnerPoints);
                    }
                }


                var Warshah = _uow.WarshahRepository.GetById(Order.WarshahId);
                inv.WarhshahCondition = Warshah.Terms;
                inv.WarshahAddress = _uow.CountryRepository.GetById(Warshah.CountryId).CountryNameAr + "-" + _uow.RegionRepository.GetById(Warshah.RegionId).RegionNameAr + "-" + _uow.CityRepository.GetById(Warshah.CityId).CityNameAr;
                inv.WarshahCity = _uow.CityRepository.GetById(Warshah.CityId).CityNameAr;
                inv.WarshahCR = Warshah.CR;
                inv.WarshahDescrit = Warshah.Distrect;
                inv.WarshahId = Warshah.Id;
                inv.WarshahName = Warshah.WarshahNameAr + "-" + Warshah.WarshahNameEn;
                inv.WarshahPhone = Warshah.LandLineNum;
                inv.WarshahPostCode = Warshah.PostalCode.ToString();
                inv.WarshahStreet = Warshah.Street;
                inv.WarshahTaxNumber = Warshah.TaxNumber;


                inv.Describtion = new string(Enumerable.Repeat(chars, length)
                                    .Select(s => s[random.Next(s.Length)]).ToArray());

                _uow.InvoiceRepository.Add(inv);


                //// Add To WarshahTechService  

                WarshahTechService warshahTechService = new WarshahTechService();
                warshahTechService.WarshahId = (int)inv.WarshahId;
                warshahTechService.InvoiceId = inv.Id;
                warshahTechService.InvoiceDate = inv.CreatedOn;
                warshahTechService.InvoiceNumber = inv.InvoiceNumber;
                warshahTechService.InvoiceSerial = inv.InvoiceSerial;
                warshahTechService.WarshahName = inv.WarshahName;
                warshahTechService.WarshahPhone = inv.WarshahPhone;
                warshahTechService.PaymentTypeInvoiceId = inv.PaymentTypeInvoiceId;
                warshahTechService.PaymentTypeName = inv.PaymentTypeName;
                warshahTechService.AfterDiscount = inv.AfterDiscount;

                //decimal percent = (10 / 100);

                var v = 10;
                decimal percent = (((decimal)v) / (100));

                warshahTechService.WarshahService = warshahTechService.AfterDiscount * percent;

                _uow.WarshahTechServiceRepository.Add(warshahTechService);


                Order.IsActive = false;
                _uow.ServiceInvoiceRepository.Update(Order);

                _uow.Save();









                foreach (var item in AllItems)
                {
                    InvoiceItem IT = new InvoiceItem();
                    IT.CreatedOn = DateTime.Now;
                    IT.Garuntee = "-";
                    IT.InvoiceId = inv.Id;
                    IT.PeacePrice = item.ItemPrice;
                    IT.Quantity = 1;
                    IT.SparePartNameAr = item.ItemName;
                    IT.SparePartNameEn = item.ItemName;
                    _uow.InvoiceItemRepository.Add(IT);
                }
                _uow.Save();


                // Invoice Fast فاتورة أمر سريع
                var sms = "";

                var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == inv.WarshahId & a.NameNotificationId == 5 & a.StatusNotificationId == 1).FirstOrDefault();

                if (notificationActive != null)
                {
                    var InvoiceLink = ActionLink + "/GetInvoiceHashById/" + inv.Describtion;

                    string messge2 = "تم اصدار فاتوره سريعة  جديده "
                    + "\n للإطلاع و مراجعة الفاتورة من خلال الرابط التالى  "
                  + "\n  " + InvoiceLink;

                    sms = "تم اصدار فاتوره  سريعة جديده "
                    + "\n للإطلاع و مراجعة الفاتورة من خلال الرابط التالى  "
                  + "\n  " + InvoiceLink;


                    _NotificationService.SetNotificationTaqnyat(Int32.Parse(inv.CarOwnerID), messge2);
                    var user = _uow.UserRepository.GetMany(a => a.WarshahId == inv.WarshahId && a.RoleId == 1).FirstOrDefault();

                    string messge = "تم اصدار فاتورة سريعة غير مسددة جديدة رقم الفاتورة  " + inv.InvoiceNumber;

                    _NotificationService.SetNotificationTaqnyat(user.Id, messge);

                }




               

                //_loyalityService.SetLoyalityPoints((int)(inv.WarshahId), Int32.Parse(inv.CarOwnerID), inv.AfterDiscount);

                // Add Bonus to technical

                var bonusPercent = _uow.BonusTechnicalRepository.GetMany(b => b.WarshahId == inv.WarshahId).FirstOrDefault();

                if(bonusPercent != null)
                {
                    decimal bonusTotal = inv.AfterDiscount * (bonusPercent.BonusPercent / 100);

                    RecordBonusTechnical bonus = new RecordBonusTechnical();
                    bonus.WarshahId = (int)inv.WarshahId;
                    bonus.CreatedOn = DateTime.Now;
                    bonus.UserId = Order.TechId;
                    bonus.Bonus = bonusTotal;

                    _uow.RecordBonusRepository.Add(bonus);
                }
                
                _uow.Save();


                return inv;
            }
            catch (Exception ex)
            {

                var Invoice = new DL.Entities.Invoice();
                Invoice = null;
                return Invoice;
            }

        }

        #endregion
        [HttpGet,Route("CreateXML")]
        public IActionResult CreateXML()
        {
            //Decalre a new XMLDocument object
            XmlDocument doc = new XmlDocument();

            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            //create the root element
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //string.Empty makes cleaner code
            XmlElement element1 = doc.CreateElement(string.Empty, "Mainbody", string.Empty);
            doc.AppendChild(element1);

            XmlElement element2 = doc.CreateElement(string.Empty, "level1", string.Empty);


            XmlElement element3 = doc.CreateElement(string.Empty, "level2", string.Empty);

            XmlText text1 = doc.CreateTextNode("Demo Text");

            element1.AppendChild(element2);
            element2.AppendChild(element3);
            element3.AppendChild(text1);


            XmlElement element4 = doc.CreateElement(string.Empty, "level2", string.Empty);
            XmlText text2 = doc.CreateTextNode("other text");
            element4.AppendChild(text2);
            element2.AppendChild(element4);
            return Ok(doc);
        }

        [HttpPost, Route("Testfatoora")]
        public IActionResult Testfatoora()
        {
           // _ = MyFatoorahOperation.Program.SendPayment( );

            return Ok();
        }
    }



}
