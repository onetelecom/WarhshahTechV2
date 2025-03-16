using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
  
     public interface IRecordBonusRepository
    { }
    public class RecordBonusRepository : Repository<RecordBonusTechnical>, IRecordBonusRepository
    {
        public RecordBonusRepository (AppDBContext ctx) : base(ctx)
        { }


    }
}
