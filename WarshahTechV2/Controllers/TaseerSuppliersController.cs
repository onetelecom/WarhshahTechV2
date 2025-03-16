using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.SuppliersDTOs;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Linq;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]

    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Sales)]
    public class TaseerSuppliersController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public TaseerSuppliersController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region TaseerSuppliersCRUD

        //Create TaseerSupplier

        [HttpPost, Route("CreateTaseerSupplier")]
        public IActionResult CreateTaseerSupplier(TaseerSupplierDTO TaseersupplierDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var TaseerSupplier = _mapper.Map<DL.Entities.TaseerSupplier>(TaseersupplierDTO);
                    TaseerSupplier.IsDeleted = false;
                    _uow.TaseerSupplierRepository.Add(TaseerSupplier);
                    _uow.Save();
                    return Ok(TaseerSupplier);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid TaseerSupplier Name");
        }

        // Get All TaseerSuppliers By Warshah Id


        [HttpGet, Route("GetAllTaseerSuppliers")]
        public IActionResult GetAllTaseerSuppliers()
        {
            var TaseerSuppliers = _uow.TaseerSupplierRepository.GetAll().ToHashSet();
            if(TaseerSuppliers.Count == 0)
            {
                return Ok(new {Status = "No Data"});
            }

            return Ok(TaseerSuppliers);
        }

        // Get  TaseerSupplier By Id


        [HttpGet, Route("GetTaseerSupplierById")]
        public IActionResult GetTaseerSupplierById(int id)
        {
            var TaseerSuppliers = _uow.TaseerSupplierRepository.GetMany(m => m.Id == id).FirstOrDefault();
            if (TaseerSuppliers == null)
            {
                return Ok("No Data");
            }
            return Ok(TaseerSuppliers);
        }






        // Update TaseerSupplier


        [HttpPost, Route("EditTaseerSupplier")]
        public IActionResult EditTaseerSupplier(EditTaseerSupplierDTO editTaseerSupplierDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var TaseerSupplier = _mapper.Map<DL.Entities.TaseerSupplier>(editTaseerSupplierDTO);
                    TaseerSupplier.UpdatedOn = DateTime.Now;
                    TaseerSupplier.IsDeleted = false;
                    _uow.TaseerSupplierRepository.Update(TaseerSupplier);
                    _uow.Save();
                    return Ok(TaseerSupplier);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid TaseerSupplier");


        }


        // Delete City  (   هنا مش هنحذف المورد و لكن هنعمله توقف عشان لو فى معاملات مع الورشة متتحذفش )

        [HttpDelete, Route("DeleteTaseerSupplier")]
        public IActionResult DeleteTaseerSupplier(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var CurrentTaseerSupplier = _uow.TaseerSupplierRepository.GetById(id);
                    var TaseerSupplier = _mapper.Map<DL.Entities.TaseerSupplier>(CurrentTaseerSupplier);
                    TaseerSupplier.IsDeleted = true;
                    TaseerSupplier.UpdatedOn= DateTime.Now;
                    _uow.TaseerSupplierRepository.Update(TaseerSupplier);
                    _uow.Save();
                    return Ok(TaseerSupplier);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid TaseerSupplier");

        }
        [HttpDelete, Route("UnDeleteTaseerSupplier")]
        public IActionResult UnDeleteTaseerSupplier(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var CurrentTaseerSupplier = _uow.TaseerSupplierRepository.GetById(id);
                    var TaseerSupplier = _mapper.Map<DL.Entities.TaseerSupplier>(CurrentTaseerSupplier);
                    TaseerSupplier.IsDeleted = false;
                    TaseerSupplier.UpdatedOn = DateTime.Now;
                    _uow.TaseerSupplierRepository.Update(TaseerSupplier);
                    _uow.Save();
                    return Ok(TaseerSupplier);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid TaseerSupplier");

        }

        #endregion
    }
}
