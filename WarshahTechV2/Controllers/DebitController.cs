using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.InvoiceDTOs;
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

    public class DebitController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public DebitController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region DebitCRUD

        //Create Debit 
        
        [HttpPost, Route("CreateDebit")]
        //public IActionResult CreateDebit(DebitNoticeDTO debitNotice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

                    

        //            var Debit = _mapper.Map<DL.Entities.DebitNotice>(debitNotice);

        //            var Debits = _uow.DebitNoticeRepository.GetMany(t => t.InvoiceId == Debit.InvoiceId).FirstOrDefault();
        //            if(Debits == null)
        //            {
        //                var invoice = _uow.InvoiceRepository.GetMany(r => r.Id == Debit.InvoiceId).FirstOrDefault();
        //                Debit.Total = invoice.Total;
        //                Debit.WarshahId = invoice.WarshahId;
        //                Debit.IsDeleted = false;
        //                _uow.DebitNoticeRepository.Add(Debit);
        //                _uow.Save();
        //                return Ok(Debit);
                       
        //            } 
        //            else
        //            {
        //                return Ok("this invoice have created a debit");
        //            }
                   
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex);
        //        }

        //    }

        //    return BadRequest("Invalid Debit");
        //}


        // Get  Debit by id


        [HttpGet, Route("GetDebit")]
        public IActionResult GetDebit(int? id)
        {

            var Debit = _uow.DebitNoticeRepository.GetMany(t=>t.Id==id).Include(t=>t.Invoice.RepairOrder.ReciptionOrder);

            return Ok(Debit);
        }


        // Get All Debits for warshah


        [HttpGet, Route("GetAllDebitsByWarshah")]
        public IActionResult GetAllDebitsByWarshah(int? id)
        {
            var Debits = _uow.DebitNoticeRepository.GetMany(t=>t.WarshahId == id).ToHashSet();
            return Ok(Debits);
        }


        // Delete Debit

        [HttpDelete, Route("DeleteDebit")]
     public IActionResult DeleteDebit(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.DebitNoticeRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Debit");


        }


        #endregion




    }
}
