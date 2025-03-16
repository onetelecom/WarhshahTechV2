using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   
     public interface ITransferRepository { }
    public class TransferRepository : Repository<Transfer>, ITransferRepository
    {
        public TransferRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}