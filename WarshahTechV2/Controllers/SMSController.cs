using BL.Infrastructure;
using DL.DTOs.SharedDTO;
using DL.Entities;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using HELPER;
using System.Security.Claims;
using WarshahTechV2.Models;


namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]

    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IOptions<MyConfig> _config;
        public SMSController(IUnitOfWork uow, IOptions<MyConfig> _config)
        {
            this.uow = uow;
            this._config = _config;
        }

        [HttpGet,Route("GetSMSCountForWarshah")]
        public IActionResult GetSMSCountForWarshah(int warshahId)
        {
            var SMS = uow.SMSCountRepository.GetMany(a => a.WarshahId == warshahId).FirstOrDefault();
            return Ok(SMS);
        }

        [HttpPost,Route("BuySMSMessages")]
        public IActionResult BuySMSMessages(BuySMSDTO buySMSDTO)
        {
            var Package = uow.SMSPackageRepository.GetById(buySMSDTO.SMSPackageId);
            SMSInvoice sMSInvoice = new SMSInvoice();
            sMSInvoice.Describtion = Package.Name;
            sMSInvoice.QTY = Package.SMSCount;
            sMSInvoice.WarshahId = buySMSDTO.warshahId;
            sMSInvoice.Date = System.DateTime.Now;
            sMSInvoice.IsActive = false;
            sMSInvoice.item = "SMS Message";
            sMSInvoice.TansactionId = " ";
            sMSInvoice.Vat = Package.Price * (decimal)0.15;
            sMSInvoice.SuTotal = Package.Price - sMSInvoice.Vat;
            uow.SMSInvoiceRepository.Add(sMSInvoice);
            uow.Save();
            //var smscountcurrent = uow.SMSCountRepository.GetMany(a => a.WarshahId == buySMSDTO.warshahId).FirstOrDefault();
            //if (smscountcurrent != null)
            //{
            //    var currentcount = smscountcurrent.MessageRemain;
            //    smscountcurrent.MessageRemain = currentcount + Package.SMSCount;
            //    uow.SMSCountRepository.Update(smscountcurrent);

            //}

            var CallBackUrl = "https://one.warshahtech.sa/api/Payment/FailedSMS";
            var ReturnUrl = "https://one.warshahtech.sa/api/Payment/SucsessSMS";
            string Link = PaymentOperation.PaymentOperationDo(sMSInvoice.Id.ToString(), CallBackUrl, ReturnUrl, double.Parse(Package.Price.ToString()), Package.Name);
            return Ok(new {Link = Link});

        }
    }
}
