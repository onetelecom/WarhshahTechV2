using BL.Infrastructure;
using DL.DTOs.SharedDTO;
using DL.DTOs.UserDTOs;
using DL.Entities;
using Helper;
using KSAEinvoice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WarshahTechV2.Models;
using Subscribtion = DL.Entities.Subscribtion;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IOptions<MyConfig> _config;
        private readonly ISubscribtionsWarshahTech _subscribtionsWarshahTech;
        public PaymentController(IUnitOfWork uow, IOptions<MyConfig> config, ISubscribtionsWarshahTech subscribtionsWarshahTech)
        {
            _config = config;
            _uow = uow;
            _subscribtionsWarshahTech=subscribtionsWarshahTech;

        }
        [HttpPost]
        [Route("Success")]
        public async Task<IActionResult> Sucsess([FromForm] PaymentRespone paymentResponse)
        {
            if (paymentResponse.respStatus == "A")
            {

                //if (DateTime.Compare(DateTime.Now,Duration.EndDate)<0)
                //{
                //    Duration.StartTime = Duration.EndDate;
                //    Duration.EndDate = Duration.StartTime.AddMonths(SelectedSub.SubDurationInMonths);
                //    _uow.DurationRepository.Update(Duration);
                //    _uow.Save();
                //    var Invoice2 = _subscribtionsWarshahTech.CreateSubscribtionInvoice(new DL.DTOs.SubscribtionDTOs.SubscribtionInvoiceDTO
                //    {
                //        PeriodSubscribtion = SelectedSub.SubDurationInMonths,
                //        TotalSubscribtion = SelectedSub.Price,
                //        TransactionRef = paymentResponse.tranRef,
                //        userFirstName = User.FirstName,
                //        UserLastName = User.LastName,
                //        WarshahId = Warshah.Id,
                //        UserId = User.Id,
                //        WarshahTaxNumber = Warshah.TaxNumber,
                //        WarshahName = Warshah.WarshahNameAr,


                //    });
                //    _uow.BalanceBankRepository.Add(new BalanceBank { WarshahId = Warshah.Id, OpeningBalance = 0, CreatedOn = DateTime.Now });
                //    _uow.ConfigrationRepository.Add(new Configration { WarshahId = Warshah.Id, GetCustomerAbroval = false, GetVAT = 15 });
                //    _uow.ExpensesTypeRepository.Add(new ExpensesType { WarshahId = Warshah.Id, ExpensesCategoryId = 1, ExpensesTypeNameAr = "شركة كهرباء السعودية", ExpensesTypeNameEn = "Saudi Electricity Company", TaxNumber = "302004655210003" });
                //    _uow.ExpensesTypeRepository.Add(new ExpensesType { WarshahId = Warshah.Id, ExpensesCategoryId = 2, ExpensesTypeNameAr = "المرتبات والآجور", ExpensesTypeNameEn = "Salaries and Wages", TaxNumber = "" });
                //    _uow.LoyalitySettingRepository.Add(new LoyalitySetting { WarshahId = Warshah.Id, LoyalityPointsPerCurrancy = 1 });
                //   _uow.SMSCountRepository.Add(new SMSCount { WarshahId = Warshah.Id,MessageRemain = 0});
                //    _uow.Save();
                //    return Redirect(_config.Value.FrontEndURL + "payment-done?Ref=" + paymentResponse.tranRef + "&Code=" + paymentResponse.respStatus + "&InvoiceId=" + Invoice2.Id);

                //}

                int.TryParse(paymentResponse.cartId, out var IntDurationId);
                var Duration = _uow.DurationRepository.GetById(IntDurationId);
                var SelectedSub = _uow.SubscribtionRepository.GetById(Duration.SubscribtionId);
                var Warshah = _uow.WarshahRepository.GetById(Duration.WarshahId);
                var User = _uow.UserRepository.GetMany(a => a.WarshahId == Warshah.Id).FirstOrDefault();
                Duration.TransactionInfo = paymentResponse.tranRef;
                Duration.IsActive = true;
                Duration.IsDeleted = false;
                Duration.StartTime = DateTime.Now;
                Duration.EndDate = DateTime.Now.AddMonths(SelectedSub.SubDurationInMonths);
                _uow.DurationRepository.Update(Duration);
                _uow.Save();
                var Invoice = _subscribtionsWarshahTech.CreateSubscribtionInvoice(new DL.DTOs.SubscribtionDTOs.SubscribtionInvoiceDTO
                {
                    PeriodSubscribtion = SelectedSub.SubDurationInMonths,
                    TotalSubscribtion=SelectedSub.Price,
                    TransactionRef= paymentResponse.tranRef,
                    userFirstName = User.FirstName,
                    UserLastName = User.LastName,
                    WarshahId=Warshah.Id,
                    UserId = User.Id,
                    WarshahTaxNumber=Warshah.TaxNumber,
                    WarshahName=Warshah.WarshahNameAr,
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


                TaxResponse response = new TaxResponse();


                TaxInv _TaxInv = new TaxInv();
                //response = await _TaxInv.CreateXml_and_SendInv(InvoiceID, "Id", "DebitAndCreditors", "IQ_KSATaxInvHeaderDebitAndCredit", "IQ_KSATaxInvItemsDebitAndCredit", "IQ_KSATaxInvHeader_PerPaid", "");
                response = await _TaxInv.CreateXml_and_SendInv(Invoice.Id, "Id", "SubscribtionInvoices", "IQ_KSATaxInvHeaderSub", "IQ_KSATaxInvItemsSub", "IQ_KSATaxInvHeader_PerPaid", "");
                return Redirect(_config.Value.FrontEndURL + "payment-done?Ref=" + paymentResponse.tranRef + "&Code=" + paymentResponse.respStatus + "&InvoiceId=" + Invoice.Id);

            }
            return Redirect(_config.Value.FrontEndURL + "payment-error");

        }
        [HttpPost]
        [Route("Failed")]
        public IActionResult Failed([FromForm] PaymentRespone paymentResponse)
        {
            return Ok(paymentResponse);
        }



        [HttpPost]
        [Route("SucsessSMS")]
        public IActionResult SucsessSMS([FromForm] PaymentRespone paymentResponse)
        {
            if (paymentResponse.respStatus == "A")
            {
                int.TryParse(paymentResponse.cartId, out var IntDurationId);
                var sMSInvoice = _uow.SMSInvoiceRepository.GetById(IntDurationId);
                var Warshah = _uow.WarshahRepository.GetById(sMSInvoice.WarshahId);

                sMSInvoice.TansactionId = paymentResponse.tranRef;
                sMSInvoice.IsActive = true;
                sMSInvoice.IsDeleted = false;
            
                sMSInvoice.Date = DateTime.Now;
             
                _uow.SMSInvoiceRepository.Update(sMSInvoice);
                _uow.Save();
                var SMSCount = _uow.SMSCountRepository.GetMany(a => a.WarshahId == sMSInvoice.WarshahId).FirstOrDefault();
                SMSCount.MessageRemain += sMSInvoice.QTY;
                _uow.SMSCountRepository.Update(SMSCount);
                _uow.Save();
                return Redirect(_config.Value.FrontEndURL + "payment-done?Ref=" + paymentResponse.tranRef + "&Code=" + paymentResponse.respStatus + "&InvoiceId=" + sMSInvoice.Id);

            }
            return Redirect(_config.Value.FrontEndURL + "payment-error");

        }
        [HttpPost]
        [Route("FailedSMS")]
        public IActionResult FailedSMS([FromForm] PaymentRespone paymentResponse)
        {
            return Ok(paymentResponse);
        }

        [HttpGet, Route("PaySub")]
        public IActionResult PaySub(int warshahId,string? copun)
            { 
          
            var Duration = _uow.DurationRepository.GetMany(a => a.WarshahId == warshahId).FirstOrDefault();
            var SelectedSub = _uow.SubscribtionRepository.GetById(Duration.SubscribtionId);
            var Price = SelectedSub.Price;
            if (copun != null)
            {

                var Cupon = _uow.CuponRepository.GetMany(a=>a.CuponName== copun).FirstOrDefault();
                if (Cupon != null)
                {
                    SelectedSub.Price = Cupon.Value;
                    _uow.CuponHistoryRepository.Add(new CuponHistory { BeforeCupon = Price, AfterCupon = Cupon.Value, CopunName = Cupon.CuponName, warshahId = warshahId, CuponValue = Cupon.Value.ToString() }) ;
                    _uow.Save();
                }

            }
            string Link = PaymentOperation.PaymentOperationDo(Duration.Id.ToString(), _config.Value.backEndURL + "api/Payment/Failed", _config.Value.backEndURL + "api/Payment/Success", double.Parse(SelectedSub.Price.ToString()), SelectedSub.SubName);
            return Ok(new { Link = Link });

        }

        [HttpPost, Route("Renewal")]
        public IActionResult Renewal(PaymentRenewalDTO paymentRenewalDTO)
        {
           
            var Duration = _uow.DurationRepository.GetMany(a=>a.WarshahId==paymentRenewalDTO.WarshahId).FirstOrDefault();
            var SelectedSub = _uow.SubscribtionRepository.GetById(paymentRenewalDTO.SubscribtionId);
            Duration.SubscribtionId = SelectedSub.Id;
            _uow.DurationRepository.Update(Duration);
            _uow.Save();
            string Link = PaymentOperation.PaymentOperationDo(Duration.Id.ToString(), _config.Value.backEndURL + "api/Payment/Failed", _config.Value.backEndURL + "api/Payment/Success", double.Parse(SelectedSub.Price.ToString()), SelectedSub.SubName);
            return Ok(new { Link = Link });

        }
        [HttpGet,Route("SubInfo")]
        public IActionResult SubInfo(int warshahId)
        {
            Subscribtion SelectedSub = new Subscribtion();
            var Duration = _uow.DurationRepository.GetMany(a => a.WarshahId == warshahId).FirstOrDefault();
            if (Duration.SubscribtionId!=null)
            {
               SelectedSub = _uow.SubscribtionRepository.GetById(Duration.SubscribtionId);

            }
            if (Duration.SubscribtionId == null )
            {

                SelectedSub.SubName = "From Admin";
            }
            var Res = new
            {
                SubName = SelectedSub.SubName,
                From = Duration.StartTime,
                To = Duration.EndDate
            };

            return Ok(Res);

        }


    }
}
