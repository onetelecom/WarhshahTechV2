using AutoMapper;
using BL.Infrastructure;
using DL.Entities;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarshahVatController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public WarshahVatController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }




        
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("SetVat")]
        public IActionResult SetVat(WarshahVat warshahVat)
        {
            
            var currentwarshah = _uow.WarshahVatRepository.GetMany(a => a.WarshahId == warshahVat.WarshahId).FirstOrDefault();
            if(currentwarshah == null)
            {
                _uow.WarshahVatRepository.Add(warshahVat);
            }
            else
            {
                warshahVat.Id = currentwarshah.Id;
               
                warshahVat.UpdatedOn = System.DateTime.Now;
                _uow.WarshahVatRepository.Update(warshahVat);
            }
           
            _uow.Save();
            return Ok(warshahVat);
        }



        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpGet, Route("GetVat")]
        public IActionResult GetVat(int  warshahid)
        {
             var vat =   _uow.WarshahVatRepository.GetMany(a=>a.WarshahId ==warshahid).FirstOrDefault();
         
            return Ok(vat);
        }



        

    }
}
