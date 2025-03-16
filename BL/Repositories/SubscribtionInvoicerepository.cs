using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface ISubscribtionInvoicerepository
    { }
    public class SubscribtionInvoicerepository : Repository<SubscribtionInvoice>, ISubscribtionInvoicerepository
    {
        public SubscribtionInvoicerepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
