using BL.Infrastructure;
using DL.DBContext;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   public interface IGenderRepository
    { }
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        public GenderRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
