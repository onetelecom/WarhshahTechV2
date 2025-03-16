using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IInspectionReportRepository
    { }
    public class InspectionReportRepository : Repository<InspectionReport>, IInspectionReportRepository
    {
        public InspectionReportRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
