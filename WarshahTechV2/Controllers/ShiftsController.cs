using BL.Infrastructure;
using DL.Entities;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
   [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

    public class ShiftsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

       

        // Constractor for controller 
        public ShiftsController(IUnitOfWork uow)
        {
          
            _uow = uow;
        }
        [HttpPost,Route("AddShift")]
        public IActionResult AddShift(WarshahShift warshahShift)
        {
            _uow.WarshahShiftRepository.Add(warshahShift);
            _uow.Save();
            return Ok(warshahShift);
        }
        [HttpGet, Route("DeactivateShift")]
        public IActionResult DeactivateShift(int shiftId)
        {
            var Shift =_uow.WarshahShiftRepository.GetById(shiftId);
            Shift.IsActive = false;
           

            _uow.WarshahShiftRepository.Update(Shift);
            _uow.Save();
            return Ok(Shift);
        }
        [HttpGet, Route("activateShift")]
        public IActionResult activateShift(int shiftId)
        {
            var Shift = _uow.WarshahShiftRepository.GetById(shiftId);
            Shift.IsActive = true;


            _uow.WarshahShiftRepository.Update(Shift);
            _uow.Save();
            return Ok(Shift);
        }

         [HttpGet,Route("GetShifts")]
         public IActionResult GetShifts (int WarshahId)
        {
            var Shifts = _uow.WarshahShiftRepository.GetMany(a => a.WarshahId == WarshahId).ToHashSet();
            return Ok(Shifts);
        }

        [HttpGet,Route("GetWorkTimes")]
        public IActionResult GetWorkTimes(int ShiftId)
        {
            var WorkTimes = _uow.WorkTimeRepository.GetMany(a => a.WarshahShiftId == ShiftId).ToHashSet();
            return Ok(WorkTimes);
        }

        [HttpPost, Route("AddWorkTime")]
        public IActionResult AddWorkTime(WorkTime WorkTime)
        {
            _uow.WorkTimeRepository.Add(WorkTime);
            _uow.Save();
            return Ok(WorkTime);
        }

        [HttpGet,Route("DeleteWorkTime")]
        public IActionResult DeleteWorkTime(int Id)
        {
            _uow.WorkTimeRepository.Delete(Id);
            _uow.Save();
            return Ok(Id);
        }

    }
}
