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
    public class CategorySparePartsController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public CategorySparePartsController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region CategoryCRUD

        //Create Category 
       
      
        [HttpPost, Route("CreateCategory")]
        public IActionResult CreateCategory(CategorySparePartsDTO categorySparePartsDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Category = _mapper.Map<DL.Entities.CategorySpareParts>(categorySparePartsDTO);
                    Category.IsDeleted = false;
                    _uow.CategorySparePartsRepository.Add(Category);
                    _uow.Save();
                    return Ok(Category);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid CategoryName");
        }



        // Get All Categories

        [HttpGet, Route("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            var Categories = _uow.CategorySparePartsRepository.GetAll().ToHashSet();
            return Ok(Categories);
        }

        // Update  Categories

        [HttpPost, Route("EditCategories")]
        public IActionResult EditCategories(EditCategorySparePartsDTO editCategorySpareParts)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Category = _mapper.Map<DL.Entities.CategorySpareParts>(editCategorySpareParts);
                    _uow.CategorySparePartsRepository.Update(Category);
                    _uow.Save();
                    return Ok(Category);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid CategoryName");


        }

        // Delete Category
        [HttpDelete, Route("DeleteCategory")]
        public IActionResult DeleteCategory(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.CategorySparePartsRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Category");


        }


        #endregion




    }
}
