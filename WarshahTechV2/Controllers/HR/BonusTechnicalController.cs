using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
using DL.DTOs.HR;
using DL.Entities.HR;
using HELPER;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WarshahTechV2.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Hr)]

    public class BonusTechnicalController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public BonusTechnicalController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }



        [HttpPost, Route("CreateBonus")]
        public IActionResult CreateBonus(BonusTechnicalDTO bonusTechnicalDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bonus = _mapper.Map<BonusTechnical>(bonusTechnicalDTO);
                    bonus.IsDeleted = false;
                    _uow.BonusTechnicalRepository.Add(bonus);
                    _uow.Save();
                    return Ok(bonus);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid bonus");
        }


        [HttpGet, Route("GetBonusTechnicalById")]
        public IActionResult GetBonusTechnicalById(int Warshahid) 
        {
            var bonus = _uow.BonusTechnicalRepository.GetMany(a=>a.WarshahId==Warshahid).FirstOrDefault();
            if (bonus == null)
            {
                return Ok();
            }
            return Ok(bonus);
        }




        [HttpPost, Route("EditBonusTechnical")]

        public IActionResult EditBonusTechnical(EditBonusTechnical editBonus)

        {

            var bonus = _mapper.Map<BonusTechnical>(editBonus);

            var currentbonus = _uow.BonusTechnicalRepository.GetMany(a => a.WarshahId == bonus.WarshahId).FirstOrDefault();

            if(currentbonus == null)
            {
                _uow.BonusTechnicalRepository.Add(bonus);
            }
            else
            {
                bonus.Id = currentbonus.Id;
                bonus.WarshahId = currentbonus.WarshahId;

                _uow.BonusTechnicalRepository.Update(bonus);
            }
          
            _uow.Save();

            return Ok(bonus);


        }


    }
}