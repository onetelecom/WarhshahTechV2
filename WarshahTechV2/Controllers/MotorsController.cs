using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
using DL.DTOs.MotorsDTOs;
using DocumentFormat.OpenXml.Office2010.Excel;
using HELPER;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
   [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

    public class MotorsController : ControllerBase
    {
        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public MotorsController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region MotorOwnerCRUD

        //Create Motor Owner

        [HttpPost, Route("CreateMotors")]
        public IActionResult CreateMotors(MotorsDTO motorOwnerDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MotorOwner = _mapper.Map<DL.Entities.Motors>(motorOwnerDTO);
                    MotorOwner.IsDeleted = false;
                    _uow.MotorsRepository.Add(MotorOwner);
                    _uow.Save();
                    return Ok(MotorOwner);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid MotorOwner");
        }
        [HttpGet,Route("UpdateMotorNumber")]
        public IActionResult UpdateMotorNumber(int motorId,string Number)
        {
            var motor = _uow.MotorsRepository.GetById(motorId);
            motor.ChassisNo = Number;
            _uow.MotorsRepository.Update(motor);
            _uow.Save();
            return Ok(motor);
        }
        // Get All Motor Make

        [HttpGet, Route("GetAllMotorOwner")]
        public IActionResult GetAllMotorOwner()
        {
            return Ok(_uow.MotorsRepository.GetAll().ToHashSet());
        }


        [HttpGet, Route("GetMotorDesc")]
        public IActionResult GetMotorDesc(int MotorId)
        {
            var Motor = _uow.MotorsRepository.GetMany(a => a.Id == MotorId).Include(a => a.motorYear).Include(a => a.motorModel).Include(a => a.motorMake).FirstOrDefault();
            
            return Ok(new {Car = Motor.motorMake.MakeNameAr + " " + Motor.motorModel.ModelNameAr + " "+Motor.motorYear.Year + " " + Motor.ChassisNo});
        }


        // Get All Motors with Owner

        [HttpGet, Route("GetMotorsByOwnerId")]
        public IActionResult GetMotorsByOwnerId(int id)
        {

            return Ok(_uow.MotorsRepository.GetMany(m => m.CarOwnerId == id).OrderByDescending(i=>i.motorMake.Id)
                .Include(a=>a.motorColor)
                .Include(a => a.motorMake)
                .Include(a => a.motorModel)
                .Include(a => a.motorYear).ToHashSet());
        }


        [HttpGet,Route("GetMotorById")]
        public IActionResult GetMotorById(int id)
        {
            return Ok(_uow.MotorsRepository.GetMany(a=>a.Id==id).Include(a => a.motorColor).Include(a => a.motorMake).Include(a => a.motorModel).Include(a => a.motorYear)); 
        }
        // Update Motor Owner

        [HttpPost, Route("EditMotorOwner")]
        public IActionResult EditMotorOwner(EditMotorsDTO motorOwnerDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    

                    var MotorDTOEntity = _uow.MotorsRepository.GetById(motorOwnerDTO.Id);
                    if (MotorDTOEntity == null)
                    {
                        return BadRequest("Not Found");
                    }
                  
                    motorOwnerDTO.UpdatedOn = DateTime.Now;//***

                    ////motorOwnerDTO.MotorMakeId = MotorDTOEntity.MotorMakeId;
                    ////motorOwnerDTO.MotorModelId = MotorDTOEntity.MotorModelId;
                    ////motorOwnerDTO.MotorYearId = MotorDTOEntity.MotorYearId;



                    //motorOwnerDTO.ChassisNo = MotorDTOEntity.ChassisNo;
                    //motorOwnerDTO.PlateNo = MotorDTOEntity.PlateNo;
                    //motorOwnerDTO.CarOwnerId = MotorDTOEntity.CarOwnerId;

                   _mapper.Map(motorOwnerDTO, MotorDTOEntity);
                    _uow.MotorsRepository.Update(MotorDTOEntity);
                    _uow.Save();
                    return Ok(MotorDTOEntity);


                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid MotorOwner");


        }


        // Delete Motor Owner
        [HttpDelete, Route("DeleteMotorOwner")]
        public IActionResult DeleteMotorOwner(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Ros = _uow.ReciptionOrderRepository.GetMany(a => a.MotorsId == Id).ToHashSet();
                    if (Ros.Count==0)
                    {
                        _uow.MotorsRepository.Delete(Id);
                        _uow.Save();
                        return Ok();
                    }
                    return BadRequest(new {MSG ="The Car Has RO" });

                   
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
                

            }

            return BadRequest("Invalid MotorOwner");


        }

        #endregion


    }
}
