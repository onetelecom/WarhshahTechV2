using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.InvoiceDTOs;
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

    public class PaymentTypeInvoiceController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public PaymentTypeInvoiceController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region PaymentTypeCRUD

        //Create PaymentType 
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]


        [HttpPost, Route("CreatePaymentType")]
        public IActionResult CreatePaymentType(PaymentTypeInvoiceDTO paymentTypeInvoiceDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var paymentType = _mapper.Map<DL.Entities.PaymentTypeInvoice>(paymentTypeInvoiceDTO);
                    paymentType.IsDeleted = false;
                    _uow.PaymentTypeInvoiceRepository.Add(paymentType);
                    _uow.Save();
                    return Ok(paymentType);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Payment Name");
        }



        // Get All PaymentType

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetAllPaymentType")]
        public IActionResult GetAllPaymentType()
        {
            var PaymentType = _uow.PaymentTypeInvoiceRepository.GetAll().ToHashSet();
            return Ok(PaymentType);
        }

        // Update  PaymentType

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]

        [HttpPost, Route("EditPaymentType")]
        public IActionResult EditPaymentType(EditPaymentInvoiceDTO editPaymentInvoiceDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var paymentType = _mapper.Map<DL.Entities.PaymentTypeInvoice>(editPaymentInvoiceDTO);
                    _uow.PaymentTypeInvoiceRepository.Update(paymentType);
                    _uow.Save();
                    return Ok(paymentType);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid PaymentType");


        }

        // Delete PaymentType
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]

        [HttpDelete, Route("DeletePaymentType")]
        public IActionResult DeletePaymentType(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.PaymentTypeInvoiceRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid PaymentType");


        }


        #endregion




    }
}
