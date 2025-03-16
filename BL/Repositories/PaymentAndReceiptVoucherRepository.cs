using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IPaymentAndReceiptVoucherRepository
    { }

    public class PaymentAndReceiptVoucherRepository : Repository<PaymentAndReceiptVoucher>, IPaymentAndReceiptVoucherRepository
    {
        public PaymentAndReceiptVoucherRepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
