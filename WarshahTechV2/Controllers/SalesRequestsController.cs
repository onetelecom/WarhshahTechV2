using BL.Infrastructure;
using BL.Security;
using DL.DTOs.Sales;
using DL.DTOs.SparePartsDTOs;
using DL.Entities;
using DL.Helper;
using DL.MailSales;
using DL.Migrations;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Wordprocessing;
using Helper;
using Helper.Triggers;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using WarshahTechV2.Models;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
   

    public class SalesRequestsController : ControllerBase
    {
        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;
        private readonly INotificationService _NotificationService;

        // Constractor for controller 
        public SalesRequestsController(IUnitOfWork uow, INotificationService NotificationService)
        {
            _uow = uow;
            _NotificationService = NotificationService;
        }
       [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost,Route("AddSalesRequest")]
        public IActionResult AddSalesRequest(List<SalesRequest> salesRequest)
        {
            if (ModelState.IsValid)
            {
                if (salesRequest[0].FromWarshah==false)
                {
                    //var IsThere = _uow.SalesRequestListRepository.GetMany(a => a.WarshahId == salesRequest[0].WarshahId).FirstOrDefault();
                    //if (IsThere != null)
                    //{
                    //    foreach (var item in salesRequest)
                    //    {

                    //        item.Status = SalesReqConst.Initial;
                    //        item.SalesRequestListId = IsThere.Id;
                    //        item.CreatedOn = System.DateTime.Now;

                    //        _uow.SalesRequestRepository.Add(item);
                    //        _uow.Save();
                    //    }
                    //    return Ok(salesRequest);
                    //}
                }
             
                SalesRequestList salesRequestList = new SalesRequestList();
                salesRequestList.WarshahId=salesRequest[0].WarshahId;
                salesRequestList.CreatedOn = System.DateTime.Now;
                salesRequestList.SalesId = null;
                _uow.SalesRequestListRepository.Add(salesRequestList);
                _uow.Save();
                foreach (var item in salesRequest)
                {

                    item.Status = SalesReqConst.Initial;
                    item.SalesRequestListId = salesRequestList.Id;
                    item.CreatedOn= System.DateTime.Now;
                    _uow.SalesRequestRepository.Add(item);
                    _uow.Save();
                }
               
                return Ok(salesRequest); 
            }
            return BadRequest(ModelState);
        }

       // [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        //[HttpGet, Route("GetAllSalesReqsForWarshah")]
        //public IActionResult GetAllSalesReqsForWarshah(int WarshahId , int pagenumber , int pagecount)
        //{
        //    var allData =new List<object>();

        //  var all =_uow.SalesRequestListRepository.GetMany(a=>a.WarshahId==WarshahId).ToHashSet();
        //    foreach (var item in all)
        //    {
        //        var request = _uow.SalesRequestRepository.GetMany(a => a.SalesRequestListId == item.Id).FirstOrDefault();

        //        DataMotor data = new DataMotor();

        //        if (request.MotorId != null)
        //        {
        //            var motor = _uow.MotorsRepository.GetMany(a => a.Id == request.MotorId).Include(all => all.motorMake).Include(all => all.motorModel).Include(all => all.motorColor).Include(all => all.motorYear).FirstOrDefault();


        //            data.Make = motor.motorMake.MakeNameAr;
        //            data.Model = motor.motorModel.ModelNameAr;
        //            data.Year = motor.motorYear.Year.ToString();
        //                if(motor.ChassisNo != null)
        //            {
        //                data.ChassisNo = motor.ChassisNo.ToString();
        //            }
        //            if(motor.PlateNo != null) { data.PlateNo = motor.PlateNo.ToString(); }
                    
        //            allData.Add(new
        //            {
        //                Request = item,

        //                Motor = data
                       


        //            }) ;
        //        }

        //        else
        //        {
        //            data.Make = request.Make;
        //            data.Model = request.Model;
        //            data.Year = request.Year; 

        //            allData.Add(new
        //            {
        //                Request = item,
        //                Motor = data   });

        //        }
        //    }
               

        //    return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = allData.Count(), Listinvoice = allData.ToPagedList(pagenumber, pagecount) });
        //}









        // [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpGet, Route("GetAllSalesReqsForWarshah")]
        public IActionResult GetAllSalesReqsForWarshah(int WarshahId, int pagenumber, int pagecount)
        {
            var allData = new List<object>();

            var SalesRequistList = 0;
            var SalesSerial = "";
            var RequestFrom = "";

            var all = _uow.SalesRequestListRepository.GetMany(a => a.WarshahId == WarshahId).ToHashSet();
            foreach (var item in all)
            {
                var request = _uow.SalesRequestRepository.GetMany(a => a.SalesRequestListId == item.Id).FirstOrDefault();

                if(request.ROID == null) {

                    RequestFrom = " من الورشة";
                }
                else
                {

                    RequestFrom = " من أمر الإصلاح";
                }


                allData.Add(
                    new{

                        SalesRequistList = (int)request.SalesRequestListId,
                        SalesSerial = "SalesRequest" + "--" + item.Id,
                        RequestFrom = RequestFrom

                    
                    });

            }


            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = allData.Count(), Listinvoice = allData.ToPagedList(pagenumber, pagecount) });
        }

        [HttpGet, Route("GetAllSalesReqsPartsForSalesReqs")]
        public IActionResult GetAllSalesReqsPartsForSalesReqs(int salesReqId)
        {

        

            var all = _uow.SalesRequestRepository.GetMany(a => a.SalesRequestListId == salesReqId).ToHashSet();

            return Ok(all);
        }
        [HttpGet,Route("GetAllSalesReqsByMotorID")]
        public IActionResult GetAllSalesReqsByMotorID(int MotorId,int WarshahId)
        {
            var All = _uow.SalesRequestRepository.GetMany(a => a.MotorId == MotorId&&a.WarshahId==WarshahId).ToHashSet().OrderByDescending(a => a.Id);
            return Ok(All);

        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.TaseerAdmin)]
        [HttpGet, Route("GetSalesReport")]
        public IActionResult GetSalesReport()
        {
            var Inetial = _uow.SalesRequestRepository.GetMany(a=>a.Status==1).ToHashSet().Count;
            var Offer = _uow.SalesRequestRepository.GetMany(a=>a.Status==2).ToHashSet().Count;
            var WOwnerApproved = _uow.SalesRequestRepository.GetMany(a=>a.Status==3).ToHashSet().Count;
            var Shipping = _uow.SalesRequestRepository.GetMany(a=>a.Status==4).ToHashSet().Count;
            var Shipped = _uow.SalesRequestRepository.GetMany(a=>a.Status==5).ToHashSet().Count;
            var WOwnerRejected = _uow.SalesRequestRepository.GetMany(a=>a.Status==6).ToHashSet().Count;

            
            return Ok(new {Inetial =Inetial , Offer = Offer , WOwnerApproved =WOwnerApproved,WOwnerRejected=WOwnerRejected,Shipped=Shipped,Shipping=Shipping});
        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.TaseerAdmin)]
        [HttpGet, Route("GetSalesReportForSales")]
        public IActionResult GetSalesReportForSales(int salesId)
        {
            var Inetial = _uow.SalesRequestRepository.GetMany(a => a.Status == 1&&a.SalesRequestList.SalesId==salesId).ToHashSet().Count;
            var Offer = _uow.SalesRequestRepository.GetMany(a => a.Status == 2 && a.SalesRequestList.SalesId == salesId).ToHashSet().Count;
            var WOwnerApproved = _uow.SalesRequestRepository.GetMany(a => a.Status == 3 && a.SalesRequestList.SalesId == salesId).ToHashSet().Count;
            var Shipping = _uow.SalesRequestRepository.GetMany(a => a.Status == 4 && a.SalesRequestList.SalesId == salesId).ToHashSet().Count;
            var Shipped = _uow.SalesRequestRepository.GetMany(a => a.Status == 5 && a.SalesRequestList.SalesId == salesId).ToHashSet().Count;
            var WOwnerRejected = _uow.SalesRequestRepository.GetMany(a => a.Status == 6 && a.SalesRequestList.SalesId == salesId).ToHashSet().Count;


            return Ok(new { Inetial = Inetial, Offer = Offer, WOwnerApproved = WOwnerApproved, WOwnerRejected = WOwnerRejected, Shipped = Shipped, Shipping = Shipping });
        }

  //      [ClaimRequirement(ClaimTypes.Role, RoleConstant.Sales)]
        [HttpGet, Route("GetAllSalesReqsForSales")]
        public IActionResult GetAllSalesReqsForSales()
        {
            var Response =new  List<object>();

            var All = _uow.SalesRequestListRepository.GetAll().OrderByDescending(a=>a.Id);
            foreach (var item in All)
            {
                try
                {
                    var res = new
                    {
                        List = item,
                        warsahName = _uow.WarshahRepository.GetById(item.WarshahId).WarshahNameAr,
                        SalesName = item.SalesId == null || item.SalesId == 0 ? "بدون مسئول" : _uow.UserRepository.GetById(item.SalesId).FirstName,
                        Items = _uow.SalesRequestRepository.GetMany(a => a.SalesRequestListId == item.Id).ToHashSet().OrderByDescending(a => a.Id)
                    };
                    Response.Add(res);
                }
                catch (Exception ex)
                {
                }
            }

            //return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = Response.Count(), Listinvoice = Response.ToPagedList(pagenumber, pagecount) });
            return Ok(Response);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Sales)]
        [HttpGet,Route("AssignSales")]
        public IActionResult AssignSales(int salesId,int ListId)
        {
            var List = _uow.SalesRequestListRepository.GetById(ListId);
            List.SalesId = salesId;
            _uow.SalesRequestListRepository.Update(List);
            _uow.Save();
            // Create Sales Order  طلب تسعير 
            var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == List.WarshahId & a.NameNotificationId == 21).FirstOrDefault();

            if (notificationActive != null)
            {


                _NotificationService.SetNotificationTaqnyat(salesId, "تم تعيينك لطلب تسعير جديد");

            }

            
            return Ok(List);
        }
       // [ClaimRequirement(ClaimTypes.Role, RoleConstant.Sales)]
        [HttpPost, Route("SendOffer")]
        public IActionResult SendOffer(SalesOfferDto SalesOfferDto)
        {
            if (ModelState.IsValid)
            {
                var SR = _uow.SalesRequestRepository.GetById(SalesOfferDto.SalesReqId);
                SR.Status = SalesReqConst.Offer;
                SR.Describtion = SR.Describtion + "-" + SalesOfferDto.Notes;
                SR.BuyPrice = SalesOfferDto.BuyPrice;
                SR.SparePartTaseerId = SalesOfferDto.SparePartTaseerId;
                SR.CreatedOn = SalesOfferDto.CreatedOn;
                var User =_uow.UserRepository.GetMany(a=>a.WarshahId==SR.WarshahId&&a.RoleId== 1).FirstOrDefault();
                _uow.SalesRequestRepository.Update(SR);
                _uow.Save();
                // Create Sales Order  طلب تسعير 
                var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == User.WarshahId & a.NameNotificationId == 21).FirstOrDefault();
                if (notificationActive != null)
                {


                    _NotificationService.SetNotificationTaqnyat(User.Id, "تم استلام عرض سعر لطلب التسعير الخاص بك");

                }

                return Ok(SR);
            }
            return BadRequest(ModelState);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("ApproveOffer")]
        public IActionResult ApproveOffer(int SalesReqId)
        {
            var SR = _uow.SalesRequestRepository.GetById(SalesReqId);
                SR.Status = SalesReqConst.WOwnerApproved;
                _uow.SalesRequestRepository.Update(SR);
                _uow.Save();
       

            return Ok(SR);
           
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("RejectOffer")]
        public IActionResult RejectOffer(int SalesReqId)
        {
            var SR = _uow.SalesRequestRepository.GetById(SalesReqId);
            SR.Status = SalesReqConst.WOwnerRejected;
            _uow.SalesRequestRepository.Update(SR);
            _uow.Save();
            return Ok(SR);

        }
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Sales)]
        [HttpPost, Route("Shipping")]
        public IActionResult Shipping(int SalesReqId)
        {
            var SR = _uow.SalesRequestRepository.GetById(SalesReqId);
             SR.Status = SalesReqConst.Shipping;
            _uow.SalesRequestRepository.Update(SR);
            _uow.Save();
            var User = _uow.UserRepository.GetMany(a => a.WarshahId == SR.WarshahId && a.RoleId == 1).FirstOrDefault();
            // Create Sales Order  طلب تسعير 
            var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == User.WarshahId & a.NameNotificationId == 21).FirstOrDefault();
            if (notificationActive != null)
            {
                _NotificationService.SetNotificationTaqnyat(User.Id, "جاري شحن طلب قطع الغيار الخاص بك");


            }

           
            return Ok(SR);

        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("Shipped")]
        public IActionResult Shipped(ShippedDTO ShippedDTO)
        {
            
            var SR = _uow.SalesRequestRepository.GetById(ShippedDTO.Id);
            SR.Status = SalesReqConst.Shipped;
            SR.SellPrice = ShippedDTO.SellPrice;            
            _uow.SalesRequestRepository.Update(SR);
            _uow.Save();
            var TaseerSp = _uow.SparePartTaseerRepository.GetById(SR.SparePartTaseerId);
            var WasrhahPart = _uow.SparePartRepository.GetMany(a => a.WarshahId == SR.WarshahId && a.PartNum== TaseerSp.PartNum).FirstOrDefault();
            var SparePart = new DL.Entities.SparePart();
            SparePart.WarshahId = SR.WarshahId;
            SparePart.Quantity = SR.QTY;
            
            SparePart.BuyingPrice = SR.BuyPrice;
            if (SR.SellPrice == 0)
            {
                decimal margin = (((decimal)ShippedDTO.MarginPercent) / (100));
                decimal addearn = SparePart.BuyingPrice * margin;
                SparePart.MarginPercent = ShippedDTO.MarginPercent;
                SparePart.PeacePrice = SparePart.BuyingPrice + addearn;
            }
            else
            {
                SparePart.PeacePrice = SR.SellPrice;
            }
           
            SparePart.SparePartName = TaseerSp.SparePartName;
            SparePart.CategorySparePartsId = TaseerSp.CategorySparePartsId;
            SparePart.SubCategoryPartsId = TaseerSp.SubCategoryPartsId;
            SparePart.MotorMakeId = TaseerSp.MotorMakeId;
            SparePart.MotorModelId = TaseerSp.MotorModelId;
            SparePart.MotorYearId = TaseerSp.MotorYearId;
            SparePart.PartDescribtion = TaseerSp.PartDescribtion;
            SparePart.PartNum = TaseerSp.PartNum;
            
            SparePart.IsDeleted = false;
            SparePart.CreatedOn = DateTime.Now;
            if (WasrhahPart!=null)
            {
                if (TaseerSp.PeacePrice==WasrhahPart.BuyingPrice & ShippedDTO.MarginPercent == 0 )
                {
                      WasrhahPart.Quantity += SR.QTY;
                      WasrhahPart.PeacePrice = ShippedDTO.SellPrice;
                      WasrhahPart.MarginPercent = 0;


                    decimal vp1 = 0;
                    var VAT1 = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == WasrhahPart.WarshahId).FirstOrDefault();
                    if (VAT1 == null)
                    {

                        var vAT = 0;
                        vp1 = (((decimal)vAT) / (100)) + 1;

                    }
                    else
                    {
                        vp1 = (((decimal)VAT1.GetVAT) / (100)) + 1;
                    }
                    
                      WasrhahPart.BuyBeforeVat = WasrhahPart.BuyingPrice / vp1;
                      WasrhahPart.VatBuy = WasrhahPart.BuyingPrice - WasrhahPart.BuyBeforeVat;
                      WasrhahPart.SellBeforeVat = WasrhahPart.PeacePrice / vp1;
                      WasrhahPart.VatSell = WasrhahPart.PeacePrice - WasrhahPart.SellBeforeVat;
                      _uow.SparePartRepository.Update(WasrhahPart);
                    TaseerSp.Quantity -= SR.QTY;
                    _uow.SparePartTaseerRepository.Update(TaseerSp);
                    _uow.Save();
                      return Ok(WasrhahPart);
                }

                if (TaseerSp.PeacePrice == WasrhahPart.BuyingPrice & ShippedDTO.MarginPercent != 0)
                    {
                        WasrhahPart.Quantity += SR.QTY;
                        WasrhahPart.MarginPercent = ShippedDTO.MarginPercent;
                        WasrhahPart.PeacePrice= SparePart.PeacePrice;
                    decimal VP2 = 0;
                    var VAT2 = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == WasrhahPart.WarshahId).FirstOrDefault();
                    if (VAT2 == null)
                    {

                        var vAT = 0;
                        VP2 = (((decimal)vAT) / (100)) + 1;

                    }
                    else
                    {
                        VP2 = (((decimal)VAT2.GetVAT) / (100)) + 1;
                    }
                    WasrhahPart.BuyBeforeVat = WasrhahPart.BuyingPrice / VP2;
                        WasrhahPart.VatBuy = WasrhahPart.BuyingPrice - WasrhahPart.BuyBeforeVat;
                        WasrhahPart.SellBeforeVat = WasrhahPart.PeacePrice / VP2;
                        WasrhahPart.VatSell = WasrhahPart.PeacePrice - WasrhahPart.SellBeforeVat;
                        _uow.SparePartRepository.Update(WasrhahPart);
                    TaseerSp.Quantity -= SR.QTY;
                    _uow.SparePartTaseerRepository.Update(TaseerSp);
                    _uow.Save();
                        return Ok(WasrhahPart);
                    }
               

            }
            decimal VP = 0;
            var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == SR.WarshahId).FirstOrDefault();
            if (VAT == null)
            {

                var vAT = 0;
                VP = (((decimal)vAT) / (100)) + 1;

            }
            else
            {
                VP = (((decimal)VAT.GetVAT) / (100)) + 1;
            }
            SparePart.BuyBeforeVat = SparePart.BuyingPrice / VP;
            SparePart.VatBuy = SparePart.BuyingPrice - SparePart.BuyBeforeVat;
            SparePart.SellBeforeVat = SparePart.PeacePrice / VP;
            SparePart.VatSell = SparePart.PeacePrice - SparePart.SellBeforeVat;
            _uow.SparePartRepository.Add(SparePart);
            TaseerSp.Quantity -= SR.QTY;
            _uow.SparePartTaseerRepository.Update(TaseerSp);
            _uow.Save();
            return Ok(SparePart);

         }



        // Send Email to Sales

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("SendEmail")]
        public void SendEmail(List<SalesOrderDTO> sparePartsDTOs)
        {

            foreach (var  sparePart in sparePartsDTOs)
            {

                MailSender sender = new MailSender();

                //string message =
                //    "بيانات طلب تسعير جديد : "
                //           + "\n  القسم الرئيسى " + sparePart.CategorySparePartsId
                //           + "\n  القسم الفرعى :  " + sparePart.SubCategoryPartsId
                //           + "\n  قطعة الغيار :  " + sparePart.SparePartName
                //           + "\n  رقم القطعة  :  " + sparePart.PartNum
                //           + "\n  نوع المركبة  :  " + sparePart.MotorMakeId
                //           + "\n  موديل المركبة  :  " + sparePart.MotorModelId
                //           + "\n  سنة الصنع :  " + sparePart.MotorYearId
                //           + "\n  الكمية  :  " + sparePart.Quantity
                //           + "\n  ملاحظات  :  " + sparePart.PartDescribtion
                //           + "\n  سعر الشراء  :  ";


                string FilePath = Directory.GetCurrentDirectory() + @"\wwwroot\EmailTemps\EmailSales.html";

                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("{categorySparePartsId}", sparePart.CategorySparePartsId).Replace("{subCategoryPartsId}",
                    sparePart.SubCategoryPartsId).Replace(" { partName } ",
                    sparePart.SparePartName) 
                   .Replace("{ partNumber }", sparePart.PartNum).Replace("{ carModel }  " , sparePart.MotorMakeId)
                   .Replace("{ carModel }", sparePart.MotorModelId).Replace("{ warshahId } ",sparePart.WarshahId)
                   .Replace("{ qty }" , sparePart.Quantity).Replace("{ describtion }", sparePart.PartDescribtion).Replace
                   ("{ Date }" , DateTime.Now.ToString());
                var email = "ruhwaradm@gmail.com";
                var email2 = "ruhwartec@gmail.com";
                var email3 = "engsalahahmed27@gmail.com";

                bool isSent = sender.SendMail(email, "ورشة تك Warshah Tech ", MailText, true);
                bool isSent2 = sender.SendMail(email2, "ورشة تك Warshah Tech ", MailText, true);
                bool isSent3 = sender.SendMail(email3, "ورشة تك Warshah Tech ", MailText, true);

            }
        }
       
    }
}
