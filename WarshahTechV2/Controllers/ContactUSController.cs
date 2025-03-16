using AutoMapper;
using BL.Infrastructure;
using DL.Entities;
using DL.MailModels;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUSController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper _mapper;
        private readonly ISMS _SMS;
        private readonly IMailService _MailService;

        public ContactUSController(IUnitOfWork uow, IMapper _mapper, ISMS sMS, IMailService MailService)
        {
            this.uow = uow;
            this._mapper = _mapper;
            _SMS = sMS;
            _MailService = MailService;
        }
        [HttpPost,Route("SendContactUs")]
        public IActionResult SendContactUs(ContactUs contactUS)
        {
            uow.ContactUSRepository.Add(contactUS);
            uow.Save();
            return Ok(contactUS);
        }
        [HttpGet,Route("GetAllContactUs")]
        public IActionResult GetAllContactUs()
        {
            return Ok(uow.ContactUSRepository.GetAll().ToHashSet());
        }
    }
}
