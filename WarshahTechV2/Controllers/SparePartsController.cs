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
using DL.Entities;
using DL.DTOs;

namespace WarshahTechV2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
 

    public class SparePartsController : ControllerBase
    {
        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public SparePartsController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region SparePartsCRUD

        //Create SparePart

        [HttpPost, Route("CreateSpartPart")]
        public IActionResult CreateSpartPart(SparePartDTO sparePartDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var SparePart = _mapper.Map<DL.Entities.SparePart>(sparePartDTO);
                    if (sparePartDTO.PartNum != null)
                    {
                        var ExistPartNum = _uow.SparePartRepository.GetAll().FirstOrDefault(i => i.PartNum == sparePartDTO.PartNum &&i.WarshahId==sparePartDTO.WarshahId);
                        if (ExistPartNum != null)
                        {
                            return BadRequest("sorry,this Part Num already exist ,please add a valid  Part Num ");

                        }

                    }
                    else
                    {
                        return BadRequest("Empty SparePart");
                    }
                    SparePart.IsDeleted = false;
                    SparePart.CreatedOn = DateTime.Now;

                    if (SparePart.PeacePrice == 0)
                    {
                        decimal margin = (((decimal)SparePart.MarginPercent) / (100));
                        decimal addearn = SparePart.BuyingPrice * margin;
                        SparePart.PeacePrice = SparePart.BuyingPrice + addearn;
                    }

                    //var Vat = _uow.ConfigrationRepository.GetMany(a => a.WarshahId == SparePart.WarshahId).FirstOrDefault().GetVAT;

                    //decimal Vat = 15;
                    //decimal VatPercent = (((decimal)Vat) / (100)) + 1;


                    decimal VP = 0;
                    var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == SparePart.WarshahId).FirstOrDefault();
                    if (VAT == null)
                    {

                        var vAT = 0;
                        VP = (((decimal)vAT) / (100)) + 1;

                    }
                    else
                    {
                        VP = (((decimal)VAT.GetVAT) / (100)) + 1;
                    }
                    SparePart.BuyBeforeVat = SparePart.BuyingPrice / VP;
                    SparePart.VatBuy = SparePart.BuyingPrice - SparePart.BuyBeforeVat;
                    SparePart.SellBeforeVat = SparePart.PeacePrice / VP;
                    SparePart.VatSell = SparePart.PeacePrice - SparePart.SellBeforeVat;

                    _uow.SparePartRepository.Add(SparePart);


                    // add to TransactionInventory

                    TransactionInventory transactionsToday = new TransactionInventory();
                    transactionsToday.CreatedOn = DateTime.Now;
                    transactionsToday.SparePartName = SparePart.SparePartName;
                    transactionsToday.WarshahId = (int)SparePart.WarshahId;
                    transactionsToday.NoofQuentity = SparePart.Quantity;
                    transactionsToday.OldQuentity = 0;
                    transactionsToday.CurrentQuentity = transactionsToday.OldQuentity + transactionsToday.NoofQuentity;

                    transactionsToday.TransactionName = "إضافة إلى المخزون";

                    _uow.TransactionInventoryRepository.Add(transactionsToday);
                    _uow.Save();
                    return Ok(SparePart);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SparePart");
        }

        // Get All SparePart

        [HttpGet, Route("GetAllSparePart")]
        public IActionResult GetAllSparePart()
        {
            var SpareParts = _uow.SparePartRepository.GetAll().ToHashSet();
            return Ok(SpareParts);
        }

        // Get All SparePart with Warshah

        [HttpGet, Route("GetSparePartByWarshahId")]
        public IActionResult GetSparePartByWarshahId(int id)
        {
            var Parts = _uow.SparePartRepository.GetMany(m => m.WarshahId == id).Include(s => s.Supplier).Include(s => s.MotorModel.MotorMake).Include(s=>s.motorYear).ToHashSet();

            return Ok(Parts);
        }

        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpGet, Route("GetSparePartByWarshahIdForOrder")]
        public IActionResult GetSparePartByWarshahIdForOrder(int id, int MakeId, int ModelId, int YearId)
        {
            var Parts = _uow.SparePartRepository.GetMany(m => m.WarshahId == id && m.MotorMakeId == MakeId && m.MotorModelId == ModelId && m.MotorYearId == YearId).Include(s => s.Supplier).Include(s => s.MotorModel.MotorMake).ToHashSet();
            return Ok(Parts);
        }

        // Get  SparePart with Id


        [HttpGet, Route("GetSparePartById")]
        public IActionResult GetSparePartById(int id)
        {
            var Part = _uow.SparePartRepository.GetMany(m => m.Id == id).FirstOrDefault();
            return Ok(Part);
        }


        // Update SparePart

        [HttpPost, Route("EditSparePart")]
        public IActionResult EditSparePart(EditSparePartDTO editSparePartDTO)
        {

            var part = _uow.SparePartRepository.GetById(editSparePartDTO.Id);

            var currentqty = part.Quantity;

            if (ModelState.IsValid)
            {

                try
                {

                    if (part.PartNum == editSparePartDTO.PartNum)
                    {
                        var SparePart = _mapper.Map<DL.Entities.SparePart>(editSparePartDTO);
                        SparePart.IsDeleted = false;
                        if (SparePart.PeacePrice == 0)
                        {
                            decimal margin = (((decimal)SparePart.MarginPercent) / (100));
                            decimal addearn = SparePart.BuyingPrice * margin;
                            SparePart.PeacePrice = SparePart.BuyingPrice + addearn;
                        }
                        // add to TransactionInventory
                        TransactionInventory transactionsToday = new TransactionInventory();
                        transactionsToday.CreatedOn = DateTime.Now;
                        transactionsToday.SparePartName = SparePart.SparePartName;
                        transactionsToday.WarshahId = (int)SparePart.WarshahId;
                        transactionsToday.NoofQuentity = SparePart.Quantity;
                        transactionsToday.OldQuentity = currentqty;
                        transactionsToday.CurrentQuentity = transactionsToday.OldQuentity + transactionsToday.NoofQuentity;
                        transactionsToday.TransactionName = "تعديل";
                        _uow.TransactionInventoryRepository.Add(transactionsToday);
                        //var Vat = _uow.ConfigrationRepository.GetMany(a => a.WarshahId == SparePart.WarshahId).FirstOrDefault().GetVAT;
                        //decimal Vat = 15;
                        //decimal VatPercent = (((decimal)Vat) / (100)) + 1;

                        decimal VP = 0;
                        var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == SparePart.WarshahId).FirstOrDefault();
                        if (VAT == null)
                        {

                            var vAT = 0;
                            VP = (((decimal)vAT) / (100)) + 1;

                        }
                        else
                        {
                            VP = (((decimal)VAT.GetVAT) / (100)) + 1;
                        }
                        SparePart.BuyBeforeVat = SparePart.BuyingPrice / VP;
                        SparePart.VatBuy = SparePart.BuyingPrice - SparePart.BuyBeforeVat;
                        SparePart.SellBeforeVat = SparePart.PeacePrice / VP;
                        SparePart.VatSell = SparePart.PeacePrice - SparePart.SellBeforeVat;
                        SparePart.Quantity = SparePart.Quantity;
                        _uow.SparePartRepository.Update(SparePart);
                       _uow.Save();

                       return Ok(SparePart);

                    }


                    if (editSparePartDTO.PartNum != null)
                    {
                        var ExistPartNum = _uow.SparePartRepository.GetAll().FirstOrDefault(i => i.PartNum == editSparePartDTO.PartNum && i.WarshahId == editSparePartDTO.WarshahId);
                        if (ExistPartNum != null)
                        {
                            return BadRequest("sorry,this Part Num already exist ,please add a valid  Part Num ");

                        }

                        var SparePart = _mapper.Map<DL.Entities.SparePart>(editSparePartDTO);
                        SparePart.IsDeleted = false;
                        if (SparePart.PeacePrice == 0)
                        {
                            decimal margin = (((decimal)SparePart.MarginPercent) / (100));
                            decimal addearn = SparePart.BuyingPrice * margin;
                            SparePart.PeacePrice = SparePart.BuyingPrice + addearn;
                        }
                        // add to TransactionInventory
                        TransactionInventory transactionsToday = new TransactionInventory();
                        transactionsToday.CreatedOn = DateTime.Now;
                        transactionsToday.SparePartName = SparePart.SparePartName;
                        transactionsToday.WarshahId = (int)SparePart.WarshahId;
                        transactionsToday.NoofQuentity = SparePart.Quantity;
                        transactionsToday.OldQuentity = currentqty;
                        transactionsToday.CurrentQuentity = transactionsToday.OldQuentity + transactionsToday.NoofQuentity;
                        transactionsToday.TransactionName = "تعديل";
                        _uow.TransactionInventoryRepository.Add(transactionsToday);
                        //var Vat = _uow.ConfigrationRepository.GetMany(a => a.WarshahId == SparePart.WarshahId).FirstOrDefault().GetVAT;
                        //decimal Vat = 15;
                        //decimal VatPercent = (((decimal)Vat) / (100)) + 1;

                        decimal VP = 0;
                        var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == SparePart.WarshahId).FirstOrDefault();
                        if (VAT == null)
                        {

                            var vAT = 0;
                            VP = (((decimal)vAT) / (100)) + 1;

                        }
                        else
                        {
                            VP = (((decimal)VAT.GetVAT) / (100)) + 1;
                        }
                        SparePart.BuyBeforeVat = SparePart.BuyingPrice / VP;
                        SparePart.VatBuy = SparePart.BuyingPrice - SparePart.BuyBeforeVat;
                        SparePart.SellBeforeVat = SparePart.PeacePrice / VP;
                        SparePart.VatSell = SparePart.PeacePrice - SparePart.SellBeforeVat;
                        SparePart.Quantity = SparePart.Quantity;
                        _uow.SparePartRepository.Update(SparePart);
                        _uow.Save();

                        return Ok(SparePart);




                    }
                    else
                    {
                        return BadRequest("Empty SparePart");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SparePart");


        }


        // Delete SparePart
        [HttpDelete, Route("DeleteSparePart")]
        public IActionResult DeleteSparePart(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.SparePartRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SparePart");


        }

        #endregion


        #region InstantParts

        [HttpPost, Route("CreateInstantSpartPart")]
        public IActionResult CreateInstantSpartPart(InstantPartDTO  sparePartDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var SparePart = _mapper.Map<DL.Entities.InstantPart>(sparePartDTO);
                    if (sparePartDTO.PartNum != null)
                    {
                        var ExistPartNum = _uow.InstantPartRepository.GetAll().FirstOrDefault(i => i.PartNum == sparePartDTO.PartNum && i.WarshahId == sparePartDTO.WarshahId);
                        if (ExistPartNum != null)
                        {
                            return BadRequest("sorry,this Part Num already exist ,please add a valid  Part Num ");

                        }

                    }
                    else
                    {
                        return BadRequest("Empty SparePart");
                    }
                    SparePart.IsDeleted = false;
                    SparePart.CreatedOn = DateTime.Now;

                  
                    decimal VP = 0;
                    var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == SparePart.WarshahId).FirstOrDefault();
                    if (VAT == null)
                    {

                        var vAT = 0;
                        VP = (((decimal)vAT) / (100)) + 1;

                    }
                    else
                    {
                        VP = (((decimal)VAT.GetVAT) / (100)) + 1;
                    }
                    SparePart.BuyBeforeVat = SparePart.BuyingPrice / VP;
                    SparePart.VatBuy = SparePart.BuyingPrice - SparePart.BuyBeforeVat;
                    SparePart.SellBeforeVat = SparePart.PeacePrice / VP;
                    SparePart.VatSell = SparePart.PeacePrice - SparePart.SellBeforeVat;

                    _uow.InstantPartRepository.Add(SparePart);


                    // add to TransactionInventory

                    //TransactionInventory transactionsToday = new TransactionInventory();
                    //transactionsToday.CreatedOn = DateTime.Now;
                    //transactionsToday.SparePartName = SparePart.SparePartName;
                    //transactionsToday.WarshahId = (int)SparePart.WarshahId;
                    //transactionsToday.NoofQuentity = SparePart.Quantity;
                    //transactionsToday.OldQuentity = 0;
                    //transactionsToday.CurrentQuentity = transactionsToday.OldQuentity + transactionsToday.NoofQuentity;

                    //transactionsToday.TransactionName = "إضافة إلى المخزون";

                    //_uow.TransactionInventoryRepository.Add(transactionsToday);
                    _uow.Save();
                    return Ok(SparePart);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SparePart");
        }

        [HttpPost, Route("EditInstantSparePart")]
        public IActionResult EditInstantSparePart(EditInstantPartDTO editSparePartDTO)
        {

            var part = _uow.SparePartRepository.GetById(editSparePartDTO.Id);

            //var currentqty = part.Quantity;

            if (ModelState.IsValid)
            {

                try
                {

                    var SparePart = _mapper.Map<DL.Entities.InstantPart>(editSparePartDTO);
                    decimal VP = 0;
                    var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == SparePart.WarshahId).FirstOrDefault();
                    if (VAT == null)
                    {

                        var vAT = 0;
                        VP = (((decimal)vAT) / (100)) + 1;

                    }
                    else
                    {
                        VP = (((decimal)VAT.GetVAT) / (100)) + 1;
                    }
                    SparePart.BuyBeforeVat = SparePart.BuyingPrice / VP;
                    SparePart.VatBuy = SparePart.BuyingPrice - SparePart.BuyBeforeVat;
                    SparePart.SellBeforeVat = SparePart.PeacePrice / VP;
                    SparePart.VatSell = SparePart.PeacePrice - SparePart.SellBeforeVat;
                    SparePart.Quantity = SparePart.Quantity;
                    SparePart.WarshahId = part.WarshahId;
                    _uow.InstantPartRepository.Update(SparePart);
                    _uow.Save();

                    return Ok(SparePart);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SparePart");


        }

        [HttpGet, Route("GetInstantSparePartById")]
        public IActionResult GetInstantSparePartById(int id)
        {
            var Part = _uow.InstantPartRepository.GetMany(m => m.Id == id).FirstOrDefault();
            return Ok(Part);
        }

        [HttpGet, Route("GetInstantSparePartByWarshahId")]
        public IActionResult GetInstantSparePartByWarshahId(int warshahid)
        {
            var Parts = _uow.InstantPartRepository.GetMany(m => m.WarshahId == warshahid).ToHashSet();

            return Ok(Parts);
        }

        #endregion


    }
}
