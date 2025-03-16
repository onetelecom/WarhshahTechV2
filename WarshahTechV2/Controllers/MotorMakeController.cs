using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.MotorsDTOs;
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
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

    public class MotorMakeController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public MotorMakeController(IUnitOfWork uow , IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region MotorMakeCRUD

        //Create Motor Make
        [HttpPost, Route("CreateMotorMake")]
        public IActionResult CreateMotorMake(MotorMakeDTO motorMakeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MotorMake = _mapper.Map<DL.Entities.MotorMake>(motorMakeDTO);
                    MotorMake.IsDeleted = false;
                    _uow.MotorMakeRepository.Add(MotorMake);
                    _uow.Save();
                    return Ok(MotorMake);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid MakeName");
        }



        // Get All Motor Make

        [HttpGet, Route("GetAllMotorMake")]
        public IActionResult GetAllMotorMake()
        {
            return Ok(_uow.MotorMakeRepository.GetAll().ToHashSet());
        }

        // Update Motor Make

        [HttpPost, Route("EditMotorMake")]
        public IActionResult EditMotorMake(EditMotorMakeDTO motorMakeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MotorMake = _mapper.Map<DL.Entities.MotorMake>(motorMakeDTO);
                    _uow.MotorMakeRepository.Update(MotorMake);
                    _uow.Save();
                    return Ok(MotorMake);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid MakeName");


        }

        // Delete Motor Make
        [HttpDelete, Route("DeleteMotorMake")]
        public IActionResult DeleteMotorMake(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.MotorMakeRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Make");


        }


        #endregion




    }
}
