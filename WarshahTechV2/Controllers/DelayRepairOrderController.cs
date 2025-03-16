using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.HR;
using DL.DTOs.JobCardDtos;
using DL.Entities;
using DL.Entities.HR;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Hr)]

    public class DelayRepairOrderController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public DelayRepairOrderController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }



        [HttpPost, Route("CreateDelayorder")]
        public IActionResult CreateDelayorder(DelayRepairOrderDTO bonusTechnicalDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bonus = _mapper.Map<DelayRepairOrder>(bonusTechnicalDTO);
                    bonus.IsDeleted = false;
                    _uow.DelayOrderRepository.Add(bonus);
                    _uow.Save();
                    return Ok(bonus);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Delay");
        }


        [HttpGet, Route("GetDelayOrderById")]
        public IActionResult GetDelayOrderById(int Warshahid)
        {
            var bonus = _uow.DelayOrderRepository.GetMany(a => a.WarshahId == Warshahid).FirstOrDefault();
            if (bonus == null)
            {
                return Ok();
            }
            return Ok(bonus);
        }




        [HttpPost, Route("EditDelayOrder")]

        public IActionResult EditDelayOrder(EditDelayRepairOrderDTO editBonus)

        {

            var bonus = _mapper.Map<DelayRepairOrder>(editBonus);

            var currentbonus = _uow.DelayOrderRepository.GetMany(a => a.WarshahId == bonus.WarshahId).FirstOrDefault();

            if (currentbonus == null)
            {
                _uow.DelayOrderRepository.Add(bonus);
            }
            else
            {
                bonus.Id = currentbonus.Id;
                bonus.WarshahId = currentbonus.WarshahId;

                _uow.DelayOrderRepository.Update(bonus);
            }

            _uow.Save();

            return Ok(bonus);


        }


    }
}