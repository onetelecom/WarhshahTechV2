using AutoMapper;
using BL.Infrastructure;
using ClosedXML.Excel;
using DL.MailModels;
using DocumentFormat.OpenXml.Wordprocessing;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper _mapper;
        private readonly ISMS _SMS;
        private readonly IMailService _MailService;

        public MarketingController(IUnitOfWork uow, IMapper _mapper, ISMS sMS, IMailService MailService)
        {
            this.uow = uow;
            this._mapper = _mapper;
            _SMS = sMS;
            _MailService = MailService;
        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpGet, Route("SendMessageToAllCustomer")]
        public IActionResult SendMessageToAllCustomer(int warshahId, string Message, bool SMS, bool Email)
        {
            string bearer = "8a6bd5813cb919536bfded6403f68d14";          
            string sender = "WARSHAHTECH";
            var AllWarshahUsersFromInvoces = uow.WarshahCarOwnersRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet();

            var smscountcurrent = uow.SMSCountRepository.GetMany(a => a.WarshahId == warshahId).FirstOrDefault();

            if(smscountcurrent != null && smscountcurrent.MessageRemain > 0)
            {
                foreach (var item in AllWarshahUsersFromInvoces)
                {

                    if (smscountcurrent.MessageRemain > 0)
                    {

                        //var phone = "+966582240552";

                        var User = uow.UserRepository.GetById(item.CarOwnerId);

                        //var User = uow.UserRepository.GetMany(a => a.Phone == phone.ToString()).FirstOrDefault();
                        string phone = User.Phone.Substring(0,5);
                        if (User != null && phone == "+9665")
                        {
                            if (SMS)
                            {
                                _SMS.SendSMS1(bearer, User.Phone, sender, Message);
                                
                            }
                            if (User.Email != null && Email)
                            {
                                _MailService.Notification(User.Email, Message);
                            }

                            uow.SMSHistoryRepository.Add(new DL.Entities.SMSHistory
                            {
                                CreatedOn = System.DateTime.Now,
                                SMSMessage = Message,
                                UserName = User.IsCompany ? User.Phone + " - " + User.CompanyName : User.Phone + " - " + User.FirstName + " - " + User.LastName,
                                WarshahId = warshahId
                            });

                            var currentcount = smscountcurrent.MessageRemain - 1;
                            smscountcurrent.MessageRemain = currentcount;


                        }



                    }

                }
            }

         

            uow.SMSCountRepository.Update(smscountcurrent);
            uow.Save();

            return Ok();
        }
        [HttpGet, Route("GetSMSHistory")]

        public IActionResult GetSMSHistory(int warshahId , int pagenumber , int pagecount)
        {
            var d = uow.SMSHistoryRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet();

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = d.Count(), Listinvoice = d.ToPagedList(pagenumber, pagecount) });


        }
    }
}
