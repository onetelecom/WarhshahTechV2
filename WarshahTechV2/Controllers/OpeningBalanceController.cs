using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.BalanceDTO;
using DL.Entities;
using Helper;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using WarshahTechV2.Models;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]
    public class OpeningBalanceController : ControllerBase
    {


        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        private readonly IBoxNow _boxNow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public OpeningBalanceController(IUnitOfWork uow, IMapper mapper , IBoxNow boxNow)
        {
            _mapper = mapper;
            _uow = uow;
            _boxNow = boxNow;
        }



        // To add open balance in box
        [HttpPost, Route("AddOpeningBalance")]
        public IActionResult AddOpeningBalance(OpenBalanceDTO openBalance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var open = _mapper.Map<DL.Entities.OpeningBalance>(openBalance);
                    var currentopen = _uow.OpeningBalanceRepository.GetMany(s => s.WarshahId == open.WarshahId).FirstOrDefault();
                    if (currentopen == null)
                    {
                        open.IsDeleted = false;
                        _uow.OpeningBalanceRepository.Add(open);
                    }
                    else
                    {

                        open.IsDeleted = false;
                        currentopen.Id = currentopen.Id;
                        currentopen.WarshahId = open.WarshahId;
                        currentopen.OpenBalance = open.OpenBalance;
                        _uow.OpeningBalanceRepository.Update(currentopen);

                    }
                    _uow.Save();

                    // Add To Transaction TODAY bOX when cash
                  
                        TransactionsToday transactionsToday = new TransactionsToday();

                        transactionsToday.WarshahId = (int)open.WarshahId;
                        transactionsToday.InvoiceNumber = 0 ;
                        transactionsToday.Vat = 0 ;
                      
                        transactionsToday.Total = open.OpenBalance;                    
                        transactionsToday.CreatedOn = DateTime.Now;
                        transactionsToday.BeforeVat = 0;                      
                        transactionsToday.TransactionName = "رصيد فى الصندوق ";

                        var boxnow = _boxNow.GetBoxNow((int)transactionsToday.WarshahId);
                        decimal previousmoney = boxnow.TotalIncome;

                        var currenttotal = _uow.TransactionTodayRepository.GetMany(a => a.WarshahId == transactionsToday.WarshahId).OrderByDescending(t => t.Id).FirstOrDefault();
                        if (currenttotal != null)
                        {
                            transactionsToday.PreviousBalance = currenttotal.CurrentBalance;
                            transactionsToday.CurrentBalance = currenttotal.CurrentBalance + transactionsToday.Total;
                        }

                        else
                        {
                            transactionsToday.PreviousBalance = previousmoney;
                            transactionsToday.CurrentBalance = previousmoney + transactionsToday.Total;

                        }




                        _uow.TransactionTodayRepository.Add(transactionsToday);

                    _uow.Save();


                    return Ok(open);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid open");
        }


        // To add open balance in spare parts
        [HttpPost, Route("AddOpenSpareParts")]
        public IActionResult AddOpenSpareParts(OpenSpartPartDTO openSpartPartDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var open = _mapper.Map<DL.Entities.OpenSpartPart>(openSpartPartDTO);
                    var currentopen = _uow.OpenSpartPartRepository.GetMany(s => s.WarshahId == open.WarshahId).FirstOrDefault();
                    if (currentopen == null)
                    {
                        open.IsDeleted = false;
                        _uow.OpenSpartPartRepository.Add(open);
                    }
                    else
                    {

                        open.IsDeleted = false;
                        currentopen.Id = currentopen.Id;
                        currentopen.WarshahId = open.WarshahId;
                        currentopen.TotalQny = open.TotalQny;
                        currentopen.TotalMoney = open.TotalMoney;
                        _uow.OpenSpartPartRepository.Update(currentopen);

                    }
                    _uow.Save();

                    return Ok(open);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid open");
        }

        // To add open balance in Banks
        [HttpPost, Route("AddBanksBalance")]
        public IActionResult AddBanksBalance(OpenBankDTO openBankDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var open = _mapper.Map<DL.Entities.OpenBalanceBank>(openBankDTO);
                    var currentopen = _uow.OpenBankBalanceRepository.GetMany(s => s.WarshahId == open.WarshahId).FirstOrDefault();
                    if (currentopen == null)
                    {
                        open.IsDeleted = false;
                        _uow.OpenBankBalanceRepository.Add(open);
                    }
                    else
                    {

                        open.IsDeleted = false;
                        currentopen.Id = currentopen.Id;
                        currentopen.WarshahId = open.WarshahId;
                        currentopen.TotalMoney = open.TotalMoney;

                        _uow.OpenBankBalanceRepository.Update(currentopen);

                    }
                    _uow.Save();

                    return Ok(open);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid open");
        }


        // To add open balance in Banks
        [HttpPost, Route("AddChequeBalance")]
        public IActionResult AddChequeBalance(OpenChequesDTO openChequesDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var open = _mapper.Map<DL.Entities.OpenBalanceCheque>(openChequesDTO);
                    var currentopen = _uow.OpenCheaqeRepository.GetMany(s => s.WarshahId == open.WarshahId).FirstOrDefault();
                    if (currentopen == null)
                    {
                        open.IsDeleted = false;
                        _uow.OpenCheaqeRepository.Add(open);
                    }
                    else
                    {

                        open.IsDeleted = false;
                        currentopen.Id = currentopen.Id;
                        currentopen.WarshahId = open.WarshahId;
                        currentopen.TotalMoney = open.TotalMoney;

                        _uow.OpenCheaqeRepository.Update(currentopen);

                    }
                    _uow.Save();

                    return Ok(open);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid open");
        }



        // To Get open balance in Box

        [HttpGet, Route("GetOpeningBalance")]
        public IActionResult GetOpeningBalance(int? warshahid)
        {
            var Total = _uow.OpeningBalanceRepository.GetMany(i => i.WarshahId == warshahid).FirstOrDefault();

            decimal total = 0;
            if (Total != null)
            {
                total = Total.OpenBalance;
            }
            else
            {
                total = 0;
            }

            return Ok(total);

        }


        // To Get open balance in Parts

        [HttpGet, Route("GetOpenSpareParts")]
        public IActionResult GetOpenSpareParts(int? warshahid)
        {
            var Total = _uow.OpenSpartPartRepository.GetMany(i => i.WarshahId == warshahid).FirstOrDefault();

            OpenSpartPartDTO openSpartPartDTO  = new OpenSpartPartDTO();
            if (Total != null)
            {
                openSpartPartDTO.TotalMoney = Total.TotalMoney;
                openSpartPartDTO.TotalQny = Total.TotalQny;
            }
            else
            {
                openSpartPartDTO.TotalMoney =0;
                openSpartPartDTO.TotalQny = 0;
            }

            return Ok(openSpartPartDTO);

        }


        // To Get open balance in Banks

        [HttpGet, Route("GetBanksBalance")]
        public IActionResult GetBanksBalance(int? warshahid)
        {
            var Total = _uow.OpenBankBalanceRepository.GetMany(i => i.WarshahId == warshahid).FirstOrDefault();

            decimal total = 0;
            if (Total != null)
            {
                total = Total.TotalMoney;
            }
            else
            {
                total = 0;
            }

            return Ok(total);

        }


        // To Get open balance in Cheques

        [HttpGet, Route("GetChequesBalance")]
        public IActionResult GetChequesBalance(int? warshahid)
        {
            var Total = _uow.OpenCheaqeRepository.GetMany(i => i.WarshahId == warshahid).FirstOrDefault();

            decimal total = 0;
            if (Total != null)
            {
                total = Total.TotalMoney;
            }
            else
            {
                total = 0;
            }

            return Ok(total);

        }

    }
}
