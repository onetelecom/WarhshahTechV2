using AutoMapper;
using BL.Infrastructure;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Linq;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]
    public class TransactionTodayBoxController : ControllerBase
    {

        


        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public TransactionTodayBoxController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }



        // Get AllTransactions  

        [HttpGet, Route("GetAllTransactionsBox")]
        public IActionResult GetAllTransactionsBox(int warshahid, int pagenumber, int pagecount)
        {
            var AllTransactions = _uow.TransactionTodayRepository.GetMany(b => b.WarshahId == warshahid).ToHashSet().OrderByDescending(i=>i.Id);


            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = AllTransactions.Count(), Listinvoice = AllTransactions.ToPagedList(pagenumber, pagecount) });

        }

        // Get AllTransactions  in Period

        [HttpGet, Route("GetAllTransactionsBoxInTime")]
        public IActionResult GetAllTransactionsBoxInTime(DateTime FromDate, DateTime ToDate, int warshahid, int pagenumber, int pagecount)
        {
            var AllTransactions = _uow.TransactionTodayRepository.GetMany(s => s.WarshahId == warshahid && s.CreatedOn >= FromDate && s.CreatedOn <= ToDate).ToHashSet().OrderByDescending(i => i.Id);

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = AllTransactions.Count(), Listinvoice = AllTransactions.ToPagedList(pagenumber, pagecount) });


        }



    }
}
