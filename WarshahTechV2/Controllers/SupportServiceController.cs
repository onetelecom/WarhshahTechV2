using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
using DL.DTOs.SupportServiceDTO;
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
  

    public class SupportServiceController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public SupportServiceController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region SupportServiceCRUD

        //Create SupportService 
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateSupportService")]
        public IActionResult CreateSupportService(SupportServiceDTO SupportServiceDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var SupportService = _mapper.Map<DL.Entities.SupportService>(SupportServiceDTO);
                    SupportService.IsDeleted = false;
                    _uow.SupportServiceRepository.Add(SupportService);
                    _uow.Save();
                    return Ok(SupportService);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SupportServiceName");
        }



        // Get All Countries
        [AllowAnonymous]
        [HttpGet, Route("GetAllSupportServices")]
        public IActionResult GetAllSupportServices()
        {
            var countries = _uow.SupportServiceRepository.GetAll().ToHashSet();
            return Ok(countries);
        }

        // Update  Countries
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("EditSupportService")]
        public IActionResult EditSupportService(EditSupportServiceDTO editSupportServiceDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var SupportService = _mapper.Map<DL.Entities.SupportService>(editSupportServiceDTO);
                    _uow.SupportServiceRepository.Update(SupportService);
                    _uow.Save();
                    return Ok(SupportService);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SupportServiceName");


        }

        // Delete SupportService
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpDelete, Route("DeleteSupportService")]
        public IActionResult DeleteSupportService(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.SupportServiceRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SupportService");


        }


        #endregion




    }

}
