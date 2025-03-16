using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
using DL.DTOs.HR;
using DL.Entities.HR;
using DocumentFormat.OpenXml.Wordprocessing;
using HELPER;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
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

    public class RecordBonusController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public RecordBonusController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }




        //Create Gender 
       
        [HttpPost, Route("CreateRecordBonus")]
        public IActionResult CreateRecordBonus(RecordBonusTechnicalDTO recordBonusTechnicalDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var record = _mapper.Map<RecordBonusTechnical>(recordBonusTechnicalDTO);
                    record.IsDeleted = false;
                    _uow.RecordBonusRepository.Add(record);
                    _uow.Save();
                    return Ok(record);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid record");
        }




        
        [HttpGet, Route("GetAllRecordsByUserId")]
        public IActionResult GetAllRecordsByUserId(int userid)
        {
            var records = _uow.RecordBonusRepository.GetMany(r=>r.UserId == userid).Include(a=>a.User).ToHashSet();
            return Ok(records);
        }




        [HttpGet, Route("GetBonusTechnicalInTime")]
        public IActionResult GetBonusTechnicalInTime(int warshahId, DateTime FromDate, DateTime ToDate)
        {

            var bonus = _uow.RecordBonusRepository.GetMany(e => e.WarshahId == warshahId && e.CreatedOn >= FromDate && e.CreatedOn <= ToDate).Include(a => a.User).ToHashSet();
            if (bonus == null)
            {
                return Ok();
            }
            return Ok(bonus);
        }



        [HttpGet, Route("GetAllRecordsByWarshahId")]
        public IActionResult GetAllRecordsByWarshahId(int Warshahid , int pagenumber, int pagecount)
        {
            var records = _uow.RecordBonusRepository.GetMany(r => r.WarshahId == Warshahid).Include(a => a.User).ToHashSet();
           
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = records.Count(), Listinvoice = records.ToPagedList(pagenumber, pagecount) });
        }


    }
}
