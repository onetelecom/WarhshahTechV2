using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.InspectionDTOs;
using DL.Entities;
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
    public class InspectionReportController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public InspectionReportController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region InspectionSectionCRUD

        // Get All InspectionSection

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetSectionIncludeItemsToCheck")]
        public IActionResult GetInspectionSectionIncludeItems(int? Warshahid , int? TemplateId)
        {

            //var receptionid = _uow.RepairOrderRepository.GetMany(t => t.Id == id).FirstOrDefault().ReciptionOrderId;
            //var inspectiontemplateId = _uow.ReciptionOrderRepository.GetMany(t => t.Id == receptionid).FirstOrDefault().InspectionTemplateId;
            //var warshahid = _uow.RepairOrderRepository.GetMany(t => t.Id == id).FirstOrDefault().WarshahId;
            var sections = _uow.InspectionSectionRepository.GetMany(t => t.InspectionTemplateId == TemplateId);

            var DTOResult = new List<SectionIncludeItems>();

            
            foreach (var section in sections)
            {
                var Op = new SectionIncludeItems();
                Op.SectionName=section.SectionNameAr;
                             
                var it = _uow.InspectionItemsRepository.GetMany(t=>t.InspectionSectionId == section.Id && t.Status == true).ToHashSet();
                Op.Items = it;
                DTOResult.Add(Op);


            }

           

            return Ok(DTOResult);
        }






        //Create InspectionReport
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpPost, Route("CreateInspectionReport")]
        public IActionResult CreateInspectionReport(InspectionReportDTO inspectionReportDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //var reports = _uow.InspectionReportRepository.GetMany(t => t.InspectionWarshahReportId == inspectionReportDTO.InspectionWarshahReportId).ToHashSet();

                    var report = _mapper.Map<DL.Entities.InspectionReport>(inspectionReportDTO);



                    //if (reports.Count != 0)
                    //{

                    //    foreach (var record in reports)
                    //    {
                    //        var LastItemArabic = record.ItemNameAr;
                    //        var LastItemEnglish = record.ItemNameEn;


                    //            if (LastItemArabic != report.ItemNameAr && LastItemEnglish != report.ItemNameEn)
                    //            {
                    //            report.IsDeleted = false;
                    //            _uow.InspectionReportRepository.Add(report);
                    //            }

                    //            else
                    //            {
                    //            _uow.InspectionReportRepository.Delete(record.Id);
                    //            report.IsDeleted = false;
                    //            _uow.InspectionReportRepository.Add(report);
                    //            }



                    //    }


                    //}

                    //else
                    //{
                    //    report.IsDeleted = false;
                    //    _uow.InspectionReportRepository.Add(report);

                    //}

                    

                    report.IsDeleted = false;

                    _uow.InspectionReportRepository.Add(report);
                    _uow.Save();
                    return Ok(report);


                    //}

                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Report");
        }

        // Get All InspectionReport

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpGet, Route("GetInspectionReportByRepairId")]
        public IActionResult GetInspectionReportByRepairId(int Reportid)
        {
            var report = _uow.InspectionReportRepository.GetMany(t => t.InspectionWarshahReportId == Reportid).ToHashSet();

            List<DL.Entities.InspectionReport> result = new List<DL.Entities.InspectionReport>();   

            

            foreach(var record in report)
            {
                var LastItemArabic = record.ItemNameAr;
                var LastItemEnglish = record.ItemNameEn;


                if (result.Count != 0)
                {
                   
                    var re = result.LastOrDefault();

                       if (LastItemArabic != re.ItemNameAr && LastItemEnglish != re.ItemNameEn)
                       {
                            result.Add(record);                           
                       }

                       else
                       {

                        result.Remove(re);
                        result.Add(record);

                       }
                   
                }
                else
                {
                    result.Add(record);
                }

                
            }

            return Ok(result);
        }

        // Update InspectionReport
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpPost, Route("EditInspectionReport")]
        public IActionResult EditInspectionItems(EditInspectionReportDTO editInspectionReportDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var report = _mapper.Map<DL.Entities.InspectionReport>(editInspectionReportDTO);
                    _uow.InspectionReportRepository.Update(report);
                    _uow.Save();
                    return Ok(report);
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
