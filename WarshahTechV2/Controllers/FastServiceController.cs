using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.FastServiceDTO;
using DL.DTOs.InspectionDTOs;
using DL.Entities;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using WarshahTechV2.Models;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FastServiceController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public FastServiceController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("AddServiceCategory")]
        public IActionResult AddServiceCategory(ServiceCategoryDTO serviceCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                var Data = _mapper.Map<DL.Entities.ServiceCategory>(serviceCategoryDTO);
                _uow.ServiceCategoryRepository.Add(Data);
                _uow.Save();
                return Ok(Data);
            }
            return BadRequest(ModelState);

        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("EditServiceCategory")]
        public IActionResult EditServiceCategory(ServiceCategory serviceCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                
                _uow.ServiceCategoryRepository.Update(serviceCategoryDTO);
                _uow.Save();
                return Ok(serviceCategoryDTO);
            }
            return BadRequest(ModelState);

        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpDelete, Route("DeleteServiceCategory")]
        public IActionResult DeleteServiceCategory(int id)
        {


            _uow.ServiceCategoryRepository.Delete(id);
            _uow.Save();
            return Ok(id);



        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpDelete, Route("DeleteService")]
        public IActionResult DeleteService(int id)
        {


            _uow.ServiceRepository.Delete(id);
            _uow.Save();
            return Ok(id);



        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpGet,Route("GetServiceInvoice")]
        public IActionResult GetServiceInvoice(int Id)
        {
            var AllItems = _uow.ServiceInvoiceItemRepository.GetMany(a => a.ServiceInvoiceId == Id).Select(a => a.ItemName).ToHashSet();
            var Order = _uow.ServiceInvoiceRepository.GetById(Id);
            return Ok(new {AllItems=AllItems,Order=Order });
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpGet,Route("GetAllServices")]
        public IActionResult GetAllServices(int warshahId)
        {
            List<CategoryServiceDTO> categoryServiceDTOs = new List<CategoryServiceDTO>();

            var AllCats = _uow.ServiceCategoryRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet();
            foreach (var item in AllCats)
            {
                CategoryServiceDTO categoryServiceDTO = new CategoryServiceDTO();
                categoryServiceDTO.CategoryName = item.Name;
                var AllSers = _uow.ServiceRepository.GetMany(a => a.ServiceCategoryId == item.Id).ToHashSet();
                List<ServiceInvoiceItemDTO> serviceInvoiceItemDTOs = new List<ServiceInvoiceItemDTO>();
                foreach (var item2 in AllSers)
                {
                    ServiceInvoiceItemDTO serviceInvoiceItemDTO = new ServiceInvoiceItemDTO();
                    serviceInvoiceItemDTO.ItemName = item2.Name;
                    serviceInvoiceItemDTO.ItemPrice = item2.Price;
                    serviceInvoiceItemDTOs.Add(serviceInvoiceItemDTO);
                   

                }
                categoryServiceDTO.serviceInvoiceItemDTOs = serviceInvoiceItemDTOs;
                categoryServiceDTOs.Add(categoryServiceDTO);
            }
            return Ok(categoryServiceDTOs);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpPost, Route("AddService")]
        public IActionResult AddService(ServiceDTO serviceDTO)
        {
            if (ModelState.IsValid)
            {
                var Data = _mapper.Map<DL.Entities.Service>(serviceDTO);
                _uow.ServiceRepository.Add(Data);
                _uow.Save();
                return Ok(Data);
            }
            return BadRequest(ModelState);

        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpPost, Route("AddServiceInvoice")]
        public IActionResult AddServiceInvoice(ServiceInvoiceDTO fastInvoiceDTO)
        {
            if (ModelState.IsValid)
            {
                var Data = _mapper.Map<DL.Entities.ServiceInvoice>(fastInvoiceDTO);
                _uow.ServiceInvoiceRepository.Add(Data);
                var one = _uow.Save();
               
                return Ok(new { ServiceInvoice = Data });
            }
            return BadRequest(ModelState);

        }
        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.tech)]

        [HttpGet,Route("GetAllFastSevices")]
        public IActionResult GetAllFastSevices(int warshahId)
        {
            return Ok(_uow.ServiceInvoiceRepository.GetMany(a => a.WarshahId == warshahId && a.IsDeleted == false).Include(a=>a.Motors.motorMake).ToHashSet());
        }
        [HttpPost,Route("AddServiceToInvoice")]
        public IActionResult AddServiceToInvoice(ServiceInvoiceItemDTO serviceInvoiceItemDTO)
        {
            var Data = _mapper.Map<DL.Entities.ServiceInvoiceItem>(serviceInvoiceItemDTO);
            _uow.ServiceInvoiceItemRepository.Add(Data);
            _uow.Save();
            var Invoice = _uow.ServiceInvoiceRepository.GetById(serviceInvoiceItemDTO.ServiceInvoiceId);
            var AllItems = _uow.ServiceInvoiceItemRepository.GetMany(a => a.ServiceInvoiceId == serviceInvoiceItemDTO.ServiceInvoiceId).ToHashSet().Sum(a=>a.ItemPrice);
            Invoice.BeforeDiscount = AllItems;
            Invoice.afterDiscount = Invoice.BeforeDiscount - Invoice.Discount;
            decimal Vat = 0;
            var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == Invoice.WarshahId).FirstOrDefault();
            if (VAT == null)
            {
                Vat = 0;

            }
            else
            {
                Vat = (((decimal)VAT.GetVAT) / (100));
            }
            Invoice.Total = Invoice.afterDiscount + (Invoice.afterDiscount * Vat);
            Invoice.Vat = Invoice.Total - Invoice.afterDiscount;
            _uow.ServiceInvoiceRepository.Update(Invoice);
            _uow.Save();
            return Ok(Invoice);

        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpPost, Route("DeleteInvoiceItem")]
        public IActionResult DeleteInvoiceItem(DeleteInvoiceItem DeleteInvoiceItem)
        {
            var item = _uow.ServiceInvoiceItemRepository.GetMany(a => a.ServiceInvoiceId == DeleteInvoiceItem.InvoiceId && a.ItemPrice == DeleteInvoiceItem.ItemPrice && a.ItemName == DeleteInvoiceItem.ItemName).FirstOrDefault();
            if (item!=null)
            {
                _uow.ServiceInvoiceItemRepository.Delete(item.Id);
                _uow.Save();
                var Invoice = _uow.ServiceInvoiceRepository.GetById(DeleteInvoiceItem.InvoiceId);
                var AllItems = _uow.ServiceInvoiceItemRepository.GetMany(a => a.ServiceInvoiceId == DeleteInvoiceItem.InvoiceId).ToHashSet().Sum(a => a.ItemPrice);
                Invoice.BeforeDiscount = AllItems;
                Invoice.afterDiscount = Invoice.BeforeDiscount - Invoice.Discount;
                decimal Vat = 0;
                var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == Invoice.WarshahId).FirstOrDefault();
                if (VAT == null)
                {

                    Vat = 0;

                }
                else
                {
                    Vat = (((decimal)VAT.GetVAT) / (100));
                }
                Invoice.Total = Invoice.afterDiscount + (Invoice.afterDiscount * Vat);
                Invoice.Vat = Invoice.Total - Invoice.afterDiscount;
                _uow.ServiceInvoiceRepository.Update(Invoice);
                _uow.Save();
                return Ok(Invoice);

            }
            return BadRequest();
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetServiceCategory")]
        public IActionResult GetServiceCategory(int warshahId)
        {
            var Data = _uow.ServiceCategoryRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet();
            return Ok(Data);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetService")]
        public IActionResult GetService(int CategoryId)
        {
            var Data = _uow.ServiceRepository.GetMany(a => a.ServiceCategoryId == CategoryId).ToHashSet();
            return Ok(Data);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpPost, Route("AddAnonymousService")]
        public IActionResult AddAnonymousService(AddAnonymousServiceDTO DTO)
        {
            if (DTO.CategoryId== "Other-اخرى")
            {
                var Cat = _uow.ServiceCategoryRepository.GetMany(a => a.Name == "Other-اخرى"&&a.WarshahId==DTO.WarshahId).FirstOrDefault();
                if (Cat==null)
                {
                    _uow.ServiceCategoryRepository.Add(new ServiceCategory
                    {
                        Name = "Other-اخرى",
                        WarshahId= DTO.WarshahId,
                    }); 
                    _uow.Save();
                    Cat = _uow.ServiceCategoryRepository.GetMany(a => a.Name == "Other-اخرى" && a.WarshahId == DTO.WarshahId).FirstOrDefault();
                }
                var Service = new DL.Entities.Service
                {
                    Name = DTO.ServiceName,
                    Price = DTO.ServicePrice,
                    ServiceCategoryId = Cat.Id,
                    WarshahId =DTO.WarshahId
                };
                _uow.ServiceRepository.Add(Service);
                _uow.Save();
                return Ok(Service);
            }

            var ServiceNew = new DL.Entities.Service
            {
                Name = DTO.ServiceName,
                Price = DTO.ServicePrice,
                ServiceCategoryId = int.Parse(DTO.CategoryId),
                 WarshahId = DTO.WarshahId
            };
            _uow.ServiceRepository.Add(ServiceNew);
            _uow.Save();
            return Ok(ServiceNew);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpPost,Route("UpdateDiscount")]
        public IActionResult UpdateDiscount(UpdateDiscountDTO DTO)
        {
            var Invoice = _uow.ServiceInvoiceRepository.GetById(DTO.FastInvoiceId);
            Invoice.Discount = DTO.Disount;
            Invoice.afterDiscount = Invoice.BeforeDiscount - Invoice.Discount;
            decimal Vat = 0;
            var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == Invoice.WarshahId).FirstOrDefault();
            if (VAT == null)
            {

                Vat = 0;

            }
            else
            {
                Vat = (((decimal)VAT.GetVAT) / (100));
            }


            Invoice.Total = Invoice.afterDiscount + (Invoice.afterDiscount * Vat);
            Invoice.Vat = Invoice.Total - Invoice.afterDiscount;

            _uow.ServiceInvoiceRepository.Update(Invoice);
            _uow.Save();
            return Ok(Invoice);
        }


        [HttpPost, Route("EditFastService")]
        public IActionResult EditFastService(EditInspectionWarshahReportDTO FastId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Currentorder = _uow.ServiceInvoiceRepository.GetById(FastId.Id);
                    Currentorder.IsDeleted = true;
                    _uow.ServiceInvoiceRepository.Update(Currentorder);
                    _uow.Save();
                    return Ok(Currentorder);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid report");


        }

    } 
}
