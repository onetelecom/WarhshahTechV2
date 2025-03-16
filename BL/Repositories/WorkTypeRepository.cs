using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IWorkTypeRepository
    { }
    public class WorkTypeRepository : Repository<WorkType>, IWorkTypeRepository
    {
        public WorkTypeRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
