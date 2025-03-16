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

    public class NationalityController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public NationalityController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


      

        //Create Nationality 
     
        [HttpPost, Route("CreateNationality")]
        public IActionResult CreateNationality(NationalityDTO nationalityDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Nationality = _mapper.Map<Nationality>(nationalityDTO);
                    Nationality.IsDeleted = false;
                    _uow.NationaltyRepository.Add(Nationality);
                    _uow.Save();
                    return Ok(Nationality);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid NationalityName");
        }



        // Get All Nationality
   
    
        [HttpGet, Route("GetAllNationality")]
        public IActionResult GetAllNationality()
        {
            var nationalities = _uow.NationaltyRepository.GetAll().ToHashSet();
            return Ok(nationalities);
        }
    }
    }
