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


    public class MotorColorController : ControllerBase
    {
        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public MotorColorController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        #region MotroColorCRUD

        //Create Motor Color
     
        [HttpPost, Route("CreateColor")]
        public IActionResult CreateColor(MotorColorDTO motorColorDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MotorColor = _mapper.Map<DL.Entities.MotorColor>(motorColorDTO);
                    MotorColor.IsDeleted = false;
                    _uow.MotorColorRepository.Add(MotorColor);
                    _uow.Save();
                    return Ok(MotorColor);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Color");
        }

        // Get All Motor Color

        [HttpGet, Route("GetAllMotorColor")]
        public IActionResult GetAllMotorColor()
        {
            return Ok(_uow.MotorColorRepository.GetAll().ToHashSet());
        }

        // Update Motor Color

        [HttpPost, Route("EditMotorColor")]
        public IActionResult EditMotorColor(EditMotorColorDTO motorColorDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MotorColor = _mapper.Map<DL.Entities.MotorColor>(motorColorDTO);
                    _uow.MotorColorRepository.Update(MotorColor);
                    _uow.Save();
                    return Ok(MotorColor);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Color");


        }

        // Delete Motor Color

        [HttpDelete, Route("DeleteMotorColor")]
        public IActionResult DeleteMotorColor(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.MotorColorRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Color");


        }



        #endregion

    }
}
