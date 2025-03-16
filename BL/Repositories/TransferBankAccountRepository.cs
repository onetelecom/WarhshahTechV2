using BL.Infrastructure;
using DL.DBContext;
using DL.DTOs.BalanceDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

     public interface ITransferBankAccountRepository
    { }
    public class TransferBankAccountRepository : Repository<TransferBankAccount>, ITransferBankAccountRepository
    { 
        public TransferBankAccountRepository(AppDBContext ctx) : base(ctx)
        { }


    
}
}
