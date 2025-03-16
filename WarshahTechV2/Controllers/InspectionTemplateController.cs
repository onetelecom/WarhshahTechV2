using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.InspectionDTOs;
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

    public class InspectionTemplateController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public InspectionTemplateController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }



        //Create InspectionTemplate
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpPost, Route("CreateInspectionTemplate")]
        public IActionResult InspectionTemplate(InspectionTemplateDTO inspectionTemplateDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var inspectionTemplate = _mapper.Map<DL.Entities.InspectionTemplate>(inspectionTemplateDTO);
                    if(inspectionTemplate.WarshahId != null)
                    {
                        inspectionTemplate.IsCommon = false;
                    }
                    inspectionTemplate.IsDeleted = false;
                    _uow.InspectionTemplateRepository.Add(inspectionTemplate);
                    _uow.Save();
                    return Ok(inspectionTemplate);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid InspectionTemplate");
        }



        // Update  InspectionTemplate

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("EditInspectionTemplate")]
        public IActionResult EditInspectionTemplate(EditInspectionTemplateDTO editInspectionTemplateDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Template = _mapper.Map<DL.Entities.InspectionTemplate>(editInspectionTemplateDTO);
                    if (Template.WarshahId != null)
                    {
                        Template.IsCommon = false;
                    }
                    _uow.InspectionTemplateRepository.Update(Template);
                    _uow.Save();
                    return Ok(Template);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Template");


        }



        // Get InspectionTemplate By Id

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetInspectionTemplateById")]
        public IActionResult GetInspectionTemplateById(int id)
        {
            var Template = _uow.InspectionTemplateRepository.GetById(id);
            return Ok(Template);
        }





        // Get All InspectionTemplate

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpGet, Route("GetAllInspectionTemplate")]
        public IActionResult GetAllInspectionTemplate(int id)
        {
            var Templates = _uow.InspectionTemplateRepository.GetMany(t=>(t.IsCommon == true && t.Describtion == null) || ( t.WarshahId == id && t.IsCommon==false) ).ToHashSet();
            return Ok(Templates);
        }




        // Delete InspectionTemplate
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpDelete, Route("DeleteInspectionTemplate")]
        public IActionResult DeleteInspectionTemplate(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.InspectionTemplateRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid InspectionTemplate");


        }


    }
}
