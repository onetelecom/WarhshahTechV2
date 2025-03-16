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
  

    public class CountryController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public CountryController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region CountryCRUD

        //Create Country 
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateCountry")]
        public IActionResult CreateCountry(CountryDTO countryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Country = _mapper.Map<DL.Entities.Country>(countryDTO);
                    Country.IsDeleted = false;
                    _uow.CountryRepository.Add(Country);
                    _uow.Save();
                    return Ok(Country);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid CountryName");
        }



        // Get All Countries
        [AllowAnonymous]
        [HttpGet, Route("GetAllCountries")]
        public IActionResult GetAllCountries()
        {
            var countries = _uow.CountryRepository.GetAll().ToHashSet();
            return Ok(countries);
        }

        // Update  Countries
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("EditCountries")]
        public IActionResult EditCountries(EditCountryDTO editCountryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Country = _mapper.Map<DL.Entities.Country>(editCountryDTO);
                    _uow.CountryRepository.Update(Country);
                    _uow.Save();
                    return Ok(Country);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid CountryName");


        }

        // Delete Country
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpDelete, Route("DeleteCountry")]
        public IActionResult DeleteCountry(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.CountryRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Country");


        }


        #endregion




    }

}
