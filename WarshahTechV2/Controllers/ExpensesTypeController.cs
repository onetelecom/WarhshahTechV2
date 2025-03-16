using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.ExpensesDTOs;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public class ExpensesTypeController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public ExpensesTypeController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region ExpensesTypeCRUD

        //Create ExpensesType

        [HttpPost, Route("CreateExpensesType")]
        public IActionResult CreateExpensesType(ExpensesTypeDTO expensesTypeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var type = _mapper.Map<DL.Entities.ExpensesType>(expensesTypeDTO);
                   
                    type.IsDeleted = false;
                    _uow.ExpensesTypeRepository.Add(type);
                    _uow.Save();
                    return Ok(type);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Type Name");
        }

        // Get All ExpensesType for ( warshah and category)


        [HttpGet, Route("GetExpensesTypeByCategoryId")]
        public IActionResult GetExpensesType(int warshahid , int categoryid)
        {
            var Types = _uow.ExpensesTypeRepository.GetMany(t => t.WarshahId == warshahid && t.ExpensesCategoryId == categoryid).Include(t=>t.ExpensesCategory ).ToHashSet();
            return Ok(Types);
        }


        // Get All ExpensesType for (warshah)


        [HttpGet, Route("GetExpensesByWarshahID")]
        public IActionResult GetExpensesByWarshahID(int warshahid)
        {
            var Types = _uow.ExpensesTypeRepository.GetMany(t => t.WarshahId == warshahid).Include(t => t.ExpensesCategory).ToHashSet();
            return Ok(Types);
        }



        // Get  ExpensesType for (Id)


        [HttpGet, Route("GetExpensesByID")]
        public IActionResult GetExpensesByID(int ExpensesId)
        {
            var Types = _uow.ExpensesTypeRepository.GetMany(t => t.Id == ExpensesId).Include(t => t.ExpensesCategory).ToHashSet();
            return Ok(Types);
        }






        // Update ExpensesType

        [HttpPost, Route("EditExpensesType")]
        public IActionResult EditExpensesType(EditExpensesTypeDTO editExpensesTypeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var type = _mapper.Map<DL.Entities.ExpensesType>(editExpensesTypeDTO);
                    _uow.ExpensesTypeRepository.Update(type);
                    _uow.Save();
                    return Ok(type);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Type");


        }


        // Delete ExpensesType

        [HttpDelete, Route("DeleteExpensesType")]
        public IActionResult DeleteExpensesType(int? Id) 
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.ExpensesTypeRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Expenses Type");


        }

        #endregion
    }

}
