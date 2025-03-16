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

    public class MotorModelController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public MotorModelController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region MotorModolCRUD

        //Create Motor Model
        [HttpPost, Route("CreateMotorModel")]
        public IActionResult CreateMotorModel(MotorModelDTO motorModelDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MotorModel = _mapper.Map<DL.Entities.MotorModel>(motorModelDTO);
                    MotorModel.IsDeleted = false;
                    _uow.MotorModelRepository.Add(MotorModel);
                    _uow.Save();
                    return Ok(MotorModel);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid MakeName");
        }

        // Get All Motor Make

        [HttpGet, Route("GetAllMotorModel")]
        public IActionResult GetAllMotorModel()
        {
            return Ok(_uow.MotorModelRepository.GetAll().ToHashSet());
        }

        [HttpGet, Route("GetModelByMakeId")]
        public IActionResult GetModelByMakeId(int id)
        {

            return Ok(_uow.MotorModelRepository.GetMany(m=>m.MotorMakeId == id));
        }

        // Update Motor Modol

        [HttpPost, Route("EditMotorModel")]
        public IActionResult EditMotorModel(EditMotorModelDTO motorModelDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MotorModel = _mapper.Map<DL.Entities.MotorModel>(motorModelDTO);
                    _uow.MotorModelRepository.Update(MotorModel);
                    _uow.Save();
                    return Ok(MotorModel);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid ModelName");


        }


        // Delete Motor Model
        [HttpDelete, Route("DeleteMotorModel")]
        public IActionResult DeleteMotorModel(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.MotorModelRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Model");


        }

        #endregion
    }
}
