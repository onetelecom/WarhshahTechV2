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

    public class MartialStatusController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public MartialStatusController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }




        //CreateMartialStatus 
    
        [HttpPost, Route("CreateMartialStatus")]
        public IActionResult CreateMartialStatus(MaritalStatusDTO maritalStatusDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var martial = _mapper.Map<MaritalStatus>(maritalStatusDTO);
                    martial.IsDeleted = false;
                    _uow.MartialStatusRepository.Add(martial);
                    _uow.Save();
                    return Ok(martial);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid MartialStatus");
        }




        [AllowAnonymous]
        [HttpGet, Route("GetAllCreateMartialStatus")]
        public IActionResult GetAllCreateMartialStatus()
        {
            var maritals = _uow.MartialStatusRepository.GetAll().ToHashSet();
            return Ok(maritals);
        }
    }
}
