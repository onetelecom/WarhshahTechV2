using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    
    public interface ISupportServiceRepository
    { }

    public class SupportServiceRepository : Repository<SupportService>, ISupportServiceRepository
    {
        public SupportServiceRepository(AppDBContext ctx) : base(ctx)
        { }


    }


}
