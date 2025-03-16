using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IInspectionTemplateRepository
    { }

    public class InspectionTemplateRepository : Repository<InspectionTemplate>, IInspectionTemplateRepository
    {
        public InspectionTemplateRepository(AppDBContext ctx) : base(ctx)
        { }
    }


}
