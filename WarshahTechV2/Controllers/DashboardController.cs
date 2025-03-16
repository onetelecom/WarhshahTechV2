using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.UserDTOs;
using DL.Entities;
using DL.MailModels;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper _mapper;
        private readonly ISMS _SMS;
        private readonly IMailService _MailService;

        public DashboardController(IUnitOfWork uow, IMapper _mapper, ISMS sMS, IMailService MailService)
        {
            this.uow = uow;
            this._mapper = _mapper;
            _SMS = sMS;
            _MailService = MailService;
        }

        [HttpGet,Route("GetAllCounts")]
        public IActionResult GetAllCounts(int warshahId)
        {
            var AllFixingRepairOrders = uow.RepairOrderRepository.GetMany(a => a.WarshahId == warshahId && a.RepairOrderStatus == 8).ToHashSet();
            var RODone = uow.RepairOrderRepository.GetMany(a => a.WarshahId == warshahId && a.RepairOrderStatus == 9).ToHashSet();
            var SparePartsWaiting = uow.RepairOrderRepository.GetMany(a => a.WarshahId == warshahId && a.RepairOrderStatus == 5).ToHashSet();
            var WarshahUsers = uow.UserRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet();
            return Ok(new { AllFixingRepairOrders = AllFixingRepairOrders,RODone=RODone,SparePartsWaiting=SparePartsWaiting,WarshahUsers=WarshahUsers });
        }
      



    }
}
