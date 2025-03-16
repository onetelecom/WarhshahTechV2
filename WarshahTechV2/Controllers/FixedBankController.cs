using AutoMapper;
using BL.Infrastructure;
using DL.Entities;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixedBankController : ControllerBase
    {



        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public FixedBankController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        //Create Bank
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateBank")]
        public IActionResult CreateBank(FixedBank bank)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    bank.IsDeleted = false;
                    _uow.FixedBankRepository.Add(bank);
                    _uow.Save();
                    return Ok(bank);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid BankName");
        }




        //Get all Bank
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllFixedBank")]
        public IActionResult GetAllFixedBank()
        {
           var Banks = _uow.FixedBankRepository.GetAll();
            return Ok(Banks);
        }




        //Get Bank
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetFixedBankById")]
        public IActionResult GetFixedBankById(int bankId)
        {
            var Bank = _uow.FixedBankRepository.GetById(bankId);
            return Ok(Bank);
        }

    }
}
