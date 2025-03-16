using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.BalanceDTO;
using DL.DTOs.SparePartsDTOs;
using DL.Entities;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
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

    public class ChequeController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public ChequeController(IUnitOfWork uow, IMapper mapper)
        {
             _mapper = mapper;
            _uow = uow;
        }


        #region ChequeCRUD

        //Create Cheque 

        [HttpPost, Route("CreateCheque")]
        public IActionResult CreateCheque(ChequeDTO chequeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {

                   
                    var cheque = _mapper.Map<DL.Entities.Cheque>(chequeDTO);

                    
                    cheque.IsDeleted = false;
                    cheque.CreatedOn = DateTime.Now;
                    // from enum  ChequeStatus = 1  لم يتم الإيداع
                    cheque.ChequeStatus = 1;
                    _uow.ChequeRepository.Add(cheque);
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


        //Create Return Cheque

       [HttpPost, Route("CreateReturnCheque")]
        public IActionResult CreateReturnCheque(ReturnChequeDTO chequeId)
        {
            if (ModelState.IsValid)
            {
                try
                {

                   
                    ChequeDTO chequeDTO = new ChequeDTO();

                   
                    var currentcheque = _uow.ChequeRepository.GetById(chequeId.CheckId);
                    //var cheque = _mapper.Map<DL.Entities.Cheque>(currentcheque);


                    chequeDTO.ChequeNumber = currentcheque.ChequeNumber;
                    chequeDTO.WarshahId = currentcheque.WarshahId;
                    chequeDTO.CreatedBy = currentcheque.CreatedBy;
                    chequeDTO.UpdatedBy = currentcheque.UpdatedBy;
                    chequeDTO.CreatedOn= currentcheque.CreatedOn;
                    chequeDTO.UpdatedOn = DateTime.Now;
                    chequeDTO.WarshahBankId = currentcheque.WarshahBankId;
                    chequeDTO.IsActive = currentcheque.IsActive;

                    chequeDTO.IsDeleted = false;
                    // from enum  ChequeStatus = 3  مرتد
                    chequeDTO.ChequeStatus = 3;
                    chequeDTO.TotalMoney = (0 - currentcheque.TotalMoney);

                    var cheque = _mapper.Map<DL.Entities.Cheque>(chequeDTO);

                    _uow.ChequeRepository.Add(cheque);

                    _uow.Save();

                    currentcheque.ChequeStatus = 4;
                    _uow.ChequeRepository.Update(currentcheque);

                    _uow.Save();
                    var usingBank = _uow.WarshahBankRepository.GetById(chequeDTO.WarshahBankId);

                    decimal currentbalance = usingBank.CurrentBalance;

                    usingBank.CurrentBalance = currentbalance + chequeDTO.TotalMoney;

                    _uow.WarshahBankRepository.Update(usingBank);

                    ChequeBankAccount chequeBankAccount = new ChequeBankAccount();


                    chequeBankAccount.ChequeId = cheque.Id;
                    chequeBankAccount.Transactiontype = "دائن / شيك مرتد";
                    chequeBankAccount.PreviousBalance = currentbalance;
                    chequeBankAccount.CurrentBalance = usingBank.CurrentBalance;
                    chequeBankAccount.WarshahBankId = cheque.WarshahBankId;
                    chequeBankAccount.CreatedOn = DateTime.Now;
                    _uow.ChequeBankAccountRepository.Add(chequeBankAccount);

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





        // Get All Cheques By Bank Id

        [HttpGet, Route("GetChequesByBankId")]
        public IActionResult GetChequesByBankId(int BankId)
        {
            var Cheques = _uow.ChequeRepository.GetMany(d => d.WarshahBankId == BankId).Include(w => w.Warshah).Include(a => a.WarshahBank.FixedBank).ToHashSet();
            return Ok(Cheques);
        }

        // Get All Cheques By Bank Id in Period

        [HttpGet, Route("GetChequesByBankIdInTime")]
        public IActionResult GetChequesByBankIdInTime(DateTime FromDate, DateTime ToDate, int BankId)
        {
            var Cheques = _uow.ChequeRepository.GetMany(d => d.WarshahBankId == BankId && d.CreatedOn >= FromDate && d.CreatedOn <= ToDate && d.ChequeStatus != 1).Include(w => w.Warshah).Include(a => a.WarshahBank.FixedBank).ToHashSet();
            
            var BankBalance = _uow.WarshahBankRepository.GetById(BankId);

            //ar chequebankaccount = _uow.ChequeBankAccountRepository
            return Ok(new { Cheques = Cheques , BankBalance = BankBalance });
        }


        // Get All Cheques By Bank Id in Period with transactions

        [HttpGet, Route("GetChequesByBankIdInTimewithTransaction")]
        public IActionResult GetChequesByBankIdInTimewithTransaction(DateTime FromDate, DateTime ToDate, int BankId , int pagenumber , int pagecount)
        {
            var Cheques = _uow.ChequeBankAccountRepository.GetMany(d => d.WarshahBankId == BankId && d.CreatedOn >= FromDate && d.CreatedOn <= ToDate ).Include(a => a.WarshahBank.FixedBank).Include(a => a.Cheque).ToHashSet();
          
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = Cheques.Count(), Listinvoice = Cheques.ToPagedList(pagenumber, pagecount) });


           
        }






        // Get All Cheques

        [HttpGet, Route("GetAllChequesByWarshahId")]
        public IActionResult GetAllChequesByWarshahId(int warshahId , int pagenumber , int pagecount)
        {
            var Cheques = _uow.ChequeRepository.GetMany(d=>d.WarshahId == warshahId).Include(w=>w.Warshah).Include(a=>a.WarshahBank.FixedBank).ToHashSet();
            
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = Cheques.Count(), Listinvoice = Cheques.ToPagedList(pagenumber, pagecount) });

        }

        // Get  Cheque by Id

        [HttpGet, Route("GetChequeById")]
        public IActionResult GetChequeById(int chequeId)
        {
            var Cheque = _uow.ChequeRepository.GetMany(d => d.Id == chequeId).Include(w => w.Warshah).FirstOrDefault();
            return Ok(Cheque);
        }




        // Get All Money in type cheques

        [HttpGet, Route("GettotalMoneyChequesByWarshahId")]
        public IActionResult GettotalMoneyChequesByWarshahId(int warshahId)
        {
            CalculateCheque cheque =  new CalculateCheque();
             
            var Cheques = _uow.ChequeRepository.GetMany(d => d.WarshahId == warshahId).Include(w => w.Warshah).ToHashSet();
            cheque.TotalCheques = Cheques.Sum(s=>s.TotalMoney);

            var DiposteCheques = _uow.ChequeRepository.GetMany(d => d.WarshahId == warshahId && d.ChequeStatus == 2 ).ToHashSet();
            cheque.DiposteCheques = DiposteCheques.Sum(s => s.TotalMoney);

            var WatiningCheques = _uow.ChequeRepository.GetMany(d => d.WarshahId == warshahId && d.ChequeStatus == 1).ToHashSet();
            cheque.WatingCheques = WatiningCheques.Sum(s => s.TotalMoney);


            return Ok(cheque);
        }


        // Update  Bank

        [HttpPost, Route("EditCheque")]
        public IActionResult EditCheque(EditChequeDTO editChequeDTO)
        {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var cheque = _mapper.Map<DL.Entities.Cheque>(editChequeDTO);
                    var currentcheque = _uow.ChequeRepository.GetById(cheque.Id);
                    // if cheque is Deposited   ايداع و تحصيل

                    if (cheque.ChequeStatus == 2)
                    {
                        var usingBank = _uow.WarshahBankRepository.GetById(cheque.WarshahBankId);

                        decimal currentbalance = usingBank.CurrentBalance;

                        usingBank.CurrentBalance = currentbalance + cheque.TotalMoney;

                        _uow.WarshahBankRepository.Update(usingBank);

                        ChequeBankAccount chequeBankAccount = new ChequeBankAccount();

                        
                        chequeBankAccount.ChequeId = cheque.Id;
                        chequeBankAccount.Transactiontype = "مدين / تحصيل";
                        chequeBankAccount.PreviousBalance = currentbalance;
                        chequeBankAccount.CurrentBalance = usingBank.CurrentBalance;
                        chequeBankAccount.CreatedOn = DateTime.Now;
                        chequeBankAccount.WarshahBankId = cheque.WarshahBankId;

                        _uow.ChequeBankAccountRepository.Add(chequeBankAccount);
                        
                      

                    }

                    // if cheque is Withdraw   سحب

                    if (cheque.ChequeStatus == 5)
                    {
                        cheque.TotalMoney = (0 - cheque.TotalMoney);

                        var usingBank = _uow.WarshahBankRepository.GetById(cheque.WarshahBankId);

                        decimal currentbalance = usingBank.CurrentBalance;

                        usingBank.CurrentBalance = currentbalance + cheque.TotalMoney;

                        _uow.WarshahBankRepository.Update(usingBank);



                        ChequeBankAccount chequeBankAccount = new ChequeBankAccount();


                        chequeBankAccount.ChequeId = cheque.Id;
                        chequeBankAccount.Transactiontype = "دائن / سحب";
                        chequeBankAccount.PreviousBalance = currentbalance;
                        chequeBankAccount.CurrentBalance = usingBank.CurrentBalance;
                        chequeBankAccount.CreatedOn = DateTime.Now;
                        chequeBankAccount.WarshahBankId = cheque.WarshahBankId;
                        _uow.ChequeBankAccountRepository.Add(chequeBankAccount);
                    }

                    cheque.CreatedOn = currentcheque.CreatedOn;
                    cheque.UpdatedOn = currentcheque.UpdatedOn;
                    cheque.PayFor = currentcheque.PayFor;
                    cheque.MobileClient = currentcheque.MobileClient;
                    _uow.ChequeRepository.Update(cheque);
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
          
            [HttpDelete, Route("Deletecheque")]
            public IActionResult Deletecheque(int? Id)
            {
                if (ModelState.IsValid)
                {
                    try
                    {

                        _uow.ChequeRepository.Delete(Id);
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

        

        #endregion




    }


}
