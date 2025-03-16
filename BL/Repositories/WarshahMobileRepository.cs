using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   public interface IWarshahMobileRepository
    { }
    public class WarshahMobileRepository : Repository<WarshahMobile>, IWarshahMobileRepository
    {
        public WarshahMobileRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
