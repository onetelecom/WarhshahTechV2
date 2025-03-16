using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
using DL.DTOs.BalanceDTO;
using DL.Entities;
using DL.Entities.HR;
using DocumentFormat.OpenXml.Wordprocessing;
using HELPER;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
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
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]
    public class BoxBankController : ControllerBase
    {
        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public BoxBankController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }



        [HttpPost, Route("CreateBox")]
        public IActionResult CreateBox(BoxBankDTO boxBankdto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var boxBank = _mapper.Map<BoxBank>(boxBankdto);
                    boxBank.IsDeleted = false;
                        boxBank.CreatedOn = DateTime.Now;
                        _uow.BoxBankRepository.Add(boxBank);

                   
                    _uow.Save();

                    TransactionsToday transactionsToday = new TransactionsToday();

                    transactionsToday.WarshahId = (int)boxBank.WarshahId;
                    transactionsToday.InvoiceNumber = boxBank.Id;
                    transactionsToday.Vat = 0;
                    transactionsToday.Total = boxBank.TotalIncome;
                    transactionsToday.CreatedOn = DateTime.Now;
                    transactionsToday.BeforeVat = 0;

                   
                    transactionsToday.TransactionName = "إيداع بنكى";

                    _uow.TransactionTodayRepository.Add(transactionsToday);


                    var bank = _uow.WarshahBankRepository.GetById( boxBank.WarshahBankId);

                    var currenttotal = bank.CurrentBalance;
                    bank.CurrentBalance = currenttotal + boxBank.TotalIncome;
                    _uow.WarshahBankRepository.Update(bank);
                    _uow.Save();

                    return Ok(boxBank);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Box");
        }


        [HttpGet, Route("GetAllTransactionsByWarshahId")]
        public IActionResult GetAllTransactionsByWarshahId(int? warshahId, int pagenumber, int pagecount )
        {

            var transactions = _uow.BoxBankRepository.GetMany(b => b.WarshahId == warshahId).Include(a => a.WarshahBank.FixedBank).Include(a=>a.User).ToHashSet();
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = transactions.Count(), Listinvoice = transactions.ToPagedList(pagenumber, pagecount) });
          
        }



    }
}
