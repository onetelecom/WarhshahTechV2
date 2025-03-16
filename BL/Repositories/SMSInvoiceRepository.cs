using BL.Helpers;
using BL.Infrastructure;
using DL.DBContext;

using DL.DTOs.SharedDTO;
using DL.Entities;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public interface ISMSInvoiceRepository
    {
     

    }

    public class SMSInvoiceRepository : Repository<SMSInvoice>, ISMSInvoiceRepository
    {

        public SMSInvoiceRepository(AppDBContext ctx) : base(ctx)
        { }


       


    }
}
