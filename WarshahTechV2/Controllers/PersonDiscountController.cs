using AutoMapper;
using BL.Infrastructure;
using DL.Entities;
using DocumentFormat.OpenXml.Wordprocessing;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WarshahTechV2.Models;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonDiscountController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public PersonDiscountController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpGet, Route("GetAllDiscounts")]
        public IActionResult GetAllDiscounts(int warshahId , int pagenumber , int pagecount)
        {
            var All = _uow.PersonDiscountRepository.GetMany(a=>a.WarshahId== warshahId).ToHashSet();
            var AllDiscounts = new List<object>();
            foreach (var item in All)
            {
                var disObkect = new
                {
                    Id= item.Id,
                    WarshahId= item.WarshahId,
                    DiscountPercentageForFixingPrice=item.DiscountPercentageForFixingPrice,
                    DiscountPercentageForSpareParts=item.DiscountPercentageForSpareParts,
                    CarOwner=_uow.UserRepository.GetById( item.CarOwnerId),

                };
                AllDiscounts.Add(disObkect);
            }
            
            return Ok(AllDiscounts);
        }
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("addDiscounts")]
        public IActionResult addDiscounts(PersonDiscount personDiscount)
        {
            _uow.PersonDiscountRepository.Add(personDiscount);
            _uow.Save();
            return Ok(personDiscount);
        }

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("updateDiscounts")]
        public IActionResult updateDiscounts(PersonDiscount personDiscount)
        {
            _uow.PersonDiscountRepository.Update(personDiscount);
            _uow.Save();
            return Ok(personDiscount);
        }

    }
}
