using AutoMapper;
using BL.Infrastructure;
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
    public class HRReportsController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public HRReportsController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }



        #region Contract Report


        // Get All Employees contract will end in month
        [AllowAnonymous]
        [HttpGet, Route("GetAllEndContract")]
        public IActionResult GetAllEndContract(int warshahId)
        {

            DateTime enddate = DateTime.Now.AddMonths(1);



            var employees = _uow.DataEmployeeRepository.GetMany(e => e.WarshahId == warshahId && e.ContractEndDate >= DateTime.Now && e.ContractEndDate <= enddate).Include(a => a.Gender).Include(a => a.Nationality)
                .Include(a => a.EmployeeShift).Include(a => a.StatusEmployment)
                .Include(a => a.Role).Include(a => a.MaritalStatus)
                .Include(a => a.ContractType).Include(a => a.Warshah).ToHashSet();
            if (employees == null)
            {
                return Ok();
            }

            return Ok(employees);
        }


        // Get All Employees contract will end in period
        [AllowAnonymous]
        [HttpGet, Route("GetAllEndContractinPeriod")]
        public IActionResult GetAllEndContractinPeriod(int warshahId , DateTime FromDate , DateTime ToDate)
        {

            var employees = _uow.DataEmployeeRepository.GetMany(e => e.WarshahId == warshahId && e.ContractEndDate >= FromDate && e.ContractEndDate <= ToDate).Include(a => a.Gender).Include(a => a.Nationality)
                .Include(a => a.EmployeeShift).Include(a => a.StatusEmployment)
                .Include(a => a.Role).Include(a => a.MaritalStatus)
                .Include(a => a.ContractType).Include(a => a.Warshah).ToHashSet();
            if (employees == null)
            {
                return Ok();
            }

            return Ok(employees);
        }



        #endregion


        #region Card Report

        // Get All Employees Card will end in month
        [AllowAnonymous]
        [HttpGet, Route("GetAllEndCard")]
        public IActionResult GetAllEndCard(int warshahId)
        {

            DateTime enddate = DateTime.Now.AddMonths(1);



            var employees = _uow.DataEmployeeRepository.GetMany(e => e.WarshahId == warshahId && e.IdCardEnd >= DateTime.Now && e.IdCardEnd <= enddate).Include(a => a.Gender).Include(a => a.Nationality)
                .Include(a => a.EmployeeShift).Include(a => a.StatusEmployment)
                .Include(a => a.Role).Include(a => a.MaritalStatus)
                .Include(a => a.ContractType).Include(a => a.Warshah).ToHashSet();
            if (employees == null)
            {
                return Ok();
            }

            return Ok(employees);
        }


        // Get All Employees Card will end in period
        [AllowAnonymous]
        [HttpGet, Route("GetAllEndCardinPeriod")]
        public IActionResult GetAllEndCardinPeriod(int warshahId, DateTime FromDate, DateTime ToDate)
        {

            var employees = _uow.DataEmployeeRepository.GetMany(e => e.WarshahId == warshahId && e.IdCardEnd >= FromDate && e.IdCardEnd <= ToDate).Include(a => a.Gender).Include(a => a.Nationality)
                .Include(a => a.EmployeeShift).Include(a => a.StatusEmployment)
                .Include(a => a.Role).Include(a => a.MaritalStatus)
                .Include(a => a.ContractType).Include(a => a.Warshah).ToHashSet();
            if (employees == null)
            {
                return Ok();
            }

            return Ok(employees);
        }





        #endregion



        #region Passport Report

        // Get All Employees Passport will end in month
        [AllowAnonymous]
        [HttpGet, Route("GetAllEndPassport")]
        public IActionResult GetAllEndPassport(int warshahId)
        {

            DateTime enddate = DateTime.Now.AddMonths(1);



            var employees = _uow.DataEmployeeRepository.GetMany(e => e.WarshahId == warshahId && e.PassportEndDate >= DateTime.Now && e.PassportEndDate <= enddate).Include(a => a.Gender).Include(a => a.Nationality)
                .Include(a => a.EmployeeShift).Include(a => a.StatusEmployment)
                .Include(a => a.Role).Include(a => a.MaritalStatus)
                .Include(a => a.ContractType).Include(a => a.Warshah).ToHashSet();
            if (employees == null)
            {
                return Ok();
            }

            return Ok(employees);
        }


        // Get All Employees Passport will end in period
        [AllowAnonymous]
        [HttpGet, Route("GetAllEndPassportinPeriod")]
        public IActionResult GetAllEndPassportinPeriod(int warshahId, DateTime FromDate, DateTime ToDate)
        {

            var employees = _uow.DataEmployeeRepository.GetMany(e => e.WarshahId == warshahId && e.PassportEndDate >= FromDate && e.PassportEndDate <= ToDate).Include(a => a.Gender).Include(a => a.Nationality)
                .Include(a => a.EmployeeShift).Include(a => a.StatusEmployment)
                .Include(a => a.Role).Include(a => a.MaritalStatus)
                .Include(a => a.ContractType).Include(a => a.Warshah).ToHashSet();
            if (employees == null)
            {
                return Ok();
            }

            return Ok(employees);
        }





        #endregion


        #region Retirement Report

        // Get All Employees Retirement will end in month
        [AllowAnonymous]
        [HttpGet, Route("GetAllRetirement")]
        public IActionResult GetAllRetirement(int warshahId)
        {

            DateTime enddate = DateTime.Now.AddMonths(1);

            var age = 60;


            var employees = _uow.DataEmployeeRepository.GetMany(e => e.WarshahId == warshahId && (enddate.Year - e.BirthDate.Year == age)).Include(a => a.Gender).Include(a => a.Nationality)
                .Include(a => a.EmployeeShift).Include(a => a.StatusEmployment)
                .Include(a => a.Role).Include(a => a.MaritalStatus)
                .Include(a => a.ContractType).Include(a => a.Warshah).ToHashSet();
            if (employees == null)
            {
                return Ok();
            }

            return Ok(employees);
        }

        #endregion



        // Get All Nationality by Id
        [AllowAnonymous]
        [HttpGet, Route("GetAllNationalityById")]
        public IActionResult GetAllNationalityById(int nationalId)
        {
            var employees = _uow.DataEmployeeRepository.GetMany(n=>n.NationalityId == nationalId).ToHashSet();
            return Ok(employees);
        }

        // Get All Gender by Id
        [AllowAnonymous]
        [HttpGet, Route("GetAllGenderById")]
        public IActionResult GetAllGenderById(int genderId)
        {
            var employees = _uow.DataEmployeeRepository.GetMany(n => n.GenderId == genderId).ToHashSet();
            return Ok(employees);
        }

        // Get All MartialStatus by Id
        [AllowAnonymous]
        [HttpGet, Route("GetAllMartialStatusById")]
        public IActionResult GetAllMartialStatusById(int martialId)
        {
            var employees = _uow.DataEmployeeRepository.GetMany(n => n.MaritalStatusId == martialId).ToHashSet();
            return Ok(employees);
        }

        // Get All MartialEmployement by Id
        [AllowAnonymous]
        [HttpGet, Route("GetAllMartialEmployementById")]
        public IActionResult GetAllMartialEmployementById(int martialemployeeId)
        {
            var employees = _uow.DataEmployeeRepository.GetMany(n => n.StatusEmploymentId == martialemployeeId).ToHashSet();
            return Ok(employees);
        }

        // Get All Shift by Id
        [AllowAnonymous]
        [HttpGet, Route("GetAllShiftById")]
        public IActionResult GetAllShiftById(int ShiftId)
        {
            var employees = _uow.DataEmployeeRepository.GetMany(n => n.EmployeeShiftId == ShiftId).ToHashSet();
            return Ok(employees);
        }

        // Get All JobTitle by Id
        [AllowAnonymous]
        [HttpGet, Route("GetAllJobTitleById")]
        public IActionResult GetAllJobTitleById(int jobtitleId)
        {
            var employees = _uow.DataEmployeeRepository.GetMany(n => n.RoleId == jobtitleId).ToHashSet();
            return Ok(employees);
        }

        // Get All ContractType by Id
        [AllowAnonymous]
        [HttpGet, Route("GetAllContractTypeById")]
        public IActionResult GetAllContractTypeById(int ContractTypeId)
        {
            var employees = _uow.DataEmployeeRepository.GetMany(n => n.ContractTypeId == ContractTypeId).ToHashSet();
            return Ok(employees);
        }


    }
}


