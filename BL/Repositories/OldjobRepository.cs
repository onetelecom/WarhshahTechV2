using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   
    public interface IOldjobRepository
    { }
    public class OldjobRepository : Repository<OldJobtitle>, IOldjobRepository
    {
        public OldjobRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
