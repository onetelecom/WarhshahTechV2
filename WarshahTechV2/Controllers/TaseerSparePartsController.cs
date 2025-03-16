using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.SparePartsDTOs;
using HELPER;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Security.Claims;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Web;
using BL.Security;

namespace WarshahTechV2.Controllers
{
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class TaseerSparePartsController : ControllerBase
    {
        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public TaseerSparePartsController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region SparePartsCRUD

        //Create SparePart

        [HttpPost, Route("CreateTaseerSpartPart")]
        public IActionResult CreateTaseerSpartPart(TaseerSparePartDTO sparePartTaseerDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var SparePartTaseer = _mapper.Map<DL.Entities.SparePartTaseer>(sparePartTaseerDTO);
                    SparePartTaseer.IsDeleted = false;
                    SparePartTaseer.CreatedOn = DateTime.Now;
                    _uow.SparePartTaseerRepository.Add(SparePartTaseer);
                    _uow.Save();
                    return Ok(SparePartTaseer);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SparePartTaseer");
        }

        // Get All SparePartTaseer

        [HttpGet, Route("GetAllSparePartTaseer")]
        public IActionResult GetAllSparePartTaseer()
        {
            var SpareParts = _uow.SparePartTaseerRepository.GetAll().Include(a=>a.motorYear).Include(a => a.MotorMake).Include(a => a.MotorModel).ToHashSet();
            return Ok(SpareParts);
        }




        // Get  SparePartTaseer Filter FromRepair

        [HttpGet, Route("GetFilterSparePartTaseerFromRepair")]
        public IActionResult GetFilterSparePartTaseerFromRepair( int motorid , string partname)
        {


            
            //var name = HttpUtility.UrlEncode(partname.ToString());

           

            var motor = _uow.MotorsRepository.GetById(motorid);

        
            var SparePart = _uow.SparePartTaseerRepository.GetMany(a=>a.MotorYearId == motor.MotorYearId && a.MotorMakeId == motor.MotorMakeId && a.MotorModelId == motor.MotorModelId && a.SparePartName ==partname).Include(a=>a.MotorModel).Include(a=>a.MotorMake).Include(a=>a.motorYear).FirstOrDefault();
            return Ok(SparePart);
        }


        // Get  SparePartTaseer Filter FromWarshah

        [HttpGet, Route("GetFilterSparePartTaseerFromWarshah")]
        public IActionResult GetFilterSparePartTaseerFromWarshah(int year, string make, string model , string partname )
        {


            var y = _uow.MotorYearRepository.GetMany(a => a.Year == year).FirstOrDefault();

            var yearId = y.Id;
            
            var SparePartAr = _uow.SparePartTaseerRepository.GetMany(a => a.MotorYearId == yearId && a.MotorMake.MakeNameAr == make && a.MotorModel.ModelNameAr == model && a.SparePartName == partname).Include(a => a.MotorModel).Include(a => a.MotorMake).Include(a => a.motorYear).FirstOrDefault();

            var SparePartEn= _uow.SparePartTaseerRepository.GetMany(a => a.MotorYearId == yearId && a.MotorMake.MakeNameEn == make && a.MotorModel.ModelNameEn == model && a.SparePartName == partname).Include(a => a.MotorModel).Include(a => a.MotorMake).Include(a => a.motorYear).FirstOrDefault();


            return Ok(new{PartAr = SparePartAr , PartEn = SparePartEn });
        }





        // Get  SparePartTaseer with Id


        [HttpGet, Route("GetSparePartTaseerById")]
        public IActionResult GetSparePartTaseerById(int id)
        {
            var PartTaseer = _uow.SparePartTaseerRepository.GetMany(m => m.Id == id).FirstOrDefault();
            return Ok(PartTaseer);
        }


        // Update SparePartTaseer

        [HttpPost, Route("EditSparePartTaseer")]
        public IActionResult EditSparePartTaseer(EditTaseerSparePartDTO editTaseerSparePartDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var TaseerSparePart = _mapper.Map<DL.Entities.SparePartTaseer>(editTaseerSparePartDTO);
                    TaseerSparePart.IsDeleted = false;
                    _uow.SparePartTaseerRepository.Update(TaseerSparePart);
                    _uow.Save();
                    return Ok(TaseerSparePart);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid TaseerSparePart");


        }


        // Delete TaseerSparePart
        [HttpDelete, Route("DeleteTaseerSparePart")]
        public IActionResult DeleteTaseerSparePart(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var s = _uow.SparePartTaseerRepository.GetById(Id);
                    if (s != null)
                    {
                        _uow.SparePartTaseerRepository.Delete(Id);
                        _uow.Save();
                        return Ok();
                    }
                    return BadRequest(new {status = "NoData"});

                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid TaseerSparePart");


        }

        #endregion


    }
}
