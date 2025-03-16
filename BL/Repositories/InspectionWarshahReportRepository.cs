using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IInspectionWarshahReportRepository
    { }
    public class InspectionWarshahReportRepository : Repository<InspectionWarshahReport>, IInspectionWarshahReportRepository
    {
        public InspectionWarshahReportRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
