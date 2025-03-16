using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
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
    

    public class CityController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region CityCRUD

        //Create City
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateCity")]
        public IActionResult CreateCity(CityDTO cityDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var City = _mapper.Map<DL.Entities.City>(cityDTO);
                    City.IsDeleted = false;
                    _uow.CityRepository.Add(City);
                    _uow.Save();
                    return Ok(City);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid CityName");
        }

        // Get All City
        [AllowAnonymous]
        [HttpGet, Route("GetAllCity")]
        public IActionResult GetAllCity()
        {
            var Cities = _uow.CityRepository.GetAll().ToHashSet();
            return Ok(Cities);
        }
        [AllowAnonymous]
        [HttpGet, Route("GetCityByRegionId")]
        public IActionResult GetCityByRegionId(int id)
        {
            var cities = _uow.CityRepository.GetMany(m => m.RegionId == id);

            return Ok(cities);
        }

        // Update City
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("EditCity")]
        public IActionResult EditCity(EditCityDTO editCityDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var City = _mapper.Map<DL.Entities.City>(editCityDTO);
                    _uow.CityRepository.Update(City);
                    _uow.Save();
                    return Ok(City);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid City");


        }


        // Delete City
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpDelete, Route("DeleteCity")]
        public IActionResult DeleteCity(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.CityRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid City");


        }

        #endregion
    }
}
