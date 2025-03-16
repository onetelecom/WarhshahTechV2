using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
using DL.DTOs.WorkType;
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


    public class WorkTypeController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public WorkTypeController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region WorkTypeCRUD

        //Create WorkType 
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateWorkType")]
        public IActionResult CreateWorkType(WorkTypeDTO WorkTypeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var WorkType = _mapper.Map<DL.Entities.WorkType>(WorkTypeDTO);
                    WorkType.IsDeleted = false;
                    _uow.WorkTypeRepository.Add(WorkType);
                    _uow.Save();
                    return Ok(WorkType);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid WorkTypeName");
        }



        // Get All WorkType
        [AllowAnonymous]
        [HttpGet, Route("GetAllWorkType")]
        public IActionResult GetAllWorkType()
        {
            var WorkType = _uow.WorkTypeRepository.GetAll().Where(i=>i.IsDeleted==false && i.IsActive==true).ToHashSet();
            return Ok(WorkType);
        }

        // Update  WorkType
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("EditWorkType")]
        public IActionResult EditWorkType(WorkTypeDTO editWorkTypeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var WorkType = _mapper.Map<DL.Entities.WorkType>(editWorkTypeDTO);
                    _uow.WorkTypeRepository.Update(WorkType);
                    _uow.Save();
                    return Ok(WorkType);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid WorkTypeName");


        }

        // Delete WorkType
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpDelete, Route("DeleteWorkType")]
        public IActionResult DeleteWorkType(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.WorkTypeRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid WorkType");


        }


        #endregion




    }

}
