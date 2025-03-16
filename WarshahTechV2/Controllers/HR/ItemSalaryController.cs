using AutoMapper;
using BL.Infrastructure;
using DL.DBContext;
using DL.DTOs.AddressDTOs;
using DL.DTOs.HR;
using DL.Entities.HR;
using HELPER;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WarshahTechV2.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Hr)]


    public class ItemSalaryController : ControllerBase
    {

      
        

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public ItemSalaryController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }




        //Create ItemSalary 
        
        [HttpPost, Route("CalculateSalaries")]
        public IActionResult CalculateSalaries(int  warshahId , int year , int month)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ItemSalaryDTO itemSalaryDTO = new ItemSalaryDTO();
                    var EmpSalary = _mapper.Map<ItemSalary>(itemSalaryDTO);

                    // Get Data employees

                    var dataEmployees = _uow.DataEmployeeRepository.GetMany(a => a.WarshahId == warshahId && a.StatusEmploymentId == 1).ToHashSet();

                    var Checksalary =  _uow.ItemSalaryRepository.GetMany(s => s.WarshahId == warshahId && s.year == year && s.Month == month)
                             .ToHashSet();

                    if(Checksalary != null)
                    {
                        foreach(var empSalary in Checksalary)
                        {
                            _uow.ItemSalaryRepository.Delete(empSalary.Id);
                            _uow.Save();
                        }
                    }

                    foreach (var employee in dataEmployees)
                    {
                        EmpSalary.IsDeleted = false;
                        EmpSalary.year = year;
                        EmpSalary.Month = month;
                        EmpSalary.WarshahId = warshahId;
                        EmpSalary.BasicSalary = employee.BasicSalary;
                        if (EmpSalary.BasicSalary == null)
                        {
                            EmpSalary.BasicSalary = 0;
                        }
                        EmpSalary.Transportation = employee.Transportation;
                        if (EmpSalary.Transportation == null)
                        {
                            EmpSalary.Transportation = 0;
                        }
                        EmpSalary.HouseAllowances = employee.HouseAllowances;
                        if (EmpSalary.HouseAllowances == null)
                        {
                            EmpSalary.HouseAllowances = 0;
                        }
                        EmpSalary.DataEmployeeId = employee.Id;
                        EmpSalary.Absence = employee.Absence;
                        if (EmpSalary.Absence == null)
                        {
                            EmpSalary.Absence = 0;
                        }
                        EmpSalary.Installment = employee.Installment;
                        if (EmpSalary.Installment == null)
                        {
                            EmpSalary.Installment = 0;
                        }
                        EmpSalary.OtherDeduction = 0;
                        if (EmpSalary.OtherDeduction == null)
                        {
                            EmpSalary.OtherDeduction = 0;
                        }


                        decimal calInsurances = (decimal)(EmpSalary.BasicSalary + EmpSalary.HouseAllowances);
                        var EmpInsurances = 11;
                        decimal DucInsurances = (((decimal)EmpInsurances) / (100));
                        EmpSalary.OtherDeduction= calInsurances * DucInsurances;


                        // Calculate Bonus

                        var allbonus = _uow.RecordBonusRepository.GetMany(b => b.UserId == employee.UserWarshahCode && b.CreatedOn.Month == month && b.CreatedOn.Year == year).ToHashSet();

                        if(allbonus == null)
                        {
                            EmpSalary.Bonus = 0;
                        }

                        else
                        {
                            EmpSalary.Bonus = allbonus.Sum(b => b.Bonus);
                        }

                        // Total Benifits ( BasicSalary + Transportation + HouseAllowances + Bonus)

                        EmpSalary.TotalBenifites = EmpSalary.BasicSalary + EmpSalary.Transportation + EmpSalary.HouseAllowances + EmpSalary.Bonus;

                        // Total Deductions ( Absence + Installment + OtherDeduction)

                        EmpSalary.TotalDeduction = EmpSalary.Absence + EmpSalary.Installment + EmpSalary.OtherDeduction;

                        // NetSalary ( Benifits -  Deductions )

                        EmpSalary.NetSalary = EmpSalary.TotalBenifites - EmpSalary.TotalDeduction;

                        if(EmpSalary.Id > 0)
                        {
                            EmpSalary.Id = 0;
                        }
                        _uow.ItemSalaryRepository.Add(EmpSalary);
                        _uow.Save();
                    }
                    

                    var empsSalaries = _uow.ItemSalaryRepository.GetMany(s => s.WarshahId == warshahId && s.year == year && s.Month == month)
                              .Include(s => s.DataEmployee).ToHashSet();

                    return Ok(empsSalaries);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Gender");
        }



        // Get Employee Salary 
        [AllowAnonymous]
        [HttpGet, Route("GetEmployeeSalary")]
        public IActionResult GetEmployeeSalary(int id)
        {
            var empSalary = _uow.ItemSalaryRepository.GetById(id);
            return Ok(empSalary);
        }


        // Get All Employee Salary  in Month
        [AllowAnonymous]
        [HttpGet, Route("GetAllEmployeesSalaries")]
        public IActionResult GetAllEmployeesSalaries(int Warshahid , int Year , int Month)
        {
            var empsSalaries = _uow.ItemSalaryRepository.GetMany(s=>s.WarshahId == Warshahid && s.year == Year && s.Month == Month)
                               .Include(s=>s.DataEmployee).ToHashSet();
             
            return Ok(empsSalaries);
        }




        [HttpPost, Route("EditEmployeeSalary")]

        public IActionResult EditEmployeeSalary(EditItemSalary editItemSalary)

        {

            var employeesalary = _mapper.Map<ItemSalary>(editItemSalary);

            // Total Benifits ( BasicSalary + Transportation + HouseAllowances + Bonus + OthersAllowances )

            employeesalary.TotalBenifites = employeesalary.BasicSalary + employeesalary.Transportation + employeesalary.HouseAllowances + employeesalary.Bonus + employeesalary.OthersAllowances;

            // Total Deductions ( Absence + Installment + OtherDeduction)

            employeesalary.TotalDeduction = employeesalary.Absence + employeesalary.Installment + employeesalary.OtherDeduction;

            // NetSalary ( Benifits -  Deductions )

            employeesalary.NetSalary = employeesalary.TotalBenifites - employeesalary.TotalDeduction;

            _uow.ItemSalaryRepository.Update(employeesalary);
            _uow.Save();

            return Ok(employeesalary);


        }
 


    }
}
