 using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.ExpensesDTOs;
using HELPER;
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
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]

    public class ExpensesCategoryController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public ExpensesCategoryController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region ExpensesCategoryCRUD

        //Create ExpensesCategory 


        [HttpPost, Route("CreateExpensesCategory")]
        public IActionResult CreateExpensesCategory(ExpensesCategoryDTO expensesCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Category = _mapper.Map<DL.Entities.ExpensesCategory>(expensesCategoryDTO);
                    Category.IsDeleted = false;
                    _uow.ExpensesCategoryRepository.Add(Category);
                    _uow.Save();
                    return Ok(Category);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Expenses Category");
        }



        // Get All Expenses Category

        [HttpGet, Route("GetAllExpensesCategory")]
        public IActionResult GetAllExpensesCategory()
        {
            var Categories = _uow.ExpensesCategoryRepository.GetAll().ToHashSet();
            return Ok(Categories);
        }

        // Update  Categories

       
        [HttpPost, Route("EditExpensesCategory")]
        public IActionResult EditCategories(EditExpensesCategoryDTO editExpensesCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Category = _mapper.Map<DL.Entities.ExpensesCategory>(editExpensesCategoryDTO);
                    _uow.ExpensesCategoryRepository.Update(Category);
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

                    _uow.ExpensesCategoryRepository.Delete(Id);
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
