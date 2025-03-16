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

    public class JobtitleController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public JobtitleController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }




        //Create Job 
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Hr)]
        [HttpPost, Route("CreateJob")]
        public IActionResult CreateJob(JobTitleDTO jobTitle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Job = _mapper.Map<JobTitle>(jobTitle);
                    Job.IsDeleted = false;
                    _uow.JobtitleRepository.Add(Job);
                    _uow.Save();
                    return Ok(Job);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Job");
        }


        [AllowAnonymous]
        [HttpGet, Route("GetJobByID")]
        public IActionResult GetJobByID(int id)
        {
            var job = _uow.JobtitleRepository.GetById(id);
            return Ok(job);
        }



        [AllowAnonymous]
        [HttpGet, Route("GetAllJob")]
        public IActionResult GetAllJob()
        {
            var jobs = _uow.JobtitleRepository.GetAll().ToHashSet();
            return Ok(jobs);
        }
    }
}
