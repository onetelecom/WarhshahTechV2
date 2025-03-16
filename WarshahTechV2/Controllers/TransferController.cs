using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.BalanceDTO;
using DL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Wordprocessing;
using PagedList;
using HELPER;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Accountant)]

    public class TransferController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public TransferController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region TransferCRUD

        //Create Transfer 

        [HttpPost, Route("CreateTransfer")]
        public IActionResult CreateTransfer(TransferDTO transferDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    var transfer = _mapper.Map<DL.Entities.Transfer>(transferDTO);


                    transfer.IsDeleted = false;
                    transfer.CreatedOn = DateTime.Now;
                    // from enum  TransferStatus = 1  لم يتم الإيداع
                    transfer.TransferStatus = 1;
                    _uow.TransferRepository.Add(transfer);
                    _uow.Save();
                    return Ok(transfer);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Transfer");
        }


        //Create Return Transfer

        [HttpPost, Route("CreateReturnTransfer")]
        public IActionResult CreateReturnTransfer(ReturnTransferDTO transferId)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    TransferDTO chequeDTO = new TransferDTO();


                    var currentcheque = _uow.TransferRepository.GetById(transferId.CheckId);
                    //var cheque = _mapper.Map<DL.Entities.Cheque>(currentcheque);


                    chequeDTO.TransferNumber = currentcheque.TransferNumber;
                    chequeDTO.WarshahId = currentcheque.WarshahId;
                    chequeDTO.CreatedBy = currentcheque.CreatedBy;
                    chequeDTO.UpdatedBy = currentcheque.UpdatedBy;
                    chequeDTO.CreatedOn = currentcheque.CreatedOn;
                    chequeDTO.UpdatedOn = DateTime.Now;
                    chequeDTO.WarshahBankId = currentcheque.WarshahBankId;
                    chequeDTO.IsActive = currentcheque.IsActive;

                    chequeDTO.IsDeleted = false;
                    // from enum  TransferStatus = 3  مرتد
                    chequeDTO.TransferStatus = 3;
                    chequeDTO.TotalMoney = (0 - currentcheque.TotalMoney);

                    var cheque = _mapper.Map<DL.Entities.Transfer>(chequeDTO);

                    _uow.TransferRepository.Add(cheque);

                    _uow.Save();

                    currentcheque.TransferStatus = 4;
                    _uow.TransferRepository.Update(currentcheque);

                    _uow.Save();
                    var usingBank = _uow.WarshahBankRepository.GetById(chequeDTO.WarshahBankId);

                    decimal currentbalance = usingBank.CurrentBalance;

                    usingBank.CurrentBalance = currentbalance + chequeDTO.TotalMoney;

                    _uow.WarshahBankRepository.Update(usingBank);

                    TransferBankAccount chequeBankAccount = new TransferBankAccount();


                    chequeBankAccount.TransferId = cheque.Id;
                    chequeBankAccount.Transactiontype = "دائن / شيك مرتد";
                    chequeBankAccount.PreviousBalance = currentbalance;
                    chequeBankAccount.CurrentBalance = usingBank.CurrentBalance;
                    chequeBankAccount.WarshahBankId = cheque.WarshahBankId;
                    chequeBankAccount.CreatedOn = DateTime.Now;
                    _uow.TransferBankAccountRepository.Add(chequeBankAccount);

                    _uow.Save();
                    return Ok(cheque);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid cheque");
        }





        // Get All transfers By Bank Id

        [HttpGet, Route("GetTransferByBankId")]
        public IActionResult GetTransferByBankId(int BankId)
        {
            var transfers = _uow.TransferRepository.GetMany(d => d.WarshahBankId == BankId).Include(w => w.Warshah).Include(a => a.WarshahBank.FixedBank).ToHashSet();
            return Ok(transfers);
        }

        // Get All transfers By Bank Id in Period

        [HttpGet, Route("GettransfersByBankIdInTime")]
        public IActionResult GettransfersByBankIdInTime(DateTime FromDate, DateTime ToDate, int BankId)
        {
            var transfers = _uow.TransferRepository.GetMany(d => d.WarshahBankId == BankId && d.CreatedOn >= FromDate && d.CreatedOn <= ToDate && d.TransferStatus != 1).Include(w => w.Warshah).Include(a => a.WarshahBank.FixedBank).ToHashSet();

            var BankBalance = _uow.WarshahBankRepository.GetById(BankId);

            //ar chequebankaccount = _uow.ChequeBankAccountRepository
            return Ok(new { transfers = transfers, BankBalance = BankBalance });
        }


        // Get All transfers By Bank Id in Period with transactions

        [HttpGet, Route("GetTransfersByBankIdInTimewithTransaction")]
        public IActionResult GetTransfersByBankIdInTimewithTransaction(DateTime FromDate, DateTime ToDate, int BankId)
        {
            var transfer = _uow.TransferBankAccountRepository.GetMany(d => d.WarshahBankId == BankId && d.CreatedOn >= FromDate && d.CreatedOn <= ToDate).Include(a => a.WarshahBank.FixedBank).Include(a => a.Transfer).ToHashSet();

            return Ok(new { transfers = transfer });
        }






        // Get All transfers

        [HttpGet, Route("GetAlltransfersByWarshahId")]
        public IActionResult GetAlltransfersByWarshahId(int warshahId, int pagenumber, int pagecount)
        {
            var transfers = _uow.TransferRepository.GetMany(d => d.WarshahId == warshahId).Include(w => w.Warshah).Include(a => a.WarshahBank.FixedBank).ToHashSet();

            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = transfers.Count(), Listinvoice = transfers.ToPagedList(pagenumber, pagecount) });

        }

        // Get  transfer by Id

        [HttpGet, Route("GettransferById")]
        public IActionResult GettransferById(int transferId)
        {
            var transfer = _uow.TransferRepository.GetMany(d => d.Id == transferId).Include(w => w.Warshah).FirstOrDefault();
            return Ok(transfer);
        }




        // Get All Money in type cheques

        [HttpGet, Route("GettotalMoneyTransfersByWarshahId")]
        public IActionResult GettotalMoneyTransfersByWarshahId(int warshahId)
        {
            CalculateTransfer transfer = new CalculateTransfer();

            var Cheques = _uow.TransferRepository.GetMany(d => d.WarshahId == warshahId).Include(w => w.Warshah).ToHashSet();
            transfer.TotalTransfers = Cheques.Sum(s => s.TotalMoney);

            var DiposteCheques = _uow.TransferRepository.GetMany(d => d.WarshahId == warshahId && d.TransferStatus == 2).ToHashSet();
            transfer.DiposteTransfers = DiposteCheques.Sum(s => s.TotalMoney);

            var WatiningCheques = _uow.TransferRepository.GetMany(d => d.WarshahId == warshahId && d.TransferStatus == 1).ToHashSet();
            transfer.WatingTransfers = WatiningCheques.Sum(s => s.TotalMoney);


            return Ok(transfer);
        }


        // Update  Bank

        [HttpPost, Route("EditTransfer")]
        public IActionResult EditTransfer(EditTransferDTO editChequeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cheque = _mapper.Map<DL.Entities.Transfer>(editChequeDTO);
                    var currentcheque = _uow.TransferRepository.GetById(cheque.Id);
                    // if cheque is Deposited   ايداع و تحصيل

                    if (cheque.TransferStatus == 2)
                    {
                        var usingBank = _uow.WarshahBankRepository.GetById(cheque.WarshahBankId);

                        decimal currentbalance = usingBank.CurrentBalance;

                        usingBank.CurrentBalance = currentbalance + cheque.TotalMoney;

                        _uow.WarshahBankRepository.Update(usingBank);

                        TransferBankAccount chequeBankAccount = new TransferBankAccount();


                        chequeBankAccount.TransferId = cheque.Id;
                        chequeBankAccount.Transactiontype = "مدين / تحصيل";
                        chequeBankAccount.PreviousBalance = currentbalance;
                        chequeBankAccount.CurrentBalance = usingBank.CurrentBalance;
                        chequeBankAccount.CreatedOn = DateTime.Now;
                        chequeBankAccount.WarshahBankId = cheque.WarshahBankId;

                        _uow.TransferBankAccountRepository.Add(chequeBankAccount);



                    }

                    // if Transfer is Withdraw   سحب

                    if (cheque.TransferStatus == 5)
                    {
                        cheque.TotalMoney = (0 - cheque.TotalMoney);

                        var usingBank = _uow.WarshahBankRepository.GetById(cheque.WarshahBankId);

                        decimal currentbalance = usingBank.CurrentBalance;

                        usingBank.CurrentBalance = currentbalance + cheque.TotalMoney;

                        _uow.WarshahBankRepository.Update(usingBank);



                        TransferBankAccount chequeBankAccount = new TransferBankAccount();


                        chequeBankAccount.TransferId = cheque.Id;
                        chequeBankAccount.Transactiontype = "دائن / سحب";
                        chequeBankAccount.PreviousBalance = currentbalance;
                        chequeBankAccount.CurrentBalance = usingBank.CurrentBalance;
                        chequeBankAccount.CreatedOn = DateTime.Now;
                        chequeBankAccount.WarshahBankId = cheque.WarshahBankId;
                        _uow.TransferBankAccountRepository.Add(chequeBankAccount);
                    }

                    cheque.CreatedOn = currentcheque.CreatedOn;
                    cheque.UpdatedOn = currentcheque.UpdatedOn;
                    cheque.PayFor = currentcheque.PayFor;
                    cheque.MobileClient = currentcheque.MobileClient;
                    _uow.TransferRepository.Update(cheque);
                    _uow.Save();







                    return Ok(cheque);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid cheque");



        }

        // Delete cheque

        [HttpDelete, Route("DeleteTransfer")]
        public IActionResult DeleteTransfer(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.TransferRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid cheque");


        }

    }

        #endregion


    }
