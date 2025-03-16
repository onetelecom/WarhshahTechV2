using AutoMapper;
using BL.Infrastructure;
using BL.Security;
using DL.DTO;
using DL.DTOs;
using DL.DTOs.BalanceDTO;
using DL.DTOs.InvoiceDTOs;
using DL.DTOs.SharedDTO;
using DL.DTOs.UserDTOs;
using DL.Entities;
using DL.Entities.HR;
using DL.MailModels;
using DL.Migrations;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using Helper;
using HELPER;
using Helpers;
using KSAEinvoice;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using Model.ApiModels;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WarshahTechV2.Models;
using User = DL.Entities.User;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ILog _log;

        private readonly ISMS _SMS;
        private readonly IAuthenticateService _authService;
        private readonly ICheckUniqes _checkUniq;

        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IMapper _mapper;
        private readonly IOptions<MyConfig> _config;
        private readonly ISubscribtionsWarshahTech _subscribtionsWarshahTech;

        private readonly INotificationService _NotificationService;
        private readonly IMailService _mailService;
        private readonly ISubscribtionCheckService _SubscribtionCheckService;
        public UserController(ISubscribtionsWarshahTech subscribtionsWarshahTech,INotificationService NotificationService, ISubscribtionCheckService SubscribtionCheckService,ISMS SMS, ICheckUniqes checkUniq, IMailService mailService, IMapper mapper,
            IHostingEnvironment hostingEnvironment, IUnitOfWork uow, IAuthenticateService authService,
            IOptions<TokenManagement> tokenManagement, ILog log, IOptions<MyConfig> config)
        {
            _SubscribtionCheckService = SubscribtionCheckService;
            _subscribtionsWarshahTech = subscribtionsWarshahTech;
            _config = config;
            _SMS = SMS;
            _NotificationService = NotificationService;
            _checkUniq = checkUniq;
            _uow = uow;
            _authService = authService;
            _hostingEnvironment = _hostingEnvironment;
            _mapper = mapper;
            _mailService = mailService;
            _log = log;


        }


        [HttpGet, Route("GetVerfiyCodebyMobile")]
        public IActionResult GetVerfiyCodebyMobile(string mobileno)
        {
            var code = _uow.VerfiyCodeRepository.GetMany(a => a.PhoneNumber == mobileno).OrderByDescending(a=>a.Id).LastOrDefault();
            if (code != null)
            {
                return Ok(code.VirfeyCode);
            }
            return Ok();
        }

        [HttpGet,Route("GetSubscribtionByWarshahId")]
        public IActionResult GetDurationByWarshahId(int warshahId)
        {
            var s = _uow.DurationRepository.GetMany(a => a.WarshahId == warshahId).Include(a=>a.Subscribtion).FirstOrDefault();
            return Ok(s);
        }
        [HttpGet, Route("GetInvoicesValueByWarshahId")]
        public IActionResult GetInvoicesValue()
        {
            var s = _uow.SubscribtionInvoicerepository.GetAll().Sum(a => a.TotalSubscribtion);
            return Ok(s);
        }


        /// <summary>
        /// Log in user this Fucn Is Used To Login User
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200"> user object and token </response>
        /// <response code="400">invalid user name or password</response>
        /// <response code="401">Unauthorized</response>
        [AllowAnonymous]
        [HttpPost, Route("LogIn")]
        public IActionResult LogIn(ApiLoginModelDTO request)
        {
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }
            var user = _authService.AuthenticateUser(request, out string token);
            if (user == null)
            {
                return BadRequest(new { erorr = "البريد الالكتروني او كلمة المرور خطأ" });

            }
            if (!user.IsActive)
            {
                return BadRequest(new { erorr = "الحساب غير مفعل توجه لبريدك الالكتروني للتفعيل" });
            }
            if (user.WarshahId != null)
            {
                var Paid = _SubscribtionCheckService.CheckIfWarshahSubscribtionStillValid(user.WarshahId);
                var Expired = _SubscribtionCheckService.CheckIfWarshahSubscribtionEndTimeStillVaild(user.WarshahId);
                var warshah = _uow.WarshahRepository.GetById(user.WarshahId);
                if (!Expired)
                {
                    return Ok(new { WarshahExpired = true ,WarshahId = user.WarshahId , Isold = warshah.IsOld });
                }
                if (!Paid)
                {
                    return Ok(new { WarshahDidntPay = true, WarshahId = user.WarshahId, Isold = warshah.IsOld });
                }
            }
            if (user != null)
            {
                ///////
                ///
                // 

                // Add Notification Setting

                if (user.WarshahId != null)

                { 
                    var currentnotification = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == user.WarshahId).ToHashSet();

                var namenotifications = _uow.NameNotificationRepository.GetAll().ToHashSet();

                if (currentnotification.Count == 0)

                {
                    foreach (var notification in namenotifications)

                    {
                        _uow.NotificationSettingRepository.Add(new NotificationSetting { WarshahId = (int)user.WarshahId, NameNotificationId = notification.Id, StatusNotificationId = 2 });


                    }


                    _uow.Save();
                }




                // Add Notification Repair Order


                var currentnotification1 = _uow.NotificationRepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId).ToHashSet();


                var namenotifications1 = _uow.NotificationRepairOrderAddingRepository.GetAll().ToHashSet();

                if (currentnotification1.Count == 0)
                {
                    foreach (var notification in namenotifications1)

                    {
                        _uow.NotificationRepairOrderRepository.Add(new NotificationRepairOrder { WarshahId = (int)user.WarshahId, NameNotificationId = notification.Id, StatusNotificationId = 2, Minutes = 0 });


                    }
                    _uow.Save();
                }


                }




                // HR Notification تنبيهات الموارد البشرية

            
                  

                

                if (user.WarshahId != null)
                {
                    var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == user.WarshahId & a.NameNotificationId == 22 & a.StatusNotificationId == 1).FirstOrDefault();


                    DateTime enddate = DateTime.Now.AddMonths(2);

                    if (notificationActive != null)
                    {

                        var employees = _uow.DataEmployeeRepository.GetMany(e => e.WarshahId == user.WarshahId && e.ContractEndDate >= DateTime.Now && e.ContractEndDate <= enddate).ToHashSet();
                        if (employees.Count > 0)
                        {

                            _NotificationService.SetNotificationTaqnyat(user.Id, "سينتهى عقود بعض الموظفين خلال شهرين من فضلك قم بالمراجعة  ");

                        }

                        var Cardsemployees = _uow.DataEmployeeRepository.GetMany(e => e.WarshahId == user.WarshahId && e.IdCardEnd >= DateTime.Now && e.IdCardEnd <= enddate).ToHashSet();
                        if (Cardsemployees.Count > 0)
                        {
                            _NotificationService.SetNotificationTaqnyat(user.Id, "ستنتهى إقامة/الهوية لبعض الموظفين خلال شهرين من فضلك قم بالمراجعة  ");
                        }

                        var Passportemployees = _uow.DataEmployeeRepository.GetMany(e => e.WarshahId == user.WarshahId && e.PassportEndDate >= DateTime.Now && e.PassportEndDate <= enddate).ToHashSet();

                        if (Passportemployees.Count > 0)
                        {

                            _NotificationService.SetNotificationTaqnyat(user.Id, "سينتهى صلاحية جواز السفر لبعض الموظفين خلال شهرين من فضلك قم بالمراجعة  ");

                        }


                        var age = 60;


                        var Retirementemployees = _uow.DataEmployeeRepository.GetMany(e => e.WarshahId == user.WarshahId && (enddate.Year - e.BirthDate.Year == age)).ToHashSet();

                        if (Retirementemployees.Count > 0)
                        {

                            _NotificationService.SetNotificationTaqnyat(user.Id, " بعض الموظفين قاربوا من سن المعاش خلال شهرين من فضلك قم بالمراجعة  ");

                        }

                    }
                }


                //   Notification Repair order

                // الأوامر بدء الاصلاح


                if (user.WarshahId != null)

                {
                    var notificationrepairorder1 = _uow.NotificationRepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.StatusNotificationId == 1 && a.NameNotification.ArabicName == "بدء الإصلاح").FirstOrDefault();

                    if (notificationrepairorder1 != null)
                    {

                        var time = notificationrepairorder1.Minutes;

                        DateTime dateTime = DateTime.Now;

                        var repairorders1 = _uow.RepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.RepairOrderStatus == 1).ToHashSet();

                        if (repairorders1.Count > 0)

                        {

                            foreach (var repairorder in repairorders1)
                            {
                                DateTime ExpectedTime = repairorder.CreatedOn.AddMinutes(time);
                                if (dateTime > ExpectedTime)
                                {
                                    _NotificationService.SetNotificationTaqnyat(user.Id, " دقيقة   " + time + "  يوجد امر إصلاح لم يتم  بدأ الإصلاح فيه منذ أكثر من   ");

                                }
                            }


                        }


                    }

                    // الاوامر تقرير الفنى 


                    var notificationrepairorder2 = _uow.NotificationRepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.StatusNotificationId == 1 && a.NameNotification.ArabicName == "تقرير الفنى").FirstOrDefault();

                    if (notificationrepairorder2 != null)
                    {

                        var time = notificationrepairorder2.Minutes;

                        DateTime dateTime = DateTime.Now;

                        var repairorders1 = _uow.RepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.RepairOrderStatus == 2).ToHashSet();

                        if (repairorders1.Count > 0)

                        {

                            foreach (var repairorder in repairorders1)
                            {

                                DateTime date = (DateTime)repairorder.UpdatedOn;

                                DateTime ExpectedTime = date.AddMinutes(time);
                                if (dateTime > ExpectedTime)
                                {
                                    _NotificationService.SetNotificationTaqnyat(user.Id, " دقيقة   " + time + "  يوجد امر إصلاح ينتظر تقرير الفنى منذ أكثر من   ");

                                }
                            }


                        }


                    }

                    // الاوامر موافقة العميل 


                    var notificationrepairorder7 = _uow.NotificationRepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.StatusNotificationId == 1 && a.NameNotification.ArabicName == "انتظار موافقة العميل").FirstOrDefault();

                    if (notificationrepairorder7 != null)
                    {

                        var time = notificationrepairorder7.Minutes;

                        DateTime dateTime = DateTime.Now;

                        var repairorders1 = _uow.RepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.RepairOrderStatus == 3).ToHashSet();

                        if (repairorders1.Count > 0)

                        {

                            foreach (var repairorder in repairorders1)
                            {
                                DateTime date = (DateTime)repairorder.UpdatedOn;

                                DateTime ExpectedTime = date.AddMinutes(time);
                                if (dateTime > ExpectedTime)
                                {
                                    _NotificationService.SetNotificationTaqnyat(user.Id, " دقيقة   " + time + "  يوجد امر إصلاح ينتظر موافقة العميل منذ أكثر من   ");

                                }
                            }


                        }


                    }


                    // الاوامر طلب قطع الغيار 


                    var notificationrepairorder3 = _uow.NotificationRepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.StatusNotificationId == 1 && a.NameNotification.ArabicName == "طلب قطع غيار").FirstOrDefault();

                    if (notificationrepairorder3 != null)
                    {

                        var time = notificationrepairorder3.Minutes;

                        DateTime dateTime = DateTime.Now;

                        var repairorders1 = _uow.RepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.RepairOrderStatus == 4).ToHashSet();

                        if (repairorders1.Count > 0)

                        {

                            foreach (var repairorder in repairorders1)
                            {
                                DateTime date = (DateTime)repairorder.UpdatedOn;

                                DateTime ExpectedTime = date.AddMinutes(time);
                                if (dateTime > ExpectedTime)
                                {
                                    _NotificationService.SetNotificationTaqnyat(user.Id, " دقيقة   " + time + "  يوجد امر إصلاح ينتظر طلب قطع الغيار منذ أكثر من   ");

                                }
                            }


                        }


                    }


                    // الاوامر انتظار الفنى 


                    var notificationrepairorder4 = _uow.NotificationRepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.StatusNotificationId == 1 && a.NameNotification.ArabicName == "انتظار الفنى").FirstOrDefault();

                    if (notificationrepairorder4 != null)
                    {

                        var time = notificationrepairorder4.Minutes;

                        DateTime dateTime = DateTime.Now;

                        var repairorders1 = _uow.RepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.RepairOrderStatus == 5).ToHashSet();

                        if (repairorders1.Count > 0)

                        {

                            foreach (var repairorder in repairorders1)
                            {
                                DateTime date = (DateTime)repairorder.UpdatedOn;

                                DateTime ExpectedTime = date.AddMinutes(time);
                                if (dateTime > ExpectedTime)
                                {
                                    _NotificationService.SetNotificationTaqnyat(user.Id, " دقيقة   " + time + "  يوجد امر إصلاح ينتظر الفنى للتصليح منذ أكثر من   ");

                                }
                            }


                        }


                    }


                    // الاوامر الإصلاح 


                    var notificationrepairorder5 = _uow.NotificationRepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.StatusNotificationId == 1 && a.NameNotification.ArabicName == "الإصلاح").FirstOrDefault();

                    if (notificationrepairorder5 != null)
                    {

                        var time = notificationrepairorder5.Minutes;

                        DateTime dateTime = DateTime.Now;

                        var repairorders1 = _uow.RepairOrderRepository.GetMany(a => a.WarshahId == user.WarshahId && a.RepairOrderStatus == 6).ToHashSet();

                        if (repairorders1.Count > 0)

                        {

                            foreach (var repairorder in repairorders1)
                            {
                                DateTime date = (DateTime)repairorder.UpdatedOn;

                                DateTime ExpectedTime = date.AddMinutes(time);
                                if (dateTime > ExpectedTime)
                                {
                                    _NotificationService.SetNotificationTaqnyat(user.Id, " دقيقة   " + time + "  يوجد امر إصلاح قيد الاصلاح و لم يتم إغلاقه منذ أكثر من   ");

                                }
                            }


                        }


                    }

                }

                //


                var UserPermission = _uow.UserpermissionRepository.GetMany(a => a.UserId == user.Id).ToHashSet();
                var Permission = new List<permission>();


                foreach (var item in UserPermission)
                {
                    if (item.PermissionId == 10)
                    {
                        item.PermissionId = 11;
                    }
                    var Per = _uow.PermissionRepository.GetMany(a => a.Id == item.PermissionId).FirstOrDefault();
                    Permission.Add(Per);
                }

                ////////
                //user.Password = null;
                if (user.WarshahId == null)
                {

                    return Ok(new
                    {
                        Permission,
                        user,
                        token,

                    });
                }

                var warshah = _uow.WarshahRepository.GetById(user.WarshahId);

                //var ao = new
                //{
                //    userNameOrEmailAddress = "WA-"+user.Id+"@warshahtech.net",
                //    password = "123qwe",
                //    rememberClient = true,
                //    tenancyName = "WA-" + warshah.Id 

                //};

                //var json = JsonConvert.SerializeObject(ao);
                //var respone = HttpOp.webPostMethodHr(json, "https://hrapi.warshahtech.net/api/services/app/Employees/CreateForWarshah");
                //if (respone != "OK")
                //{
                //    // Start Again Cuse The Task From Hr Api Faild
                //}

                var warshahSetting = _uow.ConfigrationRepository.GetMany(a => a.WarshahId == warshah.Id).FirstOrDefault();
                var City = _uow.CityRepository.GetById(warshah.CityId);
                var Country = _uow.CountryRepository.GetById(warshah.CityId);
                var Region = _uow.RegionRepository.GetById(warshah.CityId);

                return Ok(new
                {
                    Permission,
                    user,
                    token,
                    warshah,
                    City,
                    Country,
                    Region,
                    warshahSetting
                });
            }
            return BadRequest(new { erorr = "البريد الالكتروني او كلمة السر خطأ" });

        }

        [HttpGet,Route("DeleteWarshshs")]
        public IActionResult DeleteWarshshs()
        {
            var All = _uow.WarshahRepository.GetAll().ToHashSet();
            var Warshahs = All;
            foreach (var item in Warshahs)
            {
                _uow.WarshahRepository.Delete(item.Id);
            }
            _uow.Save();

            return Ok();

        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpGet,Route("GetClientsPoints")]
        public IActionResult GetClientsPoints(int warshahId)
        {
            var AllPoints = _uow.LoyalityPointRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet();
            var List = new List<object>();
            foreach (var a in AllPoints)
            {
                var op = new
                {
                    CarOwner = _uow.UserRepository.GetById(a.CarOwnerId),
                    Points = a.Points
                };
                List.Add(op);
            }
            return Ok(List);
        }
        [HttpPost,Route("SetLoyalitySetting")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        public IActionResult SetLoyalitySetting(LoyalitySettingDTO loyalitySettingDTO)
        {
            var lS =_uow.LoyalitySettingRepository.GetMany(a=>a.WarshahId==loyalitySettingDTO.WarshahId).FirstOrDefault();
            if (lS == null) 
            {
                LoyalitySetting s = new LoyalitySetting { LoyalityPointsPerCurrancy = loyalitySettingDTO.MoneyPerPoint, WarshahId = loyalitySettingDTO.WarshahId };
                _uow.LoyalitySettingRepository.Add(s);
                _uow.Save();
                return Ok(s);
            }
            lS.LoyalityPointsPerCurrancy = loyalitySettingDTO.MoneyPerPoint;
            _uow.LoyalitySettingRepository.Update(lS);
            _uow.Save();
            return Ok(lS);
        }
        [HttpPost, Route("SetLoyalityReavarseSetting")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        public IActionResult SetLoyalityReavarseSetting(LoyalitySettingRevarseDTO loyalitySettingDTO)
        {
            var lS = _uow.LoyalitySettingRevarseRepository.GetMany(a => a.WarshahId == loyalitySettingDTO.WarshahId).FirstOrDefault();
            if (lS==null)
            {
                LoyalitySettingRevarse s = new LoyalitySettingRevarse { NoofPoints = loyalitySettingDTO.NoofPoints , CurrancyPerLoyalityPoints = loyalitySettingDTO.pointPerMoney, WarshahId = loyalitySettingDTO.WarshahId };
                _uow.LoyalitySettingRevarseRepository.Add(s);
                _uow.Save();
                return Ok(s);
            }
            lS.NoofPoints = loyalitySettingDTO.NoofPoints;
            lS.CurrancyPerLoyalityPoints = loyalitySettingDTO.pointPerMoney;
            _uow.LoyalitySettingRevarseRepository.Update(lS);
            _uow.Save();
            return Ok(lS);
        }



        [HttpGet, Route("RecoverWarshah")]
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        public IActionResult RecoverWarshah(int From , int To)
        {
           
            var warshahs = _uow.WarshahRepository.GetMany(a=>a.Id >= From && a.Id <= To).ToHashSet();

            foreach(var w in  warshahs)
            {
                var users = _uow.UserRepository.GetMany(a=>a.WarshahId == w.Id).ToHashSet();

                if (users.Count > 0)
                {
                    foreach (var u in users)
                    {
                        _uow.UserRepository.Delete(u.Id);
                        _uow.WarshahRepository.Delete(w.Id);
                        _uow.Save();
                    }
                }
                else
                {
                    _uow.WarshahRepository.Delete(w.Id);
                    _uow.Save();
                }
                }

               
            

          

            return Ok();

        }




        [HttpGet,Route("GetLoyalitySetting")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        public IActionResult GetLoyalitySetting(int warshahId)
        {
            return Ok(_uow.LoyalitySettingRepository.GetMany(a => a.WarshahId == warshahId).FirstOrDefault());
        }
        [HttpGet,Route("CloneSetting")]
        public IActionResult CloneSetting(int warshahId)
        {
            var Branch=_uow.WarshahRepository.GetById(warshahId);
            if (Branch.ParentWarshahId==null)
            {
                return BadRequest(new { status = "Must Be Branch" });
            }

            var MainWarhsah= _uow.WarshahRepository.GetById(Branch.ParentWarshahId);
            var fastServiceCategory = _uow.ServiceCategoryRepository.GetMany(a=>a.WarshahId==Branch.ParentWarshahId).ToHashSet();
            foreach (var item in fastServiceCategory)
            {
                _uow.ServiceCategoryRepository.Add(new ServiceCategory
                {
                    WarshahId=Branch.Id,
                    Name=item.Name
                });
            }
            _uow.Save();
            var vat = _uow.WarshahVatRepository.GetMany(a => a.WarshahId == Branch.ParentWarshahId).ToHashSet();
            foreach (var item in vat)
            {
                _uow.WarshahVatRepository.Add(new WarshahVat
                {
                    WarshahId=Branch.Id,
                    VatFixingPrice=item.VatFixingPrice, 
                    VatSpareParts=item.VatSpareParts
                });

            }
            _uow.Save();
            var Conf = _uow.ConfigrationRepository.GetMany(a => a.WarshahId == Branch.ParentWarshahId).ToHashSet();
            foreach (var item in Conf)
            {
                _uow.ConfigrationRepository.Add(new Configration
                {
                    GetCustomerAbroval=item.GetCustomerAbroval,
                    GetVAT=item.GetVAT,
                    PeriodDayCustomerApprove=item.PeriodDayCustomerApprove,
                    WarshahId=Branch.Id
                });
            }
            _uow.Save();
            var RevarseLoyality = _uow.LoyalitySettingRepository.GetMany(a => a.WarshahId == Branch.ParentWarshahId).ToHashSet();
            foreach (var item in RevarseLoyality)
            {
                _uow.LoyalitySettingRepository.Add(new LoyalitySetting
                {
                    WarshahId=Branch.Id,
                    LoyalityPointsPerCurrancy =item.LoyalityPointsPerCurrancy
                });
            }
            _uow.Save();
            var RevarseLoyalityR= _uow.LoyalitySettingRevarseRepository.GetMany(a => a.WarshahId == Branch.ParentWarshahId).ToHashSet();
            foreach (var item in RevarseLoyalityR)
            {
                _uow.LoyalitySettingRevarseRepository.Add(new LoyalitySettingRevarse
                {
                    WarshahId = Branch.Id,
                    CurrancyPerLoyalityPoints = item.CurrancyPerLoyalityPoints,
                    NoofPoints=item.NoofPoints
                });
            }
            _uow.Save();
            return Ok(Branch);
        }
        [HttpGet,Route("GetUserPoints")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        public IActionResult GetUserPoints(int UserId)
        {
            var Points = _uow.LoyalityPointRepository.GetMany(a=>a.CarOwnerId==UserId).ToHashSet();
            var All = new List<object>();
            foreach (var point in Points)
            {
                All.Add(new
                {
                    Warshah = _uow.WarshahRepository.GetById(point.WarshahId),
                    Points = point.Points,
                });
            }
            return Ok(All);
        }
        [HttpGet, Route("GetLoyalityRevaresSetting")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        public IActionResult GetLoyalityRevaresSetting(int warshahId)
        {
            var f = _uow.LoyalitySettingRevarseRepository.GetMany(a => a.WarshahId == warshahId).FirstOrDefault();
            return Ok(f);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("AddCustomerToWarshah")]
        public IActionResult AddCustomerToWarshah(int warshahId, int UserId)
        {
            try
            {
                _uow.WarshahCustomersRepository.Add(new WarshahCustomers { UserId = UserId, WarshahId = warshahId });
                _uow.Save();

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


        }


        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllWarshahCustomers")]
        public IActionResult GetAllWarshahCustomers(int warshahId , int pagenumber, int pagecount)
        {
            var userList = _uow.WarshahCustomersRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet();
            var UserList = new List<User>();
            foreach (var item in userList)
            {
                var User = _uow.UserRepository.GetById(item.Id);
                UserList.Add(User);
            }
           
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = UserList.Count(), Listinvoice = UserList.ToPagedList(pagenumber, pagecount) });
        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllWarshahUsers")]
        public IActionResult GetAllWarshahUsers(int warshahId)
        {
            var userList = _uow.UserRepository.GetMany(a => a.WarshahId == warshahId).Include(dt => dt.Role).ToHashSet();



            return Ok(userList);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("SetWarshahType")]
        public IActionResult SetWarshahType(WarshahParams warshahParams)
        {
            if (ModelState.IsValid)
            {
                var data = _uow.WarshahParamsRepository.GetMany(a => a.WarshahId == warshahParams.WarshahId).FirstOrDefault();
                if (data != null)
                {
                    return BadRequest(new { status = "Can't Add More Than One" });
                }

                _uow.WarshahParamsRepository.Add(warshahParams);
                _uow.Save();
                return Ok(warshahParams);
            }
            return BadRequest(ModelState);
        }




        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("AddWarshahType")]
        public IActionResult AddWarshahType(WarshahTypeDTO warshahTypeDTO)

        {

            var type = _mapper.Map<DL.Entities.WarshahType>(warshahTypeDTO);
            var SavedType = _uow.WarshahTypeRepository.GetMany(a => a.WarshahId == type.WarshahId && a.WarshahFixedTypeId == type.WarshahFixedTypeId).FirstOrDefault();
            

            if (type.IsActive == true)
            {
                if (SavedType == null)
                {
                    _uow.WarshahTypeRepository.Add(type);
                }
                else
                {

                }
               
            }
            else
            {
                if (SavedType != null)
                {
                    _uow.WarshahTypeRepository.Delete(SavedType.Id);
                }
                else
                {

                }

                
            }
           
              _uow.Save();
            return Ok(warshahTypeDTO);

        }







        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("RegisterForm")]
        public IActionResult RegisterForm(RegisterFormto registerFormto)

        {

            var warshah = _mapper.Map<DL.Entities.RegisterForm>(registerFormto);

                     warshah.CreatedOn = DateTime.Now;
                    _uow.RegisterFormRepository.Add(warshah);
           
                      _uow.Save();
                       return Ok(warshah);

        }




        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpGet, Route("GetAllRegisterForm")]
        public IActionResult GetAllRegisterForm( )

        {

            var warshs = _uow.RegisterFormRepository.GetAll().ToHashSet();

            return Ok(warshs);

        }



        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpGet, Route("GetRegisterFormById")]
        public IActionResult GetRegisterFormById(int id)

        {

            var warshs = _uow.RegisterFormRepository.GetById(id);

            return Ok(warshs);

        }








        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("AddWarshahServices")]
        public IActionResult AddWarshahServices(ServicesWarshahDTO servicesWarshahDTO)

        {

            var service = _mapper.Map<DL.Entities.ServiceWarshah>(servicesWarshahDTO);


            var SavedType = _uow.ServicesWarshahRepository.GetMany(a => a.WarshahId == service.WarshahId && a.warshahServiceTypeID == service.warshahServiceTypeID).FirstOrDefault();


            if (service.IsActive == true)
            {
                if (SavedType == null)
                {
                    _uow.ServicesWarshahRepository.Add(service);
                }
                else
                {

                }

            }
            else
            {
                if (SavedType != null)
                {
                    _uow.ServicesWarshahRepository.Delete(SavedType.Id);
                }
                else
                {

                }


            }





            _uow.Save();
            return Ok(service);

        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("AddWarshahFixedType")]
        public IActionResult AddWarshahFixedType(WarshahFixedTypeDTO warshahFixedTypeDTO)

        {

            var type = _mapper.Map<DL.Entities.WarshahFixedType>(warshahFixedTypeDTO);
            _uow.WarshahFixedTypeRepository.Add(type);
            _uow.Save();
            return Ok(type);

        }




        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("DashboardCarOwner")]
        public IActionResult DashboardCarOwner(int CarOwnerId)
        {

            var carowner = _uow.UserRepository.GetById(CarOwnerId);

            // repair Order  أوامر قيد الإصلاح   (repair order open != 7)

            var RepairOrder = _uow.ReciptionOrderRepository.GetMany(o => o.CarOwnerId == CarOwnerId).ToHashSet().Count;


            // الفواتير المسددة


            var Invoices = _uow.InvoiceRepository.GetMany(t => t.CarOwnerID == CarOwnerId.ToString() || t.CarOwnerPhone == carowner.Phone).ToHashSet().Count;

            var motors = _uow.MotorsRepository.GetMany(a => a.CarOwnerId == carowner.Id).ToHashSet().Count;



            return Ok(new
            {
                RepairOrderCount = RepairOrder,
                Invoices = Invoices,
                Motors = motors


            });
        }


        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("AddWarshahLoyalityPoints")]
        public IActionResult AddWarshahLoyalityPoints(LoyalitySettingRevarseDTO loyalitySettingRevarseDTO)

        { 

            var MoneyPoint = _mapper.Map<DL.Entities.LoyalitySettingRevarse>(loyalitySettingRevarseDTO);
            _uow.LoyalitySettingRevarseRepository.Add(MoneyPoint);
            _uow.Save();
            return Ok(MoneyPoint);

        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("EditWarshahLoyalityPoints")]
        public IActionResult EditWarshahLoyalityPoints(LoyalitySettingRevarseDTO loyalitySettingRevarseDTO)

        {

            var MoneyPoint = _mapper.Map<DL.Entities.LoyalitySettingRevarse>(loyalitySettingRevarseDTO);
            _uow.LoyalitySettingRevarseRepository.Update(MoneyPoint);
            _uow.Save();
            return Ok(MoneyPoint);

        }





        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("AddFixedCountryMotor")]
        public IActionResult AddFixedCountryMotor(FixedCountryMotor fixedCountryMotor)

        {

            _uow.FixedCountryMotorRepository.Add(fixedCountryMotor);
            _uow.Save();
            return Ok(fixedCountryMotor);

        }








        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("AddWarshahModelCar")]
        public IActionResult AddWarshahModelCar(WarshahModelCarDTO warshahModelsCar)

        {

            var model = _mapper.Map<DL.Entities.WarshahModelsCar>(warshahModelsCar);

            var SavedType = _uow.WarshahModelCarRepository.GetMany(a => a.WarshahId == model.WarshahId && a.MotorModelId == model.MotorModelId).FirstOrDefault();


            if (model.IsActive == true)
            {
                if (SavedType == null)
                {
                    _uow.WarshahModelCarRepository.Add(model);
                }
                else
                {

                }

            }
            else
            {
                if (SavedType != null)
                {
                    _uow.WarshahModelCarRepository.Delete(SavedType.Id);
                }
                else
                {

                }


            }


            _uow.Save();
            return Ok(model);

        }



        
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet,Route("DeleteUserDiscount")]
        public IActionResult DeleteUserDiscount(int warshahId,int userId)
        {
            var userDiscount = _uow.PersonDiscountRepository.GetMany(a => a.CarOwnerId == userId && a.WarshahId == warshahId).FirstOrDefault();
            if (userDiscount==null)
            {
                return BadRequest(new { status = "No Discount For This Person In This Warshah" });
            }

            _uow.PersonDiscountRepository.Delete(userDiscount.Id);
            _uow.Save();
            return Ok();
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet,Route("GetUserNotificationCount")]
        public IActionResult GetUserNotificationCount(int UserId)
        {
            var Count = _uow.NotificationRepository.GetMany(a => a.UserId == UserId && a.Read == false).ToHashSet().Count();
            return Ok(Count);   
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetUserNotification")]
        public IActionResult GetUserNotification(int UserId)
        {
            var Count = _uow.NotificationRepository.GetMany(a => a.UserId == UserId && a.Read == false).ToHashSet();
            return Ok(Count);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetUserNotificationOld")]
        public IActionResult GetUserNotificationOld(int UserId)
        {
            var Count = _uow.NotificationRepository.GetMany(a => a.UserId == UserId && a.Read == true).ToHashSet();
            return Ok(Count);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("ReadAllNotification")]
        public IActionResult ReadAllNotification(int UserId)
        {
            var Noti = _uow.NotificationRepository.GetMany(a => a.UserId == UserId && a.Read == false).ToHashSet();
            foreach (var item in Noti)
            {
                item.Read = true;
                _uow.NotificationRepository.Update(item);
                _uow.Save();
            }
            return Ok();
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("AddWarshahCountryMotor")]
        public IActionResult AddWarshahCountryMotor(WarshahCountryMotor warshahCountryMotor)

        {


            var SavedType = _uow.WarshahCountryMotorRepository.GetMany(a => a.WarshahId == warshahCountryMotor.WarshahId && a.FixedCountryMotorId == warshahCountryMotor.FixedCountryMotorId).FirstOrDefault();


            if (warshahCountryMotor.IsActive == true)
            {
                if (SavedType == null)
                {
                    _uow.WarshahCountryMotorRepository.Add(warshahCountryMotor);
                }
                else
                {

                }

            }
            else
            {
                if (SavedType != null)
                {
                    _uow.WarshahCountryMotorRepository.Delete(SavedType.Id);
                }
                else
                {

                }


            }


            _uow.Save();
            return Ok(warshahCountryMotor);

        }






        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("GetWarshahType")]
        public IActionResult GetWarshahType(int warshahId)
        {
            if (ModelState.IsValid)
            {
                var data = _uow.WarshahParamsRepository.GetMany(a => a.WarshahId == warshahId).FirstOrDefault();

                return Ok(data);
            }
            return BadRequest(ModelState);
        }

       [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpGet, Route("ExtendWarshahSubscrib")]
        public IActionResult ExtendWarshahSubscrib(int warshahId)
        {
          var data = _uow.DurationRepository.GetMany(a => a.WarshahId == warshahId).FirstOrDefault();
            data.EndDate = data.EndDate.AddDays(4);
            _uow.DurationRepository.Update(data);
            _uow.Save();
            return Ok(data);

        }
        [HttpGet, Route("Pass")]
        public IActionResult Pass(string Pass)
        {
            return Ok(EncryptANDDecrypt.DecryptText(Pass));
        }

        [HttpGet, Route("GetAllWarshahParams")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        public IActionResult GetAllWarshahParams()
        {
            var Spicialists = _uow.SpicialistsRepository.GetAll();
            var Types = _uow.WarshahTypeRepository.GetAll();
            var ServiceTypes = _uow.WarshahServiceTypeRepository.GetAll();
            return Ok(new { Spicialists = Spicialists, Types = Types, ServiceTypes = ServiceTypes });
        }



        [HttpGet, Route("GetWarshahTypeModelService")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        public IActionResult GetWarshahTypeModelService(string SearchText)
        {
            var Model = _uow.MotorModelRepository.GetAll().ToHashSet();

            if (!string.IsNullOrEmpty(SearchText))
            {
                 Model =_uow.MotorModelRepository.GetMany(a=>a.ModelNameAr.Contains(SearchText)).ToHashSet();

            }
           
            var WarshahTypes = _uow.WarshahFixedTypeRepository.GetAll();
            var ServiceTypes = _uow.WarshahServiceTypeRepository.GetAll();
            var CountriesMotor = _uow.FixedCountryMotorRepository.GetAll();
            return Ok(new { Model = Model, WarshahTypes = WarshahTypes, ServiceTypes = ServiceTypes , CountriesMotor = CountriesMotor });
        }

        [HttpGet,Route("GetWarshahByTypeParams")]
        public IActionResult GetWarshahByTypeParams(string MotorModel,string warshahType,string ServiceType,string CountryMotor)
        {
            var WarshahList = new List<DL.Entities.Warshah>();

            if (!string.IsNullOrEmpty(MotorModel))
            {
               
                var Warshahs= _uow.WarshahModelCarRepository.GetMany(a => a.MotorModel.ModelNameAr.Contains(MotorModel)).ToHashSet();
                foreach (var item in Warshahs)
                {
                    var warshah = _uow.WarshahRepository.GetById(item.WarshahId);
                    WarshahList.Add(warshah);
                }
            }
            if (!string.IsNullOrEmpty(warshahType))
            {
                var data = _uow.WarshahTypeRepository.GetMany(a => a.WarshahFixedType.NameType==warshahType).ToList();
                foreach (var item in data)
                {
                    var warshah = _uow.WarshahRepository.GetById(item.WarshahId);
                    WarshahList.Add(warshah);

                }
            }
            if (!string.IsNullOrEmpty(ServiceType))
            {
                var data = _uow.ServicesWarshahRepository.GetMany(a => a.warshahServiceType.Name == ServiceType).ToList();
                foreach (var item in data)
                {
                    var warshah = _uow.WarshahRepository.GetById(item.WarshahId);
                    WarshahList.Add(warshah);

                }

            }
            if (!string.IsNullOrEmpty(CountryMotor))
            {
                var data = _uow.WarshahCountryMotorRepository.GetMany(a => a.FixedCountryMotor.CountryMotorName == CountryMotor).ToList();
                foreach (var item in data)
                {
                    var warshah = _uow.WarshahRepository.GetById(item.WarshahId);
                    WarshahList.Add(warshah);

                }

            }


            var ClearWarshahList = WarshahList.Distinct();
            return Ok(ClearWarshahList);
        }
       
        [HttpGet, Route("GetWarshahTypeModelServiceByWarshahId")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        public IActionResult GetWarshahTypeModelServiceByWarshahId(int warshahid)
        {
            var Model = _uow.WarshahModelCarRepository.GetMany(w=>w.WarshahId == warshahid && w.IsActive == true).Include(a=>a.MotorModel).ToHashSet();
            var WarshahTypes = _uow.WarshahTypeRepository.GetMany(w => w.WarshahId == warshahid && w.IsActive == true).Include(a=>a.WarshahFixedType).ToHashSet();
            var ServiceTypes = _uow.ServicesWarshahRepository.GetMany(w => w.WarshahId == warshahid && w.IsActive == true).Include(a=>a.warshahServiceType).ToHashSet();
            var CountriesMotor = _uow.WarshahCountryMotorRepository.GetMany(w => w.WarshahId == warshahid && w.IsActive == true).Include(a => a.FixedCountryMotor).ToHashSet();
            return Ok(new { Model = Model, WarshahTypes = WarshahTypes, ServiceTypes = ServiceTypes, CountriesMotor = CountriesMotor });
        }


        [AllowAnonymous]
        [HttpPost,Route("CheckPhone")]
        public IActionResult CheckPhone (CheckPhoneDTO Phone)
        {
            var user = _uow.UserRepository.Get(a => a.Phone == Phone.Phone);
            if (user == null)
            {
                return Ok(new { status = false });
            }
            return Ok(new { status = true });

        }
        [AllowAnonymous]
        [HttpPost, Route("CheckEmail")]
        public IActionResult CheckEmail(CheckPhoneDTO Phone)
        {
            var user = _uow.UserRepository.Get(a => a.Email == Phone.Phone);
            if (user == null)
            {
                return Ok(new { status = false });
            }
            return Ok(new { status = true });

        }


        [AllowAnonymous]
        [HttpGet, Route("CheckIdentitfier")]
        public IActionResult CheckIdentitfier(string Identitfier,int warshahId)
        {
            var user = _uow.WarshahRepository.Get(a => a.WarshahIdentifier == Identitfier);
            var warshah = _uow.WarshahRepository.GetById(warshahId);
            if (user == null)
            {
                return Ok(new { status = false });
            }
            if (warshah.WarshahIdentifier==Identitfier)
            {
                return Ok(new { status = false });

            }
            return Ok(new { status = true });

        }
        [AllowAnonymous]
        [HttpGet, Route("GetProfile")]
        public IActionResult GetProfile(string Id)
        {
            var Warshah = _uow.WarshahRepository.GetMany(a => a.WarshahIdentifier == Id).FirstOrDefault();
            var ListOfShift = new List<object>();
            var AllShifts = _uow.WarshahShiftRepository.GetMany(a => a.WarshahId == Warshah.Id).ToHashSet();
            foreach (var item in AllShifts)
            {
                var shift = new
                {
                    Shift = item,
                    WorkTimes = _uow.WorkTimeRepository.GetMany(a => a.WarshahShiftId == item.Id).ToHashSet()
                };
                ListOfShift.Add(shift);
            };
            var City = _uow.CityRepository.GetById(Warshah.CityId);
            var Country = _uow.CountryRepository.GetById(Warshah.CountryId);
            var Region = _uow.RegionRepository.GetById(Warshah.RegionId);

            return Ok(new { Warshah = Warshah, ListOfShift = ListOfShift, City = City, Country = Country, Region = Region });

        }



        [HttpPost, Route("RenewWarshahFromAdmin")]
        public async Task<IActionResult> RenewWarshahFromAdmin(RenewFromAdmin warshahDTO)
        {
            var warshah = _uow.WarshahRepository.GetById(warshahDTO.WarshahId);

            var User = _uow.UserRepository.GetMany(a=>a.WarshahId == warshahDTO.WarshahId && a.RoleId == 1).FirstOrDefault();

            if(User != null )
            {
                var Invoice = _subscribtionsWarshahTech.CreateSubscribtionInvoice(new DL.DTOs.SubscribtionDTOs.SubscribtionInvoiceDTO
                {
                    PeriodSubscribtion = warshahDTO.NoOFMonths,
                    TotalSubscribtion = warshahDTO.TotalPrice,
                    TransactionRef = "Admin",
                    userFirstName = User.FirstName,
                    UserLastName = User.LastName,
                    WarshahId = warshah.Id,
                    UserId = User.Id,
                    WarshahTaxNumber = warshah.TaxNumber,
                    WarshahName = warshah.WarshahNameAr,
                    Describtion = "اشتراك",
                    IntelCardCode = 521,
                    InvoiceTypeId = 2,


                });

                var data = _uow.DurationRepository.GetMany(a => a.WarshahId == warshahDTO.WarshahId).FirstOrDefault();
                data.EndDate = data.EndDate.AddMonths(warshahDTO.NoOFMonths);
                _uow.DurationRepository.Update(data);
                _uow.Save();

                TaxResponse response = new TaxResponse();


                TaxInv _TaxInv = new TaxInv();
                //response = await _TaxInv.CreateXml_and_SendInv(InvoiceID, "Id", "DebitAndCreditors", "IQ_KSATaxInvHeaderDebitAndCredit", "IQ_KSATaxInvItemsDebitAndCredit", "IQ_KSATaxInvHeader_PerPaid", "");
                response = await _TaxInv.CreateXml_and_SendInv(Invoice.Id, "Id", "SubscribtionInvoices", "IQ_KSATaxInvHeaderSub", "IQ_KSATaxInvItemsSub", "IQ_KSATaxInvHeader_PerPaid", "");


                return Ok(data);
            }

            return Ok("this warshah is fake");
        
        }

            [HttpPost, Route("AddWarshahFromAdmin")]
        public async Task<IActionResult> AddWarshahFromAdmin([FromForm] WarshahFromAdminDTO warshahDTO)
        {
           

            if (ModelState.IsValid)
            { //addwarshah 
                if (warshahDTO.WarshahLogo != null)
                {
                    var IsImage = FileCheckHelper.IsImage(warshahDTO.WarshahLogo.OpenReadStream());
                    if (!IsImage)
                    {
                        return BadRequest(new { Erorr = "Only Images Allowed" });
                    }
                    var IsBiggerThan1MB = CheckFileSizeHelper.IsBeggerThan1MB(warshahDTO.WarshahLogo);
                    if (IsBiggerThan1MB)
                    {
                        return BadRequest(new { Erorr = "Only Images Less Than 1MB Allowed" });

                    }
                }
                var Erorrs = _checkUniq.CheckUniqeValue(new DL.DTOs.SharedDTO.UniqeDTO { CivilId = warshahDTO.CivilId , Mobile = warshahDTO.Phone });
                if (Erorrs.Count() > 0)
                {
                    return Ok(new { Erorr = Erorrs });
                }

                var Warshah = _mapper.Map<DL.Entities.Warshah>(warshahDTO);
                Warshah.WarshahIdentifier = "WA-" + Warshah.Id;
                if (warshahDTO.WarshahLogo != null)
                {
                    Warshah.WarshahLogo = FileHelper.FileUpload(warshahDTO.WarshahLogo, _hostingEnvironment, UploadPathes.Logos);

                }
                else
                {
                    Warshah.WarshahLogo = "NoLogo";
                }
                _uow.WarshahRepository.Add(Warshah);
                _uow.Save();

                /////////////////////////////////// add user 
                ///

                SMSCount sMSCount = new SMSCount();
                sMSCount.WarshahId = Warshah.Id;
                sMSCount.MessageRemain = 0;
                _uow.SMSCountRepository.Add(sMSCount);
                _uow.Save();

                var User = _mapper.Map<DL.Entities.User>(warshahDTO);
                User.IsActive = true;
                User.IsPhoneConfirmed = true;
                User.WarshahId = Warshah.Id;

                User.Password = EncryptANDDecrypt.EncryptText(warshahDTO.Password);
                _uow.UserRepository.Add(User);
                _uow.Save();




                var Invoice = _subscribtionsWarshahTech.CreateSubscribtionInvoice(new DL.DTOs.SubscribtionDTOs.SubscribtionInvoiceDTO
                {
                    PeriodSubscribtion = warshahDTO.MonthCount,
                    TotalSubscribtion = warshahDTO.Price,
                    TransactionRef = "Admin",
                    userFirstName = User.FirstName,
                    UserLastName = User.LastName,
                    WarshahId = Warshah.Id,
                    UserId = User.Id,
                    WarshahTaxNumber = Warshah.TaxNumber,
                    WarshahName = Warshah.WarshahNameAr,
                    Describtion = "اشتراك",
                    IntelCardCode = 521,
                    InvoiceTypeId = 2,


                });
                DL.Entities.Subscribtion subscribtion = new DL.Entities.Subscribtion();
               
                Duration Duration = new Duration();
                Duration.StartTime = DateTime.Now;
                Duration.EndDate = DateTime.Now.AddMonths(warshahDTO.MonthCount);
                Duration.WarshahId= Warshah.Id;
                
                _uow.DurationRepository.Add(Duration);
                _uow.Save();

                TaxResponse response = new TaxResponse();


                TaxInv _TaxInv = new TaxInv();
                //response = await _TaxInv.CreateXml_and_SendInv(InvoiceID, "Id", "DebitAndCreditors", "IQ_KSATaxInvHeaderDebitAndCredit", "IQ_KSATaxInvItemsDebitAndCredit", "IQ_KSATaxInvHeader_PerPaid", "");
                response = await _TaxInv.CreateXml_and_SendInv(Invoice.Id, "Id", "SubscribtionInvoices", "IQ_KSATaxInvHeaderSub", "IQ_KSATaxInvItemsSub", "IQ_KSATaxInvHeader_PerPaid", "");

                var warshah2 = _uow.WarshahRepository.GetById(Warshah.Id);
                if (User.RoleId == 1)
                {
                    var s = new
                    {
                        tenancyName = "WA-" + User.WarshahId,
                        name = warshah2.WarshahNameAr,
                        adminEmailAddress = "WA-" + User.Id + "@warshahtech.net"
                    };
                    var json = JsonConvert.SerializeObject(s);
                    var respone = HttpOp.webPostMethodHr(json, "https://hrapi.warshahtech.net/api/services/app/Tenant/CreateForWarshahTech");
                    if (respone != "OK")
                    {
                        // Start Again Cuse The Task From Hr Api Faild
                    }

                    AddUserRoles(User);
                    return Ok(new { User = User, Warshah = warshah2 });

                }
                return Ok(new {User =User ,Warshah=warshah2});
            }
            return Ok(new { User = User, Warshah = warshahDTO });

        }
        [AllowAnonymous]
        [HttpPost, Route("AddWarshah")]
        public async Task<IActionResult> AddWarshah([FromForm] WarshahDTO warshahDTO)
             //public IActionResult AddWarshah( WarshahDTO warshahDTO)
        {
            if (ModelState.IsValid)
            {
                if (warshahDTO.WarshahLogo != null)
                {
                    var IsImage = FileCheckHelper.IsImage(warshahDTO.WarshahLogo.OpenReadStream());
                    if (!IsImage)
                    {
                        return BadRequest(new { Erorr = "Only Images Allowed" });
                    }
                    var IsBiggerThan1MB = CheckFileSizeHelper.IsBeggerThan1MB(warshahDTO.WarshahLogo);
                    if (IsBiggerThan1MB)
                    {
                        return BadRequest(new { Erorr = "Only Images Less Than 1MB Allowed" });

                    }
                }

                var Warshah = _mapper.Map<DL.Entities.Warshah>(warshahDTO);
                Warshah.WarshahIdentifier = "WA-" + Warshah.Id;
                if (warshahDTO.WarshahLogo != null)
                {
                    Warshah.WarshahLogo = FileHelper.FileUpload(warshahDTO.WarshahLogo, _hostingEnvironment, UploadPathes.Logos);

                }
                else
                {
                    Warshah.WarshahLogo = "NoLogo";
                }
                _uow.WarshahRepository.Add(Warshah);
                _uow.Save();
              


                //AddInspectionItemsToWarshah(Warshah.Id);

                //StartPayment
                var Duration = new Duration();
                var SelectedSub = _uow.SubscribtionRepository.GetById(warshahDTO.SubscribtionId);
                var pricebrforcupon = SelectedSub.Price;
                Duration.SubscribtionId = SelectedSub.Id;
                Duration.WarshahId = Warshah.Id;
                Duration.StartTime = DateTime.Now;
                Duration.EndDate = DateTime.Now.AddMonths(SelectedSub.SubDurationInMonths);
                Duration.IsActive = false;
                _uow.DurationRepository.Add(Duration);
                _uow.Save();
             
                decimal Price = 0;
                if (warshahDTO.Coupon)
                {
                    var Cupon = _uow.CuponRepository.GetById(warshahDTO.CouponId);
                    SelectedSub.Price = Cupon.Value;
                   
                }


                if (SelectedSub.Price == 0)
                {
                    //var User = _uow.UserRepository.GetMany(a => a.WarshahId == Warshah.Id).FirstOrDefault();
                    var Invoice = _subscribtionsWarshahTech.CreateSubscribtionInvoice(new DL.DTOs.SubscribtionDTOs.SubscribtionInvoiceDTO
                    {
                        PeriodSubscribtion = SelectedSub.SubDurationInMonths,
                        TotalSubscribtion = 0,
                        TransactionRef = "KSA94",
                        userFirstName = Warshah.WarshahNameAr,
                        UserLastName = Warshah.WarshahNameAr,
                        WarshahId = Warshah.Id,
                        //UserId = Warshah,
                        WarshahTaxNumber = Warshah.TaxNumber,
                        WarshahName = Warshah.WarshahNameAr,
                        Describtion = "اشتراك",
                        IntelCardCode = 521,
                        InvoiceTypeId = 2,


                    });

                    _uow.BalanceBankRepository.Add(new BalanceBank { WarshahId = Warshah.Id, OpeningBalance = 0, CreatedOn = DateTime.Now });
                    _uow.ConfigrationRepository.Add(new Configration { WarshahId = Warshah.Id, GetCustomerAbroval = false, GetVAT = 0 });
                    _uow.ExpensesTypeRepository.Add(new ExpensesType { WarshahId = Warshah.Id, ExpensesCategoryId = 1, ExpensesTypeNameAr = "شركة كهرباء السعودية", ExpensesTypeNameEn = "Saudi Electricity Company", TaxNumber = "302004655210003" });
                    _uow.ExpensesTypeRepository.Add(new ExpensesType { WarshahId = Warshah.Id, ExpensesCategoryId = 2, ExpensesTypeNameAr = "المرتبات والآجور", ExpensesTypeNameEn = "Salaries and Wages", TaxNumber = "" });
                    _uow.LoyalitySettingRepository.Add(new LoyalitySetting { WarshahId = Warshah.Id, LoyalityPointsPerCurrancy = 1 });
                    _uow.SMSCountRepository.Add(new SMSCount { WarshahId = Warshah.Id, MessageRemain = 0 });
                    _uow.Save();

                    return Ok(Warshah);


                 }
                    


                string Link = PaymentOperation.PaymentOperationDo(Duration.Id.ToString(), _config.Value.backEndURL+"api/Payment/Failed", _config.Value.backEndURL + "api/Payment/Success", double.Parse(SelectedSub.Price.ToString()), SelectedSub.SubName);

                var warshah = _uow.WarshahRepository.GetById(Warshah.Id);






                // Add Notification Setting

                if (warshah != null) 
                
                {
                    var namenotifications = _uow.NameNotificationRepository.GetAll().ToHashSet();


                    foreach (var notification in namenotifications)
                    
                    {
                        _uow.NotificationSettingRepository.Add(new NotificationSetting { WarshahId = warshah.Id, NameNotificationId = notification.Id , StatusNotificationId = 2 });
                    }

                    _uow.Save();
                }


                // Add Notification Repair Order
                if (warshah != null)

                {
                    var namenotifications = _uow.NotificationRepairOrderAddingRepository.GetAll().ToHashSet();


                    foreach (var notification in namenotifications)

                    {
                        _uow.NotificationRepairOrderRepository.Add(new NotificationRepairOrder { WarshahId = warshah.Id, NameNotificationId = notification.Id, StatusNotificationId = 2 , Minutes = 0 });
                    }

                    _uow.Save();
                }




                return Ok(new { warshah = Warshah, Link = Link });
                //return Ok(Warshah); 
            }
            return BadRequest(ModelState);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpPost, Route("EditWarshah")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        public IActionResult EditWarshah([FromForm] EditWarshahDTO warshahDTO)
        {
            if (ModelState.IsValid)
            {

                
                var Warshah = _mapper.Map<DL.Entities.Warshah>(warshahDTO);
             
                var OldLogo = _uow.WarshahRepository.GetById(warshahDTO.Id);
                if (OldLogo != null)
                {
                    Warshah.WarshahLogo = OldLogo.WarshahLogo;
                    Warshah.IsOld = OldLogo.IsOld;
                    Warshah.OldWarshahId = OldLogo.OldWarshahId;
                    _uow.WarshahRepository.Update(Warshah);
                    _uow.Save();
                }
                var User = _uow.UserRepository.GetMany(a => a.WarshahId == Warshah.Id && a.RoleId == 1).FirstOrDefault();

                //Edit Warshah and User                تعديل بيانات الورشة و المستخدم

                var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == User.WarshahId & a.NameNotificationId == 23 & a.StatusNotificationId == 1).FirstOrDefault();
                if (notificationActive != null)
                {
                    _NotificationService.SetNotificationTaqnyat(User.Id, "تم تعديل بيانات الورشه ");


                }

                return Ok(Warshah);
            }
            return BadRequest(ModelState);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Recicp)]
        [HttpPost, Route("AddCarOwner")]
        public IActionResult AddCarOwner([FromForm] CarOwnerDTO CarOwnerDTO)
        {
            if (ModelState.IsValid)
            {
                var Erorrs = _checkUniq.CheckUniqeValue(new DL.DTOs.SharedDTO.UniqeDTO { CivilId = CarOwnerDTO.CivilId, Email = CarOwnerDTO.Email, Mobile = CarOwnerDTO.Phone });
                if (Erorrs.Count() > 0)
                {
                    return Ok(new { Erorr = Erorrs });
                }
                var Data = new User();
                if (CarOwnerDTO.IdImage != null)
                {
                    var IsImage = FileCheckHelper.IsImage(CarOwnerDTO.IdImage.OpenReadStream());
                    if (!IsImage)
                    {
                        return BadRequest(new { Erorr = "Only Images Allowed" });
                    }
                    var IsBiggerThan1MB = CheckFileSizeHelper.IsBeggerThan1MB(CarOwnerDTO.IdImage);
                    if (IsBiggerThan1MB)
                    {
                        return BadRequest(new { Erorr = "Only Images Less Than 1MB Allowed" });

                    }
                    Data = _mapper.Map<DL.Entities.User>(CarOwnerDTO);
                    Data.IdImage = FileHelper.FileUpload(CarOwnerDTO.IdImage, _hostingEnvironment, UploadPathes.Logos);
                }
                else
                {
                    Data = _mapper.Map<DL.Entities.User>(CarOwnerDTO);
                    Data.IdImage = "None";

                }

                _uow.UserRepository.Add(Data);
                Data.IsActive = false;
                Data.IsPhoneConfirmed = false;
                Data.WarshahId = null;
                var Pass = CreatePassword(6);
                Data.Password = EncryptANDDecrypt.EncryptText(Pass);
                var enpass = EncryptANDDecrypt.DecryptText(Data.Password);

                _uow.Save();
                var message = "تم تسجيل حسابك كمالك مركبة  بنجاح فى نظام ورشة تك ";
                string bearer = "8a6bd5813cb919536bfded6403f68d14";
                string sender = "WARSHAHTECH";
                _SMS.SendSMS1(bearer, Data.Phone, sender, message);
                //_SMS.SendSMS(Data.Phone, message,Data.WarshahId, false);
                _uow.AuthrizedPersonRepository.Add(new AuthrizedPerson { UserId = Data.Id, FirstName = CarOwnerDTO.AuthPersonFirstName, LastName = CarOwnerDTO.AuthPersonLastName, Phone = CarOwnerDTO.AuthPersonPhone });
                _uow.Save();
                var EncId = EncryptANDDecrypt.EncryptText(Data.Id.ToString());
                VerifyCodeHelper verifyCodeHelper = new VerifyCodeHelper(_uow, _SMS, _mailService);
                verifyCodeHelper.SendOTP(Data.Phone, Data.Id, Data.Email);
                AddUserRoles(Data);
                var AuthrizedPerson = _uow.AuthrizedPersonRepository.GetMany(a => a.UserId == Data.Id);
                return Ok(new { Data, AuthrizedPerson });
            }
            return BadRequest(ModelState);
        }


















        [AllowAnonymous]
        [HttpPost, Route("AddCarOwnerAnonymous")]
        public IActionResult AddCarOwnerAnonymous([FromForm] CarOwnerDTO CarOwnerDTO)
        {
            if (ModelState.IsValid)
            {

                var Erorrs = _checkUniq.CheckUniqeValue(new DL.DTOs.SharedDTO.UniqeDTO { CivilId = CarOwnerDTO.CivilId, Email = CarOwnerDTO.Email, Mobile = CarOwnerDTO.Phone });
                if (Erorrs.Count() > 0)
                {
                    return Ok(new { Erorr = Erorrs });
                }
                var Data = new User();
                if (CarOwnerDTO.IdImage != null)
                {
                    var IsImage = FileCheckHelper.IsImage(CarOwnerDTO.IdImage.OpenReadStream());
                    if (!IsImage)
                    {
                        return BadRequest(new { Erorr = "Only Images Allowed" });
                    }
                    var IsBiggerThan1MB = CheckFileSizeHelper.IsBeggerThan1MB(CarOwnerDTO.IdImage);
                    if (IsBiggerThan1MB)
                    {
                        return BadRequest(new { Erorr = "Only Images Less Than 1MB Allowed" });

                    }
                    Data = _mapper.Map<DL.Entities.User>(CarOwnerDTO);
                    Data.IdImage = FileHelper.FileUpload(CarOwnerDTO.IdImage, _hostingEnvironment, UploadPathes.Logos);
                }
                else
                {
                    Data = _mapper.Map<DL.Entities.User>(CarOwnerDTO);
                    Data.IdImage = "None";

                }

                _uow.UserRepository.Add(Data);             
                Data.WarshahId = null;
                Data.IsActive = false;
                Data.IsPhoneConfirmed = false;
                Data.Password = EncryptANDDecrypt.EncryptText(Data.Password);
                //var message = "تم تسجيل حسابك كمالك مركبة  بنجاح فى نظام ورشة تك "; 

                //_SMS.SendSMS(Data.Phone, message , null , false);
                _uow.UserRepository.Add(Data);
                _uow.Save();

                _uow.AuthrizedPersonRepository.Add(new AuthrizedPerson { UserId = Data.Id, FirstName = CarOwnerDTO.AuthPersonFirstName, LastName = CarOwnerDTO.AuthPersonLastName, Phone = CarOwnerDTO.AuthPersonPhone });
                _uow.Save();
                var EncId = EncryptANDDecrypt.EncryptText(Data.Id.ToString());
                VerifyCodeHelper verifyCodeHelper = new VerifyCodeHelper(_uow, _SMS, _mailService);
                verifyCodeHelper.SendOTP(Data.Phone, Data.Id, Data.Email);
                AddUserRoles(Data);
                var AuthrizedPerson = _uow.AuthrizedPersonRepository.GetMany(a => a.UserId == Data.Id);
                return Ok(new { Data, AuthrizedPerson });
            }
            return BadRequest(ModelState);
        }









        [NonAction]
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpPost, Route("AddRole")]
        public IActionResult AddRole(string RoleName)
        {
            _uow.RoleRepository.Add(new DL.Entities.Role { Name = RoleName });
            _uow.Save();

            return Ok(RoleName);
        }

        [HttpPost, Route("SetLogo")]
        public IActionResult SetLogo([FromForm] LogoDTO dTO)
        {


            if (dTO.Logo != null)
            {
                var IsImage = FileCheckHelper.IsImage(dTO.Logo.OpenReadStream());
                if (!IsImage)
                {
                    return BadRequest(new { Erorr = "Only Images Allowed" });
                }
                var IsBiggerThan1MB = CheckFileSizeHelper.IsBeggerThan1MB(dTO.Logo);
                if (IsBiggerThan1MB)
                {
                    return BadRequest(new { Erorr = "Only Images Less Than 1MB Allowed" });

                }
                var Warshah = _uow.WarshahRepository.GetById(dTO.WarshahId);

                Warshah.WarshahLogo = FileHelper.FileUpload(dTO.Logo, _hostingEnvironment, UploadPathes.Logos);

                _uow.WarshahRepository.Update(Warshah);
                _uow.Save();
                return Ok(Warshah);

            }
            return BadRequest();
        }

        [HttpGet, Route("SetConf")]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        public IActionResult SetConf(int warshahId)
        {
            var Conf = _uow.ConfigrationRepository.GetMany(a => a.WarshahId == warshahId).FirstOrDefault();


            if (Conf == null)
            {
                Configration s = new Configration { GetCustomerAbroval = true , WarshahId = warshahId };
                _uow.ConfigrationRepository.Add(s);
                _uow.Save();
                return Ok(s);


            }


            if (Conf.GetCustomerAbroval == true)
            {
                Conf.GetCustomerAbroval = false;
            }
            else
            {
                Conf.GetCustomerAbroval = true;
            }
            _uow.ConfigrationRepository.Update(Conf);
            _uow.Save();
            return Ok(Conf);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetConf")]
        public IActionResult GetConf(int warshahId)
        {
            var Conf = _uow.ConfigrationRepository.GetMany(a => a.WarshahId == warshahId).FirstOrDefault();

            return Ok(Conf);
        }
        ////   [ClaimRequirement(ClaimTypes.Role, RoleConstant.AddUserRole)]
        //[HttpPost, Route("AddUserRole")]
        //public IActionResult AddUserRole(int UserId, int RoleId)
        //{
        //    _uow.UserRolesRepository.Add(new DL.Entities.UserRoles { RoleId = RoleId, UserId = UserId });
        //    _uow.Save();

        //    return Ok();
        //}
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpGet, Route("GetAllPermissionx")]
        public IActionResult GetAllPermissionx()
        {

            return Ok(_uow.PermissionRepository.GetAll().ToHashSet());
        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetWarshahTechs")]
        public IActionResult GetWarshahTechs(int warshahID)
        {
            var techs = _uow.UserRepository.GetMany(a => a.WarshahId == warshahID && a.RoleId == 3).ToHashSet();
            List<object> Data = new();
            foreach (var item in techs)
            {
                var IsBusy = _uow.RepairOrderRepository.GetMany(a => a.TechId == item.Id).ToHashSet().Count;
                if (IsBusy>0)
                {
                    Data.Add(new
                    {
                        IsBusy = true,
                        Tech = item,
                        OrdersCount = IsBusy
                    });
                }
                else
                {
                    Data.Add(new
                    {
                        IsBusy = false,
                        Tech = item,
                        OrdersCount = IsBusy
                    });
                }
            }

            return Ok(Data);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpDelete, Route("DeleteUser")]
        public IActionResult DeleteUser(int UserID)
        {
            var OldUser = _uow.UserRepository.GetById(UserID);
            OldUser.IsDeleted = true;
            _uow.UserRepository.Update(OldUser);
            _uow.Save();
            return Ok(OldUser);
        }


       
        [HttpPost, Route("testSms")]
        public IActionResult testSms()
        {
            var Phone = "+966582240552";
            var message = " رسالة جديدة  ";
            _SMS.SendSMS(Phone, message, 53 , false);
            return Ok();

        }



        [HttpPost, Route("testSmstaqnyat")]
        public IActionResult testSmstaqnyat()
        {
            Taqnyat.Taqnyat taqnyt = new Taqnyat.Taqnyat();
            string bearer = "8a6bd5813cb919536bfded6403f68d14";
            string body = "رسالة من صلاح ";
            string recipients = "966582240552";
            string sender = "WARSHAHTECH";


            var message = _SMS.SendSMS1(bearer, recipients, sender, body);

           


            //var senders = _SMS.GetSenders(bearer);

            return Ok(message);
        }

       





        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetCarOwnerByWarshahId")]
        public IActionResult GetCarOwnerByWarshahId(int warshahid, int pagenumber, int pagecount)
        {
          var allcarowners =  _uow.WarshahCarOwnersRepository.GetMany(a=>a.WarshahId == warshahid).Include(a=>a.CarOwner).ToHashSet();

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = allcarowners.Count(), ListCarOwners = allcarowners.ToPagedList(pagenumber, pagecount) });
        }




        [ClaimRequirement(ClaimTypes.Role, RoleConstant.TaseerAdmin)]
        [HttpDelete, Route("DeleteUserTaseer")]
        public IActionResult DeleteUserTaseer(int UserID)
        {
            var OldUser = _uow.UserRepository.GetById(UserID);

            _uow.UserRepository.Delete(OldUser.Id);
            _uow.Save();
            return Ok(OldUser);
        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpPost, Route("EditUser")]
        public IActionResult EditUser(EditUserDTO user)
        {
            var CommingUser = _uow.UserRepository.GetById(user.Id);
            if (CommingUser.Email != user.Email)
            {
                var Erorrs = _checkUniq.CheckUniqeValue(new DL.DTOs.SharedDTO.UniqeDTO { CivilId = Guid.NewGuid().ToString(), Email = user.Email, Mobile = Guid.NewGuid().ToString() }); ;
                if (Erorrs.Count() > 0)
                {
                    return Ok(new { Erorr = Erorrs });
                }
            }
            if (CommingUser.Phone != user.Phone)
            {
                var Erorrs = _checkUniq.CheckUniqeValue(new DL.DTOs.SharedDTO.UniqeDTO { CivilId = Guid.NewGuid().ToString(), Email = Guid.NewGuid().ToString(), Mobile = user.Phone }); ;
                if (Erorrs.Count() > 0)
                {
                    return Ok(new { Erorr = Erorrs });
                }
            }
            if (CommingUser.CivilId != user.CivilId)
            {
                var Erorrs = _checkUniq.CheckUniqeValue(new DL.DTOs.SharedDTO.UniqeDTO { CivilId = user.CivilId, Email = Guid.NewGuid().ToString(), Mobile = Guid.NewGuid().ToString() }); ;
                if (Erorrs.Count() > 0)
                {
                    return Ok(new { Erorr = Erorrs });
                }
            }

            user.IsDeleted = false;
            user.IsActive = true;
            user.UpdatedBy = user.Id;
            var CurrentUser = _uow.UserRepository.GetMany(a => a.Id == user.Id).FirstOrDefault();
            var data = _mapper.Map<DL.Entities.User>(user);
            data.Password = CurrentUser.Password;
            _uow.UserRepository.Update(data);
            _uow.Save();

            //Edit Warshah and User                تعديل بيانات الورشة و المستخدم

            var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == user.WarshahId & a.NameNotificationId == 23).FirstOrDefault();
            if (notificationActive != null)
            {


                _NotificationService.SetNotificationTaqnyat(data.Id, "تم تعديل بياناتك الشخصية");
            }
            return Ok(data);
        }


     







        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpGet, Route("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            return Ok(_uow.RoleRepository.GetAll().ToHashSet());
        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]


        [HttpGet, Route("GetUserPermissions")]
        public IActionResult GetUserPermissions(int UserId)
        {
            var UserPermissions = _uow.UserpermissionRepository.GetMany(a => a.UserId == UserId).ToHashSet();
            var permissions = new List<permission>();
            foreach (var item in UserPermissions)
            {

                var Per = _uow.PermissionRepository.GetMany(a => a.Id == item.PermissionId).FirstOrDefault();
                permissions.Add(Per);
            }

            return Ok(new { permissions = permissions, User = _uow.UserRepository.GetById(UserId) });
        }




        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetUser")]
        public IActionResult GetUser(int id)
        {

            return Ok(_uow.UserRepository.GetById(id));
        }




        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]


        [HttpPost, Route("AddUserPermission")]
        public IActionResult AddUserPermission(int UserID, int PermissionId)
        {
            var UserPermission = new UserPermission();
            UserPermission.PermissionId = PermissionId;
            UserPermission.UserId = UserID;
            _uow.UserpermissionRepository.Add(UserPermission);
            _uow.Save();
            return Ok(UserPermission);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpPost, Route("RemoveUserPermission")]
        public IActionResult  RemoveUserPermission(int UserID, int PermissionId)
        {
            var UP = _uow.UserpermissionRepository.GetMany(a => a.PermissionId == PermissionId && a.UserId == UserID).FirstOrDefault();
            _uow.UserpermissionRepository.Delete(UP.Id);
            _uow.Save();
            return Ok();
        }












        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpPost, Route("AddWarshahUser")]
        public IActionResult AddWarshahUser(UserDTO request)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    var Erorrs = _checkUniq.CheckUniqeValue(new DL.DTOs.SharedDTO.UniqeDTO { CivilId = request.CivilId, Email = request.Email, Mobile = request.Phone });
                    if (Erorrs.Count() > 0)
                    {
                        return Ok(new { Erorr = Erorrs });
                    }




                    var User = _mapper.Map<DL.Entities.User>(request);
                    User.IsActive = true;
                    User.IsPhoneConfirmed = true;


                    var Pass = CreatePassword(6);
                    User.Password = EncryptANDDecrypt.EncryptText(Pass);
                    _uow.Save();
                    var enpass = EncryptANDDecrypt.DecryptText(User.Password);
                    var message = "تم تسجيل حسابك بنجاح فى نظام ورشة تك " ;
                    string bearer = "8a6bd5813cb919536bfded6403f68d14";
                    string sender = "WARSHAHTECH";
                    _SMS.SendSMS1(bearer, User.Phone, sender, message);


                    //_SMS.SendSMS(User.Phone, message,User.WarshahId, false);
                    _uow.UserRepository.Add(User);
                    _uow.Save();




                    //var EncId = EncryptANDDecrypt.EncryptText(User.Id.ToString());
                    //VerifyCodeHelper verifyCodeHelper = new VerifyCodeHelper(_uow, _SMS, _mailService);
                    //verifyCodeHelper.SendOTP(User.Phone, User.Id, User.Email);
                    AddUserRoles(User);
                   
                    DL.Entities.Warshah warshah = _uow.WarshahRepository.GetById(User.WarshahId);
                    //var ao = new
                    //{
                    //    tenancyName = "WA-" + warshah.Id,
                    //    employeeCode = "WA-" + User.Id,
                    //    fullNameAr = User.FirstName + " " + User.LastName,
                    //    fullNameEn = User.FirstName + " " + User.LastName,
                    //    mobileNo = User.Phone

                    //};

                    //var json = JsonConvert.SerializeObject(ao);
                    //var respone = HttpOp.webPostMethodHr(json, "https://hrapi.warshahtech.net/api/services/app/Employees/CreateForWarshah");
                    //if (respone != "OK")
                    //{
                    //    // Start Again Cuse The Task From Hr Api Faild
                    //}

                    DataEmployee employee = new DataEmployee();


                    employee.WarshahId = (int)User.WarshahId;
                    employee.UserWarshahCode = User.Id;

                    // Get last EmployeesID number for each warshash

                    int lastId = 0;
                    var EmployeesIDlast = _uow.DataEmployeeRepository.GetMany(i => i.WarshahId == employee.WarshahId).OrderByDescending(t => t.Id).FirstOrDefault();
                    if (EmployeesIDlast == null)
                    {
                        lastId = 1;
                    }
                    else
                    {
                        int lastnumber = EmployeesIDlast.Id;
                        lastId = lastnumber + 1;
                    }

                    employee.AttendanceCode = "Emp-" + employee.WarshahId + "-" + lastId;

                    employee.EmployeeNameAr = User.FirstName + " " + User.LastName;
                    employee.MobileNo = User.Phone;
                    employee.RoleId = User.RoleId;
                    employee.StatusEmploymentId = 1;
                    employee.CreatedOn = DateTime.Now;
                    _uow.DataEmployeeRepository.Add(employee);
                    _uow.Save();



                    return Ok(new { User = User, Employee = employee });

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }

            }
            return BadRequest("Invalid Username or Password");
        }






        [ClaimRequirement(ClaimTypes.Role, RoleConstant.TaseerAdmin)]

        [HttpPost, Route("AddTaseerUser")]
        public IActionResult AddTaseerUser(UserDTO request)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    var Erorrs = _checkUniq.CheckUniqeValue(new DL.DTOs.SharedDTO.UniqeDTO { CivilId = request.CivilId, Email = request.Email, Mobile = request.Phone });
                    if (Erorrs.Count() > 0)
                    {
                        return Ok(new { Erorr = Erorrs });
                    }




                    var User = _mapper.Map<DL.Entities.User>(request);
                    User.IsActive = true;
                    User.IsPhoneConfirmed = true;


                    var Pass = CreatePassword(6);
                    User.Password = EncryptANDDecrypt.EncryptText(Pass);
                    _uow.Save();
                    var message = "كلمة المرور الخاصة بك فى نظام ورشة تك هى :" + Pass;
                    string bearer = "8a6bd5813cb919536bfded6403f68d14";
                    string sender = "WARSHAHTECH";
                    _SMS.SendSMS1(bearer, User.Phone, sender, message);
                    //_SMS.SendSMS(User.Phone, message,User.WarshahId, false);
                    _uow.UserRepository.Add(User);
                    _uow.Save();





                    //var EncId = EncryptANDDecrypt.EncryptText(User.Id.ToString());
                    //VerifyCodeHelper verifyCodeHelper = new VerifyCodeHelper(_uow, _SMS, _mailService);
                    //verifyCodeHelper.SendOTP(User.Phone, User.Id, User.Email);
                    AddUserRoles(User);
                    return Ok(User);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }

            }
            return BadRequest("Invalid Username or Password");
        }


        [HttpGet, Route("GetTaseerUsers")]
        public IActionResult GetTaseerUsers()
        {
            var Users = _uow.UserRepository.GetMany(a => a.RoleId == 8 || a.RoleId == 9 || a.RoleId == 10 || a.RoleId == 7).ToHashSet();
            return Ok(Users);
        }
        [HttpGet, Route("GetMessageCount")]
        public IActionResult GetMessageCount(int warshahID)
        {
            var Users = _uow.SMSCountRepository.GetMany(a => a.WarshahId == warshahID).FirstOrDefault();
            return Ok(Users);
        }

        [HttpPost, Route("AddMessageSMS")]
        public IActionResult AddMessageSMS(AddSMSDTO sMSDTO)
        {
            //SMSCount sMSCount = new SMSCount();
            //sMSCount.WarshahId = 222;
            //sMSCount.MessageRemain = 10;
            //_uow.SMSCountRepository.Add(sMSCount);
            //_uow.Save();
            var MSGs = _uow.SMSCountRepository.GetMany(a => a.WarshahId == sMSDTO.WarshahId).FirstOrDefault();
            MSGs.MessageRemain += sMSDTO.MSGCount;
            _uow.SMSCountRepository.Update(MSGs);
            _uow.Save();
            return Ok(MSGs);
        }


        [AllowAnonymous]
        [HttpPost, Route("Register")]
        public IActionResult Register(UserDTO request)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    var Erorrs = _checkUniq.CheckUniqeValue(new DL.DTOs.SharedDTO.UniqeDTO { CivilId = request.CivilId, Email = request.Email, Mobile = request.Phone });
                    if (Erorrs.Count() > 0)
                    {
                        return Ok(new { Erorr = Erorrs });
                    }




                    var User = _mapper.Map<DL.Entities.User>(request);
                    User.IsActive = false;
                    User.IsPhoneConfirmed = false;

                    User.Password = EncryptANDDecrypt.EncryptText(request.Password);
                    _uow.UserRepository.Add(User);
                    _uow.Save();





                    var EncId = EncryptANDDecrypt.EncryptText(User.Id.ToString());
                    VerifyCodeHelper verifyCodeHelper = new VerifyCodeHelper(_uow, _SMS, _mailService);
                    verifyCodeHelper.SendOTP(User.Phone, User.Id, User.Email);
                    var warshah = _uow.WarshahRepository.GetById(User.WarshahId);
                    if (User.RoleId == 1)
                    {
                        var s = new
                        {
                            tenancyName = "WA-" + User.WarshahId,
                            name = warshah.WarshahNameAr,
                            adminEmailAddress = "WA-" + User.Id + "@warshahtech.net"
                        };
                        var json = JsonConvert.SerializeObject(s);
                        var respone = HttpOp.webPostMethodHr(json, "https://hrapi.warshahtech.net/api/services/app/Tenant/CreateForWarshahTech");
                        if (respone != "OK")
                        {
                            // Start Again Cuse The Task From Hr Api Faild
                        }

                        AddUserRoles(User);
                        return Ok(User);
                    }
                    return Ok(User);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }

            }
            return BadRequest("Invalid Username or Password");
        }

        [AllowAnonymous]
        [HttpGet, Route("DeleteUserByEmail")]
        public IActionResult DeleteUserByEmail(string Mail)
        {
            var User = _uow.UserRepository.GetMany(a => a.Email == Mail).FirstOrDefault();
            if (User != null)
            {
                _uow.UserRepository.Delete(User.Id);
                _uow.Save();
                return Ok("User Deleted " + Mail);
            }
            return Ok("No User To Delete");

        }






        [AllowAnonymous]
        [HttpPost, Route("ResendActivation")]
        public IActionResult ResendActivation(ForgetPassDTO forgetPassDTO)
        {

            var User = _uow.UserRepository.GetMany(a => a.Phone == forgetPassDTO.MobileNum).FirstOrDefault();
            if (User != null)
            {

                VerifyCodeHelper verifyCodeHelper = new VerifyCodeHelper(_uow, _SMS, _mailService);
                verifyCodeHelper.SendOTP(User.Phone, User.Id, User.Email);


                return Ok(new { Status = "Sent" });
            }
            return BadRequest(new { Error = "No User With This Mail" });
        }




        [AllowAnonymous]
        [HttpGet, Route("ActivateAccount")]
        public IActionResult ActivateAccount([FromQuery] string EncId)
        {
            try
            {
                var decodedId = EncId.Replace(" ", "+");
                var Id = EncryptANDDecrypt.DecryptText(decodedId);
                var IntId = int.Parse(Id);
                var User = _uow.UserRepository.GetById(IntId);
                User.IsActive = true;
                _uow.UserRepository.Update(User);
                _uow.Save();

                return Redirect("http://161.97.65.140:10004/#/session/account-activated");

            } 
            catch (Exception ex)
            {
                _log.LogErorr(new Log { ActionName = "ActivateAccount", ControllerName = "User", Message = ex.ToString() });

                return Content(ex.ToString());
            }


        }
        [NonAction]
        private static string DecodeUrlString(string url)
        {
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }
        [HttpGet,Route("DecodePass")]
        public IActionResult DecodePass(string Pass)
        {
            return Ok(EncryptANDDecrypt.DecryptText(Pass));

           
        }


        [HttpGet, Route("ReturnPass")]
        public IActionResult ReturnPass(string Pass)
        {
            return Ok(EncryptANDDecrypt.EncryptText(Pass));

           
        }


        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpPost, Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var OldHashedPass = EncryptANDDecrypt.EncryptText(changePasswordDto.OldPassword);
                    var User = _uow.UserRepository.GetMany(a => a.Id == changePasswordDto.UserId && a.Password == OldHashedPass).FirstOrDefault();
                    if (User != null)
                    {
                        User.Password = EncryptANDDecrypt.EncryptText(changePasswordDto.NewPassword);
                        _uow.UserRepository.Update(User);
                        _uow.Save();
                        _log.LogAudit(new Log { ActionName = "ChangePassword", ControllerName = "User", Message = "Ok" });

                        //Edit Warshah and User                تعديل بيانات الورشة و المستخدم

                        var notificationActive = _uow.NotificationSettingRepository.GetMany(a => a.WarshahId == User.WarshahId & a.NameNotificationId == 23).FirstOrDefault();
                        if (notificationActive != null)
                        {


                            _NotificationService.SetNotificationTaqnyat(User.Id, "تم تغيير كلمة المرور الخاصة بك ");
                        }

                      

                        return Ok(new { MSG = "Password Changed" });
                    }
                    return BadRequest(new { erorr = "Wrong Password Or  User " });

                }
                catch (Exception ex)
                {
                    _log.LogErorr(new Log { ActionName = "ChangePassword", ControllerName = "User", Message = ex.InnerException.ToString() });

                    return BadRequest(ex);
                }
            }
            return BadRequest(ModelState);



        }
        public class ForgetPassDTO
        {
            public string MobileNum { get; set; }
        }
        [AllowAnonymous]
        [HttpPost, Route("ForgetPassword")]
        public IActionResult ForgetPassword(ForgetPassDTO MobileNum)
        {
            var User = _uow.UserRepository.GetMany(a => a.Phone == MobileNum.MobileNum).FirstOrDefault();


            User = _uow.UserRepository.GetMany(a => a.Phone == MobileNum.MobileNum).FirstOrDefault();




            if (User != null)
            {
                try
                {
                    VerifyCodeHelper verifyCodeHelper = new VerifyCodeHelper(_uow, _SMS, _mailService);
                    verifyCodeHelper.SendOTP(User.Phone, User.Id, User.Email);
                    _log.LogAudit(new Log { ActionName = "ForgetPassword", ControllerName = "User", Message = "Ok" });

                    return Ok(new { Status = "Sent" });
                }
                catch (Exception ex)
                {
                    _log.LogErorr(new Log { ActionName = "ForgetPassword", ControllerName = "User", Message = ex.Message.ToString() });

                    return BadRequest(new { erorr = ex });

                }
            }
            return BadRequest(new { Erorr = "No Such User" });


        }
        [AllowAnonymous]
        [HttpGet, Route("ForgetPasswordOTP")]
        public IActionResult ForgetPasswordOTP([FromQuery] string OTPCode)
        {
            var OtpCode = int.Parse(OTPCode);
            var VC = _uow.VerfiyCodeRepository.GetMany(a => a.VirfeyCode == OtpCode).FirstOrDefault();
            if (VC != null)
            {
                int res = DateTime.Compare(VC.Date, DateTime.Now);
                if (res > 0)
                {
                    var User = _uow.UserRepository.GetMany(a => a.Id == VC.UserId).FirstOrDefault();
                    if (User != null)
                    {
                        var EncUserId = EncryptANDDecrypt.EncryptText(User.Id.ToString());
                        _uow.VerfiyCodeRepository.Delete(VC.Id);
                        _uow.Save();
                        _log.LogAudit(new Log { ActionName = "ForgetPasswordOTP", ControllerName = "User", Message = "Ok" });

                        return Ok(new { EncryptedUserId = EncUserId });
                    }
                    return BadRequest(new { Erorr = " No User For This Code" });

                }
                return BadRequest(new { Erorr = " Code Expired" });
            }
            return BadRequest(new { Erorr = "No Such Code" });
        }

        [AllowAnonymous]
        [HttpPost, Route("ForgetPasswordPost")]
        public IActionResult ForgetPasswordPost([FromBody] ForgetPasswordDTO forgetPasswordDTO)
        {
            var IdDec = EncryptANDDecrypt.DecryptText(forgetPasswordDTO.EncId);
            var UserId = Convert.ToInt32(IdDec);
            var User = _uow.UserRepository.GetById(UserId);
            if (User != null)
            {
                User.Password = EncryptANDDecrypt.EncryptText(forgetPasswordDTO.NewPassword);
                _uow.UserRepository.Update(User);
                _uow.Save();
                _log.LogAudit(new Log { ActionName = "ForgetPasswordPost", ControllerName = "User", Message = "Ok" });

                return Ok(new { MSG = "Password Changed" });
            }
            return BadRequest(new { erorr = "Worng User Id " });

        }


        [AllowAnonymous]
        [HttpGet, Route("ActivateAccountOTP")]
        public IActionResult ActivateViaCode(int VirifyCode)
        {

            VerifyCodeHelper verifyCodeHelper = new VerifyCodeHelper(_uow, _SMS, _mailService);
            var Result = verifyCodeHelper.ActivateOTP(VirifyCode);

            if (Result)
            {

                return Ok(Result);
            }
            return BadRequest();
        }
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetUserByPhone")]
        public IActionResult GetUserByPhone(string Phone)
        {
            if (Phone != null)
            {
                if (Phone.StartsWith(" "))
                {
                    Phone = Phone.Replace(" ", "+");
                }
            }

            var User = _uow.UserRepository.GetMany(a =>a.Phone == Phone).FirstOrDefault();
            if (User != null && User.RoleId == 2)
            {
                var auth = _uow.AuthrizedPersonRepository.GetMany(a => a.UserId == User.Id).FirstOrDefault();
                return Ok(new { User, auth , flag = 1 });
            }

            if(User != null && User.RoleId != 2 )
            {
                var auth = _uow.AuthrizedPersonRepository.GetMany(a => a.UserId == User.Id).FirstOrDefault();
                return Ok(new { User, auth , Flag = 2  });
            }


            return Ok(new { User , Flag = 3});


        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetUserByPhoneDiscount")]
        public IActionResult GetUserByPhoneDiscount(string Phone)
        {
            if (Phone != null)
            {
                if (Phone.StartsWith(" "))
                {
                    Phone = Phone.Replace(" ", "+");
                }
            }

            var User = _uow.UserRepository.GetMany(a => a.RoleId == 2 && a.Phone == Phone).FirstOrDefault();
            if (User != null)
            {
                var Discount = _uow.PersonDiscountRepository.GetMany(a => a.CarOwnerId == User.Id).ToHashSet();

                var auth = _uow.AuthrizedPersonRepository.GetMany(a => a.UserId == User.Id).FirstOrDefault();
                return Ok(new { User, auth , Discount });
            }
            return Ok(new { User });


        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("SetVat")]
        public IActionResult SetVat(SetVatDTO setVatDTO)
        {
            var conf = _uow.ConfigrationRepository.GetMany(a => a.WarshahId == setVatDTO.WarshahId).FirstOrDefault();
            if (conf == null)
            {
                Configration s = new Configration { GetVAT = setVatDTO.Vat , WarshahId = setVatDTO.WarshahId };
                _uow.ConfigrationRepository.Add(s);
                _uow.Save();
                return Ok(s);


            }
            conf.GetVAT = setVatDTO.Vat;
            _uow.ConfigrationRepository.Update(conf);
            _uow.Save();
            return Ok(conf);
        }


        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpGet, Route("GetAllWarshahTechServiceByWarshahId")]
        public IActionResult GetAllWarshahTechServiceByWarshahId(int Warshahid)
        {
            var warshahservice = _uow.WarshahTechServiceRepository.GetMany(a => a.WarshahId == Warshahid).ToHashSet(); 
            return Ok(warshahservice);
        }

        [AllowAnonymous]
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetUserByEmail")]
        public IActionResult GetUserByEmail(string Email)
        {


            var User = _uow.UserRepository.GetMany(a => a.RoleId == 2 && a.Email == Email).FirstOrDefault();
            if (User != null)
            {
                var auth = _uow.AuthrizedPersonRepository.GetMany(a => a.UserId == User.Id).FirstOrDefault();
                return Ok(new { User, auth });
            }
            return Ok(new { User });
        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Sales)]
        [HttpGet, Route("GetWarshahInfo")]
        public IActionResult GetWarshahInfo(int warshahId)
        {
            var w = _uow.WarshahRepository.GetById(warshahId);

            object Warshah = new
            {
                Name = w.WarshahNameAr,
                Logo = w.WarshahLogo,
                LandLine = w.LandLineNum,
                Country = _uow.CountryRepository.GetById(w.CountryId).CountryNameAr,
                Region = _uow.RegionRepository.GetById(w.RegionId).RegionNameAr,
                City = _uow.CityRepository.GetById(w.CityId).CityNameAr
            };
            var WarshahUser = _uow.UserRepository.GetMany(a => a.WarshahId == warshahId && a.RoleId == 1).FirstOrDefault();
            WarshahUser.Password = "";

            return Ok(new { Warshah = Warshah, WarshahUser = WarshahUser });
        }
       // [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpGet, Route("GetWarshahBranches")]
        public IActionResult GetWarshahBranches(int warshahId)
        {
            var w = _uow.WarshahRepository.GetMany(a=>a.ParentWarshahId==warshahId).ToHashSet();
            List<object> d = new List<object>();
            foreach (var item in w)
            {
                var User = _uow.UserRepository.GetMany(a => a.WarshahId == item.Id && a.RoleId == 1).FirstOrDefault();
                User.Password = EncryptANDDecrypt.DecryptText(User.Password);
                var op = new
                {
                    User = User,
                    Warshah = item
                };
                d.Add(op);
            }
            return Ok(d);
        }

        [AllowAnonymous]
        [HttpPost, Route("ActivateEmailAccount")]
        public IActionResult ActivateEmailAccount(string Email)
        {
            try
            {
                var User = _uow.UserRepository.GetAll().Where(a => a.Email.ToLower() == Email.ToLower()).FirstOrDefault();
                User.IsActive = true;
                _uow.UserRepository.Update(User);
                _uow.Save();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }


        }
        //warshah Owner Roles = 1 
        //Car Owner Roles = 2 
        //Tech  Roles = 3
        //Receptionist  Roles = 4 
        //acountant Roles =5 
        //System Admin Roles = 6
        //TaseerAdmin (Sales Admin) Role = 7
        //TaseerLeader Role=8
        //Taseer Addountant Role = 9
        //Sales Role = 10

        [NonAction]
        public void AddUserRoles(User user)
        {
            //warshah Owner Roles
            if (user.RoleId == 1)
            {
                Type t = typeof(RoleConstant);
                FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
                permission[] permissions = new permission[fields.Length];


                for (int i = 0; i < fields.Length; i++)
                {

                    _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = i + 1, UserId = user.Id });



                }
                _uow.Save();
                //Car Owner Roles
            }
            else if (user.RoleId == 2)
            {
                var Per = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();

                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                _uow.Save();
            }
            //HrRole
            else if (user.RoleId == 11)
            {
                var Per = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.Hr).FirstOrDefault();
                var Per2 = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();

                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per2.Id, UserId = user.Id });
                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });



                _uow.Save();
            }
            //System Admin Roles
            else if (user.RoleId == 6)
            {
                Type t = typeof(RoleConstant);
                FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
                permission[] permissions = new permission[fields.Length];


                for (int i = 0; i < fields.Length; i++)
                {

                    _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = i + 1, UserId = user.Id });



                }
                _uow.Save();
            }
            //acountant
            else if (user.RoleId == 5)
            {
                var Per = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.Accountant).FirstOrDefault();
                var Per2 = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();

                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per2.Id, UserId = user.Id });
                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                _uow.Save();
            }
            //Tech  Roles
            else if (user.RoleId == 3)
            {
                var Per = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.tech).FirstOrDefault();
                var Per2 = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();

                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per2.Id, UserId = user.Id });
                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                _uow.Save();
            } //Receptionist  Roles
            else if (user.RoleId == 4)
            {
                var Per = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.Recicp).FirstOrDefault();
                var Per2 = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();

                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per2.Id, UserId = user.Id });
                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                _uow.Save();
            }
            //Sales Role
            else if (user.RoleId == 10)
            {
                var Per = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.Sales).FirstOrDefault();
                var Per2 = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();



                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });

                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per2.Id, UserId = user.Id });




                _uow.Save();
            }
            //TaseerAdmin (Sales Admin) Role
            else if (user.RoleId == 7)
            {
                var Per = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.TaseerAdmin).FirstOrDefault();



                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                _uow.Save();
            }
            //TaseerLeader Role
            else if (user.RoleId == 8)
            {
                var Per = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.TaseerLeader).FirstOrDefault();



                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                _uow.Save();
            }
            //Taseer Addountant Role
            else if (user.RoleId == 9)
            {
                var Per = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.TaseerAccountant).FirstOrDefault();
                var Per2 = _uow.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();
                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });
                _uow.UserpermissionRepository.Add(new UserPermission { PermissionId = Per2.Id, UserId = user.Id });




                _uow.Save();
            }



        }



    }
}
