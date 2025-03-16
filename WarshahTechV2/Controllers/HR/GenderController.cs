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

    public class GenderController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public GenderController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }




        //Create Gender 
       
        [HttpPost, Route("CreateGender")]
        public IActionResult CreateGender(GenderDTO genderDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Gender = _mapper.Map<Gender>(genderDTO);
                    Gender.IsDeleted = false;
                    _uow.GenderRepository.Add(Gender);
                    _uow.Save();
                    return Ok(Gender);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Gender");
        }




        [AllowAnonymous]
        [HttpGet, Route("GetAllGender")]
        public IActionResult GetAllGender()
        {
            var gender = _uow.GenderRepository.GetAll().ToHashSet();
            return Ok(gender);
        }
    }
}
