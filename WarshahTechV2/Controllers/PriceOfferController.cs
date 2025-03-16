using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
using DL.DTOs.InvoiceDTOs;
using DL.DTOs.JobCardDtos;
using DL.Entities;
using DL.MailModels;
using DL.Migrations;
using DocumentFormat.OpenXml.Office2010.Excel;
using Helper;
using HELPER;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Linq;
using System.Security.Claims;
using WarshahTechV2.Models;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceOfferController : ControllerBase
    {

        private readonly IUnitOfWork _uow;
        private readonly ILog _log;

        private readonly ISMS _SMS;
        private readonly MailSettings _mailSettings;





        private readonly INotificationService _NotificationService;


        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMailService _mailService;
        public PriceOfferController (INotificationService NotificationService, IHostingEnvironment _hostingEnvironment, ISMS SMS, IMailService mailService, IMapper mapper,
          IUnitOfWork uow
          )
        {
            _SMS = SMS;

            _uow = uow;
            this._hostingEnvironment = _hostingEnvironment;
            _NotificationService = NotificationService;
            _mapper = mapper;
            _mailService = mailService;


        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("AddPriceOffer")]
        public IActionResult AddPriceOffer(DataPriceOfferDTO dataPrice)
        {
            try
            {
                var Data = _mapper.Map<DL.Entities.PriceOffer>(dataPrice);

                var Offernumber = _uow.PriceOfferRepository.GetMany(i => i.WarshahId == Data.WarshahId).OrderByDescending(t => t.OfferNumber).FirstOrDefault();
                if (Offernumber == null)
                {
                    Data.OfferNumber = 1;
                }
                else
                {
                    int lastnumber = Offernumber.OfferNumber;
                    Data.OfferNumber = lastnumber + 1;
                }

                Data.CreatedOn = DateTime.Now;

                var model = _uow.MotorModelRepository.GetMany(c => c.Id == dataPrice.MotorModelId)?.FirstOrDefault().ModelNameAr;

                var make = _uow.MotorMakeRepository.GetMany(c => c.Id == dataPrice.MotorMakeId)?.FirstOrDefault().MakeNameAr;

                 Data.CarType = make + " - " + model;

            
                _uow.PriceOfferRepository.Add(Data);
                _uow.Save();
           




                return Ok(Data);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }


        }

         
        [HttpPost, Route("AddSparePartToPriceOffer")]
        public IActionResult AddSparePartToPriceOffer(PriceOfferItemDTO priceOfferItem)
        {
             var Parts = _mapper.Map<DL.Entities.PriceOfferItem>(priceOfferItem);
             Parts.CreatedOn = DateTime.Now;
            _uow.PriceOfferItemRepository.Add(Parts);
            _uow.Save();
            return Ok(Parts);
        }


        [HttpPost, Route("RemoveSparePartFromPriceOffer")]
        public IActionResult RemoveSparePartFromPriceOffer(RemoveFromPriceOfferDTO removeFromPrice)
        {
            var SparePart = _uow.PriceOfferItemRepository.GetMany(a => a.PriceOfferId == removeFromPrice.OfferId && a.SparePartNameAr == removeFromPrice.PartName).FirstOrDefault();
            if (SparePart != null)
            {
                _uow.PriceOfferItemRepository.Delete(SparePart.Id);
                _uow.Save();
                return Ok(SparePart);

            }
            return BadRequest(new { status = "No Part For The Repair Order" });

        }


        [HttpGet, Route("GetPartsWithPriceOfferID")]
        public IActionResult GetPartsWithPriceOfferID(int offerid)
        {
            var Parts = _uow.PriceOfferItemRepository.GetMany(p => p.PriceOfferId == offerid).ToHashSet().OrderByDescending(a => a.Id);
            return Ok(Parts);
        }


        [HttpPost, Route("AddServiceToPriceOffer")]
        public IActionResult AddServiceToPriceOffer(ServicePriceOfferDTO priceOfferItem)
        {
            var Parts = _mapper.Map<DL.Entities.ServicePriceOffer>(priceOfferItem);
            Parts.CreatedOn = DateTime.Now;
            _uow.ServicePriceOfferRepository.Add(Parts);
            _uow.Save();
            return Ok(Parts);
        }


        [HttpPost, Route("RemoveServicesFromPriceOffer")]
        public IActionResult RemoveServicesFromPriceOffer(RemoveServiceFromOffer removeFromPrice)
        {
            var currentservice = _uow.ServicePriceOfferRepository.GetById(removeFromPrice.ServiceId);
                _uow.ServicePriceOfferRepository.Delete(removeFromPrice.ServiceId);
                _uow.Save();
                return Ok(currentservice.PriceOfferId);

           

        }


        [HttpGet, Route("GetServicesWithPriceOfferID")]
        public IActionResult GetServicesWithPriceOfferID(int offerid)
        {
            var Parts = _uow.ServicePriceOfferRepository.GetMany(p => p.PriceOfferId == offerid).ToHashSet().OrderByDescending(a => a.Id);
            return Ok(Parts);
        }


        [HttpPost, Route("UpdateAndCalculatePriceOffer")]
        public IActionResult UpdateAndCalculatePriceOffer(UpdateAndCalculateOfferDTO priceoffer)
        {

            var priceoffercurrent = _uow.PriceOfferRepository.GetById(priceoffer.PriceOfferID);          
            var services = _uow.ServicePriceOfferRepository.GetMany(p => p.PriceOfferId == priceoffer.PriceOfferID).ToHashSet();
            var parts = _uow.PriceOfferItemRepository.GetMany(p => p.PriceOfferId == priceoffer.PriceOfferID).ToHashSet();
            decimal totalfixingservices = 0;
            decimal totalparts = 0;
            decimal totalfixingparts = 0;

            if (services.Count > 0)
            {
                totalfixingservices = services.Sum(s => s.FixingPrice);
            }

            if (parts.Count > 0)
            {
                totalfixingparts = parts.Sum(s => s.FixingPrice);
            }

            if (parts != null)
                {
                    foreach (var part in parts)
                    {

                        var Qty = part.Quantity;
                        var PeacePrice = part.PeacePrice;
                        var Cost = Qty * PeacePrice;
                        totalparts += Cost;
                    }

             }

            priceoffercurrent.FixingPrice = totalfixingservices + totalfixingparts;

            priceoffercurrent.BeforeDiscount = totalfixingservices + totalfixingparts + totalparts;

            priceoffercurrent.AfterDiscount = priceoffercurrent.BeforeDiscount - priceoffercurrent.Deiscount;

            decimal Vat = 0;
            var VAT = _uow.ConfigrationRepository.GetMany(t => t.WarshahId == priceoffercurrent.WarshahId).FirstOrDefault();
            if (VAT == null)
            {

                Vat = 0;

            }
            else
            {
                Vat = (((decimal)VAT.GetVAT) / (100));
            }
            priceoffercurrent.VatMoney = priceoffercurrent.AfterDiscount * Vat;
            priceoffercurrent.Total = priceoffercurrent.AfterDiscount + priceoffercurrent.VatMoney;

            _uow.PriceOfferRepository.Update(priceoffercurrent);
            _uow.Save();

            return Ok(priceoffercurrent);
        }


        [HttpGet, Route("GetPriceOfferWithPriceOfferID")]
        public IActionResult GetPriceOfferWithPriceOfferID(int offerid)
        {
            var priceoffer = _uow.PriceOfferRepository.GetMany(p => p.Id == offerid).Include(a=>a.Warshah).Include(a=>a.MotorModel).Include(a => a.MotorMake)
                .Include(a => a.MotorColor).Include(a => a.MotorYear).ToHashSet();

            var AllParts = _uow.PriceOfferItemRepository.GetMany(p => p.PriceOfferId == offerid).OrderByDescending(a => a.Id).ToHashSet();

            var AllServices = _uow.ServicePriceOfferRepository.GetMany(p => p.PriceOfferId == offerid).OrderByDescending(a => a.Id).ToHashSet();



            return Ok(new { priceoffer = priceoffer , AllParts = AllParts , AllServices = AllServices});
        }



        [HttpGet, Route("GetPriceOfferWithCarOwnerphone")]
        public IActionResult GetPriceOfferWithCarOwnerphone(string CarOwnerPhone)
        {
            var priceoffers = _uow.PriceOfferRepository.GetMany(p => p.CarOwnerPhone == CarOwnerPhone && p.IsActive == true).Include(a => a.Warshah).Include(a => a.MotorModel).Include(a => a.MotorMake)
                .Include(a => a.MotorColor).Include(a => a.MotorYear).ToHashSet();
           
            return Ok(priceoffers);
        }


        [HttpGet, Route("GetALLPriceOfferWithWarshahId")]
        public IActionResult GetALLPriceOfferWithWarshahId(int warshahid ,int pagenumber, int pagecount, string SearchText)
        {
            var priceoffers = _uow.PriceOfferRepository.GetMany(p => p.WarshahId == warshahid && p.IsActive == true).Include(a => a.Warshah).Include(a => a.MotorModel).Include(a => a.MotorMake)
                .Include(a => a.MotorColor).Include(a => a.MotorYear).ToHashSet();

            if (SearchText != null)
            {

                priceoffers = _uow.PriceOfferRepository.GetMany(t => t.WarshahId == warshahid && t.IsActive == true && t.CarOwnerName.Contains(SearchText)).ToHashSet();
                
                
            }

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = priceoffers.Count(), Listinvoice = priceoffers.ToPagedList(pagenumber, pagecount) });

        }


        [HttpGet, Route("GetALLPriceOfferWithWarshahIdInTime")]
        public IActionResult GetALLPriceOfferWithWarshahIdInTime(int warshahid, int pagenumber, int pagecount, DateTime FromDate, DateTime ToDate, string SearchText)
        {
            var priceoffers = _uow.PriceOfferRepository.GetMany(t => t.WarshahId == warshahid && t.IsActive == true && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate).Include(a => a.Warshah).Include(a => a.MotorModel).Include(a => a.MotorMake)
                .Include(a => a.MotorColor).Include(a => a.MotorYear).ToHashSet();

            if (SearchText != null)
            {

                priceoffers = _uow.PriceOfferRepository.GetMany(t => t.WarshahId == warshahid && t.IsActive == true && t.CreatedOn >= FromDate && t.CreatedOn <= ToDate && t.CarOwnerName.Contains(SearchText)).ToHashSet();


            }

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = priceoffers.Count(), Listinvoice = priceoffers.ToPagedList(pagenumber, pagecount) });

        }



        [HttpPost, Route("EditPriceOffer")]
        public IActionResult EditPriceOffer(EditPriceOfferDTO editPriceOffer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var City = _mapper.Map<DL.Entities.PriceOffer>(editPriceOffer);

                    var pricecurrent = _uow.PriceOfferRepository.GetById(City.Id);
                    pricecurrent.IsActive = false;
                    _uow.PriceOfferRepository.Update(pricecurrent);
                    _uow.Save();
                    return Ok(pricecurrent);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid City");


        }
    }
}
