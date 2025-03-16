using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.SparePartsDTOs;
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
    public class SubCategoryPartsController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public SubCategoryPartsController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region SubCategoryPartsCRUD

        //Create SubCategoryParts
  
        [HttpPost, Route("CreateSubCategoryPart")]
        public IActionResult CreateSubCategoryPart(SubCategoryPartsDTO subCategoryPartsDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var SubCategoryParts = _mapper.Map<DL.Entities.SubCategoryParts>(subCategoryPartsDTO);
                    SubCategoryParts.IsDeleted = false;
                    _uow.SubCategoryPartsRepository.Add(SubCategoryParts);
                    _uow.Save();
                    return Ok(SubCategoryParts);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SubCategoryPartsName");
        }

        // Get All SubCategoryParts

        [HttpGet, Route("GetAllSubCategoryParts")]
        public IActionResult GetAllSubCategoryParts()
        {
            var SubCategoryParts = _uow.SubCategoryPartsRepository.GetAll().ToHashSet();
            return Ok(SubCategoryParts);
        }

        [HttpGet, Route("GetSubPartsByCategoryId")]
        public IActionResult GetSubPartsByCategoryId(int id)
        {
            var SubParts = _uow.SubCategoryPartsRepository.GetMany(m => m.CategorySparePartsId == id);

            return Ok(SubParts);
        }

        // Update SubCategoryParts

        [HttpPost, Route("EditSubCategoryParts")]
        public IActionResult EditSubCategoryParts(EditSubCategoryPartsDTO editSubCategoryPartsDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var SubCategory = _mapper.Map<DL.Entities.SubCategoryParts>(editSubCategoryPartsDTO);
                    _uow.SubCategoryPartsRepository.Update(SubCategory);
                    _uow.Save();
                    return Ok(SubCategory);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SubCategory");


        }


        // Delete SubCategory
        [HttpDelete, Route("DeleteSubCategory")]
        public IActionResult DeleteSubCategory(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.SubCategoryPartsRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SubCategory");


        }

        #endregion
    }
}
