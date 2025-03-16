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

    public class ContractTypeController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public ContractTypeController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }




        //Create Nationality 
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateContract")]
        public IActionResult CreateContract(ContractTypeDT contractTypeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Contract = _mapper.Map<ContractType>(contractTypeDTO);
                    Contract.IsDeleted = false;
                    _uow.ContractTypeRepository.Add(Contract);
                    _uow.Save();
                    return Ok(Contract);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid ContractType");
        }



       
        [AllowAnonymous]
        [HttpGet, Route("GetAllContractType")]
        public IActionResult GetAllContractType()
        {
            var contracts = _uow.ContractTypeRepository.GetAll().ToHashSet();
            return Ok(contracts);
        }
    }
}

