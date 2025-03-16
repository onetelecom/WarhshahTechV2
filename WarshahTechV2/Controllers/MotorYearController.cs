using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.MotorsDTOs;
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

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

    public class MotorYearController : ControllerBase
    {
        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public MotorYearController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        #region MotroYearCRUD

        //Create Motor Year
        [HttpPost, Route("CreateYears")]
        public IActionResult CreateYears(MotorYearDTO motorYearDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MotorYear = _mapper.Map<DL.Entities.MotorYear>(motorYearDTO);
                    MotorYear.IsDeleted = false;
                    _uow.MotorYearRepository.Add(MotorYear);
                    _uow.Save();
                    return Ok(MotorYear);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Year");
        }

        // Get All Motor Year

        [HttpGet, Route("GetAllMotorYear")]
        public IActionResult GetAllMotorYear()
        {
            return Ok(_uow.MotorYearRepository.GetMany(a=>a.Year < 2031).ToHashSet().OrderByDescending(i=>i.Year));
        }

        // Update Motor Year

        [HttpPost, Route("EditMotorYear")]
        public IActionResult EditMotorYear(EditMotorYearDTO motorYearDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MotorYear = _mapper.Map<DL.Entities.MotorYear>(motorYearDTO);
                    _uow.MotorYearRepository.Update(MotorYear);
                    _uow.Save();
                    return Ok(MotorYear);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Year");


        }

        // Delete Motor Year
        [HttpDelete, Route("DeleteMotorYear")]
        public IActionResult DeleteMotorYear(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    _uow.MotorYearRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Year");


        }


        // Create MotorOwner By Warshash 



        #endregion



    }
}
