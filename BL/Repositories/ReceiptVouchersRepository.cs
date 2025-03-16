using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
  

    public interface IReceiptVouchersRepository
    { }

    public class ReceiptVouchersRepository : Repository<ReceiptVoucher>, IReceiptVouchersRepository
    {
        public ReceiptVouchersRepository(AppDBContext ctx) : base(ctx)
        { }


    }


}
