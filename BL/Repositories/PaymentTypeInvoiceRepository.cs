using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   

    public interface IPaymentTypeInvoiceRepository
    { }

    public class PaymentTypeInvoiceRepository : Repository<PaymentTypeInvoice>, IPaymentTypeInvoiceRepository
    {
        public PaymentTypeInvoiceRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
