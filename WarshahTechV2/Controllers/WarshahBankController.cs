using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.BalanceDTO;
using DL.Entities;
using DL.Migrations;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WarshahTechV2.Models;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
   
    public class WarshahBankController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public WarshahBankController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }



        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]

        [HttpPost, Route("CreateWarshahBank")]
        public IActionResult CreateWarshahBank(WarshahBankDTO warshahBankDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bank = _mapper.Map<DL.Entities.WarshahBank>(warshahBankDTO);
                    bank.CurrentBalance = bank.OpenBalance;
                    bank.IsDeleted = false;
                    _uow.WarshahBankRepository.Add(bank);
 
                    _uow.Save();

                    return Ok(bank);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Warshah Bank");
        }



        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]

        [HttpGet, Route("GetWarshahBank")]
        public IActionResult GetWarshahBank(int? BankId)
        {

            var bank = _uow.WarshahBankRepository.GetMany(a=>a.Id == BankId).Include(a => a.FixedBank).FirstOrDefault();
            return Ok(bank);
        }


        //[ClaimRequirement(ClaimTypes.Role, RoleConstant.Recicp)]

        [HttpGet, Route("GetAllBanksByWarshahId")]
        public IActionResult GetAllBanksByWarshahId(int? warshahId)
        {

            var banks = _uow.WarshahBankRepository.GetMany(b=>b.WarshahId == warshahId).Include(a=>a.FixedBank).ToHashSet();
            return Ok(banks);
        }



        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]
        

        [HttpGet, Route("GetYestardayBalanceBankbyWarshahId")]
        public IActionResult GetYestardayBalanceBankbyWarshahId(int? warshahId)
        {

            var warshah = _uow.WarshahRepository.GetById(warshahId);

            DateTime FromDate = warshah.CreatedOn;

            DateTime ToDate = DateTime.Today;

            List<YestardayBalanceBank> yestardayBalanceBanks = new List<YestardayBalanceBank>();

            var banks = _uow.WarshahBankRepository.GetMany(b => b.WarshahId == warshahId).ToHashSet();

            decimal totalcheque = 0;

            decimal witherdcheque = 0;

            decimal totaltransfer = 0;

            decimal withardtransfer = 0;

            foreach (var bank in banks)
            {
                var fixedbank = _uow.FixedBankRepository.GetById(bank.FixedBankId); 

                YestardayBalanceBank yesbalance = new YestardayBalanceBank();

                var DiposteCheques = _uow.ChequeRepository.GetMany(d => d.WarshahBankId == bank.Id && d.ChequeStatus == 2 && d.CreatedOn >= FromDate && d.CreatedOn <= ToDate).ToHashSet();
                if(DiposteCheques != null)
                {
                    totalcheque = DiposteCheques.Sum(s => s.TotalMoney);
                }

                var WitherdCheques = _uow.ChequeRepository.GetMany(d => d.WarshahBankId == bank.Id && d.ChequeStatus == 5 && d.CreatedOn >= FromDate && d.CreatedOn <= ToDate).ToHashSet();
                if (WitherdCheques != null)
                {
                    witherdcheque = WitherdCheques.Sum(s => s.TotalMoney);
                }


                var DiposteTransfer = _uow.TransferRepository.GetMany(d => d.WarshahBankId == bank.Id && d.TransferStatus == 2 && d.CreatedOn >= FromDate && d.CreatedOn <= ToDate).ToHashSet();
                if (DiposteTransfer != null)
                {
                    totaltransfer = DiposteTransfer.Sum(s => s.TotalMoney);
                }

                var WithardTransfer = _uow.TransferRepository.GetMany(d => d.WarshahBankId == bank.Id && d.TransferStatus == 5 && d.CreatedOn >= FromDate && d.CreatedOn <= ToDate).ToHashSet();
                if (WithardTransfer != null)
                {
                    withardtransfer = WithardTransfer.Sum(s => s.TotalMoney);
                }

                decimal currentbalance = bank.OpenBalance;


                yesbalance.YestardayBalance = (currentbalance + totaltransfer + totalcheque) - (withardtransfer + witherdcheque) ;



                yesbalance.BankName = fixedbank.BankNameAr;
                yesbalance.AccountName = bank.AccountName;
                yesbalance.BankAccountNumber = bank.BankAccountNumber;

                yestardayBalanceBanks.Add(yesbalance);

            }

            

            return Ok(yestardayBalanceBanks);
        }


        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]
       
        [HttpPost, Route("EditWarshahBank")]
        public IActionResult EditWarshahBank(EditWarshahBankDTO editWarshahBankDTO)
        {
            
                if (ModelState.IsValid)
                {
                    try
                    {
                        var bank = _mapper.Map<DL.Entities.WarshahBank>(editWarshahBankDTO);
                        bank.UpdatedOn = DateTime.Now;
                        _uow.WarshahBankRepository.Update(bank);
                        _uow.Save();
                        return Ok(bank);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }

                }

                return BadRequest("Invalid Bank");


            

        }

    }
}
