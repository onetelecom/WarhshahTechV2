using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
using DL.DTOs.HR;
using DL.Entities.HR;
using HELPER;
using Microsoft.EntityFrameworkCore;
using DL.DTOs.InvoiceDTOs;
using DL.Migrations;
using PagedList;

namespace WarshahTechV2.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]

    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Hr)]
    public class DataEmployeeController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public DataEmployeeController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }



        [AllowAnonymous]
        [HttpGet, Route("GetAllEmployeesByWarshahId")]
        public IActionResult GetAllEmployeesByWarshahId(int Warshahid, int pagenumber, int pagecount)
        {
            var Employees = _uow.DataEmployeeRepository.GetMany(e=>e.WarshahId == Warshahid)
                .Include(a => a.Gender).Include(a => a.Nationality)
                .Include(a => a.EmployeeShift).Include(a => a.StatusEmployment)
                .Include(a => a.Role).Include(a => a.MaritalStatus)
                .Include(a => a.ContractType).Include(a=>a.Warshah).ToHashSet();
            if (Employees == null)
            {
                return Ok();
            }


            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = Employees.Count(), Listinvoice = Employees.ToPagedList(pagenumber, pagecount) });

          
        }



        
        [HttpGet, Route("GetEmployeeById")]
        public IActionResult GetEmployeeById(int employeeid)
        {
            var employee = _uow.DataEmployeeRepository.GetMany(a=>a.Id == employeeid)
                .Include(a=>a.Gender).Include(a=>a.Nationality)
                .Include(a=>a.EmployeeShift).Include(a=>a.StatusEmployment)
                .Include(a=>a.Role).Include(a=>a.MaritalStatus)
                .Include(a=>a.ContractType).Include(a => a.Warshah)
                .Include(a=>a.City.Region.Country).FirstOrDefault();
            if(employee == null)
            {
                return Ok();
            }
            return Ok(employee);
        }





        [HttpPost, Route("AddDataEmployee")]

        public IActionResult AddDataEmployee(DataEmployeeDTO dataEmployee)

        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<DataEmployee>(dataEmployee);


                // Get last EmployeesID number for each warshash

                    int lastId = 0;
                    var EmployeesIDlast = _uow.DataEmployeeRepository.GetMany(i => i.WarshahId == employee.WarshahId).OrderByDescending(t => t.Id).FirstOrDefault();
                    if (EmployeesIDlast == null)
                    {
                        lastId = 1;
                    }
                    else
                    {
                        int lastnumber = EmployeesIDlast.Id;
                        lastId = lastnumber + 1;
                    }

                    employee.AttendanceCode = "Emp-" + employee.WarshahId + "-" + lastId;

                employee.StatusEmploymentId = 1;
                employee.CreatedOn = DateTime.Now;
                _uow.DataEmployeeRepository.Add(employee);
                _uow.Save();

                return Ok(employee);
            }

            return BadRequest(ModelState);



        }




        [HttpPost, Route("EditDataEmployee")]

        public IActionResult EditDataEmployee (EditDataEmployee editDataEmployee)   
        
        {
            if(ModelState.IsValid)
            {
                var employee = _mapper.Map<DataEmployee>(editDataEmployee);

                var currentemployee = _uow.DataEmployeeRepository.GetById(editDataEmployee.Id);
                var user = _uow.UserRepository.GetMany(a => a.Phone == currentemployee.MobileNo).FirstOrDefault();

                employee.UserWarshahCode = currentemployee.UserWarshahCode;
                employee.WarshahId = currentemployee.WarshahId;
              
                if(employee.StatusEmploymentId == 2)
                {
                    employee.MobileNo = "0000";

                    if(user != null)
                    {
                        user.Phone = "0000";
                        _uow.UserRepository.Update(user);
                    }
                }



                _uow.DataEmployeeRepository.Update(employee);
                _uow.Save();

                return Ok(employee);
            }

         return BadRequest(ModelState);
           


        }



        [HttpGet, Route("GetEmployeesByRoleId")]
        public IActionResult GetEmployeesByRoleId(int roleId)
        {
            var employees = _uow.DataEmployeeRepository.GetMany(a => a.RoleId == roleId)
                .Include(a => a.Gender).Include(a => a.Nationality)
                .Include(a => a.EmployeeShift).Include(a => a.StatusEmployment)
                .Include(a => a.Role).Include(a => a.MaritalStatus)
                .Include(a => a.ContractType).Include(a => a.Warshah)
                .Include(a => a.City.Region.Country).ToHashSet();
            if (employees == null)
            {
                return Ok();
            }
            return Ok(employees);
        }


    }
}