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

    public class BalanceBankController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public BalanceBankController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region BankCRUD


        // Get Balancebank for warshah id

        [HttpGet, Route("GetBalancebank")]
        public IActionResult GetBalancebank(int warshahid)
        {

            // from enum  PaymentTypeInvoiceId = 2   بطاقة ائتمان
            var cardInvoice = _uow.InvoiceRepository.GetMany(s => s.WarshahId == warshahid && s.InvoiceStatusId == 2 && s.PaymentTypeInvoiceId ==2).ToHashSet();
            decimal tcardinvoice = cardInvoice.Sum(s => s.Total);





            // from enum  ChequeStatus = 2   تم الإيداع
            //var Cheques = _uow.ChequeRepository.GetMany(d => d.WarshahId == warshahid && d.ChequeStatus==2).ToHashSet();
            //decimal tchequeinvoice = Cheques.Sum(s => s.TotalMoney);


            // جمع كل الرصيد الحالى فى البنوك 
            var TotalBanks = _uow.WarshahBankRepository.GetMany(d => d.WarshahId == warshahid).ToHashSet();
            decimal tchequeinvoice = TotalBanks.Sum(s => s.CurrentBalance);

            var currentbalance = _uow.BalanceBankRepository.GetMany(b => b.WarshahId == warshahid).FirstOrDefault();
            currentbalance.CurrentBalance = currentbalance.OpeningBalance + tcardinvoice + tchequeinvoice ;
            _uow.BalanceBankRepository.Update(currentbalance);
            _uow.Save();

            return Ok(currentbalance);
        }



        // Get InventoryBalance for warshah id

        [HttpGet, Route("InventoryBalance")]
        public IActionResult InventoryBalance(int warshahid)
        {

            InventoryBalance inventoryBalance =    new InventoryBalance();  
           
            var AllParts = _uow.SparePartRepository.GetMany(s => s.WarshahId == warshahid).ToHashSet();

          
                foreach (var part in AllParts)
                {

                    var Qty = part.Quantity;
                    var BuyPrice = part.BuyingPrice;
                    var Cost = Qty * BuyPrice;

                inventoryBalance.TotalBalance += Cost;
                }


            inventoryBalance.TotalQty = AllParts.Sum(s => s.Quantity);
            inventoryBalance.WarshahId = warshahid;
         

            return Ok(inventoryBalance);
        }


        #endregion




    }
}
