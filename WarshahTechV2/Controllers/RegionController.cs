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
 

    public class RegionController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public RegionController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region RegionCRUD

        //Create Region
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateRegion")]
        public IActionResult CreateRegion(RegionDTO regionDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Region = _mapper.Map<DL.Entities.Region>(regionDTO);
                    Region.IsDeleted = false;
                    _uow.RegionRepository.Add(Region);
                    _uow.Save();
                    return Ok(Region);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid RegionName");
        }

        // Get All Region
        [AllowAnonymous]
        [HttpGet, Route("GetAllRegion")]
        public IActionResult GetAllRegion()
        {
            var Regions = _uow.RegionRepository.GetAll().ToHashSet();
            return Ok(Regions);
        }
        [AllowAnonymous]
        [HttpGet, Route("GetReginByCountryId")]
        public IActionResult GetReginByCountryId(int id)
        {
            var Regions = _uow.RegionRepository.GetMany(m => m.CountryId == id);

            return Ok(Regions);
        }

        // Update Region
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("EditRegion")]
        public IActionResult EditRegion(EditRegionDTO editRegionDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Region = _mapper.Map<DL.Entities.Region>(editRegionDTO);
                    _uow.RegionRepository.Update(Region);
                    _uow.Save();
                    return Ok(Region);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Region");


        }


        // Delete Region
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpDelete, Route("DeleteRegion")]
        public IActionResult DeleteRegion(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.RegionRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Region");


        }

        #endregion
    }
}
