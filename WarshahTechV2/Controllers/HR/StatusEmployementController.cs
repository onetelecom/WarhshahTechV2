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

    public class StatusEmployementController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public StatusEmployementController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }




        //Create Status 
        [HttpPost, Route("CreateStatus")]
        public IActionResult CreateStatus(StatusEmploymentDTO statusEmploymentDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Status = _mapper.Map<StatusEmployment>(statusEmploymentDTO);
                    Status.IsDeleted = false;
                    _uow.StatusEmployementRepository.Add(Status);
                    _uow.Save();
                    return Ok(Status);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Status");
        }



        // Get All Status

        [HttpGet, Route("GetAllStatus")]
        public IActionResult GetAllStatus()
        {
            var status = _uow.StatusEmployementRepository.GetAll().ToHashSet();
            return Ok(status);
        }
    }
}

