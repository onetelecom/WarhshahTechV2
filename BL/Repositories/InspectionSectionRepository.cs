using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
 
    public interface IInspectionSectionRepository
    { }

    public class InspectionSectionRepository : Repository<InspectionSection>, IInspectionSectionRepository
    {
        public InspectionSectionRepository(AppDBContext ctx) : base(ctx)
        { }
    }

}
