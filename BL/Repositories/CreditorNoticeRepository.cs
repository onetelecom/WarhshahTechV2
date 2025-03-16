using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface ICreditorNoticeRepository
    { }

    public class CreditorNoticeRepository : Repository<CreditorNotice>, ICreditorNoticeRepository
    {
        public CreditorNoticeRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
