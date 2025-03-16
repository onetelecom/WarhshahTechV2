using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.InvoiceDTOs;
using DL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarshahTechV2.Models;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]

    public class CreditorController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public CreditorController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region CreditorCRUD

        //Create Creditor 
         
        [HttpPost, Route("CreateCreditor")]
        //public IActionResult CreateCreditor(CreditorNoticeDTO creditorNotice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            var Creatidor = _mapper.Map<DL.Entities.CreditorNotice>(creditorNotice);
        //            var Creatidors = _uow.CreditorNoticeRepository.GetMany(t => t.InvoiceId == Creatidor.InvoiceId).FirstOrDefault();
        //            if(Creatidors == null)
        //            {
        //                var invoice = _uow.InvoiceRepository.GetMany(r => r.Id == creditorNotice.InvoiceId).FirstOrDefault();
        //                var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == invoice.WarshahId).FirstOrDefault().GetVAT;
        //                decimal Vat = (((decimal)VAT) / (100));
        //                Creatidor.ReturnVat = Creatidor.ReturnMoney * Vat;
        //                Creatidor.Total = Creatidor.ReturnMoney + Creatidor.ReturnVat;
        //                Creatidor.WarshahId = invoice.WarshahId;
        //                Creatidor.IsDeleted = false;
        //                _uow.CreditorNoticeRepository.Add(Creatidor);
        //                _uow.Save();
        //                return Ok(Creatidor);
        //            }

        //            else
        //            {
        //                return Ok("this invoice have created a Creatidor");
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex);
        //        }

        //    }

        //    return BadRequest("Invalid Creatidor");
        //}



        // Get  Creditor by id


        [HttpGet, Route("GetCreditor")]
        public IActionResult GetCreditor(int? id)
        {

            var GetCreditor = _uow.CreditorNoticeRepository.GetMany(t => t.Id == id).Include(t => t.Invoice.RepairOrder.ReciptionOrder);

            return Ok(GetCreditor);
        }




        // Get All Creditors


        [HttpGet, Route("GetAllCreditorsByWarshah")]
        public IActionResult GetAllCreditorsByWarshah(int? id)
        {
            var Creditors = _uow.CreditorNoticeRepository.GetMany(t => t.WarshahId == id).ToHashSet();
         
            return Ok(Creditors);
        }


        // Update Creditors

        [HttpPost, Route("EditCreditors")]
        public IActionResult EditCreditors(EditCreaditorNoticeDTO editCreaditorNoticeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Creditors = _mapper.Map<DL.Entities.CreditorNotice>(editCreaditorNoticeDTO);
                    var CurrentCreditor = _uow.CreditorNoticeRepository.Get(t => t.Id == Creditors.Id);
                    Creditors.WarshahId = CurrentCreditor.WarshahId;
                    Creditors.InvoiceId = CurrentCreditor.InvoiceId;
                    decimal Vat = 0;
                    var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == Creditors.WarshahId).FirstOrDefault();
                    if (VAT == null)
                    {

                        var vAT = 15;
                        Vat = (((decimal)vAT) / (100));

                    }
                    else
                    {
                        Vat = (((decimal)VAT.GetVAT) / (100));
                    }
                    Creditors.ReturnVat = Creditors.ReturnMoney * Vat;
                    Creditors.Total = Creditors.ReturnMoney + Creditors.ReturnVat;
                    _uow.CreditorNoticeRepository.Update(Creditors);
                    _uow.Save();
                    return Ok(Creditors);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Creditors");


        }


        // Delete Debit

        [HttpDelete, Route("DeleteCreditors")]
        public IActionResult DeleteCreditors(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.CreditorNoticeRepository.Delete(Id);
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
