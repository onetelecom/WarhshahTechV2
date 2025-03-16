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

    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]
    public class SuppliersController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public SuppliersController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region SuppliersCRUD

        //Create Supplier

        [HttpPost, Route("CreateSupplier")]
        public IActionResult CreateSupplier(SupplierDTO supplierDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Supplier = _mapper.Map<DL.Entities.Supplier>(supplierDTO);
                    Supplier.IsDeleted = false;
                    _uow.SupplierRepository.Add(Supplier);
                    _uow.Save();
                    return Ok(Supplier);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Supplier Name");
        }

        // Get All Suppliers By Warshah Id


        [HttpGet, Route("GetAllSuppliers")]
        public IActionResult GetAllSuppliers(int id)
        {
            var Suppliers = _uow.SupplierRepository.GetMany(m => m.WarshahId == id && m.IsDeleted==false).Include(s=>s.City.Region.Country).ToHashSet();
            if(Suppliers.Count == 0)
            {
                return Ok(new {Status = "No Data"});
            }
            return Ok( Suppliers);

        }

        // Get  Supplier By Id


        [HttpGet, Route("GetSupplierById")]
        public IActionResult GetSupplierById(int id)
        {
            var Suppliers = _uow.SupplierRepository.GetMany(m => m.Id == id).Include(s => s.City.Region.Country).FirstOrDefault();
            if (Suppliers == null)
            {
                return Ok("No Data");
            }
            return Ok(Suppliers);
        }






        // Update Supplier


        [HttpPost, Route("EditSupplier")]
        public IActionResult EditSupplier(EditSupplierDTO editSupplierDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Supplier = _mapper.Map<DL.Entities.Supplier>(editSupplierDTO);
                    Supplier.UpdatedOn = DateTime.Now;
                    Supplier.IsDeleted = false;
                    _uow.SupplierRepository.Update(Supplier);
                    _uow.Save();
                    return Ok(Supplier);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Supplier");


        }


        // Delete City  (   هنا مش هنحذف المورد و لكن هنعمله توقف عشان لو فى معاملات مع الورشة متتحذفش )

        [HttpDelete, Route("DeleteSupplier")]
        public IActionResult DeleteSupplier(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var CurrentSupplier = _uow.SupplierRepository.GetById(id);
                    var Supplier = _mapper.Map<DL.Entities.Supplier>(CurrentSupplier);
                    Supplier.IsDeleted = true;
                    Supplier.UpdatedOn= DateTime.Now;
                    _uow.SupplierRepository.Update(Supplier);
                    _uow.Save();
                    return Ok(Supplier);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Supplier");

        }

        #endregion
    }
}
