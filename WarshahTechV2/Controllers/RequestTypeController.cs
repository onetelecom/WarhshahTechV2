using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
using DL.DTOs.RequestType;
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


    public class RequestTypeController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public RequestTypeController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region RequestTypeCRUD

        //Create RequestType 
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateRequestType")]
        public IActionResult CreateRequestType(RequestTypeDto RequestTypeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var RequestType = _mapper.Map<DL.Entities.RequestType>(RequestTypeDTO);
                    RequestType.IsDeleted = false;
                    _uow.RequestTypeRepository.Add(RequestType);
                    _uow.Save();
                    return Ok(RequestType);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid RequestTypeName");
        }



        // Get All RequestType
        [AllowAnonymous]
        [HttpGet, Route("GetAllRequestType")]
        public IActionResult GetAllRequestType()
        {
            var RequestType = _uow.RequestTypeRepository.GetAll().ToHashSet();
            return Ok(RequestType);
        }

        // Update  RequestType
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("EditRequestType")]
        public IActionResult EditRequestType(RequestTypeDto editRequestTypeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var RequestType = _mapper.Map<DL.Entities.RequestType>(editRequestTypeDTO);
                    _uow.RequestTypeRepository.Update(RequestType);
                    _uow.Save();
                    return Ok(RequestType);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid RequestTypeName");


        }

        // Delete RequestType
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpDelete, Route("DeleteRequestType")]
        public IActionResult DeleteRequestType(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.RequestTypeRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid RequestType");


        }


        #endregion




    }

}
