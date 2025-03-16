using BL.Infrastructure;
using DocumentFormat.OpenXml.Spreadsheet;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class SubscribtionsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;


        // Constractor for controller 
        public SubscribtionsController(IUnitOfWork uow)
        {
           
            _uow = uow;
        }
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpGet, Route("GetAllCupons")]
        public IActionResult GetAllCupons()
        {
            return Ok(_uow.CuponRepository.GetAll());
        }
        [HttpGet,Route("GetAllSubs")]
        public IActionResult GetAllSubs()
        {
            return Ok(_uow.SubscribtionRepository.GetMany(a=>a.IsActive==true && a.Describtion != "Old").ToHashSet());
        }

        [HttpGet, Route("GetOldSubs")]
        public IActionResult GetOldSubs()
        {
            return Ok(_uow.SubscribtionRepository.GetMany(a => a.IsActive == true && a.Describtion == "Old").ToHashSet());
        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpGet, Route("GetWarshahComment")]
        public IActionResult GetWarshahComment(int warshahId)
        {
            return Ok(_uow.WarshahDisableReasonRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet());
        }
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpGet, Route("GetGlobalSetting")]
        public IActionResult GetGlobalSetting()
        {
            return Ok(_uow.GlobalSettingRepository.GetAll());
        }
    }
}
