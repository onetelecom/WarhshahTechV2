using AutoMapper;
using BL.Infrastructure;
using BL.Security;
using DL.DTOs.InvoiceDTOs;
using DL.DTOs.JobCardDtos;
using DL.Entities;
using DL.Enums;
using DL.Helper;
using DL.MailModels;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Helper;
using HELPER;
using Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Claims;
using System.Threading.Tasks;
using WarshahTechV2.Models;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILog _log;

        private readonly ISMS _SMS;
        private readonly MailSettings _mailSettings;





        private readonly INotificationService _NotificationService;


        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMailService _mailService;
        public OrdersController(INotificationService NotificationService, IHostingEnvironment _hostingEnvironment, ISMS SMS, IMailService mailService, IMapper mapper,
          IUnitOfWork uow
          )
        {
            _SMS = SMS;

            _uow = uow;
            this._hostingEnvironment = _hostingEnvironment;
            _NotificationService = NotificationService;
            _mapper = mapper;
            _mailService = mailService;


        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Recicp)]

        [HttpGet, Route("GetAllReciptionOrders")]
        public IActionResult GetAllReciptionOrders(int warshahId, int pagenumber, int pagecount)
        {
            var rec = _uow.ReciptionOrderRepository.GetMany(a => a.warshahId == warshahId).Include(a => a.Reciption).Include(a => a.Tecnican).Include(a => a.MotorId).Include(a => a.CarOwner).ToHashSet().OrderByDescending(a => a.Id);

            return Ok(rec.ToPagedList(pagenumber, pagecount));

        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetAllOrdersByOwner")]
        public IActionResult GetAllOrdersByOwner(int OwnerId, int pagenumber, int pagecount)
        {
            var allOrders = _uow.RepairOrderRepository.GetMany(a => a.ReciptionOrder.CarOwnerId == OwnerId).Include(a => a.ReciptionOrder.MotorId.motorMake).Include(a => a.ReciptionOrder.Tecnican).Include(a => a.ReciptionOrder.MotorId.motorModel).ToHashSet().OrderByDescending(a => a.Id);
            List<AllOrdersDTO> list = new List<AllOrdersDTO>();
            foreach (var item in allOrders)
            {
                var RO = new AllOrdersDTO
                {

                    TechName = item.ReciptionOrder.Tecnican.FirstName + item.ReciptionOrder.Tecnican.LastName,
                    Id = item.Id,
                    Status = item.RepairOrderStatus,
                    Car = item.ReciptionOrder.MotorId.motorMake.MakeNameAr + " " + item.ReciptionOrder.MotorId.motorModel.ModelNameAr + " " + item.ReciptionOrder.MotorId.PlateNo
                };
                list.Add(RO);

            }


            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = list.Count(), Listinvoice = list.ToPagedList(pagenumber, pagecount) });

        }
        [HttpGet, Route("GetAllinvoiceByOwner")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetAllinvoiceByOwner(int OwnerId, int pagenumber, int pagecount)
        {
            var Invocies = _uow.InvoiceRepository.GetMany(a => a.CarOwnerID == OwnerId.ToString()).ToHashSet().OrderByDescending(a => a.Id);

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = Invocies.Count(), Listinvoice = Invocies.ToPagedList(pagenumber, pagecount) });


        }



        [HttpGet, Route("GetAllRepairOrders")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetAllRepairOrders(int warshahId, int pagenumber, int pagecount)
        {
            var allOrders = _uow.RepairOrderRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet().OrderByDescending(a => a.Id).OrderByDescending(a => a.Id);

            //var salesrequestorder = _uow.SalesRequestRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet().OrderByDescending(a => a.Id);


            List<ViewRepairOrderDTO> list = new List<ViewRepairOrderDTO>();
            foreach (var item in allOrders)
            {
                var r = _uow.ReciptionOrderRepository.GetMany(a => a.Id == item.ReciptionOrderId || item.ReciptionOrderId == null).Include(a => a.MotorId).Include(a => a.MotorId.motorMake).Include(a => a.MotorId.motorModel).Include(a => a.CarOwner).FirstOrDefault();
                var salesorder = _uow.SalesRequestRepository.GetMany(a => a.ROID == item.Id).FirstOrDefault();

                var CarOwnername = "";

                if (r.CarOwner.IsCompany == true)
                {
                    CarOwnername = r.CarOwner.CompanyName + " " + r.CarOwner.Phone;
                }

                else
                {
                    CarOwnername = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone;
                }

                if (salesorder != null)
                {
                    var RO = new ViewRepairOrderDTO
                    {
                        TechId = item.TechId,
                        Id = item.Id,
                        CarOwner = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone,
                        Car = r.MotorId.motorMake.MakeNameAr + " " + r.MotorId.motorModel.ModelNameAr + " " + r.MotorId.PlateNo,
                        SalesRequestStatus = salesorder.Status,
                        Status = item.RepairOrderStatus.ToString()

                    };
                    list.Add(RO);
                }

                else
                {
                    var RO = new ViewRepairOrderDTO
                    {
                        TechId = item.TechId,
                        Id = item.Id,

                        CarOwner = CarOwnername,
                        Car = r.MotorId.motorMake.MakeNameAr + " " + r.MotorId.motorModel.ModelNameAr + " " + r.MotorId.PlateNo,
                        Status = item.RepairOrderStatus.ToString()


                    };
                    list.Add(RO);
                }


            }

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = allOrders.Count(), Listorders = list.ToPagedList(pagenumber, pagecount) });

        }





        [HttpGet, Route("GetClosedRepairOrders")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetClosedRepairOrders(int warshahId, int pagenumber, int pagecount)
        {
            var allOrders = _uow.RepairOrderRepository.GetMany(a => a.WarshahId == warshahId && a.RepairOrderStatus == 7).ToHashSet().OrderByDescending(a => a.Id);

            //var salesrequestorder = _uow.SalesRequestRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet().OrderByDescending(a => a.Id);




            List<ViewRepairOrderDTO> list = new List<ViewRepairOrderDTO>();
            foreach (var item in allOrders)
            {

                var getTechName = _uow.UserRepository.GetById(item.TechId);

                var r = _uow.ReciptionOrderRepository.GetMany(a => a.Id == item.ReciptionOrderId || item.ReciptionOrderId == null).Include(a => a.MotorId).Include(a => a.MotorId.motorMake).Include(a => a.MotorId.motorModel).Include(a => a.CarOwner).FirstOrDefault();
                var salesorder = _uow.SalesRequestRepository.GetMany(a => a.ROID == item.Id).FirstOrDefault();

                var CarOwnername = "";

                if (r.CarOwner.IsCompany == true)
                {
                    CarOwnername = r.CarOwner.CompanyName + " " + r.CarOwner.Phone;
                }

                else
                {
                    CarOwnername = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone;
                }

                if (salesorder != null)
                {
                    var RO = new ViewRepairOrderDTO
                    {
                        TechId = item.TechId,
                        TechName = getTechName.FirstName + " " + getTechName.LastName,
                        Id = item.Id,
                        CarOwner = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone,
                        Car = r.MotorId.motorMake.MakeNameAr + " " + r.MotorId.motorModel.ModelNameAr + " " + r.MotorId.PlateNo,
                        SalesRequestStatus = salesorder.Status

                    };
                    list.Add(RO);
                }

                else
                {
                    var RO = new ViewRepairOrderDTO
                    {
                        TechId = item.TechId,
                        TechName = getTechName.FirstName + " " + getTechName.LastName,

                        Id = item.Id,
                        CarOwner = CarOwnername,
                        Car = r.MotorId.motorMake.MakeNameAr + " " + r.MotorId.motorModel.ModelNameAr + " " + r.MotorId.PlateNo,


                    };
                    list.Add(RO);
                }


            }

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = allOrders.Count(), Listinvoice = list.ToPagedList(pagenumber, pagecount) });

        }




        [HttpGet, Route("GetRejectRepairOrders")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetRejectRepairOrders(int warshahId, int pagenumber, int pagecount)
        {
            var allOrders = _uow.RepairOrderRepository.GetMany(a => a.WarshahId == warshahId && a.RepairOrderStatus == 9).ToHashSet().OrderByDescending(a => a.Id);

            //var salesrequestorder = _uow.SalesRequestRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet().OrderByDescending(a => a.Id);




            List<ViewRepairOrderDTO> list = new List<ViewRepairOrderDTO>();
            foreach (var item in allOrders)
            {

                var getTechName = _uow.UserRepository.GetById(item.TechId);

                var r = _uow.ReciptionOrderRepository.GetMany(a => a.Id == item.ReciptionOrderId || item.ReciptionOrderId == null).Include(a => a.MotorId).Include(a => a.MotorId.motorMake).Include(a => a.MotorId.motorModel).Include(a => a.CarOwner).FirstOrDefault();
                var salesorder = _uow.SalesRequestRepository.GetMany(a => a.ROID == item.Id).FirstOrDefault();

                var CarOwnername = "";

                if (r.CarOwner.IsCompany == true)
                {
                    CarOwnername = r.CarOwner.CompanyName + " " + r.CarOwner.Phone;
                }

                else
                {
                    CarOwnername = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone;
                }

                if (salesorder != null)
                {
                    var RO = new ViewRepairOrderDTO
                    {
                        TechId = item.TechId,

                        TechName = getTechName.FirstName + " " + getTechName.LastName,
                        Id = item.Id,
                        CarOwner = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone,
                        Car = r.MotorId.motorMake.MakeNameAr + " " + r.MotorId.motorModel.ModelNameAr + " " + r.MotorId.PlateNo,
                        SalesRequestStatus = salesorder.Status

                    };
                    list.Add(RO);
                }

                else
                {
                    var RO = new ViewRepairOrderDTO
                    {
                        TechId = item.TechId,
                        TechName = getTechName.FirstName + " " + getTechName.LastName,

                        Id = item.Id,
                        CarOwner = CarOwnername,
                        Car = r.MotorId.motorMake.MakeNameAr + " " + r.MotorId.motorModel.ModelNameAr + " " + r.MotorId.PlateNo,


                    };
                    list.Add(RO);
                }


            }

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = allOrders.Count(), Listinvoice = list.ToPagedList(pagenumber, pagecount) });


        }






        [HttpGet, Route("GetReadyCarRepairOrders")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetReadyCarRepairOrders(int warshahId, int pagenumber, int pagecount)
        {
            var allOrders = _uow.RepairOrderRepository.GetMany(a => a.WarshahId == warshahId && a.RepairOrderStatus == 7).ToHashSet().OrderByDescending(a => a.Id);

            //var salesrequestorder = _uow.SalesRequestRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet().OrderByDescending(a => a.Id);
            List<ViewRepairOrderDTO> list = new List<ViewRepairOrderDTO>();
            foreach (var item in allOrders)
            {
                var inv = _uow.InvoiceRepository.GetMany(i => i.RepairOrderId == item.Id).FirstOrDefault();

                if (inv == null || (inv != null && (inv.InvoiceStatusId == 1 || inv.InvoiceStatusId == 4)))
                {
                    var r = _uow.ReciptionOrderRepository.GetMany(a => a.Id == item.ReciptionOrderId || item.ReciptionOrderId == null).Include(a => a.MotorId).Include(a => a.MotorId.motorMake).Include(a => a.MotorId.motorModel).Include(a => a.CarOwner).FirstOrDefault();
                    var salesorder = _uow.SalesRequestRepository.GetMany(a => a.ROID == item.Id).FirstOrDefault();

                    var CarOwnername = "";

                    if (r.CarOwner.IsCompany == true)
                    {
                        CarOwnername = r.CarOwner.CompanyName + " " + r.CarOwner.Phone;
                    }

                    else
                    {
                        CarOwnername = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone;
                    }

                    if (salesorder != null)
                    {
                        var RO = new ViewRepairOrderDTO
                        {
                            TechId = item.TechId,
                            Id = item.Id,
                            CarOwner = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone,
                            Car = r.MotorId.motorMake.MakeNameAr + " " + r.MotorId.motorModel.ModelNameAr + " " + r.MotorId.PlateNo,
                            SalesRequestStatus = salesorder.Status

                        };
                        list.Add(RO);
                    }

                    else
                    {
                        var RO = new ViewRepairOrderDTO
                        {
                            TechId = item.TechId,
                            Id = item.Id,
                            CarOwner = CarOwnername,
                            Car = r.MotorId.motorMake.MakeNameAr + " " + r.MotorId.motorModel.ModelNameAr + " " + r.MotorId.PlateNo,


                        };
                        list.Add(RO);
                    }


                }


            }

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = list.Count(), Listinvoice = list.ToPagedList(pagenumber, pagecount) });

        }






        [HttpGet, Route("GetNotClosedRepairOrders")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetNotClosedRepairOrders(int warshahId, int pagenumber, int pagecount)
        {
            var allOrders = _uow.RepairOrderRepository.GetMany(a => a.WarshahId == warshahId && a.RepairOrderStatus < 7).ToHashSet().OrderByDescending(a => a.Id);

            //var salesrequestorder = _uow.SalesRequestRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet().OrderByDescending(a => a.Id);


            List<ViewRepairOrderDTO> list = new List<ViewRepairOrderDTO>();
            foreach (var item in allOrders)
            {
               
                    var getTechName = _uow.UserRepository.GetById(item.TechId);


                if (item.ReciptionOrderId != null)
                {
                    var r = _uow.ReciptionOrderRepository.GetMany(a => a.Id == item.ReciptionOrderId).Include(a => a.MotorId).Include(a => a.MotorId.motorMake).Include(a => a.MotorId.motorModel).Include(a => a.CarOwner).FirstOrDefault();
                    var salesorder = _uow.SalesRequestRepository.GetMany(a => a.ROID == item.Id).FirstOrDefault();

                    var CarOwnername = "";

                    if (r.CarOwner.IsCompany == true)
                    {
                        CarOwnername = r.CarOwner.CompanyName + " " + r.CarOwner.Phone;
                    }

                    else
                    {
                        CarOwnername = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone;
                    }

                    if (salesorder != null)
                    {
                        var RO = new ViewRepairOrderDTO
                        {
                            TechId = item.TechId,
                            TechName = getTechName.FirstName + " " + getTechName.LastName,
                            Id = item.Id,
                            CarOwner = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone,
                            Car = r.MotorId.motorMake.MakeNameAr + " " + r.MotorId.motorModel.ModelNameAr + " " + r.MotorId.PlateNo,
                            SalesRequestStatus = salesorder.Status

                        };
                        list.Add(RO);
                    }

                    else
                    {
                        var RO = new ViewRepairOrderDTO
                        {
                            TechId = item.TechId,
                            TechName = getTechName.FirstName + " " + getTechName.LastName,

                            Id = item.Id,
                            CarOwner = CarOwnername,
                            Car = r.MotorId.motorMake.MakeNameAr + " " + r.MotorId.motorModel.ModelNameAr + " " + r.MotorId.PlateNo,


                        };
                        list.Add(RO);
                    }

                }

                else
                {
                    var r = _uow.InspectionWarshahReportRepository.GetMany(r => r.WarshahId == warshahId).Include(a => a.CarOwner).Include(a => a.Motors).FirstOrDefault();
                    var salesorder = _uow.SalesRequestRepository.GetMany(a => a.ROID == item.Id).FirstOrDefault();

                    var CarOwnername = "";

                    if (r.CarOwner.IsCompany == true)
                    {
                        CarOwnername = r.CarOwner.CompanyName + " " + r.CarOwner.Phone;
                    }

                    else
                    {
                        CarOwnername = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone;
                    }

                    if (salesorder != null)
                    {
                        var RO = new ViewRepairOrderDTO
                        {
                            TechId = item.TechId,
                            TechName = getTechName.FirstName + " " + getTechName.LastName,
                            Id = item.Id,
                            CarOwner = r.CarOwner.FirstName == null ? " " : r.CarOwner.FirstName + " " + r.CarOwner.Phone,
                            SalesRequestStatus = salesorder.Status

                        };
                        list.Add(RO);
                    }

                    else
                    {
                        var RO = new ViewRepairOrderDTO
                        {
                            TechId = item.TechId,
                            TechName = getTechName.FirstName + " " + getTechName.LastName,

                            Id = item.Id,
                            CarOwner = CarOwnername,


                        };
                        list.Add(RO);
                    }

                }

            }

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = list.Count(), Listinvoice = list.ToPagedList(pagenumber, pagecount) });

        }






        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("AddReciptionOrder")]
        public IActionResult AddReciptionOrder(ReciptionOrderDTO reciptionOrderDTO)
        {
            try
            {
                var Data = _mapper.Map<DL.Entities.ReciptionOrder>(reciptionOrderDTO);

                Data.CreatedOn = DateTime.Now;
                Data.CreatedBy = Data.ReciptionId;
                // status from enum receptionOrder  1 = open
                Data.StatusId = 1;
                _uow.ReciptionOrderRepository.Add(Data);
                _uow.Save();
                var RepairOrder = new DL.Entities.RepairOrder();
                RepairOrder.CreatedOn = DateTime.Now;
                RepairOrder.CreatedBy = reciptionOrderDTO.ReciptionId;

                RepairOrder.Deiscount = 0;
                RepairOrder.FixingPrice = 0;
                RepairOrder.Garuntee = "";
                RepairOrder.KMOut = 0;
                RepairOrder.BeforeDiscount = 0;
                RepairOrder.AfterDiscount = 0;
                RepairOrder.VatMoney = 0;
                RepairOrder.Total = 0;
                RepairOrder.IsDeleted = false;
                // status from enum RepairOrder  1 = wating
                RepairOrder.RepairOrderStatus = 1;
                RepairOrder.ReciptionOrderId = Data.Id;
                RepairOrder.TechId = Data.TecnicanId;
                RepairOrder.TechReview = "";
                RepairOrder.WarshahId = reciptionOrderDTO.warshahId;
                _uow.RepairOrderRepository.Add(RepairOrder);
                _uow.Save();

                // Reciption Order أمر استقبال

                var warshahphone = _uow.UserRepository.GetMany(a => a.WarshahId == reciptionOrderDTO.warshahId && a.RoleId == 1).FirstOrDefault().Phone;
                var wphone = warshahphone.Substring(4);
                var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == RepairOrder.WarshahId & a.NameNotificationId == 6 & a.StatusNotificationId == 1).FirstOrDefault();





                //var warshah = _uow.WarshahRepository.GetById(RepairOrder.WarshahId);
                //string messge2 = "تم استقبال سيارتك في ورشة " + warshah.WarshahNameAr
                //+ "\nجوال" + wphone;


                //string messge3 = "تم استقبال سياره جديده وتعينك فني عليها " + warshah.WarshahNameAr;

                //if(warshah.Id == 53)
                //{
                //    Taqnyat.Taqnyat taqnyt = new Taqnyat.Taqnyat();
                //    string bearer = "8a6bd5813cb919536bfded6403f68d14";
                //    string body = "رسالة من صلاح ";
                //    //string recipients = "966582240552";
                //    string recipients = "966507510510";
                //    string sender = "WARSHAHTECH";


                //    var message = _SMS.SendSMS1(bearer, recipients, sender, messge2);
                //}





                if (notificationActive != null)
                {

                    var warshah = _uow.WarshahRepository.GetById(RepairOrder.WarshahId);
                    string messge2 = "تم استقبال سيارتك في ورشة " + warshah.WarshahNameAr
                    + "\nجوال" + wphone;


                    string messge3 = "تم استقبال سياره جديده وتعينك فني عليها " + warshah.WarshahNameAr;

                    //Taqnyat.Taqnyat taqnyt = new Taqnyat.Taqnyat();
                    //string bearer = "8a6bd5813cb919536bfded6403f68d14";
                    //string body = "رسالة من صلاح ";
                    //string recipients = "966582240552";
                    //string sender = "WARSHAHTECH";

                    var carowner = _uow.UserRepository.GetById(reciptionOrderDTO.CarOwnerId);
                    var tech = _uow.UserRepository.GetById(RepairOrder.TechId);

                    //var sms1 = _SMS.SendSMS1(bearer, tech.Phone, sender, messge3);
                    //var sms2 = _SMS.SendSMS1(bearer, carowner.Phone, sender, messge2);

                    _NotificationService.SetNotificationTaqnyat(RepairOrder.TechId, messge3);
                    _NotificationService.SetNotificationTaqnyat(reciptionOrderDTO.CarOwnerId, messge2);

                }




                return Ok(Data);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }


        }





        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpPost, Route("CreateRepairOrderFromInspection")]
        public IActionResult CreateRepairOrderFromInspection(int? ReportInspectionId)
        {
            try
            {

                var Report = _uow.InspectionWarshahReportRepository.GetById(ReportInspectionId);

                var ItemsChecked = _uow.InspectionReportRepository.GetMany(r => r.InspectionWarshahReportId == ReportInspectionId).ToHashSet().OrderByDescending(a => a.Id);

                //var Data = _mapper.Map<DL.Entities.ReciptionOrder>(reciptionOrderDTO);
                //Data.CreatedOn = DateTime.Now;
                //Data.CreatedBy = Data.ReciptionId;
                //// status from enum receptionOrder  1 = open
                //Data.StatusId = 1;
                //_uow.ReciptionOrderRepository.Add(Data);
                //_uow.Save();
                var RepairOrder = new DL.Entities.RepairOrder();
                RepairOrder.CreatedOn = DateTime.Now;
                RepairOrder.CreatedBy = Report.TechnicalID;
                RepairOrder.InspectionTemplateId = Report.TemplateId;
                RepairOrder.Deiscount = 0;
                RepairOrder.FixingPrice = 0;
                RepairOrder.Garuntee = "";
                RepairOrder.KMOut = 0;
                RepairOrder.BeforeDiscount = 0;
                RepairOrder.AfterDiscount = 0;
                RepairOrder.VatMoney = 0;
                RepairOrder.Total = 0;
                RepairOrder.IsDeleted = false;
                // status from enum RepairOrder  1 = wating
                RepairOrder.RepairOrderStatus = 1;
                //RepairOrder.ReciptionOrderId = Data.Id;
                RepairOrder.TechId = Report.TechnicalID;
                RepairOrder.TechReview = "";
                RepairOrder.WarshahId = Report.WarshahId;
                RepairOrder.InspectionWarshahReportId = ReportInspectionId;
                _uow.RepairOrderRepository.Add(RepairOrder);
                _uow.Save();
                // Change from Inspection to Repair تحويل أمر فحص لأمر اصلاح

                var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == RepairOrder.WarshahId & a.NameNotificationId == 7 & a.StatusNotificationId == 1).FirstOrDefault();

                if (notificationActive != null)
                {

                    _NotificationService.SetNotificationTaqnyat(RepairOrder.TechId, "تم تحويل طلب الفحص الى اصلاح وتحويله لك ");
                    _NotificationService.SetNotificationTaqnyat(Report.CarOwnerId, "تم تحويل طلب الفحص الخاص بكم الى امر اصلاح ");
                }
                return Ok(ItemsChecked);

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


        }


        [HttpGet, Route("ApproveOrder")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult ApproveOrder(int ROID)
        {
            var Order = _uow.RepairOrderRepository.GetById(ROID);
            Order.RepairOrderStatus = 4;
            _uow.RepairOrderRepository.Update(Order);
            _uow.Save();
            return Ok(Order);
        }
        [HttpGet, Route("DeclineOrder")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult DeclineOrder(int ROID)
        {
            var Order = _uow.RepairOrderRepository.GetById(ROID);
            Order.RepairOrderStatus = 10;
            _uow.RepairOrderRepository.Update(Order);
            _uow.Save();
            return Ok(Order);
        }


        [HttpGet, Route("GetRepairOrder")]
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult GetRepairOrder(int id)
        {
            var CurrentRepairOrder = _uow.RepairOrderRepository.GetMany(a => a.Id == id).FirstOrDefault();
            HashSet<InspectionReport> items = new HashSet<InspectionReport>();

            List<DL.Entities.InspectionReport> result = new List<DL.Entities.InspectionReport>();


            if (CurrentRepairOrder.ReciptionOrderId == null)
            {
                var ItemsChecked = _uow.InspectionReportRepository
                    .GetMany(r => r.InspectionWarshahReportId == CurrentRepairOrder.InspectionWarshahReportId)
                    .OrderByDescending(a => a.Id).ToHashSet();

                foreach (var record in ItemsChecked)
                {
                    var LastItemArabic = record.ItemNameAr;
                    var LastItemEnglish = record.ItemNameEn;


                    if (result.Count != 0)
                    {

                        var re = result.LastOrDefault();

                        if (LastItemArabic != re.ItemNameAr && LastItemEnglish != re.ItemNameEn)
                        {
                            result.Add(record);
                        }

                        else
                        {

                            result.Remove(re);
                            result.Add(record);

                        }

                    }
                    else
                    {
                        result.Add(record);
                    }


                }



                CurrentRepairOrder = _uow.RepairOrderRepository.GetMany(a => a.Id == id).Include(a => a.Tech).Include(a => a.InspectionWarshahReport)
                    .Include(a => a.InspectionWarshahReport.CarOwner)
                    .Include(a => a.ReciptionOrder.MotorId.motorModel)

                                                          .Include(a => a.InspectionWarshahReport.Motors)

                                        .Include(a => a.InspectionWarshahReport.Motors.motorModel)

                    .Include(a => a.InspectionWarshahReport.Motors.motorModel.MotorMake)
                                        .Include(a => a.InspectionWarshahReport.Motors.motorYear)
                                                                                .Include(a => a.InspectionWarshahReport.Motors.motorColor)

                                                                                .Include(a => a.InspectionWarshahReport.Motors.motorMake)


                    .FirstOrDefault();

                items = ItemsChecked;
            }
            else
            {
                CurrentRepairOrder = _uow.RepairOrderRepository.GetMany(a => a.Id == id).Include(a => a.Tech).Include(a => a.ReciptionOrder)

                    .Include(a => a.ReciptionOrder.CarOwner)
                    .Include(a => a.ReciptionOrder.MotorId.motorMake)
                          .Include(a => a.ReciptionOrder.MotorId.motorColor)
                          .Include(a => a.ReciptionOrder.MotorId.motorYear)
                            .Include(a => a.ReciptionOrder.MotorId.motorModel)

                    .Include(a => a.InspectionWarshahReport.Motors)
                    .Include(a => a.InspectionWarshahReport.Motors.motorModel)
                      .Include(a => a.InspectionWarshahReport.Motors.motorModel.MotorMake)
                      .Include(a => a.InspectionWarshahReport.Motors.motorYear)
                       .Include(a => a.InspectionWarshahReport.Motors.motorColor)
                    .Include(a => a.ReciptionOrder.Reciption).FirstOrDefault();

            }


            var Warshah = _uow.WarshahRepository.GetById(CurrentRepairOrder.WarshahId);
            var ROSpareParts = _uow.RepairOrderSparePartRepository.GetMany(a => a.RepairOrderId == CurrentRepairOrder.Id).Include(a => a.Tech).Include(a => a.SparePart).ToHashSet().OrderByDescending(a => a.Id);
            var OrderServices = _uow.RepairOrderServicesRepository.GetMany(a => a.OrderId == CurrentRepairOrder.Id).ToHashSet();


            return Ok(new { Ro = CurrentRepairOrder, Warshah = Warshah, ROSpareParts = ROSpareParts, ItemsChecked = result, OrderServices = OrderServices });
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]
        [HttpGet, Route("GetPartsWithRepairOrderID")]
        public IActionResult GetPartsWithRepairOrderID(int id)
        {
            var Parts = _uow.RepairOrderSparePartRepository.GetMany(p => p.RepairOrderId == id).Include(t => t.SparePart).Include(t => t.Tech).ToHashSet().OrderByDescending(a => a.Id);
            return Ok(Parts);
        }

        [HttpGet, Route("CheckPersonDiscount")]

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        public IActionResult CheckPersonDiscount(int CarOwnerId)
        {
            var Discount = _uow.PersonDiscountRepository.GetMany(a => a.CarOwnerId == CarOwnerId).FirstOrDefault();
            return Ok(Discount);
        }

        [HttpGet, Route("RoHistory")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        public IActionResult RoHistory(int ROID)
        {
            var AllHistory = _uow.RoHistoryRepository.GetMany(a => a.RepairOrderId == ROID).ToHashSet().OrderByDescending(a => a.Id);
            return Ok(AllHistory);
        }
        [HttpPost, Route("UpdateRepairOrder")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        public IActionResult UpdateRepairOrder(EditRepairOrderDTO repairOrder)
        {
            var order = _mapper.Map<DL.Entities.RepairOrder>(repairOrder);

            var rorder = _uow.RepairOrderRepository.Get(t => t.Id == repairOrder.Id);

            var receptionOrder = _uow.ReciptionOrderRepository.Get(r => r.Id == rorder.ReciptionOrderId);

            var VatPercent = _uow.WarshahVatRepository.GetMany(a => a.WarshahId == rorder.WarshahId).FirstOrDefault();


            var Report = _uow.InspectionWarshahReportRepository.GetMany(r => r.Id == rorder.InspectionWarshahReportId).FirstOrDefault();

            // status from enum ReceptionOrder  2 = Closed
            if (receptionOrder != null)
            {
                var PersonDiscount = _uow.PersonDiscountRepository.GetMany(a => a.CarOwnerId == receptionOrder.CarOwnerId).FirstOrDefault();
                receptionOrder.StatusId = 2;
                _uow.ReciptionOrderRepository.Update(receptionOrder);
                order.IsDeleted = false;
                order.WarshahId = receptionOrder.warshahId;
                order.CreatedBy = receptionOrder.ReciptionId;
                order.UpdatedBy = receptionOrder.TecnicanId;
                order.TechId = receptionOrder.TecnicanId;

                order.InspectionWarshahReportId = null;
                // calculate TotalPrice for parts using in repairorder
                var Parts = _uow.RepairOrderSparePartRepository.GetMany(p => p.RepairOrderId == order.Id);
                decimal TotalPrice = 0;
                decimal fixparts = 0;
                if (Parts != null)
                {
                    foreach (var part in Parts)
                    {

                        var Qty = part.Quantity;
                        var PeacePrice = part.PeacePrice;
                        var Cost = Qty * PeacePrice;
                        fixparts += part.FixPrice;
                        TotalPrice += Cost;
                    }

                }
                order.SparePartsTotal = TotalPrice;
                decimal TotalDiscount = 0;
                decimal AfterFixing = 0;
                decimal AfterParts = 0;
                decimal BeforeDiscFixing = 0;
                decimal BeforeDiscParts = 0;

                decimal TotolVat = 0;
                decimal AfterFixingVat = 0;
                decimal AfterPartsVat = 0;
                decimal BeforeVatFixing = 0;
                decimal BeforeVatParts = 0;

                decimal serviceprice = 0;
                var Services = _uow.RepairOrderServicesRepository.GetMany(p => p.OrderId == order.Id);
                if (Services != null)
                {
                    foreach (var part in Services)
                    {


                        var PeacePrice = part.Price;
                        serviceprice += PeacePrice;
                    }

                }


                order.FixingPrice = order.FixingPrice + fixparts + serviceprice;


                order.BeforeDiscount = order.FixingPrice + order.SparePartsTotal;


                // to calculate spareVat and fixingVat
                if (VatPercent != null)
                {

                    var FixingVat = (decimal)VatPercent.VatFixingPrice / 100;
                    order.VatFixingMoney = order.FixingPrice * FixingVat;
                    var PartsVat = (decimal)VatPercent.VatSpareParts / 100;
                    order.VatSpareMoney = order.SparePartsTotal * PartsVat;

                }

                if (PersonDiscount != null)
                {

                  
                        var FixingDiscount = (decimal)PersonDiscount.DiscountPercentageForFixingPrice / 100;
                        order.DiscFixingMoney = order.FixingPrice * FixingDiscount;
                        TotalDiscount += order.DiscFixingMoney;
                  
                        var SPDiscount = (decimal)PersonDiscount.DiscountPercentageForSpareParts / 100;
                        order.DiscSpareMoney = order.SparePartsTotal * SPDiscount;
                        TotalDiscount = order.DiscFixingMoney + order.DiscSpareMoney;

                    
                    order.Deiscount = TotalDiscount;
                    order.BeforeDiscount = order.FixingPrice + order.SparePartsTotal;
                    order.AfterDiscount = order.BeforeDiscount - order.Deiscount;
                }

                order.BeforeDiscount = ((decimal)(order.FixingPrice)) + order.SparePartsTotal;
                order.AfterDiscount = order.BeforeDiscount - order.Deiscount;

                decimal Vat = 0;
                var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == order.WarshahId).FirstOrDefault();
                if (VAT == null)
                {
                    Vat = 0;
                }
                else
                {
                    Vat = (((decimal)VAT.GetVAT) / (100));
                }

                order.VatMoney = order.AfterDiscount * Vat;
                order.VatMoney = order.VatMoney + order.VatFixingMoney + order.VatSpareMoney;
                order.Total = order.AfterDiscount + order.VatMoney;
                //order.InspectionTemplateId = receptionOrder.InspectionTemplateId;
                // status from enum RepairOrder  8 = Open
                order.RepairOrderStatus = rorder.RepairOrderStatus;
                order.ReciptionOrderId = rorder.ReciptionOrderId;
                _uow.RepairOrderRepository.Update(order);
            }


            // repair order after inspection

            else
            {
                var PersonDiscount = _uow.PersonDiscountRepository.GetMany(a => a.CarOwnerId == Report.CarOwnerId).FirstOrDefault();

                //receptionOrder.StatusId = 2;
                //_uow.ReciptionOrderRepository.Update(receptionOrder);
                order.IsDeleted = false;
                order.WarshahId = Report.WarshahId;
                //order.CreatedBy = receptionOrder.ReciptionId;
                order.UpdatedBy = Report.TechnicalID;
                order.TechId = Report.TechnicalID;
                order.BeforeDiscount = ((decimal)(order.FixingPrice));
                order.AfterDiscount = order.BeforeDiscount - order.Deiscount;

                // calculate TotalPrice for parts using in repairorder
                var Parts = _uow.RepairOrderSparePartRepository.GetMany(p => p.RepairOrderId == order.Id);
                decimal TotalPrice = 0;
                decimal fixparts = 0;
                if (Parts != null)
                {
                    foreach (var part in Parts)
                    {

                        var Qty = part.Quantity;
                        var PeacePrice = part.PeacePrice;
                        var Cost = Qty * PeacePrice;
                        fixparts += part.FixPrice;
                        TotalPrice += Cost;
                    }

                }
                order.SparePartsTotal = TotalPrice;
                decimal TotalDiscount = 0;
                decimal AfterFixing = 0;
                decimal AfterParts = 0;
                decimal BeforeDiscFixing = 0;
                decimal BeforeDiscParts = 0;

                decimal TotolVat = 0;
                decimal AfterFixingVat = 0;
                decimal AfterPartsVat = 0;
                decimal BeforeVatFixing = 0;
                decimal BeforeVatParts = 0;

                decimal serviceprice = 0;
                var Services = _uow.RepairOrderServicesRepository.GetMany(p => p.OrderId == order.Id);
                if (Services != null)
                {
                    foreach (var part in Services)
                    {


                        var PeacePrice = part.Price;
                        serviceprice += PeacePrice;
                    }

                }
                order.FixingPrice = order.FixingPrice + fixparts + serviceprice;
                order.BeforeDiscount = order.FixingPrice + order.SparePartsTotal;
                // to calculate spareVat and fixingVat
                if (VatPercent != null)
                {

                    var FixingVat = (decimal)VatPercent.VatFixingPrice / 100;
                    order.VatFixingMoney = order.FixingPrice * FixingVat;
                    var PartsVat = (decimal)VatPercent.VatSpareParts / 100;
                    order.VatSpareMoney = order.SparePartsTotal * PartsVat;

                }

                if (PersonDiscount != null)
                {

                    
                        var FixingDiscount = (decimal)PersonDiscount.DiscountPercentageForFixingPrice / 100;
                        order.DiscFixingMoney = order.FixingPrice * FixingDiscount;
                        TotalDiscount += order.DiscFixingMoney;
                  
                        var SPDiscount = (decimal)PersonDiscount.DiscountPercentageForSpareParts / 100;
                        order.DiscSpareMoney = order.SparePartsTotal * SPDiscount;
                        TotalDiscount = order.DiscFixingMoney + order.DiscSpareMoney;

                    
                    order.Deiscount = TotalDiscount;
                    order.BeforeDiscount = order.FixingPrice + order.SparePartsTotal;
                    order.AfterDiscount = order.BeforeDiscount - order.Deiscount;
                }

                order.BeforeDiscount = ((decimal)(order.FixingPrice)) + order.SparePartsTotal;
                order.AfterDiscount = order.BeforeDiscount - order.Deiscount;

                decimal Vat = 0;
                var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == order.WarshahId).FirstOrDefault();
                if (VAT == null)
                {

                    Vat = 0;

                }
                else
                {
                    Vat = (((decimal)VAT.GetVAT) / (100));
                }
                order.VatMoney = order.AfterDiscount * Vat;
                order.VatMoney = order.VatMoney + order.VatFixingMoney + order.VatSpareMoney;
                order.Total = order.AfterDiscount + order.VatMoney;

                order.InspectionTemplateId = Report.TemplateId;
                // status from enum RepairOrder  8= Open
                order.RepairOrderStatus = rorder.RepairOrderStatus;
                order.ReciptionOrderId = rorder.ReciptionOrderId;
                order.InspectionWarshahReportId = Report.Id;
                _uow.RepairOrderRepository.Update(order);
            }



            _uow.Save();

            return Ok(order);
        }





        // To change status repair order
        [HttpPost, Route("ChangeStatusRepairOrder")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        public IActionResult ChangeStatusRepairOrder(int? repairorderid, int? statusid)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var order = _uow.RepairOrderRepository.GetMany(t => t.Id == repairorderid).Include(a => a.ReciptionOrder).FirstOrDefault();
                    order.RepairOrderStatus = (int)statusid;
                    order.UpdatedOn = DateTime.Now;
                    _uow.RepairOrderRepository.Update(order);
                    _uow.Save();

                    var RepairOrder = _uow.RepairOrderRepository.GetById(repairorderid);

                    // Reject Repair Order    رفض أمر اصلاح
                    var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == RepairOrder.WarshahId & a.NameNotificationId == 8 & a.StatusNotificationId == 1).FirstOrDefault();
                    if ( RepairOrder.RepairOrderStatus == 9 && notificationActive != null)
                    {
                        _NotificationService.SetNotificationTaqnyat(order.TechId, "تم رفض امر اصلاح ");
                    }
                  
                    //if (notificationActive != null)
                    //{


                    //    _NotificationService.SetNotification(order.TechId, "تم تغيير حالة امر الإصلاح بنجاح");
                    //    _NotificationService.SetNotification(order.ReciptionOrder.CarOwnerId, "تم تغيير حالة امر الإصلاح بنجاح");
                    //}
                    return Ok(order);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Order");



        }





        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpPost, Route("AddSparePartToRepairOrder")]
        public IActionResult AddSparePartToRepairOrder(RepairOrderSparePartsDTO repairOrderSparePart)
        {
            var Parts = _mapper.Map<DL.Entities.RepairOrderSparePart>(repairOrderSparePart);
            Parts.CreatedOn = DateTime.Now;
            _uow.RepairOrderSparePartRepository.Add(Parts);
            _uow.Save();
            return Ok(Parts);
        }

        [HttpGet, Route("RemoveServiceFromOrder")]
        public IActionResult RemoveServiceFromOrder(int serviceId)
        {
            _uow.RepairOrderServicesRepository.Delete(serviceId);
            _uow.Save();
            return Ok();
        }
        [HttpPost, Route("AddSerivceToRepairOrder")]
        public IActionResult AddSerivceToRepairOrder(RepairOrderServices repairOrderServices)
        {
            _uow.RepairOrderServicesRepository.Add(repairOrderServices);
            _uow.Save();
            return Ok(repairOrderServices);
        }
        [HttpGet, Route("GetRepairOrdersServices")]
        public IActionResult GetRepairOrdersServices(int OrdersId)
        {
            var f = _uow.RepairOrderServicesRepository.GetMany(a => a.OrderId == OrdersId).ToHashSet();
            List<object> list = new List<object>();
            foreach (var item in f)
            {
                list.Add(new
                {
                    ServiceId = item.Id,
                    ServiceName = item.Name,
                    ServicePrice = item.Price,
                    Gruntee = item.Gruntee,
                    Tech = _uow.UserRepository.GetById(item.TechId)
                });
            }
            return Ok(list);
        }

        //   [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("RejectOrder")]
        public IActionResult RejectOrder(ResonToRejectOrder resonToRejectOrder)
        {
            _uow.ResonToRejectOrderRepository.Add(resonToRejectOrder);



            _uow.Save();
            var Order = _uow.RepairOrderRepository.GetById(resonToRejectOrder.OrderId);
            var User = _uow.UserRepository.GetMany(a => a.WarshahId == Order.WarshahId && a.RoleId == 1).FirstOrDefault().Id;

            // Reject Repair Order    رفض أمر اصلاح
            var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == Order.WarshahId & a.NameNotificationId == 8).FirstOrDefault();
            if (notificationActive != null)
            {

                _NotificationService.SetNotificationTaqnyat(User, "تم رفض امر اصلاح !!");
            }
            return Ok(resonToRejectOrder);
        }
        //  [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("GetOrderRegection")]
        public IActionResult GetOrderRegection(int orderId)
        {
            var data = _uow.ResonToRejectOrderRepository.GetMany(a => a.OrderId == orderId).FirstOrDefault();
            return Ok(data);
        }


        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]


        [HttpPost, Route("removeSparePartfromRepairOrder")]
        public IActionResult removeSparePartfromRepairOrder(RemoveSPFromRO removeSPFromRO)
        {
            var SparePart = _uow.RepairOrderSparePartRepository.GetMany(a => a.RepairOrderId == removeSPFromRO.ROID && a.SparePartId == removeSPFromRO.SPID).FirstOrDefault();
            if (SparePart != null)
            {
                _uow.RepairOrderSparePartRepository.Delete(SparePart.Id);
                _uow.Save();
                return Ok(SparePart);

            }
            return BadRequest(new { status = "No Part For The Repair Order" });

        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpGet, Route("GetROPics")]

        public IActionResult GetROPics(int ROID)
        {
            var AllPics = _uow.RepairOrderImageRepository.GetMany(a => a.RepairOrderId == ROID).ToHashSet().OrderByDescending(a => a.Id);
            return Ok(AllPics);
        }
        [HttpGet, Route("PrintcarRepairorderInfo")]
        public IActionResult PrintcarRepairorderInfo(int OrderId)
        {
            var warshahid = _uow.RepairOrderRepository.GetMany(a => a.Id == OrderId).FirstOrDefault().WarshahId;

            var user = _uow.UserRepository.GetMany(a => a.WarshahId == warshahid && a.RoleId == 1).FirstOrDefault();

            var PrintcarRepairInfo = _uow.RepairOrderRepository.GetMany(a => a.Id == OrderId && a.IsActive == true && a.IsDeleted == false)
                .Include(a => a.Tech).Where(i => i.IsActive == true && i.IsDeleted == false)
                .Include(a => a.ReciptionOrder).Where(i => i.IsActive == true && i.IsDeleted == false)
                .Include(a => a.ReciptionOrder.CarOwner).Where(i => i.IsActive == true && i.IsDeleted == false)
                .Include(a => a.Warshah).Where(i => i.IsActive == true && i.IsDeleted == false)

                .Include(a => a.ReciptionOrder.MotorId).Where(i => i.IsActive == true && i.IsDeleted == false)
                                .Include(a => a.ReciptionOrder.MotorId.motorModel).Where(i => i.IsActive == true && i.IsDeleted == false)
                                                                .Include(a => a.ReciptionOrder.MotorId.motorYear).Where(i => i.IsActive == true && i.IsDeleted == false)
                                                              .Include(a => a.ReciptionOrder.MotorId.motorColor).Where(i => i.IsActive == true && i.IsDeleted == false)

                .Include(a => a.ReciptionOrder.Reciption).FirstOrDefault();

            return Ok(new { PrintcarRepairInfo = PrintcarRepairInfo, user = user });
        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpPost, Route("AddImageToRepairOrder")]
        public IActionResult AddImageToRepairOrder([FromForm] RepairOrderImageDTO repairOrderImageDTO)
        {
            if (repairOrderImageDTO.ImageName == null)
            {
                return BadRequest(new { image = "No Pic" });
            }
            var IsImage = FileCheckHelper.IsImage(repairOrderImageDTO.ImageName.OpenReadStream());
            if (!IsImage)
            {
                return BadRequest(new { Erorr = "Only Images Allowed" });
            }
            var IsBiggerThan1MB = CheckFileSizeHelper.IsBeggerThan1MB(repairOrderImageDTO.ImageName);
            if (IsBiggerThan1MB)
            {
                return BadRequest(new { Erorr = "Only Images Less Than 1MB Allowed" });

            }
            var data = _mapper.Map<DL.Entities.RepairOrderImage>(repairOrderImageDTO);
            data.ImageName = FileHelper.FileUpload(repairOrderImageDTO.ImageName, _hostingEnvironment, UploadPathes.ROPics);
            _uow.RepairOrderImageRepository.Add(data);
            _uow.Save();
            return Ok(data);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpPost, Route("RemoveImageFromRO")]
        public IActionResult RemoveImageFromRO(string ImageName)
        {
            var Image = _uow.RepairOrderImageRepository.GetMany(a => a.ImageName == ImageName).FirstOrDefault();
            _uow.RepairOrderImageRepository.Delete(Image.Id);
            _uow.Save();
            return Ok();
        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Recicp)]

        [HttpPost, Route("DeleteReciptionOrder")]
        public IActionResult DeleteReciptionOrder(int Id)
        {
            try
            {


                _uow.ReciptionOrderRepository.Delete(Id);
                _uow.Save();
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


        }
    }
}
