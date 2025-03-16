using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.BalanceDTO;
using DL.DTOs.SparePartsDTOs;
using HELPER;
using Microsoft.AspNetCore.Authorization;
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
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]

    public class BankController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public BankController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region BankCRUD

        //Create Bank 

        [HttpPost, Route("CreateBank")]
        public IActionResult CreateBank(BankDTO bankDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //this function is For Verfying The Content Comming From The Front End 
                    var bank = _mapper.Map<DL.Entities.Bank>(bankDTO);
                    var currentbank = _uow.BankRepository.GetMany(s => s.WarshahId == bank.WarshahId).FirstOrDefault();
                    if (currentbank == null)
                    {
                        bank.IsDeleted = false;
                        _uow.BankRepository.Add(bank);
                    }
                    else
                    {

                        bank.IsDeleted = false;
                        currentbank.WarshahId = bank.WarshahId;
                        currentbank.CurrentBalance = bank.CurrentBalance;
                        _uow.BankRepository.Update(currentbank);

                    }
                    _uow.Save();
                   
                    return Ok(bank);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Box");
        }


        [HttpGet, Route("GetBankCurrentInTime")]
        public IActionResult GetBankCurrentInTime(DateTime FromDate, DateTime ToDate, int warshahid)
        {
            decimal totalcheque = 0;
            decimal totalTransfer = 0;
            decimal totalinvoicesCard = 0;
            var DiposteCheques = _uow.ChequeRepository.GetMany(d => d.WarshahId == warshahid && d.ChequeStatus == 2 && d.CreatedOn.Day >= FromDate.Day && d.CreatedOn.Day <= ToDate.Day).ToHashSet();
            if(DiposteCheques != null)
            {
                 totalcheque = DiposteCheques.Sum(s => s.TotalMoney);
            }
            

            var DiposteTransfer = _uow.TransferRepository.GetMany(d => d.WarshahId == warshahid && d.TransferStatus == 2 && d.CreatedOn.Day >= FromDate.Day && d.CreatedOn.Day <= ToDate.Day).ToHashSet();
            if(DiposteTransfer != null)
            {
                totalTransfer = DiposteTransfer.Sum(s => s.TotalMoney);
            }
            

            var TotalinvoicesCard = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.InvoiceStatusId == 2 && s.PaymentTypeInvoiceId == 2 && s.CreatedOn.Day >= FromDate.Day && s.CreatedOn.Day <= ToDate.Day).ToHashSet();
           if(TotalinvoicesCard != null)
            {
                 totalinvoicesCard = TotalinvoicesCard.Sum(s => s.Total);

            }


            var currentBankIncome = _uow.BankRepository.GetMany(a=>a.WarshahId==warshahid).FirstOrDefault();

            if(currentBankIncome != null)
            {
                decimal currentIncome = currentBankIncome.CurrentBalance;
                BankDTO bankMoneyDTO = new BankDTO();
                bankMoneyDTO.WarshahId = (int)warshahid;
                bankMoneyDTO.CurrentBalance = (currentIncome + totalcheque + totalinvoicesCard + totalTransfer);
                return Ok(bankMoneyDTO);
            }

          return Ok();
        }


        #endregion




    }


}
