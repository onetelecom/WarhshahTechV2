using AutoMapper;
using BL.Infrastructure;
using DL.DTOs;
using DL.DTOs.AddressDTOs;
using DL.Entities;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]

    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]
    public class WarshahMobileController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public WarshahMobileController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region WarshahMobileCRUD

        //Create WarshahMobile

        [HttpPost, Route("CreateWarshahMobile")]
        public IActionResult CreateWarshahMobile(List<WarshahMobileDTO> WarshahMobileDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    foreach (var item in WarshahMobileDTO)
                    {
                        var WarshahMobile = _mapper.Map<DL.Entities.WarshahMobile>(item);

                        if(item.Mobile ==null)
                        {
                            return BadRequest("Invalid Mobile ..");

                        }
                        if (item.WarshahId == 0)
                        {
                            return BadRequest("Invalid WarshahId ..");

                        }

                        var existwarsha = _uow.WarshahRepository.GetById(item.WarshahId);
                        if (existwarsha == null)
                        {

                            return BadRequest("WarshahId  Does not Exist");


                        }

                        WarshahMobile.IsDeleted = false;
                        _uow.WarshahMobileRepository.Add(WarshahMobile);
                        _uow.Save();
                    }
                    return Ok(WarshahMobileDTO);


                }



                catch (Exception ex)
                {
                    return BadRequest(" Internal server error" + " " + ex.Message);

                }






            }

            return BadRequest(" Internal server error" );
        }




        [HttpGet, Route("GetWarshahMobileById")]
        public IActionResult GetWarshahMobileById(int id)
        {
            var WarshahMobile = _uow.WarshahMobileRepository.GetMany(m => m.WarshahId == id).Where(i=>i.IsDeleted==false && i.IsActive==true).Include(s => s.Warshah).ToHashSet();
            if (WarshahMobile == null)
            {
                return Ok("Not Found");
            }
            return Ok(WarshahMobile);
        }


        [HttpPost, Route("EditWarshahMobile")]
        public IActionResult EditWarshahMobile(int Id,WarshahMobileDTO WarshahMobileDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (WarshahMobileDTO == null)
                    {

                        return BadRequest("Data Not Found");

                    }
                    if (!ModelState.IsValid)
                    {
                        return BadRequest("Invalid model object");


                    }
                    var WarshahMobileEntity = _uow.WarshahMobileRepository.GetById(Id);
                    if (WarshahMobileEntity == null)
                    {
                        return BadRequest("Not Found");
                    }
                    WarshahMobileEntity.CreatedBy = WarshahMobileEntity.CreatedBy;
                    WarshahMobileEntity.CreatedOn = WarshahMobileEntity.CreatedOn;
                    WarshahMobileEntity.UpdatedOn = DateTime.Now;//***
                    _mapper.Map(WarshahMobileDTO, WarshahMobileEntity);
                    _uow.WarshahMobileRepository.Update(WarshahMobileEntity);
                    _uow.Save();

                    return BadRequest("Data have Modified Successfully");

                }
                catch (Exception ex)
                {

                    return BadRequest("Internal server error");

                }
            }

            return BadRequest(" Internal server error" );


        }



        [HttpDelete, Route("DeleteWarshahMobile")]
        public IActionResult DeleteWarshahMobile(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Getwarshah = _uow.WarshahMobileRepository.GetById(id);
                    var GetwarshahMobile = _mapper.Map<DL.Entities.WarshahMobile>(Getwarshah);
                    GetwarshahMobile.IsDeleted = true;
                    GetwarshahMobile.UpdatedOn = DateTime.Now;
                    _uow.WarshahMobileRepository.Update(GetwarshahMobile);
                    _uow.Save();
                    return Ok(GetwarshahMobile);
                }
                catch (Exception ex)
                {
                    return BadRequest(" Internal server error");
                }

            }

            return BadRequest(" Internal server error");

        }

        #endregion
    }
}
