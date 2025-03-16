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

    public class EmployeeShiftController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public EmployeeShiftController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }




        //Create Nationality 
     
        [HttpPost, Route("CreateShift")]
        public IActionResult CreateShift(EmployeeShiftDTO employeeShiftDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var shift = _mapper.Map<EmployeeShift>(employeeShiftDTO);
                    shift.IsDeleted = false;
                    _uow.EmployeeShiftRepository.Add(shift);
                    _uow.Save();
                    return Ok(shift);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid shift");
        }



        // Get All shift
        [AllowAnonymous]
        [HttpGet, Route("GetAllshift")]
        public IActionResult GetAllshift()
        {
            var shift = _uow.EmployeeShiftRepository.GetAll().ToHashSet();
            return Ok(shift);
        }
    }
}
