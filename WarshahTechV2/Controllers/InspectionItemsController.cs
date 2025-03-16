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
    public class InspectionItemsController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public InspectionItemsController      (IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region InspectionItemsCRUD

        //Create InspectionItems
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpPost, Route("CreateInspectionItems")]
        public IActionResult CreateInspectionSection(InspectionItemsDTO inspectionItemsDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = _mapper.Map<DL.Entities.InspectionItem>(inspectionItemsDTO);
                    //if (item.WarshahId != null)
                    //{
                    //    item.IsCommon = false;
                    //}
                    item.IsDeleted = false;
                    item.Status = false;
                    _uow.InspectionItemsRepository.Add(item);
                    _uow.Save();
                    return Ok(item);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid itemName");
        }

        // Get All InspectionItems

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetInspectionItems")]
        public IActionResult GetInspectionItems(int id)
        {
            var items = _uow.InspectionItemsRepository.GetMany(t=>t.WarshahId == id).ToHashSet();
            return Ok(items.GroupBy(t => t.InspectionSectionId));
        }



        // Get  Item

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetItem")]
        public IActionResult GetItem(int Itemid)
        {
            var item = _uow.InspectionItemsRepository.GetById(Itemid);
            return Ok(item);
        }


        // Update InspectionItems
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpPost, Route("EditInspectionItems")]
        public IActionResult EditInspectionItems(EditInspectionItemsDTO editInspectionItemsDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = _mapper.Map<DL.Entities.InspectionItem>(editInspectionItemsDTO);
                    var currentitem = _uow.InspectionItemsRepository.GetById(item.Id);
                    item.InspectionSectionId = currentitem.InspectionSectionId;
                    item.WarshahId = currentitem.WarshahId;
                    _uow.InspectionItemsRepository.Update(item);
                    _uow.Save();
                    return Ok(item);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Item");


        }


        // Delete InspectionItem
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpDelete, Route("DeleteInspectionItem")]
        public IActionResult DeleteInspectionItem(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.InspectionItemsRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid InspectionItem");


        }

        #endregion
    }



}
