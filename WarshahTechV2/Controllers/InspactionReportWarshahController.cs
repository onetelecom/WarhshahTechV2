using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.InspectionDTOs;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]

    public class InspactionReportWarshahController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public InspactionReportWarshahController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region InspactionReportWarshahCRUD

        //Create Report 
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateReport")]
        public IActionResult CreateReport(InspectionWarshahReportDTO inspectionWarshahReport)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var insp = _mapper.Map<DL.Entities.InspectionWarshahReport>(inspectionWarshahReport);

                    if(inspectionWarshahReport.KM_IN == null)
                    {
                        inspectionWarshahReport.KM_IN = 0;
                    }
                    insp.IsDeleted = false;
                    insp.CreatedOn = DateTime.Now;  
                    _uow.InspectionWarshahReportRepository.Add(insp);
                    _uow.Save();
                  
                
                    return Ok(insp);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Report");
        }


        // Get All Report

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpGet, Route("GetReportById")]
        public IActionResult GetReportById(int? ReportId)
        {
            var Reports = _uow.InspectionReportRepository.GetMany(r => r.InspectionWarshahReportId == ReportId).ToHashSet();
            return Ok(Reports);

        }



        // Get All Reports By WarshahId

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpGet, Route("GetAllReportByWarshahId")]
        public IActionResult GetAllReport(int? warshahid)
        {
            var Reports = _uow.InspectionWarshahReportRepository.GetMany(r=>r.WarshahId == warshahid && r.IsDeleted == false).Include(a=>a.CarOwner).Include(a=>a.Motors).ToHashSet();
            return Ok(Reports);

        }


        // Get All Reports by Car owner

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetAllReportByCarowner")]
        public IActionResult GetAllReportByCarowner(int? carownerid)
        {
            var Reports = _uow.InspectionWarshahReportRepository.GetMany(r => r.CarOwnerId == carownerid && r.IsDeleted == false).Include(a=>a.CarOwner).ToHashSet();
            return Ok(Reports);

        }


        // Edit ot cancle order
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpPost, Route("EditInspectionWarshahReport")]
        public IActionResult EditInspectionWarshahReport(EditInspectionWarshahReportDTO insporder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                 var   Currentorder = _uow.InspectionWarshahReportRepository.GetById(insporder.Id);
                       Currentorder.IsDeleted = true;
                    _uow.InspectionWarshahReportRepository.Update(Currentorder);
                    _uow.Save();
                    return Ok(Currentorder);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid report");


        }

        #endregion




    }
}
