using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface ITransactionInventoryRepository
    {


    }

    public class TransactionInventoryRepository : Repository<TransactionInventory>, ITransactionInventoryRepository
    {

        public TransactionInventoryRepository(AppDBContext ctx) : base(ctx)
        { }





    }
}
